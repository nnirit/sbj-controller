using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NationalInstruments.DAQmx;
using System.Threading;

namespace SBJController
{
    public partial class SBJController
    {
        public bool AquireCalibrationData(SBJControllerSettingsForCalibration settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isCancelled = false;
            int finalFileNumber = settings.CalibirationSettings.CurrentFileNumber;
            double currentVoltage;

            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            //
            // Apply voltage with desired tool: Task or Keithley
            //
            ApplyVoltageIfNeeded(settings.CalibirationSettings.UseKeithley,
                                 settings.CalibirationSettings.Bias,
                                 0.0);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.CalibirationSettings.IsFileSavingRequired, settings.CalibirationSettings.Path);
           
            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

            //
            // Create the task
            //
            m_task = GetCalibrationTask(settings, worker, e);

            //
            // Reach to contact before we start openning the junction
            // If EM is enabled and we're after the first cycle, use the EM.
            // If user asked to stop than exit
            //
            isCancelled = (settings.ElectromagnetSettings.IsEMEnable) ?
                           EMTryObtainShortCircuit(settings, worker, e) :
                           TryObtainShortCircuit(settings.CalibirationSettings.ShortCircuitVoltage, worker, e);
            if (isCancelled)
            {
                m_stepperMotor.Shutdown();
                return isCancelled;
            }
            
            //
            // physical channel will include both simple and complex channels. 
            // 
            physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);
            
            //
            // Main loop for data aquisition
            //
            for (int i = 0; i < settings.CalibirationSettings.TotalNumberOfCycles; i++)
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
                        ObtainOpenJunctionByStepperMotorForCalibration(settings, settings.CalibirationSettings.TriggerVoltage, worker, e);

                        //
                        // from now on we will use the EM, so we don't want the stepper motor to stay on. 
                        //
                        m_stepperMotor.Shutdown();
                        continue;
                    }
                    //else
                    //{
                    //    EMBeginOpenJunction(settings);
                    //}
                }
                else
                {
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

                    currentVoltage = Math.Abs(AnalogIn(0));

                    if (currentVoltage > 0.7*settings.CalibirationSettings.ShortCircuitVoltage)
                    {
                        //
                        // Open the Junction 
                        //
                        ObtainOpenJunctionByStepperMotorForCalibration(settings, settings.CalibirationSettings.TriggerVoltage, worker, e);
                    }
                    else
                    {
                        //
                        // Close the Junction
                        //
                        CloseJunctionByStepperMotorForCalibration(settings, settings.CalibirationSettings.ShortCircuitVoltage, worker, e);
                    }

                    m_task.Stop();
                }

                //
                // calculate the physical data for each channel
                //
                GetPhysicalData(physicalChannels);

                // 
                // Increase file number by one
                //
                finalFileNumber++;

                if (settings.CalibirationSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
                }

                //
                // Signal UI we have the data
                //
                if (DataAquired != null)
                {
                    DataAquired(this, new DataAquiredEventArgs(physicalChannels, finalFileNumber));
                }
            }

            //
            // Finish the measurement properly
            //
            m_task.Dispose();
            m_stepperMotor.Shutdown();

            return (isCancelled || e.Cancel);
        }
        private double GetDataAfterEachStep(SBJControllerSettingsForCalibration settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double[,] dataAquired=null;
            AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_task.Stream);

            dataAquired = reader.ReadMultiSample(-1);

            if (settings.ChannelsSettings.ActiveChannels.Count != dataAquired.GetLength(0))
            {
                throw new SBJException("Number of data channels doesn't fit the recieved data.");
            }

            return AverageVoltageAfterEachStep(dataAquired);
        }
        private Task GetCalibrationTask(SBJControllerSettingsForCalibration settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            TryObtainShortCircuit(settings.CalibirationSettings.ShortCircuitVoltage, worker, e);

            //
            // Determines the direction of the current - 
            // Either positive (then voltage is negative) or negative (then voltage is positive number)
            //
            bool isPositiveVoltage = AnalogIn(0) > 0;


            AnalogEdgeReferenceTriggerSlope triggerSlope = isPositiveVoltage ? AnalogEdgeReferenceTriggerSlope.Falling : AnalogEdgeReferenceTriggerSlope.Rising;
            double triggerVoltage = isPositiveVoltage ? settings.CalibirationSettings.TriggerVoltage * (-1) : settings.CalibirationSettings.TriggerVoltage;

            //
            // Create the task with its propertites
            //
            TaskProperties taskProperties = new TaskProperties(settings.CalibirationSettings.SampleRate,
                                                               settings.CalibirationSettings.PretriggerSamples,
                                                               settings.ChannelsSettings.ActiveChannels);

            return m_daqController.CreateContinuousAITask(taskProperties);
        }
        private bool ObtainOpenJunctionByStepperMotorForCalibration(SBJControllerSettingsForCalibration settings, double openCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentOpenCircuit = false;
            bool isTempOpenCircuit = false;
            List<double> rawDataList=null;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_stepperMotor.Direction = StepperDirection.UP;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = m_stepperMotor.MinDelay;

            //
            // Open the junction
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
                // Move up one step and check the voltage afterwards
                //
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(m_stepperMotor.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));
                
                //
                // Get Data after each step and save it.
                // Save the data for each cycle
                //
                rawDataList.Add(GetDataAfterEachStep(settings, worker, e));
                
                //
                // If the junction is open, both current voltage and voltgae after stepping
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

            //
            // Assign the aquired data for each channel
            //                
            AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertToMatrix(rawDataList));

            return e.Cancel;
        }
        private double[,] ConvertToMatrix(List<double> rawDataList)
        {
            double[,] data = new double[1,rawDataList.Count];
            double[] rawData = rawDataList.ToArray();
 
            for(int i=0; i<rawData.GetLength(0); i++)
            {
                for (int j = 0; j < rawData.GetLength(1); j++)
                {
                    data[i, j] = rawData[j];
                }
            }
            return data;
        }
        private bool CloseJunctionByStepperMotorForCalibration(SBJControllerSettingsForCalibration settings, double TriggerVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentClosedCircuit = false;
            bool isTempClosedCircuit = false;
            List<double> rawDataList = null;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_stepperMotor.Direction = StepperDirection.DOWN;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = m_stepperMotor.MinDelay;

            //
            // Close the junction
            //
            while (!isPermanentClosedCircuit)
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
                // Move up one step and check the voltage afterwards
                //
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(m_stepperMotor.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));
                
                //
                // Get Data after each step and save it.
                // Save the data for each cycle
                //
                rawDataList.Add(GetDataAfterEachStep(settings, worker, e));

                //
                // If the junction is closed, both current voltage and voltgae after stepping
                // should be bigger than the closed circuit threshold.
                //
                isTempClosedCircuit = (currentVoltage > Math.Abs(TriggerVoltage)) &&
                                     (voltageAfterStepping > Math.Abs(TriggerVoltage));

                //
                // If we think we've reached closed circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempClosedCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentClosedCircuit = currentVoltage > Math.Abs(TriggerVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }

            //
            // Assign the aquired data for each channel
            //
            AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertToMatrix(rawDataList));    

            return e.Cancel;
        }
        private bool EMTryObtainShortCircuit(SBJControllerSettingsForCalibration settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            switch (EMShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.CalibirationSettings.ShortCircuitVoltage, worker, e))
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
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                    MoveStepsByStepperMotor(StepperDirection.DOWN, 100);
                    return EMTryObtainShortCircuit(settings, worker, e);
            }
            return true;
        }
        private int SaveData(SBJControllerSettingsForCalibration settings, IList<IDataChannel> activeChannels, IList<IDataChannel> physicalChannels, int fileNumber)
        {
            string path = settings.CalibirationSettings.Path;
            int finalNumber = fileNumber;

            //
            // we need to save the raw data only from the active channels, since the complex channels has the same raw data.
            //
            foreach (var channel in activeChannels)
            {
                //
                // save raw data to file
                //
                finalNumber = WriteSingleArrayToFile(path, c_rawDataFolderName, channel.Name, fileNumber, channel.RawData[0]);
            }

            //
            // saveing the physical and additional data from all the channels, including the complex
            //
            foreach (var channel in physicalChannels)
            {
                //
                // save physical data to file
                //
                finalNumber = WriteSingleArrayToFile(path, c_physicalDataFolderName, channel.Name, finalNumber, channel.PhysicalData[0]);

                //
                // save addition data to files
                //
                fileNumber = WriteListOfArraysToFile(path, c_additionalDataFolderName, channel.Name, finalNumber, channel.AdditionalData);
            }
            return finalNumber;
        }
        private double AverageVoltageAfterEachStep(double[,] dataAfterEachSter)
        {
            double average = 0;
            for (int i = 0; i < dataAfterEachSter.GetLength(0); i++)
            {
                for (int j = 0; j < dataAfterEachSter.GetLength(1); j++)
                {
                    average = average + dataAfterEachSter[i,j] / dataAfterEachSter.GetLength(0);
                }
            }
            return average;
        }
    }
    public class SBJControllerSettingsForCalibration
    {
        public CalibrationSBJControllerSettings CalibirationSettings { get; set; }
        public ElectroMagnetSBJControllerSettings ElectromagnetSettings { get; set; }
        public ChannelsSettings ChannelsSettings { get; set; }

        public SBJControllerSettingsForCalibration(CalibrationSBJControllerSettings calibirationSettings,
                                    ElectroMagnetSBJControllerSettings electromagnetSettings,
                                    ChannelsSettings channelsSettings)
        {
            CalibirationSettings = calibirationSettings;
            ElectromagnetSettings = electromagnetSettings;                        
            ChannelsSettings = channelsSettings;
        }
    }
    public class CalibrationSBJControllerSettings
    {
    
        public double Bias { get; set; }
        public string Gain { get; set; }
        public double TriggerVoltage { get; set; }
        public double TriggerConductance { get; set; }
        public int TotalSamples { get; set; }
        public int SampleRate { get; set; }
        public int PretriggerSamples { get; set; }
        public bool IsFileSavingRequired { get; set; }
        public string Path { get; set; }
        public int CurrentFileNumber { get; set; }
        public int TotalNumberOfCycles { get; set; }
        public double ShortCircuitVoltage { get; set; }
        public bool EnableElectroMagnet { get; set; }
        public bool UseKeithley { get; set; }

        public CalibrationSBJControllerSettings (double bias, string gain, double triggerVoltage,
                                     double triggerConductance, bool isFileSavingRequired, int sampleRate,
                                     int totalSamples, int pretriggerSamples,
                                     string path, int currentFileNumber, int totalNUmberOfCycles,
                                     double shourtCircuitVoltage, bool EnableElectroMagnet, bool UseKeithley)
        {
            Bias = bias;
            Gain = gain;
            TriggerConductance = triggerConductance;
            TriggerVoltage = triggerVoltage;
            SampleRate = sampleRate;
            TotalSamples = totalSamples;
            PretriggerSamples = pretriggerSamples;
            IsFileSavingRequired = isFileSavingRequired;
            Path = path;
            CurrentFileNumber = currentFileNumber;
            TotalNumberOfCycles = totalNUmberOfCycles;
            ShortCircuitVoltage = shourtCircuitVoltage;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("Calibration Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString() + Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }  
}
