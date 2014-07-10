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
using System.Linq;

namespace SBJController
{
    /// <summary>
    /// Represents the main class for controlling the Squeezable Break Junction
    /// </summary>
    public partial class SBJController
    {
        #region Private Members
        private StepperMotor m_stepperMotor;
        private Amplifier m_amplifier;
        private SourceMeter m_sourceMeter;
        private ILaserController m_LaserController;
        private TaborEOMController m_taborFirstEOMController;
        private TaborEOMController m_taborSecondEOMController;
        private LockIn m_LockIn;
        private LambdaZup m_lambdaZup;
        private DataAcquisitionController m_daqController;
        private Task m_triggeredTask;
        private Task m_realTimeTask;
        private AnalogEdgeReferenceTriggerSlope m_triggerSlope;
        private double m_triggerVoltage;
        private bool m_quitRealTimeOperation;
        private bool m_quitJunctionOpenningOperation;
        private bool m_quitJunctionClosingOperation;
        private const string c_settingsFileName = "Params.txt";
        private const string c_dataFileName = "StatDAQ_";
        private const string c_lockInSignalFileName = "LockInSignal_";
        private const string c_lockInPhaseFileName = "LockInPhase_";
        private const string c_rawDataFolderName = "RawData\\";
        private const string c_physicalDataFolderName = "PhysicalData\\";
        private const string c_additionalDataFolderName = "AdditionalData\\";
        private delegate void OpenJunctionMethodDelegate(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e);
        private delegate void CloseJunctionMethodDelegate(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e);
        public delegate void DataAquiredEventHandler(object sender, DataAquiredEventArgs e);
        public event DataAquiredEventHandler DataAquired;
        public event DataAquiredEventHandler DoneReadingData;
        #endregion

        #region Properties
        
        public Task TriggeredTask
        {
            get { return m_triggeredTask; }
            set { m_triggeredTask = value; }
        }

        public Task RealTimeTask
        {
            get { return m_realTimeTask; }
            set { m_realTimeTask = value; }
        }        
        
        public bool QuitJunctionOpenningOperation
        {
            get { return m_quitJunctionOpenningOperation; }
            set { m_quitJunctionOpenningOperation = value; }
        }

        public bool QuitJunctionClosingOperation
        {
            get { return m_quitJunctionClosingOperation; }
            set { m_quitJunctionClosingOperation = value; }
        }

        public bool QuitRealTimeOperation
        {
            get { return m_quitRealTimeOperation; }
            set { m_quitRealTimeOperation = value; }
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

        public LockIn LockIn
        {
            get { return m_LockIn; }
            set { m_LockIn = value; }
        }    

        public ILaserController LaserController
        {
            get { return m_LaserController; }
            set { m_LaserController = value; }
        }

        public TaborEOMController TaborFirstEOM
        {
            get { return m_taborFirstEOMController; }
            set { m_taborFirstEOMController = value; }
        }

        public TaborEOMController TaborSecondEOM
        {
            get { return m_taborSecondEOMController; }
            set { m_taborSecondEOMController = value; }
        }

        public LambdaZup LambdaZup
        {
            get { return m_lambdaZup; }
            set { m_lambdaZup = value; }
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
            m_quitJunctionClosingOperation = false;
            m_LockIn = new LockIn();
            m_lambdaZup = new LambdaZup();
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
        }

        /// <summary>
        /// Open the junction asynchronously
        /// </summary>
        /// <param name="settings"></param>       
        private void BeginOpenJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            OpenJunctionMethodDelegate openJunctionDelegate = new OpenJunctionMethodDelegate(OpenJunction);
            AsyncCallback callback = new AsyncCallback(EndOpenJunction);
            IAsyncResult asyncResult = openJunctionDelegate.BeginInvoke(settings, worker, e, callback, openJunctionDelegate);
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
        private void OpenJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
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
                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                m_stepperMotor.MoveSingleStep();
                double currentVoltage = AnalogIn(0);

                //
                // If voltgae had been changed in 0.0001% then switch to slow mode
                // Note that voltage can be negative so we must take the absoulute value
                //
                if (!isDelayedChanged && (Math.Abs(currentVoltage) < Math.Abs(initialVoltage) * 0.95))
                {
                    m_stepperMotor.Delay = settings.GeneralSettings.StepperWaitTime2;
                    isDelayedChanged = true;
                }
                Thread.Sleep(m_stepperMotor.Delay);
            }
        }

        /// <summary>
        /// Close the junction asynchronously
        /// </summary>
        /// <param name="settings"></param>       
        private void BeginCloseJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            CloseJunctionMethodDelegate closeJunctionDelegate = new CloseJunctionMethodDelegate(CloseJunction);
            AsyncCallback callback = new AsyncCallback(EndCloseJunction);
            IAsyncResult asyncResult = closeJunctionDelegate.BeginInvoke(settings, worker, e, callback, closeJunctionDelegate);
        }

        /// <summary>
        /// End junction openning
        /// </summary>
        /// <param name="asyncResult"></param>
        private void EndCloseJunction(IAsyncResult asyncResult)
        {
            CloseJunctionMethodDelegate closeJunctionDelegate = (CloseJunctionMethodDelegate)asyncResult.AsyncState;
            closeJunctionDelegate.EndInvoke(asyncResult);
        }

        /// <summary>
        /// Open the junction
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private void CloseJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // Set the direction of the movement and stepping mode
            // And configure the first setpper delay.
            //
            m_stepperMotor.Direction = StepperDirection.DOWN;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = settings.GeneralSettings.StepperWaitTime1;

            //
            // Read the initial voltgae before we've done anything
            // Keep polling the value until it is abit different than 0.
            // This is a problem of instability with Flaxer's card.
            //
            double initialVoltage = AnalogIn(0);
            while (initialVoltage == 0.0)
            {
                initialVoltage = AnalogIn(0);
            }
            bool isDelayedChanged = false;            
            m_quitJunctionClosingOperation = false;

            //
            // Until we've not been signaled from outer thread to stop we'll continue moving up.
            //
            while (!m_quitJunctionClosingOperation)
            {
                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                m_stepperMotor.MoveSingleStep();

                double currentVoltage = AnalogIn(0);

                //
                // If voltgae had been changed in 0.0001% then switch to slow mode
                // Note that voltage can be negative so we must take the absoulute value
                // The two numbers must be of the same sign in order for us to compare them
                //
                if (!isDelayedChanged && (currentVoltage * initialVoltage > 0 ) && (Math.Abs(currentVoltage) > Math.Abs(initialVoltage) * 50))
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
        private void SaveSettingsIfNeeded(object settings, bool IsFileSavingRequired, string path)
        {
            //
            // Verify that saving is required
            //
            if(!IsFileSavingRequired)
            {
                return;
            }

            //
            // Write the Params.txt file.
            //
            using (StreamWriter file = new StreamWriter(Path.Combine(path, c_settingsFileName), true))
            {
                file.WriteLine("---------------------------------------------");
                file.WriteLine(DateTime.Now.ToString());
                foreach (var settingsProperty in settings.GetType().GetProperties())
                {
                    file.WriteLine(settingsProperty.GetValue(settings, null).ToString());                    
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
        private int SaveData(string path, IList<IDataChannel> activeChannels, IList<IDataChannel> physicalChannels, int fileNumber)
        {
            int finalNumber = fileNumber;
         
            //
            // we need to save the raw data only from the active channels, since the complex channels has the same raw data.
            //
            foreach (var channel in activeChannels)
            {
                //
                // If it is a simple data channel we can append all the data sets.
                //
                double[] data = channel.RawData[0];
                if (channel is SimpleDataChannel)
                {
                    List<double> flatList = new List<double>();
                    for (int i = 0; i < channel.RawData.Count; i++)
                    {
                        flatList.AddRange(channel.RawData[i]);
                    }
                    data = flatList.ToArray();
                }

                
                //
                // save raw data to file
                //
                finalNumber = WriteSingleArrayToFile(path, c_rawDataFolderName, channel.Name, fileNumber, data);
            }

            //
            // saveing the physical and additional data from all the channels, including the complex
            //
            foreach (var channel in physicalChannels)
            {
                //
                // save physical data to file
                //
                finalNumber = WriteSingleArrayToFile(path, c_physicalDataFolderName, channel.Name, finalNumber, channel.PhysicalData[0]) ;
                
                //
                // save addition data to files
                //
                fileNumber = WriteListOfArraysToFile(path, c_additionalDataFolderName, channel.Name, finalNumber, channel.AdditionalData);             
            }
            return finalNumber;
        }

        /// <summary>
        /// write a double[] data to a .txt file, each data point in a new line.
        /// </summary>
        /// <param name="path">the path in which to save the file</param>
        /// <param name="subFolderName">sub-folder in which to save the file</param>
        /// <param name="channelName">The name of the channel</param>
        /// <param name="fileNumber">number of the file</param>
        /// <param name="data">the data to write</param>
        /// <returns></returns>
        private int WriteSingleArrayToFile(string path, string subFolderName, string channelName, int fileNumber, double[] data)
        {
            int finalNumber;
            string fullPath = GetFileName(path, subFolderName, channelName, fileNumber, out finalNumber, -1);

            //
            // create a folder if needed, does nothing if not
            //
            FileInfo fileInfo = new FileInfo(fullPath);
            (fileInfo).Directory.Create();

            //
            // Write each data point in a new line
            //
            using (StreamWriter file = new StreamWriter(fullPath))
            {
                for (int j = 0; j < data.Length; j++)
                {
                    file.WriteLine(data[j]);
                }
            }
            return finalNumber;
        }

        /// <summary>
        /// Write a List<List<double[]>> into a .txt file, two data points in each line.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subFolderName"></param>
        /// <param name="channelName"></param>
        /// <param name="fileNumber"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private int WriteListOfArraysToFile(string path, string subFolderName, string channelName, int fileNumber, IList<IList<double[]>> data)
        {
            //
            // run over all the items (IV cycles) in the list
            //
            for (int k = 0; k < data.Count; k++)
            {
                string fullPath = GetFileName(path, subFolderName, channelName, fileNumber, out fileNumber, k + 1);
                
                //
                // create a folder if needed, does nothing if not
                //
                (new FileInfo(fullPath)).Directory.Create();

                //
                // Write each data point in a new line
                //
                using (StreamWriter file = new StreamWriter(fullPath))
                {
                    //
                    // run over all dots in one IV cycle
                    //
                    for (int j = 0; j < data[k][0].Length; j++)
                    {
                        //
                        // write the current and then the voltage on the same line in the file
                        //
                        StringBuilder dataInLine = new StringBuilder(data[k][0][j].ToString());
                        dataInLine.Append(" ");
                        dataInLine.Append(data[k][1][j]);
                        file.WriteLine(dataInLine);
                    }
                }
            }
            return fileNumber;            
        }

        /// <summary>
        /// Get the file name.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="channelName"></param>
        /// <param name="fileNumber">number of trace</param>
        /// <param name="secondaryNumber">put -1 if not needed!</param>
        /// <returns></returns>
        private string GetFileName(string path, string subFolderName, string channelName, int fileNumber, out int finalFileNumber, int secondaryNumber)
        {
            StringBuilder fileNameStringBuilder = new StringBuilder(channelName);
            fileNameStringBuilder.Append(DateTime.Now.ToString("ddMMyy"));
            fileNameStringBuilder.Append("_");
            fileNameStringBuilder.Append(fileNumber);
            if (secondaryNumber > 0)
            {
                fileNameStringBuilder.Append("_");
                fileNameStringBuilder.Append(secondaryNumber);
            }
            fileNameStringBuilder.Append(".txt");

            string fileFullPath = Path.Combine(Path.Combine(path, subFolderName), fileNameStringBuilder.ToString());

            //
            // As long as the data file exists increase file number
            //
            while (File.Exists(fileFullPath))
            {
                fileFullPath = GetFileName(path, subFolderName, channelName, ++fileNumber, out finalFileNumber, secondaryNumber);
            }
            
            finalFileNumber = fileNumber;
            
            return fileFullPath;
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

            //ILaserController laserController = null;

            if (settings.LaserSettings.LaserMode.Equals("IODrive"))
            {
                (m_LaserController as IODriveLaserController).SetAmplitude(settings.LaserSettings.LaserAmplitudeVolts);                
            }
            else
            {
                if (settings.LaserSettings.LaserMode.Equals("DC"))
                {
                    (m_LaserController as TaborLaserController).SetDCMode(settings.LaserSettings.LaserAmplitudeVolts);
                }
                else
                {
                    if (settings.LaserSettings.LaserMode.Equals("Square"))
                    {
                        (m_LaserController as TaborLaserController).SetSquareMode(settings.LaserSettings.LaserFrequency, settings.LaserSettings.LaserAmplitudeVolts);                        
                    }
                    else
                    {
                        throw new SBJException("Invalid laser mode. Expected modes: DC or Sqaure");
                    }
                }
            }

            if (settings.LaserSettings.IsFirstEOMOn)
            {
                m_taborFirstEOMController.SetSinusoidMode(settings.LaserSettings.FirstEOMFrequency);
                m_taborFirstEOMController.TurnOn();
            }

            if (settings.LaserSettings.IsSecondEOMOn)
            {
                m_taborSecondEOMController.SetSinusoidMode(settings.LaserSettings.SecondEOMFrequency);
                m_taborSecondEOMController.TurnOn();
        }
        }

        /// <summary>
        /// Get the task for the data aquisition
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Task GetMultipleChannelsTriggeredTask(SBJControllerSettings settings, RunDirection runDirection, AnalogEdgeReferenceTriggerSlope triggerSlope, double triggerVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            AnalogEdgeReferenceTriggerSlope localTriggerSlope;
            double localTriggerVoltage;

            //
            // Determine the trigger slope direction and voltage according to sign of the measured signal.
            // This is done only once, for the first time this method is called.
            // Then these value are saved as class members.
            //
            if (triggerSlope > 0)
            {
                localTriggerSlope = triggerSlope;
                localTriggerVoltage = triggerVoltage;
            }
            else
            {
                //
                // Reach to contact in order to retrieve the signal.
                // 
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitVoltage, worker, e);
                }
                else
                {
                    TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, settings.GeneralSettings.UseShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitDelayTime, worker, e);
                }

                //
                // Determines the direction of the current - 
                // Either positive (then voltage is negative) or negative (then voltage is positive number)
                //
                bool isPositiveVoltage = AnalogIn(0) > 0;

                //
                // Trigger's slope depends both on voltage sign and also on the direction of measurement - break or make junction.
                //
                if (isPositiveVoltage)
                {
                    localTriggerSlope = (runDirection == RunDirection.Break) ? AnalogEdgeReferenceTriggerSlope.Falling : AnalogEdgeReferenceTriggerSlope.Rising;
                }
                else
                {
                    localTriggerSlope = (runDirection == RunDirection.Break) ? AnalogEdgeReferenceTriggerSlope.Rising : AnalogEdgeReferenceTriggerSlope.Falling;
                }

                localTriggerVoltage = isPositiveVoltage ? settings.GeneralSettings.TriggerVoltage * (-1) : settings.GeneralSettings.TriggerVoltage;

                //
                // Open the junction once again as if we didn't do anything.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMTryObtainOpenCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.OpenCircuitVoltage, worker, e);
                }
                else
                {
                    TryObtainOpenCircuit(settings.GeneralSettings.OpenCircuitVoltage, worker, e);
                }
            }            

            //
            // Create the task with its propertites
            //
            TriggeredTaskProperties taskProperties = new TriggeredTaskProperties(settings.ChannelsSettings.ActiveChannels,                                                                                 
                                                                                 settings.GeneralSettings.SampleRate,
                                                                                 settings.GeneralSettings.TotalSamples,
                                                                                 localTriggerVoltage,
                                                                                 settings.GeneralSettings.PretriggerSamples,
                                                                                 localTriggerSlope);           

            return m_daqController.CreateMultipleChannelsTriggeredTask(taskProperties);
        }

        /// <summary>
        /// Assign the aquired data to each active channel
        /// </summary>
        /// <param name="activeChannels">The list of active channels from which we sampled our data</param>
        /// <param name="rawData">The raw data as receieved from the DAQ card</param>
        private void AssignRawDataToChannels(IList<IDataChannel> activeChannels, double[,] rawData)
        {
            //
            // The number of rows in rawData matrix must be equal to the number of channels
            //
            if (activeChannels.Count != rawData.GetLength(0))
            {
                throw new SBJException("The number of active channels doesn't match the number of channels within the aquired data.");
            }

            for (int i = 0; i < rawData.GetLength(0); i++)
            {
                List<double> channelRawData = new List<double>(rawData.GetLength(1));
                for (int j = 0; j< rawData.GetLength(1); j++)
                {
                    channelRawData.Add(rawData[i, j]);
                }

                activeChannels[i].RawData.Add(channelRawData.ToArray());
            }
        }

        /// <summary>
        /// Get the channels to display from the active channels from which we sampled from.
        /// </summary>
        /// <param name="activeChannels">The current active channels</param>
        /// <returns>A complete set of the relevant data channels that can be displayed</returns>
        private List<IDataChannel> GetChannelsForDisplay(IList<IDataChannel> activeChannels)
        {
            //
            // Check for complex data channels possible combinations
            //
            List<IDataChannel> complexLockInInternalChannels = new List<IDataChannel>();
            List<IDataChannel> complexLockInExternalChannels = new List<IDataChannel>();
            List<IDataChannel> complexIVChannels = new List<IDataChannel>();

            for (int i = 0; i < activeChannels.Count; i++)
            {               
                if (activeChannels[i].GetType().Equals(typeof(LockInXInternalSourceDataChannel)) ||
                    activeChannels[i].GetType().Equals(typeof(LockInYInternalSourceDataChannel)))
                {
                    complexLockInInternalChannels.Add(activeChannels[i]);
                }

                if (activeChannels[i].GetType().Equals(typeof(LockInXExternalSourceChannel)) ||
                   activeChannels[i].GetType().Equals(typeof(LockInYExternalSourceChannel)))
                {
                    complexLockInExternalChannels.Add(activeChannels[i]);
                }

                if (activeChannels[i].GetType().Equals(typeof(IVInputDataChannel)) ||
                    activeChannels[i].GetType().Equals(typeof(InputVoltageMonitorChannel)))
                {
                    complexIVChannels.Add(activeChannels[i]);
                }
            }

            //
            // Each active channels is an option channel for display.
            // Also we must add available complex data channel which we couldn't
            // sampled directly.
            //
            IList<IDataChannel> possibleChannelsForDisplay = new List<IDataChannel>(activeChannels);

            if (complexLockInInternalChannels.Count == 2)
            {
                LockInXYInternalSourceDataChannel XYChannel = new LockInXYInternalSourceDataChannel(complexLockInInternalChannels[0].DataConvertionSettings);
                XYChannel.RawData = new List<double[]>(complexLockInInternalChannels[0].RawData);
                XYChannel.RawData.Add(complexLockInInternalChannels[1].RawData[0]);
                possibleChannelsForDisplay.Add(XYChannel);
            }

            if (complexLockInExternalChannels.Count == 2)
            {
                LockInXYExtrenalSourceDataChannel XYChannel = new LockInXYExtrenalSourceDataChannel(complexLockInExternalChannels[0].DataConvertionSettings);
                XYChannel.RawData = new List<double[]>(complexLockInExternalChannels[0].RawData);
                XYChannel.RawData.Add(complexLockInExternalChannels[1].RawData[0]);
                possibleChannelsForDisplay.Add(XYChannel);
            }


            if (complexIVChannels.Count == 2)
            {
                IVProcessedDataChannel IVProcessedChannel = new IVProcessedDataChannel(complexIVChannels[0].DataConvertionSettings);
                IVProcessedChannel.RawData = new List<double[]>(complexIVChannels[0].RawData);
                IVProcessedChannel.RawData.Add(complexIVChannels[1].RawData[0]);
                possibleChannelsForDisplay.Add(IVProcessedChannel);
            } 
            return possibleChannelsForDisplay as List<IDataChannel>;
        }

        /// <summary>
        /// Calculates the physical data in each channel.
        /// </summary>
        /// <param name="physicalChannels"></param>
        /// <returns></returns>
        private void GetPhysicalData(IList<IDataChannel> physicalChannels)
        {
            foreach (var channel in physicalChannels)
            {
                channel.ConvertToPhysicalData();
            }
        }

        /// <summary>
        /// Clear raw data from channels list
        /// </summary>
        /// <param name="activeChannels"></param>
        private void ClearRawData(IList<IDataChannel> activeChannels)
        {
            foreach (var channel in activeChannels)
            {
                channel.RawData.Clear();
            }
        }

        /// <summary>
        /// Converts the data to matrix form
        /// </summary>
        /// <param name="data">The data as a list of lists</param>
        /// <returns>The data as a metrix</returns>
        private double[,] ConvertDataToMatrix(List<List<double>> data)
        {
            //
            // The number of items in the lists (i.e the number of lists)
            // is the number of data channels.
            //
            int numberOfChannles = data.Count;

            //
            // The number of data points per channel is the number of items in each inner list
            //
            int numberOfDataPointsPerChannel = data[0].Count;
            
            //
            // Init matrix and populate it
            //
            double[,] dataAsMatrix = new double[numberOfChannles, numberOfDataPointsPerChannel];
            for (int i = 0; i < numberOfChannles; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    dataAsMatrix[i, j] = data[i][j];
                }
            }
            return dataAsMatrix;
        }

        /// <summary>
        /// Average on the data set
        /// </summary>
        /// <param name="fullDataSet">The full data set to be averaged on</param>
        /// <param name="numberOfPointsToAverage">The number of points to average</param>
        /// <returns>A new averaged data set</returns>
        private List<List<double>> GetAveragedData(List<List<double>> fullDataSet, int numberOfPointsToAverage)
        {
            //
            // The number of channels is the number of lists in the data
            //
            int numberOfChannels = fullDataSet.Count;

            //
            // Initialize parameters
            //
            List<List<double>> averagedData = new List<List<double>>(numberOfChannels);
            double averagePoint = 0;

            //
            // Iterate over the channels and average each ones data
            //
            for (int i = 0; i < numberOfChannels; i++)
            {
                //
                // The new list of averaged data is now initialized
                //
                List<double> channelAveragedData = new List<double>();

                //
                // As long as we have more items in the original list
                // than the number of items we are required to averaged on
                // then we can continue averaging.
                //
                while (fullDataSet[i].Count >= numberOfPointsToAverage)
                {
                    //
                    // Take first amount of items and average.
                    //
                    averagePoint = fullDataSet[i].Take(numberOfPointsToAverage).Average();
                    channelAveragedData.Add(averagePoint);

                    //
                    // After being used we can remove these items from the orginial list
                    // So that in the next iteration it will not be taken again
                    //
                    fullDataSet[i].RemoveRange(0, numberOfPointsToAverage);     
                }

                //
                // Since we are out of the "while" loop these means that we are left
                // with less than 'numberOfPointsToAverage' in the original list,
                // So we'll take what is left and average on them.
                //
                averagePoint = fullDataSet[i].Take(numberOfPointsToAverage).Average();
                channelAveragedData.Add(averagePoint);

                //
                // Now we can add the new averaged data set to the returned variable
                //
                averagedData.Add(channelAveragedData);
            }

            return averagedData;
        }

        /// <summary>
        /// Get an average data point for each sampled channel
        /// </summary>
        /// <param name="dataAquired">The acquired data</param>
        /// <returns>List of all the average data from each channel</returns>
        private List<double> GetAverageDataValue(double[,] dataAquired)
        {
            int numberOfDataSampled = dataAquired.GetLength(1);
            int numberOfChannels = dataAquired.GetLength(0);
            List<double> averageData = new List<double>(numberOfChannels);

            //
            // Flattens the matrix to a list so that each row is appended after the row before.
            // The result is one list: 2D [a,a,a;b,b,b;c,c,c] ==> 1D (a,a,a,b,b,b,c,c,c)
            //
            IEnumerable<double> flatMatrix = dataAquired.Cast<double>();
            for (int i = 0; i < numberOfChannels; i++)
            {
                //
                // Take only the data relevant for the specific channel.
                // This is why we skip some of the data.
                //
                averageData.Add(flatMatrix.Skip(numberOfDataSampled * i).Take(numberOfDataSampled).Average());
            }
            return averageData;
        }

        /// <summary>
        /// Aquire data from an open position while closing the junction
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool PerformMakeJunctionCycles(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double[,] dataAquired;
            bool isCancelled = false;
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;
            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            //
            // Change the gain power to 5 before reaching contact
            // to ensure full contact current
            //
            if (settings.GeneralSettings.UseDefaultGain)
            {
                m_amplifier.ChangeGain(5);
            }

            //
            // Configure the gain to the desired one if we changed it to E5 for reaching contact            
            //
            if (settings.GeneralSettings.UseDefaultGain)
            {
                int gainPower;
                Int32.TryParse(settings.GeneralSettings.Gain, out gainPower);
                m_amplifier.ChangeGain(gainPower);
            }

            //
            // Before stat measuring, bring to contact.
            // Use stepper motor only since we could be far away from reaching contact
            // It is an important step since we can't be sure in what position we are currently standing.
            //
            TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, settings.GeneralSettings.UseShortCircuitDelayTime,settings.GeneralSettings.ShortCircuitDelayTime, worker, e);

            //
            // Now we can begin our measurements cycles
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
                // Turn off the laser before we open the junction
                //
                if (settings.LaserSettings.IsLaserOn)
                {
                    m_LaserController.TurnOff();
                    Thread.Sleep(5000);
                }

                //
                // Before we start the measurement cycles we reach to contact and then open
                // It is important to reach to contact before trying to open since
                // there could be a situation where the junction was not closed entirely in the previous 
                // cycle and so it still seems open where actually it is not.
                // No need to do it in the first cycle since this is done outside the for loop.
                //
                if (settings.ElectromagnetSettings.IsEMEnable && i > 0)
                {
                    EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitVoltage, worker, e);
                }
                else
                {
                    TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, settings.GeneralSettings.UseShortCircuitDelayTime,settings.GeneralSettings.ShortCircuitDelayTime, worker, e);
                }

                //
                // Reach to open circuit for initial position
                // If we intend to use the EM, for the first cycle only, use the stepper motor since
                // we initially reached to contact with the stepper motor and the EM doesn't have enough force
                // to withdraw the cantiliver from this intially squeezed position.
                //
                isCancelled = (settings.ElectromagnetSettings.IsEMEnable && i > 0) ?
                              EMTryObtainOpenCircuit(settings.ElectromagnetSettings.EMOpenCircuitDelayTime, settings.GeneralSettings.OpenCircuitVoltage, worker, e) :
                              TryObtainOpenCircuit(settings.GeneralSettings.OpenCircuitVoltage, worker, e);

                if (isCancelled)
                {
                    break;
                }

                //
                // Turn back on the laser
                //
                if (settings.LaserSettings.IsLaserOn)
                {
                    m_LaserController.TurnOn();
                }

                //
                // Start making the junction.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMBeginCloseJunction(settings, worker, e);
                }
                else
                {
                    BeginCloseJunction(settings, worker, e);
                }

                //
                // Start the task and wait for the data
                //
                try
                {
                    m_triggeredTask.Start();
                }
                catch (DaqException ex)
                {
                    throw new SBJException("Error occured when tryin to start DAQ task", ex);
                }

                AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_triggeredTask.Stream);

                try
                {
                    dataAquired = reader.ReadMultiSample(-1);

                    if (dataAquired.GetLength(1) < settings.GeneralSettings.TotalSamples)
                    {
                        //
                        // If from some reason we weren't able to 
                        // receive all data points, ignore and continue;
                        //
                        m_triggeredTask.Stop();
                        continue;
                    }

                    if (settings.ChannelsSettings.ActiveChannels.Count != dataAquired.GetLength(0))
                    {
                        throw new SBJException("Number of data channels doesn't fit the recieved data.");
                    }
                }
                catch (DaqException)
                {
                    //
                    // Probably timeout.
                    // Ignore this cycle and rerun.
                    //
                    m_triggeredTask.Stop();
                    continue;
                }

                //
                // At this point the reader has returned with all the data
                // so we can stop the openning of the junction.
                //
                m_quitJunctionClosingOperation = true;
                m_triggeredTask.Stop();

                //
                // Assign the aquired data for each channel.
                // First clear all data from previous interation.
                //                
                ClearRawData(settings.ChannelsSettings.ActiveChannels);
                AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, dataAquired);

                //
                // physical channel will include both simple and complex channels. 
                // 
                physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

                //
                // calculate the physical data for each channel
                //
                GetPhysicalData(physicalChannels);

                // 
                // Increase file number by one
                // Save data if needed
                //
                finalFileNumber++;
                if (settings.GeneralSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.GeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
                }

                //
                // Signal UI we have the data
                //
                if (DataAquired != null)
                {
                    DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
                }
            }
            return e.Cancel || isCancelled;
        }

        /// <summary>
        /// Try openning the junction. Opened position is determined by the 'Open Circuit Voltage'.
        /// </summary>
        /// <param name="openCircuitVoltage"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool TryObtainOpenCircuit(double openCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // Set the direction of the movement and stepping mode
            // And configure the delay to minimum for fast operation
            //
            m_stepperMotor.Direction = StepperDirection.UP;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = m_stepperMotor.MinDelay;

            //
            // Read the initial voltgae before we are doing anything
            //
            double currentVoltage = Math.Abs(AnalogIn(0));
            double voltageAfterStepping = currentVoltage;
            bool isTempOpenCircuit = false;
            bool isPermanentOpenCircuit = false;

            //
            // Until we've not been signaled from outer thread to stop we'll continue moving up.
            //
            while (!isPermanentOpenCircuit)
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
                // If we reach open circuit both current voltage and voltgae after stepping
                // should be smaller than the open circuit threshold.
                //
                isTempOpenCircuit = (currentVoltage < Math.Abs(openCircuitVoltage)) &&
                                    (voltageAfterStepping < Math.Abs(openCircuitVoltage));

                //
                // If we think we've reached open circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempOpenCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentOpenCircuit = currentVoltage < Math.Abs(openCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }
            return e.Cancel;
        }

        /// <summary>
        /// Aquire data from a close position while openning the junction
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool PerformBreakJunctionCycles(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double[,] dataAquired;
            bool isCancelled = false;
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;
            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            for (int i = 0; i < settings.GeneralSettings.TotalNumberOfCycles; i++)
            {
                //
                // Cancel the operation if user asked for
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
                    m_LaserController.TurnOff();
                    Thread.Sleep(5000);
                }

                //
                // Change the gain power to 5 before reaching contact
                // to ensure full contact current
                //
                if (settings.GeneralSettings.UseDefaultGain)
                {
                    m_amplifier.ChangeGain(5);
                }

                //
                // Reach to contact before we start openning the junction
                // If EM is enabled and we're after the first cycle, use the EM.
                // If user asked to stop then exit
                //
                isCancelled = (settings.ElectromagnetSettings.IsEMEnable && i > 0) ?
                               EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitVoltage, worker, e) :
                               TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, settings.GeneralSettings.UseShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitDelayTime, worker, e);
                if (isCancelled)
                {
                    break;
                }

                //
                // Configure the gain to the desired one if we changed it to E5
                // before strating the measurement.
                // And also this is the time to switch the laser on.
                //
                if (settings.GeneralSettings.UseDefaultGain)
                {
                    int gainPower;
                    Int32.TryParse(settings.GeneralSettings.Gain, out gainPower);
                    m_amplifier.ChangeGain(gainPower);
                }


                if (settings.LaserSettings.IsLaserOn)
                {
                    m_LaserController.TurnOn();
                }

                //
                // Start openning the junction.
                // If EM is enabled and we're after the first cycle, use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    if (i == 0)
                    {
                        //
                        // open the junction by stepper motor. this function does it indipendently; 
                        // it doesn't wait for the trigger task to finish. and we stay on the current thread. 
                        //
                        ObtainOpenJunctionByStepperMotor(settings.GeneralSettings.TriggerVoltage, worker, e);

                        //
                        // from now on we will use the EM, so we don't want the stepper motor to stay on. 
                        //
                        m_stepperMotor.Shutdown();
                        continue;
                    }
                    else
                    {
                        EMBeginOpenJunction(settings, worker, e);
                    }
                }
                else
                {
                    BeginOpenJunction(settings, worker, e);
                }

                //
                // Start the task and wait for the data
                //
                try
                {
                    m_triggeredTask.Start();
                }
                catch (DaqException ex)
                {
                    throw new SBJException("Error occured when tryin to start DAQ task", ex);
                }

                AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_triggeredTask.Stream);

                try
                {
                    dataAquired = reader.ReadMultiSample(-1);

                    if (dataAquired.GetLength(1) < settings.GeneralSettings.TotalSamples)
                    {
                        //
                        // If from some reason we weren't able to 
                        // receive all data points, ignore and continue;
                        //
                        m_triggeredTask.Stop();
                        continue;
                    }

                    if (settings.ChannelsSettings.ActiveChannels.Count != dataAquired.GetLength(0))
                    {
                        throw new SBJException("Number of data channels doesn't fit the recieved data.");
                    }
                }
                catch (DaqException ex  )
                {
                    if (ex.Error == -88709 || ex.Error == -88710) 
                    {
                        //
                        // User asked to stop so the task was aborted from the UI thread
                        //
                        break;
                    }
                    else
                    {
                        //
                        // Probably timeout.
                        // Ignore this cycle and rerun.
                        //
                        m_triggeredTask.Stop();
                        continue;
                    }
                }

                //
                // At this point the reader has returned with all the data
                // so we can stop the openning of the junction.
                //
                m_quitJunctionOpenningOperation = true;
                m_triggeredTask.Stop();

                //
                // Assign the aquired data for each channel.
                // First clear all data from previous interation.
                //                
                ClearRawData(settings.ChannelsSettings.ActiveChannels);
                AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, dataAquired);

                //
                // physical channel will include both simple and complex channels. 
                // 
                physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

                //
                // calculate the physical data for each channel
                //
                GetPhysicalData(physicalChannels);

                // 
                // Increase file number by one
                // Save data if needed
                //
                finalFileNumber++;
                if (settings.GeneralSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.GeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
                }

                //
                // Signal UI we have the data
                //
                if (DataAquired != null)
                {
                    DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
                }
            }

            return e.Cancel || isCancelled;
        }

        /// <summary>
        /// Close the junction until triggered conductance is reached. Then stop moving.
        /// This method performs no stablization operation on the cantiliver once it is stopped.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ReachToPositionByMovingDown(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double[,] dataAquired = new double[1000, 1000];
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;
            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            for (int i = 0; i < settings.GeneralSettings.TotalNumberOfCycles; i++)
            {
                //
                // Cancel the operation if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                // 
                // Real time operation is the operation where data is aquired on the fly 
                // and constantly updated on the UI.
                //
                m_quitRealTimeOperation = false;

                //
                // physical channel will include both simple and complex channels. 
                // 
                physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

                //                
                // Clear all data from previous interation.
                //                
                ClearRawData(settings.ChannelsSettings.ActiveChannels);

                //
                // Configure the triggered task. The is the task that is used just in order to notify us
                // that we've reached the desired position and it's time to start recording the data.
                //
                m_triggeredTask = GetMultipleChannelsTriggeredTask(settings, RunDirection.Make, m_triggerSlope, m_triggerVoltage, worker, e);
                m_triggeredTask.EveryNSamplesReadEventInterval = settings.GeneralSettings.TotalSamples;
                m_triggeredTask.Done += new TaskDoneEventHandler(OnTaskDoneClosing);
                m_triggeredTask.Control(TaskAction.Verify);
                m_triggerSlope = m_triggeredTask.Triggers.ReferenceTrigger.AnalogEdge.Slope;
                m_triggerVoltage = m_triggeredTask.Triggers.ReferenceTrigger.AnalogEdge.Level;

                //
                // Create the real time task for recording the data.
                //
                m_realTimeTask = GetContinuousAITask(settings.GeneralSettings.SampleRate, settings.ChannelsSettings.ActiveChannels);
                AnalogMultiChannelReader realTimeReader = new AnalogMultiChannelReader(m_realTimeTask.Stream);

                //
                // Start openning the junction and bring the cantiliver to a position where we can start closing the junction as requested.
                // If EM is enabled use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMTryObtainOpenCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.OpenCircuitVoltage, worker, e);
                }
                else
                {
                    TryObtainOpenCircuit(settings.GeneralSettings.OpenCircuitVoltage, worker, e);
                }

                //
                // Start closing the junction. Async operation.
                // If EM is enabled use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMBeginCloseJunction(settings, worker, e);
                }
                else
                {
                    BeginCloseJunction(settings, worker, e);
                }

                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Start the triggered task.
                //
                m_triggeredTask.Start();

                //
                // Wait for the corssover of the trigger conductance point.
                // If the user asked to stop the operation on the external thread then
                // WaitUntilDone will throw an expection. We can ignore that and return.
                //
                try
                {
                    m_triggeredTask.WaitUntilDone();
                }
                catch (DaqException)
                {
                    break;
                }

                //
                // If we reached this point then it means that the triggered task had finished and
                // signaled us that we reached the desired conductance point.                 
                //
                              

                //
                // As long as we are not stopped from the UI by the user continue recording the data. 
                //
                while (!m_quitRealTimeOperation)
                {
                    //
                    // Read operation implicity start the task without the need to call Start() method.
                    //
                    try
                    {
                        dataAquired = realTimeReader.ReadMultiSample(-1);
                    }
                    catch (DaqException)
                    {
                        continue;
                    }


                    if (dataAquired.Length == 0)
                    {
                        continue;
                    }

                    //
                    // Assign the aquired data for each channel.
                    //                            
                    AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, dataAquired);

                    //
                    // calculate the physical data for each channel
                    //
                    GetPhysicalData(physicalChannels);

                    //
                    // Signal UI we have the data
                    //
                    if (DataAquired != null)
                    {
                        DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
                    }
                }

                if (DoneReadingData != null)
                {
                    DoneReadingData(this, null);
                }

                m_realTimeTask.Stop();
                m_realTimeTask.Dispose();

                // 
                // Increase file number by one
                // Save data if needed
                //
                finalFileNumber++;
                if (settings.GeneralSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.GeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
                }
            }

            m_triggeredTask.Dispose();
            m_realTimeTask.Dispose();
            m_triggerSlope = 0;
            m_triggerVoltage = 0;
            return e.Cancel;
        }

        /// <summary>
        /// Reach to position specified by conductance value and then stop and aqcuire data until asked to stop.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ReachToPositionByMovingUp(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double[,] dataAcquired = new double[1000, 1000];
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;
            List<IDataChannel> physicalChannels = new List<IDataChannel>();


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
                // This flag is used to signal us when the user asked to stop the real time data acquisition
                //
                m_quitRealTimeOperation = false;
                m_triggeredTask = GetMultipleChannelsTriggeredTask(settings, RunDirection.Break, m_triggerSlope, m_triggerVoltage, worker, e);
                m_triggeredTask.EveryNSamplesReadEventInterval = settings.GeneralSettings.TotalSamples;
                m_triggeredTask.Done += new TaskDoneEventHandler(OnTaskDoneOpenning);
                m_triggeredTask.Control(TaskAction.Verify);
                m_triggerSlope = m_triggeredTask.Triggers.ReferenceTrigger.AnalogEdge.Slope;
                m_triggerVoltage = m_triggeredTask.Triggers.ReferenceTrigger.AnalogEdge.Level;

                //
                // physical channel will include both simple and complex channels. 
                // 
                physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

                //
                // Assign the aquired data for each channel.
                // First clear all data from previous interation.
                //                
                ClearRawData(settings.ChannelsSettings.ActiveChannels);

                //
                // Create the tasks: One for triggering us to stop and the other for start monitoring the data
                //
                m_realTimeTask = GetContinuousAITask(settings.GeneralSettings.SampleRate, settings.ChannelsSettings.ActiveChannels);
                AnalogMultiChannelReader realTimeReader = new AnalogMultiChannelReader(m_realTimeTask.Stream);

                //
                // Start closing the junction.
                // If EM is enabled use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitVoltage, worker, e);
                }
                else
                {
                    TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, settings.GeneralSettings.UseShortCircuitDelayTime,settings.GeneralSettings.ShortCircuitDelayTime, worker, e);
                }

                //
                // Start openning the junction. ASync operation.
                // If EM is enabled use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    EMBeginOpenJunction(settings, worker, e);
                }
                else
                {
                    BeginOpenJunction(settings, worker, e);
                }

                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Start the triggered task. 
                //
                m_triggeredTask.Start();

                //
                // If the user asked to stop the operation on the external thread then
                // WaitUntilDone will throw an expection. We can ignore that and return.
                //
                try
                {
                    m_triggeredTask.WaitUntilDone();
                }
                catch (DaqException)
                {
                    //
                    // We got here if the user asked to stop the operation
                    //
                    break;
                }

                //
                // We reach this point only after we reached the desired conductance value.
                // As long as the user didn't ask to stop the operation continue recording the data.
                //
                while (!m_quitRealTimeOperation)
                {
                    //
                    // Read operation implicity start the task without the need to call Start() method.
                    //
                    try
                    {
                        dataAcquired = realTimeReader.ReadMultiSample(-1);
                    }
                    catch (DaqException)
                    {
                        continue;
                    }


                    if (dataAcquired.Length == 0)
                    {
                        continue;
                    }
                    //
                    // Assign the aquired data for each channel.
                    //                            
                    AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, dataAcquired);

                    //
                    // calculate the physical data for each channel
                    //
                    GetPhysicalData(physicalChannels);

                    //
                    // Signal UI we have the data
                    //
                    if (DataAquired != null)
                    {
                        DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
                    }
                }

                if (DoneReadingData != null)
                {
                    DoneReadingData(this, null);
                }
                m_realTimeTask.Stop();
                m_realTimeTask.Dispose();

                // 
                // Increase file number by one
                // Save data if needed
                //
                finalFileNumber++;
                if (settings.GeneralSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.GeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
                }
            }

            m_triggeredTask.Dispose();
            m_realTimeTask.Dispose();
            m_triggerSlope = 0;
            m_triggerVoltage = 0;
            return e.Cancel;
        }

        /// <summary>
        /// Fired when we crossed the trigger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTaskDoneOpenning(object sender, TaskDoneEventArgs e)
        {
            //            
            // Stop moving and start recording the data
            //
            m_quitJunctionOpenningOperation = true;

            //
            // We need to dispose the triggered task here in order to start the real time task on te main thread.
            //
            m_triggeredTask.Dispose();
        }

        /// <summary>
        /// Fired when we crossed the trigger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTaskDoneClosing(object sender, TaskDoneEventArgs e)
        {
            //            
            // Stop moving and start recording the data
            //
            m_quitJunctionClosingOperation = true;

            //
            // We need to dispose the triggered task here in order to start the real time task on te main thread.
            //
            m_triggeredTask.Dispose();
        }

        /// <summary>
        /// Add the acquired data to the other data points that were already read from that channel.
        /// </summary>
        /// <param name="fullData"></param>
        /// <param name="dataAquired"></param>
        /// <returns></returns>
        private List<List<double>> AccumulateData(List<List<double>> fullData, double[,] dataAquired)
        {
            int numberOfDataSampled = dataAquired.GetLength(1);
            int numberOfChannels = dataAquired.GetLength(0);

            //
            // Flattens the matrix to a list so that each row is appended after the row before.
            // The result is one list: 2D [a,a,a;b,b,b;c,c,c] ==> 1D (a,a,a,b,b,b,c,c,c)
            //
            IEnumerable<double> flatMatrix = dataAquired.Cast<double>();
            for (int i = 0; i < numberOfChannels; i++)
            {
                //
                // Add the relevant data points to the desired list (which represents a channel)
                // Since we have flatten the list, this requires skipping some data points.
                //
                fullData[i].AddRange(flatMatrix.Skip(numberOfDataSampled * i).Take(numberOfDataSampled));
            }
            return fullData;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Try and obtain short circut
        /// </summary>
        /// <param name="shortCircuitVoltage">The voltage indicator for short circuit</param>        
        /// <returns>True whether the operation was cancelled. False otherwise</returns>
        public bool TryObtainShortCircuit(double shortCircuitVoltage, bool useShortCircuitDelayTime, int shortCircuitDelayTime, BackgroundWorker worker, DoWorkEventArgs e)
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
            if (!useShortCircuitDelayTime)
            {
                m_stepperMotor.Delay = m_stepperMotor.MinDelay;
            }
            else
            {
                m_stepperMotor.Delay = shortCircuitDelayTime;
            }

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
                m_stepperMotor.MoveMultipleSteps(10);
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
                    Thread.Sleep(1000);
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
            //
            // Apply voltage with desired tool: Task or Keithley
            //
            ApplyVoltageIfNeeded(settings.GeneralSettings.UseKeithley, 
                                 settings.GeneralSettings.Bias, 
                                 settings.GeneralSettings.BiasError);

            //
            // Use Lambda Zup to apply constant voltage on external electromagnet if needed
            //
            UseLambdaZupIfNeeded(settings.LambdaZupSettings.IsLambdaZupEnable,
                                 settings.LambdaZupSettings.OutputVoltage);

            //
            // Configure the laser if needed for this run
            //
            ConfigureLaserIfNeeded(settings);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.GeneralSettings.IsFileSavingRequired, settings.GeneralSettings.Path);

            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

            //
            // Create the task
            //
            switch (settings.GeneralSettings.RunDirection)
            {
                case RunDirection.Both:
                    //TODO: Add implementation for both direction measurement
                    break;
                case RunDirection.Break:
                    m_triggeredTask = GetMultipleChannelsTriggeredTask(settings, RunDirection.Break, m_triggerSlope, m_triggerVoltage,  worker, e);                    
                    isCancelled = PerformBreakJunctionCycles(settings, worker, e);
                    break;
                case RunDirection.Make:
                    m_triggeredTask = GetMultipleChannelsTriggeredTask(settings, RunDirection.Make, m_triggerSlope, m_triggerVoltage, worker, e);
                    isCancelled = PerformMakeJunctionCycles(settings, worker, e);                    
                    break;
            }     

            //
            // Finish the measurement properly
            //
            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOff();
            }
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_electroMagnet.Shutdown();
            }
            if (settings.LaserSettings.IsFirstEOMOn)
            {
                m_taborFirstEOMController.TurnOff();
            }
            if (settings.LaserSettings.IsSecondEOMOn)
            {
                m_taborSecondEOMController.TurnOff();
            }
            if (settings.LambdaZupSettings.IsLambdaZupEnable)
            {
                m_lambdaZup.TurnOffOutput();
            }
            m_triggeredTask.Dispose();
            m_stepperMotor.Shutdown();

            return isCancelled;
        }      

        /// <summary>
        /// Manually aquire data
        /// This method continuously poll the buffer for data until it is stopped by the user.
        /// </summary>
        /// <param name="settings">The settings for running the aquisition</param>
        /// <returns>True whether the operation was cacled by the user. False otherwise.</returns>
        public bool AquireDataManually(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // Apply voltage with desired tool: Task or Keithley
            //
            ApplyVoltageIfNeeded(settings.GeneralSettings.UseKeithley,
                                 settings.GeneralSettings.Bias,
                                 settings.GeneralSettings.BiasError);

            bool isCancelled = false;
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;
       
            //
            // The array is intialized with size for 1 minute sampling.
            //
            double[,] dataAquired = new double[1000, 1000];

            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            //
            // Configure the laser if needed for this run
            //
            ConfigureLaserIfNeeded(settings);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.GeneralSettings.IsFileSavingRequired, settings.GeneralSettings.Path);

            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

            //
            // Create the task
            //
            m_triggeredTask = GetContinuousAITask(settings.GeneralSettings.SampleRate, settings.ChannelsSettings.ActiveChannels);
            
            //
            // If EM is enabled, and we are asked to skip the first cycle (that is done by the stepper motor), 
            // then return.
            //
            if (settings.ElectromagnetSettings.IsEMEnable && settings.ElectromagnetSettings.IsEMSkipFirstCycleEnable)
            {
                m_stepperMotor.Shutdown();
                return false;
            }

            //
            // Turn off the laser before we reach contact
            //
            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOff();
                Thread.Sleep(5000);
            }

            //
            // Change the gain power to 5 before reaching contact
            // to ensure full contact current
            //
            if (settings.GeneralSettings.UseDefaultGain)
            {
                m_amplifier.ChangeGain(5);
            }

            //
            // Reach to contact before we start openning the junction
            // If EM is enabled and we're after the first cycle, use the EM.
            // If user asked to stop than exit
            //
            isCancelled = (settings.ElectromagnetSettings.IsEMEnable) ?
                           EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, 
                                                   settings.GeneralSettings.ShortCircuitVoltage, worker, e) :
                           TryObtainShortCircuit(settings.GeneralSettings.ShortCircuitVoltage, settings.GeneralSettings.UseShortCircuitDelayTime,settings.GeneralSettings.ShortCircuitDelayTime, worker, e);
            if (isCancelled)
            {
                return false;
            }

            //
            // Configure the gain to the desired one before strating the measurement.
            // And also this is the time to switch the laser on.
            //
            if (settings.GeneralSettings.UseDefaultGain)
            {
                int gainPower;
                Int32.TryParse(settings.GeneralSettings.Gain, out gainPower);
                m_amplifier.ChangeGain(gainPower);
            }


            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOn();
            }

            //
            // Start openning the junction.
            // If EM is enabled then use it.
            //
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_stepperMotor.Shutdown();
                EMBeginOpenJunction(settings, worker, e);                
            }
            else
            {
                BeginOpenJunction(settings, worker, e);
            }

            //
            // Start the task and wait for the data
            //
            try
            {
                m_triggeredTask.Start();
            }
            catch (DaqException ex)
            {
                throw new SBJException("Error occured when tryin to start DAQ task", ex);
            }

            AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_triggeredTask.Stream);
            List<List<double>> averagedData = new List<List<double>>(settings.ChannelsSettings.ActiveChannels.Count);

            for (int i = 0; i < averagedData.Capacity; i++)
            {
                averagedData.Add(new List<double>());
            }

            try
            {
                //
                // Before getting all the data clear the lists.
                //
                ClearRawData(settings.ChannelsSettings.ActiveChannels);              

                //
                // As long as the user didn't ask to stop the acquisition 
                // (which is signaled by the stop of the stepper motion)
                // we coninue sampling.
                //
                while (!m_quitJunctionOpenningOperation)
                {
                    //
                    // Read all available data points in the buffer that
                    // were not read so far.
                    //
                    dataAquired = reader.ReadMultiSample(-1);

                    //
                    // Get average for the acquired the data and assign to variable
                    //
                    List<double> averageDataValues = GetAverageDataValue(dataAquired);
                    for (int i = 0; i < averageDataValues.Count; i++)
                    {                        
                        averagedData[i].Add(averageDataValues[i]);
                    }
                      
                    dataAquired = null;
 
                    //
                    // Cancel the operatin if user asked for
                    // We do it at the end of the loop to make sure we 
                    // saved all the data we have available.
                    //
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                }                
            }
            catch (DaqException)
            {
                //
                // In case of an error just return
                //
                m_triggeredTask.Stop();
                m_triggeredTask.Dispose();
                if (m_LaserController != null)
                {
                m_LaserController.TurnOff();
                }
                if (m_taborFirstEOMController != null)
                {
                    m_taborFirstEOMController.TurnOff();
                }
                if (m_taborSecondEOMController != null)
                {
                    m_taborSecondEOMController.TurnOff();
                }
                return false;
            }

            //
            // At this point the user had requested to stop the data aquisition.
            // By signaling "stop". We can stop the task.
            //            
            m_triggeredTask.Stop();

            //
            // Assign the aquired data for each channel after an average process
            //                    
            AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertDataToMatrix(GetAveragedData(averagedData, 5000)));

            //
            // physical channel will include both simple and complex channels. 
            // 
            physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

            //
            // calculate the physical data for each channel
            //
            GetPhysicalData(physicalChannels);

            // 
            // Increase file number by one
            // Save data if needed
            //
            finalFileNumber++;
            if (settings.GeneralSettings.IsFileSavingRequired)
            {
                finalFileNumber = SaveData(settings.GeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
            }

            //
            // Signal UI we have the data
            //
            if (DataAquired != null)
            {
                DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
            }
            

            //
            // Finish the measurement properly
            //
            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOff();
            }
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_electroMagnet.Shutdown();
            }
            if (settings.LaserSettings.IsFirstEOMOn)
            {
                m_taborFirstEOMController.TurnOff();
            }
            if (settings.LaserSettings.IsSecondEOMOn)
            {
                m_taborSecondEOMController.TurnOff();
            }

            m_triggeredTask.Dispose();
            m_stepperMotor.Shutdown();

            return (isCancelled || e.Cancel);
        }

        /// <summary>
        /// Manually aquire data
        /// This method continuously poll the buffer for data until it is stopped by the user.
        /// </summary>
        /// <param name="settings">The settings for running the aquisition</param>
        /// <returns>True whether the operation was cacled by the user. False otherwise.</returns>
        public bool AquireDataContinuously(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // Apply voltage with desired tool: Task or Keithley
            //
            ApplyVoltageIfNeeded(settings.GeneralSettings.UseKeithley,
                                 settings.GeneralSettings.Bias,
                                 settings.GeneralSettings.BiasError);

            bool isCancelled = false;
            int finalFileNumber = settings.GeneralSettings.CurrentFileNumber;

            //
            // The array is intialized with size for 1 minute sampling.
            //
            double[,] dataAquired = new double[1000, 1000];

            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            //
            // Configure the laser if needed for this run
            //
            ConfigureLaserIfNeeded(settings);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.GeneralSettings.IsFileSavingRequired, settings.GeneralSettings.Path);

            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

            //
            // Create the task
            //
            m_triggeredTask = GetContinuousAITask(settings.GeneralSettings.SampleRate, settings.ChannelsSettings.ActiveChannels);

            //
            // If EM is enabled, and we are asked to skip the first cycle (that is done by the stepper motor), 
            // then return.
            //
            if (settings.ElectromagnetSettings.IsEMEnable && settings.ElectromagnetSettings.IsEMSkipFirstCycleEnable)
            {
                m_stepperMotor.Shutdown();
                return false;
            } 

            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOn();
            }
           

            //
            // Start the task and wait for the data
            //
            try
            {
                m_triggeredTask.Start();
            }
            catch (DaqException ex)
            {
                throw new SBJException("Error occured when tryin to start DAQ task", ex);
            }

            AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_triggeredTask.Stream);
            List<List<double>> fullData = new List<List<double>>(settings.ChannelsSettings.ActiveChannels.Count);

            for (int i = 0; i < fullData.Capacity; i++)
            {
                fullData.Add(new List<double>());
            }

            try
            {
                //
                // Before getting all the data clear the lists.
                //
                ClearRawData(settings.ChannelsSettings.ActiveChannels);

                //
                // As long as the user didn't ask to stop the acquisition 
                // (which is signaled by the stop of the stepper motion)
                // we coninue sampling.
                //
                while (!worker.CancellationPending)
                {
                    //
                    // Read all available data points in the buffer that
                    // were not read so far.
                    //
                    dataAquired = reader.ReadMultiSample(-1);
                    fullData = AccumulateData(fullData, dataAquired);                    
                    dataAquired = null;                  
                }
            }
            catch (DaqException)
            {
                //
                // In case of an error just return
                //
                m_triggeredTask.Stop();
                m_triggeredTask.Dispose();
                if (m_LaserController != null)
                {
                    m_LaserController.TurnOff();
                }
                if (m_taborFirstEOMController != null)
                {
                    m_taborFirstEOMController.TurnOff();
                }
                if (m_taborSecondEOMController != null)
                {
                    m_taborSecondEOMController.TurnOff();
                }
                return false;
            }

            //
            // At this point the user had requested to stop the data aquisition.
            // By signaling "stop". We can stop the task.
            //            
            m_triggeredTask.Stop();

            //
            // Assign the aquired data for each channel after an average process
            //                    
            AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertDataToMatrix(fullData));

            //
            // physical channel will include both simple and complex channels. 
            // 
            physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

            //
            // calculate the physical data for each channel
            //
            GetPhysicalData(physicalChannels);

            // 
            // Increase file number by one
            // Save data if needed
            //
            finalFileNumber++;
            if (settings.GeneralSettings.IsFileSavingRequired)
            {
                finalFileNumber = SaveData(settings.GeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
            }

            //
            // Signal UI we have the data
            //
            if (DataAquired != null)
            {
                DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
            }


            //
            // Finish the measurement properly
            //
            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOff();
            }
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_electroMagnet.Shutdown();
            }
            if (settings.LaserSettings.IsFirstEOMOn)
            {
                m_taborFirstEOMController.TurnOff();
            }
            if (settings.LaserSettings.IsSecondEOMOn)
            {
                m_taborSecondEOMController.TurnOff();
            }

            m_triggeredTask.Dispose();
            m_stepperMotor.Shutdown();

            return (isCancelled || e.Cancel);
        }

        /// <summary>
        /// Move cantiliver to position until trigger is reached; Then stop.
        /// </summary>
        /// <param name="sBJControllerSettings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        public bool ReachPosition(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isCancelled = false;
            //
            // Apply voltage with desired tool: Task or Keithley
            //
            ApplyVoltageIfNeeded(settings.GeneralSettings.UseKeithley,
                                 settings.GeneralSettings.Bias,
                                 settings.GeneralSettings.BiasError);

            //
            // Use Lambda Zup to apply constant voltage on external electromagnet if needed
            //
            UseLambdaZupIfNeeded(settings.LambdaZupSettings.IsLambdaZupEnable,
                                 settings.LambdaZupSettings.OutputVoltage);

            //
            // Configure the laser if needed for this run
            //
            ConfigureLaserIfNeeded(settings);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.GeneralSettings.IsFileSavingRequired, settings.GeneralSettings.Path);

            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

           
            switch (settings.GeneralSettings.RunDirection)
            {
                case RunDirection.Both:
                    //TODO: Add implementation for both direction measurement
                    break;
                case RunDirection.Break:                    
                    isCancelled = ReachToPositionByMovingUp(settings, worker, e);
                    break;
                case RunDirection.Make:                       
                    isCancelled = ReachToPositionByMovingDown(settings, worker, e);
                    break;
            }

            //
            // Finish the measurement properly
            //
            if (settings.LaserSettings.IsLaserOn)
            {
                m_LaserController.TurnOff();
            }
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_electroMagnet.Shutdown();
            }
            if (settings.LaserSettings.IsFirstEOMOn)
            {
                m_taborFirstEOMController.TurnOff();
            }
            if (settings.LaserSettings.IsSecondEOMOn)
            {
                m_taborSecondEOMController.TurnOff();
            }
            if (settings.LambdaZupSettings.IsLambdaZupEnable)
            {
                m_lambdaZup.TurnOffOutput();
            }
            m_triggeredTask.Dispose();
            m_stepperMotor.Shutdown();

            return isCancelled;            
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
        public void FixBias(double shortCircuitVoltage, double bias, bool useShortCircuitDelayTime, int shortCircuitDelayTime, BackgroundWorker worker, DoWorkEventArgs e)
        {
            int i = 0;
            bool isBiasedFixed = false;
            double biasFixValue = 0;
            double currentVoltage = 0;

            //
            // First, reach to contact
            //
            TryObtainShortCircuit(shortCircuitVoltage,useShortCircuitDelayTime,shortCircuitDelayTime, worker, e);
            int shortCircuitVoltageSign = AnalogIn(0) > 0 ? 1 : -1;

            //
            // Measurements with triggered must be carried on with negative voltage.
            // So we must check whether the bias sign should be flipped.
            //
            int biasSignFactor = shortCircuitVoltageSign > 0 ? -1 : 1;
            int biasSign = bias > 0 ? 1 : -1;


            //
            // Set bias to zero and lets check where we stand.
            // If everything is OK then at zero bias the current 
            // should be also zero. If it's not, correction should be made.
            //
            m_sourceMeter.SetBias(0);
            double previousVoltage = AnalogIn(0);
            int previousVoltageSign = previousVoltage > 0 ? 1 : -1;

            //
            // We need to know if bias needs to be increased or decreased in order to make the correction.
            // That depends on the bias direction and the measured voltage.            
            //
            int biasChangeFactor = 1;
            if (biasSignFactor * biasSign > 0)
            {
                //
                // If we are here than the measured voltage has oposite sign than the bias
                //
                biasChangeFactor = previousVoltageSign > 0 ? 1 : -1; 

            }
            else
            {
                //
                // If we are here than the measured voltage has the same sign as the bias.
                //
                biasChangeFactor = previousVoltageSign > 0 ? -1 : 1;                 
            }

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
                    e.Result = new Bias((biasFixValue + ((i - 1) * 0.0005 * biasChangeFactor)) / 2 , biasSignFactor);
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
            Application xlsLogBook = new Application();
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

        /// <summary>
        /// if we can't use the keithey we will use the DAQ as a source instead.
        /// </summary>
        public void ApplyVoltageIfNeeded(bool useKeithley, double bias, double biasError)
        {
            //
            // if we didn't check the keithley as a source for bias,  let's try to connect to it
            //
            if (!useKeithley)
            {
                try
                {
                    SourceMeter.Connect();
                    SourceMeter.SetBias(bias + biasError);
                }
                catch (SBJException)
                {
                    //
                    // the keithley doesn't connect. let's try the DAQ
                    //
                    try
                    {
                        StartConstantOutputTask(bias);
                    }
                    catch (DaqException ex)
                    {
                        throw new SBJException("Error occured when tryin to start DAQ output task", ex);
                    }
                }
            }
        }

        /// <summary>
        /// if lambdaZup is needed, then set an output and turn it on. 
        /// </summary>
        public void UseLambdaZupIfNeeded(bool useLambdaZup, double voltage)
        {
            if (useLambdaZup)
            {
                m_lambdaZup.SetVoltage(voltage);
                m_lambdaZup.TurnOnOutput();
            }
        }

        /// <summary>
        /// if the bias was applied by the DAQ device, close it and stop the task. 
        /// if it was applied by the keithley - leave it on. 
        /// </summary>
        public void StopApplyingVoltageIfNeeded()
        {
            //
            // if we applied the bias by the DAQ device, we need to stop the task. 
            //
            if (OutputTask != null)
            {
                OutputTask.Stop();
                OutputTask.Dispose();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
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
            if (m_LaserController != null)
            {
                m_LaserController.Disconnect();
            }
            m_LockIn.Shutdown();
            if (m_triggeredTask != null)
            {
                m_triggeredTask.Dispose();
            }
        }
        #endregion

        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double AnalogIn(byte channel);
        #endregion       
    }

    internal class Bias
    {
        internal double Error { get; set; }
        internal double Sign { get; set; }

        public Bias(double error, double sign)
        {
            Error = error;
            Sign = sign;
        }
    }
}

