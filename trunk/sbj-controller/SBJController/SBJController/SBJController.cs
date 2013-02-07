using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using NationalInstruments.DAQmx;
using SBJController.Properties;
using System.Text;
using System.Collections.Generic;

namespace SBJController
{
    /// <summary>
    /// Represents the main class for controlling the Squeezable Break Junction
    /// </summary>
    public class SBJController
    {
        #region Private Members
        private StepperMotor m_stepperMotor;
        private ElectroMagnet m_electroMagnet;
        private Amplifier m_amplifier;
        private SourceMeter m_sourceMeter;
        private Tabor m_taborLaserController;
        private DataAcquisitionController m_daqController;
        private Task m_task;
        private bool m_quitJunctionOpenningOperation;
        private const string c_settingsFileName = "Params.txt";
        private const string c_dataFileName = "StatDAQ_";
        private const string c_lockInSignalFileName = "LockInSignal_";
        private const string c_lockInPhaseFileName = "LockInPhase_";
        private delegate void OpenJunctionMethodDelegate(SBJControllerSettings settings);
        private delegate bool EMOpenJunctionMethodDelegate(SBJControllerSettings settings);
        public delegate void DataAquiredEventHandler(object sender, DataAquiredEventArgs e);
        public event DataAquiredEventHandler DataAquired;
        #endregion

        #region Properties
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
        }
        public bool QuitJunctionOpenningOperation
        {
            get { return m_quitJunctionOpenningOperation; }
            set { m_quitJunctionOpenningOperation = value; }
        }
        public StepperMotor StepperMotor
        {
            get { return m_stepperMotor; }
            set { m_stepperMotor = value; }
        }

        public SourceMeter SourceMeter
        {
            get { return m_sourceMeter; }
            set { m_sourceMeter = value; }
        }        

        public Tabor Tabor
        {
            get { return m_taborLaserController; }
            set { m_taborLaserController = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SBJController()
        {
            m_stepperMotor = new StepperMotor();
            m_electroMagnet = new ElectroMagnet();
            m_amplifier = new Amplifier();
            m_sourceMeter = new SourceMeter();
            m_daqController = new DataAcquisitionController();
            m_quitJunctionOpenningOperation = false;
            m_taborLaserController = new Tabor();
            InitializeComponents();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initialize external components for GPIB useage
        /// </summary>
        /// <exception cref="SBJException">Incase connection to one of the external components was unsuccessful</exception>
        private void InitializeComponents()
        {
            m_amplifier.Connect();
            m_sourceMeter.Connect();            
        }

        /// <summary>
        /// Open the junction asynchronously
        /// </summary>
        /// <param name="settings"></param>       
        private void BeginOpenJunction(SBJControllerSettings settings)
        {
            OpenJunctionMethodDelegate openJunctionDelegate = new OpenJunctionMethodDelegate(OpenJunction);
            AsyncCallback callback = new AsyncCallback(EndOpenJunction);
            IAsyncResult asyncResult = openJunctionDelegate.BeginInvoke(settings, callback, openJunctionDelegate);
        }
    
        /// <summary>
        /// End junction openning
        /// </summary>
        /// <param name="asyncResult"></param>
        private void EndOpenJunction(IAsyncResult asyncResult)
        {
            OpenJunctionMethodDelegate openJunctionDelegate = (OpenJunctionMethodDelegate)asyncResult.AsyncState;
            openJunctionDelegate.EndInvoke(asyncResult);
        }

        /// <summary>
        /// Open the junction
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private void OpenJunction(SBJControllerSettings settings)
        {
            //
            // Set the direction of the movement and stepping mode
            // And configure the first setpper delay (shorter) - faster movement
            //
            m_stepperMotor.Direction = StepperDirection.UP;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = settings.GeneralSettings.StepperWaitTime1;

            //
            // Read the initial voltgae before we've done anything
            //
            double initialVoltage = AnalogIn(0);
            bool isDelayedChanged = false;
            m_quitJunctionOpenningOperation = false;

            //
            // Until we've not been signaled from outer thread to stop we'll continue moving up.
            //
            while (!m_quitJunctionOpenningOperation)
            {
                m_stepperMotor.MoveSingleStep();
                double currentVoltage = AnalogIn(0);

                //
                // If voltgae had been changed in 0.0001% then switch to slow mode
                // Note that voltage can be negative so we must take the absoulute value
                //
                if (!isDelayedChanged && (Math.Abs(currentVoltage) < Math.Abs(initialVoltage) * 0.9999))
                {
                    m_stepperMotor.Delay = settings.GeneralSettings.StepperWaitTime2;
                    isDelayedChanged = true;
                }
                Thread.Sleep(m_stepperMotor.Delay);
            }
        }

        /// <summary>
        /// Save this run settings to Params file.
        /// </summary>
        /// <param name="settings"></param>
        private void SaveSettingsIfNeeded(SBJControllerSettings settings)
        {
            //
            // Verify that saving is required
            //
            if (settings.GeneralSettings.IsFileSavingRequired == false)
            {
                return;
            }

            //
            // Write the Params.txt file.
            //
            using (StreamWriter file = new StreamWriter(Path.Combine(settings.GeneralSettings.Path, c_settingsFileName), true))
            {
                file.WriteLine("---------------------------------------------");
                file.WriteLine(DateTime.Now.ToString());
                foreach (var settingsProperty in settings.GetType().GetProperties())
                {
                    foreach (var property in settingsProperty.PropertyType.GetProperties())
                    {
                        file.WriteLine(property.Name + ":\t" + property.GetValue(settingsProperty.GetValue(settings,null), null).ToString());
                    }
                }               
            }
        }
        
        /// <summary>
        /// Save aquired data to .txt file
        /// </summary>
        /// <param name="path">The path to write to</param>
        /// <param name="dataAquired">The data to write</param>
        /// <param name="fileNumber">The file number</param>
        /// <returns>The final file number in case of any changes (duplicates)</returns>
        private int SaveData(string path, double[,] dataAquired, int fileNumber)
        {
            int numberOfChannels = dataAquired.GetLength(0);
            int numberOfDataPoints = dataAquired.GetLength(1);
            int finalNumber = fileNumber;
            List<string> filesNames = GetFilesNames(finalNumber);
            string firstFileFullPath = Path.Combine(path, filesNames[0]);

            //
            // As long as the data file exists increase file number
            //
            while (File.Exists(firstFileFullPath))
            {
                filesNames = GetFilesNames(++finalNumber);
                firstFileFullPath = Path.Combine(path, filesNames[0]);
            }


            for (int i = 0; i < numberOfChannels; i++)
            {
                string fullPath =  Path.Combine(path, filesNames[i]);
                //
                // Write each data point in a new line
                //
                using (StreamWriter file = new StreamWriter(fullPath))
                {
                    for (int j = 0; j < numberOfDataPoints; j++)
                    {
                        file.WriteLine(dataAquired[i, j]);
                    }
                }
            }

            return finalNumber;
        }

        /// <summary>
        /// Generate the file name for the current cycle
        /// </summary>
        /// <param name="fileNumber"></param>
        /// <returns>The name of the file</returns>
        private List<string> GetFilesNames(int fileNumber)
        {
            List<string> filesNames = new List<string>(3);

            //
            // Generate file name for channel 1
            //
            StringBuilder channelOneStringBuilder = new StringBuilder(c_dataFileName);
            channelOneStringBuilder.Append(DateTime.Now.ToString("ddMMyy"));
            channelOneStringBuilder.Append("_");
            channelOneStringBuilder.Append(fileNumber);
            channelOneStringBuilder.Append(".txt");

            //
            // Generate file name for channel 2 and channel 3
            //
            string channelOneFileName = channelOneStringBuilder.ToString();
            string channelTwoFileName = channelOneFileName.Replace(c_dataFileName, c_lockInSignalFileName);
            string channelThreeFileName = channelOneFileName.Replace(c_dataFileName, c_lockInPhaseFileName);

            filesNames.Add(channelOneFileName);
            filesNames.Add(channelTwoFileName);
            filesNames.Add(channelThreeFileName);

            return filesNames;
        }
        
        /// <summary>
        /// Configure laser parameters.
        /// </summary>
        /// <param name="settings">The settings by which the laser is to be configured</param>
        /// <exception cref="SBJException"></exception>
        private void ConfigureLaserIfNeeded(SBJControllerSettings settings)
        {
            //
            // Return if no need to turn the laser on
            //
            if (!settings.LaserSettings.IsLaserOn)
            {
                return;
            }

            if (settings.LaserSettings.LaserMode.Equals("DC"))
            {
                m_taborLaserController.SetDCMode();
                m_taborLaserController.SetDcModeAmplitude(settings.LaserSettings.LaserAmplitude);
            }
            else
            {
                if (settings.LaserSettings.LaserMode.Equals("Square"))
                {
                    m_taborLaserController.SetSquareMode();
                    m_taborLaserController.SetSquareModeAmplitude(settings.LaserSettings.LaserAmplitude);
                    m_taborLaserController.SetSquareModeFrequency(settings.LaserSettings.LaserFrequency);
                }
                else
                {
                    throw new SBJException("Invalid laser mode. Expected modes: DC or Sqaure");
                }
            }
        }

        /// <summary>
        /// Get the task for the data aquisition
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Task GetMultipleChannelsTriggeredTask(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, worker, e);

            //
            // Determines the direction of the current - 
            // Either positive (then voltage is negative) or negative (then voltage is positive number)
            //
            bool isPositiveVoltage = AnalogIn(0) > 0;


            AnalogEdgeReferenceTriggerSlope triggerSlope = isPositiveVoltage ? AnalogEdgeReferenceTriggerSlope.Falling : AnalogEdgeReferenceTriggerSlope.Rising;
            double triggerVoltage = isPositiveVoltage ? settings.GeneralSettings.TriggerVoltage * (-1) : settings.GeneralSettings.TriggerVoltage;

            //
            // Create the task with its propertites
            //
            TriggeredTaskProperties taskProperties = new TriggeredTaskProperties(settings.LockInSettings.IsLockInSignalEnable,
                                                                                 settings.LockInSettings.IsLockInPhaseSignalEnable,
                                                                                 settings.GeneralSettings.SampleRate,
                                                                                 settings.GeneralSettings.TotalSamples,
                                                                                 triggerVoltage,
                                                                                 settings.GeneralSettings.PretriggerSamples,
                                                                                 triggerSlope);

            return m_daqController.CreateMultipleChannelsTriggeredTask(taskProperties);
        }

        //TODO: Add Summary here
        private void ApplyVoltageOnElectroMagnetIfNeeded(bool isEMEnabled)
        {
            if (isEMEnabled)
            {
                //TODO: Move 6.5 to constants
                m_electroMagnet.SetVoltage(6.5);
            }
        }
      
        #region ElectroMagnet

        /// <summary>
        /// Open the junction asynchronously by the ElectroMagnet
        /// </summary>
        /// <param name="settings"></param>       
        private void EMBeginOpenJunction(SBJControllerSettings settings)
        {
            EMOpenJunctionMethodDelegate emOpenJunctionDelegate = new EMOpenJunctionMethodDelegate(EMTryOpenJunction);
            AsyncCallback callback = new AsyncCallback(EMEndOpenJunction);
            IAsyncResult asyncResult = emOpenJunctionDelegate.BeginInvoke(settings, callback, emOpenJunctionDelegate);       
        }

        /// <summary>
        /// Try open junction by the EM, by calling EMOpenJunction.
        /// if min voltage exceeded without the junction being opened, do a few steps by the stepper motor, then retry EM (recursion).
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool EMTryOpenJunction(SBJControllerSettings settings)
        {
            //
            // if the EM reached voltage 0 without opening the junction, 
            // return to higher voltage on EM, do some steps by the stepper motor and retry opening by EM.
            //
            if (!EMOpenJunction(settings))
            {
                m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, 6.5);
                m_stepperMotor.Direction = StepperDirection.UP;
                m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
                m_stepperMotor.Delay = m_stepperMotor.MinDelay;
                m_stepperMotor.MoveMultipleSteps(100);
                m_stepperMotor.Shutdown();
                return EMTryOpenJunction(settings);
            }
            return true;
        }

        /// <summary>
        /// Open the junction by the ElectroMagnet.
        /// If min voltage exceeded without the junction being opened, return false. 
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private bool EMOpenJunction(SBJControllerSettings settings)
        {
            //
            // Set the direction of the movement and stepping mode
            // And configure the first setpper delay (shorter) - faster movement
            //
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = settings.ElectromagnetSettings.EMFastDelayTime;

            //
            // Read the initial voltgae before we've done anything
            //
            double initialVoltage = AnalogIn(0);
            bool isDelayedChanged = false;
            m_quitJunctionOpenningOperation = false;

            //
            // Until we've not been signaled from outer thread to stop we'll continue moving up.
            //
            while (!m_quitJunctionOpenningOperation)
            {
                if (!m_electroMagnet.MoveSingleStep())
                {
                    //
                    // if min Votlage on EM was exceeded return false.
                    //
                    return false;
                }
                double currentVoltage = AnalogIn(0);

                //
                // If voltgae had been changed in 0.0001% then switch to slow mode
                // Note that voltage can be negative so we must take the absoulute value
                //
                if (!isDelayedChanged && (Math.Abs(currentVoltage) < Math.Abs(initialVoltage) * 0.9999))
                {
                    m_electroMagnet.Delay = settings.ElectromagnetSettings.EMSlowDelayTime;
                    isDelayedChanged = true;
                }

                //
                // If hold-on trigger was exceeded, wait 10 ms and check if still true.
                //
                if (settings.ElectromagnetSettings.IsEMHoldOnEnable && Math.Abs(currentVoltage) < Math.Abs(settings.ElectromagnetSettings.EMHoldOnMaxVoltage * 1.1))
                {
                    Thread.Sleep(10);
                    if (Math.Abs(currentVoltage) < Math.Abs(settings.ElectromagnetSettings.EMHoldOnMaxVoltage * 1.1))
                    {
                        //
                        // trigger was truely exceeded. try to hold on to a certain junction voltage.
                        //
                        if (!StabilizeJunction(currentVoltage, settings.ElectromagnetSettings.EMHoldOnMaxVoltage, settings.ElectromagnetSettings.EMHoldOnMinVoltage))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                Thread.Sleep(m_electroMagnet.Delay);
            }
            return true;
        }

        /// <summary>
        /// A feedback loop that stabilizes the junction's voltage between maxVoltage and minVoltage.
        /// </summary>
        /// <param name="currentVoltage"></param>
        /// <param name="triggerVoltage"></param>
        /// <returns>false if min or max voltage was exceeded, true if the process has been stopped from another thread.</returns>
        private bool StabilizeJunction(double currentVoltage, double maxVoltage, double minVoltage)
        {
            double deviation = 0;
            double oldDev1=0, oldDev2=0, oldDev3=0, oldDev4=0, oldDev5=0;
            double diff = 0;
            
            //
            // The moment we entered this function, try to stop the junction from closing by opening a little.
            //
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = 5;
            m_electroMagnet.MoveMultipleSteps(5);

            //
            // this loop runs until the thread is stopped from the background, 
            // or until one of the limits of the EM voltage is exceeded.
            //
            while (!m_quitJunctionOpenningOperation)
            {
                currentVoltage = AnalogIn(0);
                if (currentVoltage > 10)
                {
                    currentVoltage = 10;
                }
                if (currentVoltage < -10)
                {
                    currentVoltage = -10;
                }
                //
                // save older deviations
                //
                oldDev5 = oldDev4;
                oldDev4 = oldDev3;
                oldDev3 = oldDev2;
                oldDev2 = oldDev1;
                oldDev1 = deviation;
                deviation = 0;
                
                //
                // if voltage at the junction is too low, move the electrodes one step closer
                //
                if (Math.Abs(currentVoltage) < Math.Abs(minVoltage))
                {
                    //
                    // calculate the deviation from target voltage. deviation is negative. 
                    //
                    deviation = Math.Abs(currentVoltage) - Math.Abs(minVoltage);

                    //
                    // calculate the derivative in order to know the slop of the trace. we average on 3 dots. 
                    //
                    diff = 0.333 * (oldDev2 + oldDev1 + deviation - oldDev5 - oldDev4 - oldDev3);

                    m_electroMagnet.Direction = StepperDirection.DOWN;
                    if (!m_electroMagnet.MoveSingleStep())
                    {
                        //
                        // if max Votlage on EM was exceeded return false.
                        //
                        return false;
                    }
                }

                //
                // if voltage at the junction is too high, open the junction one step
                //
                if (Math.Abs(currentVoltage) > Math.Abs(maxVoltage))
                {
                    //
                    // calculate the deviation from target voltage
                    //
                    deviation = Math.Abs(currentVoltage) - Math.Abs(maxVoltage);

                    //
                    // calculate the derivative in order to know the slop of the trace.
                    // if it's negative its good since we want the voltage to go down. 
                    // so we take the minus of diff.
                    //
                    diff = -0.333 * (oldDev2 + oldDev1 + deviation - oldDev5 - oldDev4 - oldDev3);

                    m_electroMagnet.Direction = StepperDirection.UP;
                    if (!m_electroMagnet.MoveSingleStep())
                    {
                        //
                        // if min Votlage on EM was exceeded return false.
                        //
                        return false;
                    }
                }
                             
                //
                // deviation is between 0-10. that means: time delay between 13-55 ms. 
                // the larger the deviation, the faster we move (=> shorter time delay) 
                // the values of diff can change between (+10) - (-10). if it's positive we're good and want to step slower. if negative - we want to step faster.
                //
                Thread.Sleep((int)(diff + 5 + 100/(Math.Abs(deviation) + 2)));              
            }
            return true;
        }

        /// <summary>
        /// End junction openning by EM
        /// </summary>
        /// <param name="asyncResult"></param>
        private void EMEndOpenJunction(IAsyncResult asyncResult)
        {
            EMOpenJunctionMethodDelegate emOpenJunctionDelegate = (EMOpenJunctionMethodDelegate)asyncResult.AsyncState;
            emOpenJunctionDelegate.EndInvoke(asyncResult);
        }

        /// <summary>
        /// Try obtain short circuit by ElectroMagnet, by calling EMShortCircuit.
        /// If max EM voltage was exceeded without getting a short circuit, use the stepper motor and retry by recurcion. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool EMTryObtainShortCircuit(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            switch (EMShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitVoltage, worker, e))
            {
                case 0:
                    //
                    // short contact succeeded. 
                    //
                    return false;

                case 1:
                    //
                    // the proccess was cancelled by the user.
                    //
                    return true;

                case 2:
                    //
                    // we've reached the max voltage on the EM without getting to contact.
                    // return voltage to zero and get the electrodes closer by the stepper motor, 
                    // then start over again. 
                    //
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, 6.5);
                    m_stepperMotor.Direction = StepperDirection.DOWN;
                    m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
                    m_stepperMotor.Delay = m_stepperMotor.MinDelay;
                    m_stepperMotor.MoveMultipleSteps(100);
                    m_stepperMotor.Shutdown();
                    return EMTryObtainShortCircuit(settings, worker, e);
            }
            return true;
        }

        /// <summary>
        /// Try and obtain short circut by the ElectroMagnet
        /// </summary>
        /// <param name="shortCircuitVoltage">The voltage indicator for short circuit</param>        
        /// <returns>2 whether max EM voltage was exceeded. 1 whether the operation was cancelled. 0 otherwise</returns>
        private int EMShortCircuit(int shortCircuitDelayTime, double shortCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentShortCircuit = false;
            bool isTempShortCircuit = false;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set EM direction and delay
            // 
            m_electroMagnet.Direction = StepperDirection.DOWN;
            m_electroMagnet.Delay = shortCircuitDelayTime;

            //
            // Reach to contact
            //
            while (!isPermanentShortCircuit)
            {
                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Move down 10 steps and check the voltage afterwords.
                // if the EM exceeded max voltage return 2.
                //
                if (!m_electroMagnet.MoveMultipleSteps(10))
                {
                    return 2;
                }
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If we reach contact both current voltage and voltgae after stepping
                // should be smaller than the short circuit threshold (since voltage is negative)
                //
                isTempShortCircuit = (currentVoltage > Math.Abs(shortCircuitVoltage)) &&
                                     (voltageAfterStepping > shortCircuitVoltage);

                //
                // If we think we've reached short circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempShortCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentShortCircuit = currentVoltage > Math.Abs(shortCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }
            return (e.Cancel ? 1 : 0);
        }
  
        #endregion
        
        #endregion

        #region Public Methods
        /// <summary>
        /// Try and obtain short circut
        /// </summary>
        /// <param name="shortCircuitVoltage">The voltage indicator for short circuit</param>        
        /// <returns>True whether the operation was cancelled. False otherwise</returns>
        public bool TryObtainShortCircuit(double shortCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentShortCircuit = false;
            bool isTempShortCircuit = false;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
            m_stepperMotor.Direction = StepperDirection.DOWN;
            m_stepperMotor.Delay = m_stepperMotor.MinDelay;

            //
            // Reach to contact
            //
            while (!isPermanentShortCircuit)
            {
                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                
                //
                // Move down 100 steps and check the voltage afterwords
                //
                m_stepperMotor.MoveMultipleSteps(100);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If we reach contact both current voltage and voltgae after stepping
                // should be smaller than the short circuit threshold (since voltage is negative)
                //
                isTempShortCircuit = (currentVoltage > Math.Abs(shortCircuitVoltage)) &&
                                     (voltageAfterStepping > shortCircuitVoltage);

                //
                // If we think we;ve reached short circuit than wait
                // for 10msec and than chekc again to verify this is permanent.
                //
                if (isTempShortCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentShortCircuit = currentVoltage > Math.Abs(shortCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }            
            }
            return e.Cancel;
        }

        /// <summary>
        /// Data aquisition
        /// </summary>
        /// <param name="settings">The settings for running the aquisition</param>
        /// <returns>True whether the operation was cacled by the user. False otherwise.</returns>
        public bool AquireData(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isCancelled = false;
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;
            double[,] dataAquired;

            //
            // Configure the laser if needed for this run
            //
            ConfigureLaserIfNeeded(settings);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings);

            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

            //
            // Create the task
            //
            m_task = GetMultipleChannelsTriggeredTask(settings, worker, e);


            //
            // Main loop for data aquisition
            //
            for (int i = 0; i < settings.GeneralSettings.TotalNumberOfCycles; i++)
            {
                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // if EM is enabled, and we are asked to skip the first cycle (that is done by the stepper motor), 
                // move on to the next cycle.
                //
                if (settings.ElectromagnetSettings.IsEMEnable && settings.ElectromagnetSettings.IsEMSkipFirstCycleEnable && i == 0)
                {
                    m_stepperMotor.Shutdown();
                    continue;
                }

                //
                // Turn off the laser before we reach contact
                //
                if (settings.LaserSettings.IsLaserOn)
                {
                    m_taborLaserController.TurnOff();
                    Thread.Sleep(5000);
                }

                //
                // Change the gain power to 5 before reaching contact
                // to ensure full contact current
                //
                m_amplifier.ChangeGain(5);

                //
                // Reach to contact before we start openning the junction
                // If EM is enabled and we're after the first cycle, use the EM.
                // If user asked to stop than exit
                //
                isCancelled = (settings.ElectromagnetSettings.IsEMEnable && i > 0) ? 
                               EMTryObtainShortCircuit(settings, worker, e) : 
                               TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, worker, e);
                if (isCancelled)
                {
                    break;
                }
                
                //
                // Configure the gain to the desired one before strating the measurement.
                // And also this is the time to switch the laser on.
                //
                int gainPower;
                Int32.TryParse(settings.GeneralSettings.Gain, out gainPower);
                m_amplifier.ChangeGain(gainPower);


                if (settings.LaserSettings.IsLaserOn)
                {
                    m_taborLaserController.TurnOn();
                }

                //
                // Start openning the junction.
                // If EM is enabled and we're after the first cycle, use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable && i > 0)
                {
                    EMBeginOpenJunction(settings);
                }
                else
                {
                    BeginOpenJunction(settings);
                }

                //
                // Start the task and wait for the data
                //
                
                try
                {
                    m_task.Start();
                }
                catch (DaqException ex)
                {
                    throw new SBJException("Error occured when tryin to start DAQ task", ex);
                }

                AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_task.Stream);
                
                try
                {
                    dataAquired = reader.ReadMultiSample(-1);

                    if (dataAquired.Length < settings.GeneralSettings.TotalSamples)
                    {
                        //
                        // If from some reason we weren't able to 
                        // receive all data points, ignore and continue;
                        //
                        m_task.Stop();
                        continue;
                    }
                }
                catch (DaqException)
                {
                    //
                    // Probably timeout.
                    // Ignore this cycle and rerun.
                    //
                    m_task.Stop();
                    continue;
                }

                //
                // At this point the reader has returned with all the data
                // so we can stop the openning of the junction.
                //
                m_quitJunctionOpenningOperation = true;
                m_task.Stop();

                //
                // if EM is enabled, the first trace was done by the stepper motor and we want to ignore it.
                //
                if (settings.ElectromagnetSettings.IsEMEnable && i == 0)
                {
                    //
                    // from now on we use the EM, so we don't want the stepper motor to stay on.
                    //
                    m_stepperMotor.Shutdown();
                    continue;
                }

                // 
                // Increase file number by one
                // Save data if needed
                //
                finalFileNumber++;
                if (settings.GeneralSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.GeneralSettings.Path, dataAquired, finalFileNumber);
                }

                //
                // Signal UI we have the data
                //
                if (DataAquired != null)
                {
                    DataAquired(this, new DataAquiredEventArgs(dataAquired, finalFileNumber));
                }                
            }

            //
            // Finish the measurement properly
            //
            if (settings.LaserSettings.IsLaserOn)
            {
                m_taborLaserController.TurnOff();
            }
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_electroMagnet.Shutdown();
            }
            m_task.Dispose();
            m_stepperMotor.Shutdown();

            return (isCancelled || e.Cancel);
        }
       
        /// <summary>
        /// Move the stepper up
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        public void MoveStepperUp(BackgroundWorker worker, DoWorkEventArgs e)
        {
            m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
            m_stepperMotor.Direction = StepperDirection.UP;
            while (!worker.CancellationPending)
            {
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(m_stepperMotor.MinDelay);
            }
            e.Cancel = true;
        }


        /// <summary>
        /// Fix the bias incase of drift in the source meter
        /// </summary>
        /// <param name="shortCircuitVoltage"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        public void FixBias(double shortCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            int i = 0;
            bool isBiasedFixed = false;
            double biasFixValue = 0;
            double currentVoltage = 0;

            //
            // First, reach to contact
            //
            TryObtainShortCircuit(shortCircuitVoltage, worker, e);

            //
            // Set bias to zero and lets check where we stand.
            // If everything is OK then at zero bias the current 
            // should be also zero. If it's not, correction should be made.
            //
            m_sourceMeter.SetBias(0);
            double previousVoltage = AnalogIn(0);

            //
            // We need to know if voltage needs to be increased or decreased.
            // If we measure positive voltage that means that the current 
            // is negative and the bias should be increased.
            //
            int biasChangeFactor = previousVoltage > 0 ? 1 : -1;


            while (!worker.CancellationPending && !isBiasedFixed)
            {
                //
                // Change the bias with accuracy of 0.0005;
                //
                biasFixValue = ++i * 0.0005 * biasChangeFactor;
                m_sourceMeter.SetBias(biasFixValue);
                
                //
                // Wait before reading the voltage again
                //
                Thread.Sleep(1000);
                currentVoltage = AnalogIn(0);

                //
                // We know we've fixed the bias only when the current 
                // changed its sign so the multiplication of the two
                // values would negative. 
                //
                if (currentVoltage * previousVoltage < 0)
                {
                    isBiasedFixed = true;

                    //
                    // Set result to the correction value which is the average
                    // between the current bias and the previous one.
                    //
                    e.Result = (biasFixValue + ((i - 1) * 0.0005 * biasChangeFactor)) / 2;
                }
                else
                {
                    //
                    // Still we havent crossed the zero point so
                    // update the voltage value and continue
                    // 
                    previousVoltage = currentVoltage;
                }
            }
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Save sample's data to log book
        /// </summary>
        /// <param name="bottom">The bottom data</param>
        /// <param name="top">The top data</param>
        public void WriteToSamplesLog(Sample bottom, Sample top)
        {
            //
            // Try and open excel file
            //
            Microsoft.Office.Interop.Excel.Application xlsLogBook = new Microsoft.Office.Interop.Excel.Application();
            if (xlsLogBook == null)
            {
                throw new SBJException("Excel is not installed on current machine.");
            }

            //
            // Get the excel file from the settings file
            //
            string logBookPath = Settings.Default.SamplesLogBookPath;
            if (!File.Exists(logBookPath))
            {
                throw new SBJException(string.Format("Can't find sample's logbook on path: {0}.", logBookPath));
            }

            //
            // Open the first workbook and sheet
            // 
            Workbook workbook = xlsLogBook.Workbooks.Open(logBookPath,  Type.Missing,
                                                          false, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing);
            Worksheet worksheet = workbook.ActiveSheet as Worksheet;

            //
            // Find the first empty row that is free for editing
            //
            int lastUsedRow = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
            int freeRow = lastUsedRow + 1;

            //
            // Write the data
            //
            worksheet.Cells[freeRow, 1] = DateTime.Now.ToShortDateString();
            worksheet.Cells[freeRow, 2] = bottom.ID;
            worksheet.Cells[freeRow, 3] = bottom.FirstLayerMetal.ToString();
            worksheet.Cells[freeRow, 4] = bottom.FirstLayerThickness;
            worksheet.Cells[freeRow, 5] = bottom.SecondLayerMetal.ToString();
            worksheet.Cells[freeRow, 6] = bottom.SecondLayerThickness;
            worksheet.Cells[freeRow, 7] = bottom.ElectrodeWidth;
            worksheet.Cells[freeRow, 8] = bottom.Molecule;
            worksheet.Cells[freeRow, 9] = bottom.Solvent;
            worksheet.Cells[freeRow, 10] = bottom.Dry;
            worksheet.Cells[freeRow, 11] = bottom.Piranha;
            worksheet.Cells[freeRow, 12] = bottom.Refabricated;
            worksheet.Cells[freeRow, 13] = bottom.Comments;

            //
            // Now write the top data
            //
            worksheet.Cells[freeRow, 14] = top.ID;
            worksheet.Cells[freeRow, 15] = top.FirstLayerMetal.ToString();
            worksheet.Cells[freeRow, 16] = top.FirstLayerThickness;
            worksheet.Cells[freeRow, 17] = top.SecondLayerMetal.ToString();
            worksheet.Cells[freeRow, 18] = top.SecondLayerThickness;
            worksheet.Cells[freeRow, 19] = top.ElectrodeWidth;
            worksheet.Cells[freeRow, 20] = top.Molecule;
            worksheet.Cells[freeRow, 21] = top.Solvent;
            worksheet.Cells[freeRow, 22] = top.Dry;
            worksheet.Cells[freeRow, 23] = top.Piranha;
            worksheet.Cells[freeRow, 24] = top.Refabricated;
            worksheet.Cells[freeRow, 25] = top.Comments;

            //
            // Use this code to save the file for the forst time.
            //
            //workbook.SaveAs(logBookPath, 51, null, null, null, null, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);

            //
            // Save and exit. Free resources as this is a COM object and we don't want the excel.exe process to hang.
            //
            workbook.Save();
            workbook.Close(true, Type.Missing, Type.Missing);            
            xlsLogBook.Quit();
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(xlsLogBook);
        }

        /// <summary>
        /// Open samples logbook
        /// </summary>
        public void OpenSamplesLog()
        {
            Microsoft.Office.Interop.Excel.Application xlsLogBook = new Microsoft.Office.Interop.Excel.Application();
            if (xlsLogBook == null)
            {
                throw new SBJException("Excel is not installed on current machine.");
            }
            string logBookPath = Settings.Default.SamplesLogBookPath;
            Workbook workbook = xlsLogBook.Workbooks.Open(logBookPath, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing, Type.Missing,
                                                          Type.Missing);
            xlsLogBook.Visible = true;
        }

        /// <summary>
        /// Change the applied bias
        /// </summary>
        /// <param name="bias">New bias in volts</param>
        /// <exception cref="SBJException"></exception>
        public void ChangeBias(double bias)
        {
            m_sourceMeter.SetBias(bias);
        }

        /// <summary>
        /// Chage the applied gain
        /// </summary>
        /// <param name="gain">The new gain value</param>
        /// <exception cref="SBJException"></exception>
        public void ChangeGain(int gain)
        {
            m_amplifier.ChangeGain(gain);
        }

        public void ControllerTabControlDeselected()
        {
            m_electroMagnet.Shutdown();
        }

        internal void ShutDown()
        {
            m_stepperMotor.Shutdown();
            m_sourceMeter.Shutdown();
            m_amplifier.Shutdown();
            m_electroMagnet.Shutdown();
            m_taborLaserController.Shutdown();
            if (m_task != null)
            {
                m_task.Dispose();
            }
        }
        #endregion

        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double AnalogIn(byte channel);
        #endregion       
    
 
    }
}

