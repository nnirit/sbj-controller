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
            m_stepperMotor.Delay = settings.StepperWaitTime1;

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
                     m_stepperMotor.Delay = settings.StepperWaitTime2;
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
            if (settings.IsFileSavingRequired == false)
            {
                return;
            }

            //
            // Write the Params.txt file.
            //
            using (StreamWriter file = new StreamWriter(Path.Combine(settings.Path, c_settingsFileName), true))
            {
                file.WriteLine("---------------------------------------------");
                file.WriteLine(DateTime.Now.ToString());
                foreach (var property in settings.GetType().GetProperties())
                {
                    file.WriteLine(property.Name + ":\t" + property.GetValue(settings, null).ToString());
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
            if (!settings.IsLaserOn)
            {
                return;
            }

            if (settings.LaserMode.Equals("DC"))
            {
                m_taborLaserController.SetDCMode();
                m_taborLaserController.SetDcModeAmplitude(settings.LaserAmplitude);
            }
            else
            {
                if (settings.LaserMode.Equals("Square"))
                {
                    m_taborLaserController.SetSquareMode();
                    m_taborLaserController.SetSquareModeAmplitude(settings.LaserAmplitude);
                    m_taborLaserController.SetSquareModeFrequency(settings.LaserFrequency);
                }
                else
                {
                    throw new SBJException("Invalid laser mode. Expected modes: DC or Sqaure");
                }
            }
        }
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
            int finalFileNumber;
            double[,] dataAquired;

            //
            // Configure the laser if needed for this run
            //
            ConfigureLaserIfNeeded(settings);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings);

            TryObtainShortCircuit(settings.ShortCircuitVoltage, worker, e);
            double currentVoltage = AnalogIn(0);

            //
            // If we are measuring positive values of voltages at contact
            // that means that the trigger edge should be falling.
            //
            AnalogEdgeReferenceTriggerSlope triggerSlope = (currentVoltage > 0) ? AnalogEdgeReferenceTriggerSlope.Falling : AnalogEdgeReferenceTriggerSlope.Rising;
            double triggerVoltage = (currentVoltage > 0) ? settings.TriggerVoltage * (-1) : settings.TriggerVoltage; 

            //
            // Create the task with its propertites
            //
            TriggeredTaskProperties taskProperties = new TriggeredTaskProperties(settings.IsLockInSignalEnable, 
                                                                                 settings.IsLockInPhaseSignalEnable,
                                                                                 settings.SampleRate,
                                                                                 settings.TotalSamples,
                                                                                 triggerVoltage,
                                                                                 settings.PretriggerSamples,
                                                                                 triggerSlope);
           
            m_task = m_daqController.CreateMultipleChannelsTriggeredTask(taskProperties);
            
         


            //
            // Main loop for data aquisition
            //
            for (int i = 0; i < settings.TotalNumberOfCycles; i++)
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
                // Turn off the laser before we reach contact
                //
                if (settings.IsLaserOn)
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
                // If user asked to stop than exit
                //
                isCancelled = TryObtainShortCircuit(settings.ShortCircuitVoltage, worker, e);
                if (isCancelled)
                {
                    break;
                }
                
                //
                // Configure the gain to the desired one before strating the measurement.
                // And also this is the time to switch the laser on.
                //
                int gainPower;
                Int32.TryParse(settings.Gain, out gainPower);
                m_amplifier.ChangeGain(gainPower);
                if (settings.IsLaserOn)
                {
                    m_taborLaserController.TurnOn();
                }

                //
                // Start openning the junction.
                //
                BeginOpenJunction(settings);

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

                    if (dataAquired.Length < settings.TotalSamples)
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
                // Save data if needed
                //
                finalFileNumber = settings.CurrentFileNumber + i + 1;
                if (settings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.Path, dataAquired, finalFileNumber);
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
            if (settings.IsLaserOn)
            {
                m_taborLaserController.TurnOff();
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
            Application xlsLogBook = new Application();
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

        internal void ShutDown()
        {
            m_stepperMotor.Shutdown();
            m_sourceMeter.Shutdown();
            m_amplifier.Shutdown();
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

