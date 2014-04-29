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
        /// <summary>
        /// Acquire data for Calibration.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool AquireCalibrationData(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isCancelled = false;
            int finalFileNumber = settings.CalibirationSettings.CurrentFileNumber;
            double currentVoltage;

            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            //
            // Apply voltage with desired tool: Task or Keithley
            //
            ApplyVoltageIfNeeded(settings.CalibirationSettings.UseKeithley,
                                 settings.CalibirationSettings.Bias, 0.0);

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.CalibirationSettings.IsFileSavingRequired, settings.CalibirationSettings.Path);
           
            //
            //apply initial voltage on the EM
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.ElectromagnetSettings.IsEMEnable);

            //
            // Reach to contact before we start doint the cycles
            //
            isCancelled = CalibrationInitialShortCircuit(settings, worker, e);
               
            //
            // if user cancelled work during the execution of the last function, close everything and exit.
            //
            if (isCancelled)
            {
                m_stepperMotor.Shutdown();
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    m_electroMagnet.Shutdown();
                }
                return isCancelled;
            }

            //
            // Create the task
            //
            m_triggeredTask = GetCalibrationTask(settings, worker, e);

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

                currentVoltage = Math.Abs(AnalogIn(0));

                //
                // Start opening and/or closing the junction.
                // If EM is enabled, use the EM.
                //
                if (settings.ElectromagnetSettings.IsEMEnable)
                {
                    if (settings.CalibirationSettings.MeasurementType == CalibrationMeasurementType.OpenJunction)
                    {
                        //
                        // save only the opening process
                        //
                        ObtainOpenJunctionByElectroMagnetForCalibration(settings, worker, e);
                        EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.CalibirationSettings.ShortCircuitVoltage, worker, e);
                    }

                    if (settings.CalibirationSettings.MeasurementType == CalibrationMeasurementType.CloseJunction)
                    {
                        //
                        // save only the closing process
                        //
                        ObtainOpenJunctionByElectroMagnet(settings.CalibirationSettings.TriggerVoltage, worker, e);
                        CloseJunctionByElectroMagnetForCalibration(settings, worker, e);
                    }

                    if (settings.CalibirationSettings.MeasurementType == CalibrationMeasurementType.BothOpenAndClose)
                    {
                        if (currentVoltage > 0.7 * settings.CalibirationSettings.ShortCircuitVoltage)
                        {
                            //
                            // Open the Junction with saving
                            //
                            ObtainOpenJunctionByElectroMagnetForCalibration(settings, worker, e);
                        }
                        else
                        {
                            //
                            // Close the Junction with saving
                            //
                            CloseJunctionByElectroMagnetForCalibration(settings, worker, e);
                        }
                    }
                }
                else
                {
                    if (settings.CalibirationSettings.MeasurementType == CalibrationMeasurementType.OpenJunction)
                    {
                        //
                        // save only the opening process
                        //
                        ObtainOpenJunctionByStepperMotorForCalibration(settings, worker, e);
                        TryObtainShortCircuit(settings.CalibirationSettings.ShortCircuitVoltage, worker, e);
                    }

                    if (settings.CalibirationSettings.MeasurementType == CalibrationMeasurementType.CloseJunction)
                    {
                        //
                        // save only the closing process
                        //
                        ObtainOpenJunctionByStepperMotor(settings.CalibirationSettings.TriggerVoltage, worker, e);
                        CloseJunctionByStepperMotorForCalibration(settings, worker, e);
                    }

                    if (settings.CalibirationSettings.MeasurementType == CalibrationMeasurementType.BothOpenAndClose)
                    {
                        if (currentVoltage > 0.7 * settings.CalibirationSettings.ShortCircuitVoltage)
                        {
                            //
                            // Open the Junction with saving
                            //
                            ObtainOpenJunctionByStepperMotorForCalibration(settings, worker, e);
                        }
                        else
                        {
                            //
                            // Close the Junction with saving
                            //
                            CloseJunctionByStepperMotorForCalibration(settings, worker, e);
                        }
                    }  
                }

                //
                // data acquisition is done for this trace, stop the task
                //
                m_triggeredTask.Stop();

                //
                // if operation was cancelled by user during the closing/opening, quit without saving this trace.
                //
                if (e.Cancel)
                {
                    break;
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
                    finalFileNumber = SaveData(settings.CalibirationSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
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
            m_triggeredTask.Dispose();
            m_stepperMotor.Shutdown();
            if (settings.ElectromagnetSettings.IsEMEnable)
            {
                m_electroMagnet.Shutdown();
            }

            return (isCancelled || e.Cancel);
        }

        /// <summary>
        /// Reach to contact by EM or Stepper motor.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns>did the user stopped the work.</returns>
        private bool CalibrationInitialShortCircuit(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isCancelled = false;
           
            //
            // Reach to contact by stepper motor.
            // We don't want to use stepper motor ONLY in the case we use the EM AND the skip-steppermotor is checked. 
            //
            if (!(settings.ElectromagnetSettings.IsEMEnable && settings.ElectromagnetSettings.IsEMSkipFirstCycleEnable))
            {
                isCancelled = TryObtainShortCircuit(settings.CalibirationSettings.ShortCircuitVoltage, worker, e);
                
                //
                // if we want to use EM, than we need to open the junction by the stepper motor and only then close it by the EM
                //
                if (settings.ElectromagnetSettings.IsEMEnable && !isCancelled)
                {
                    isCancelled = ObtainOpenJunctionByStepperMotor(settings.CalibirationSettings.TriggerVoltage, worker, e);

                    //
                    // from now on we will be using the electroMagnet, so lets turn the stepper motor off and move to the next cycle
                    //
                    m_stepperMotor.Shutdown();
                }
            }

            //
            // if EM is enabled, then reach contact by EM
            //
            if (settings.ElectromagnetSettings.IsEMEnable && !isCancelled)
            {
                EMTryObtainShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.CalibirationSettings.ShortCircuitVoltage, worker, e);
            }

            return isCancelled;
        }

        /// <summary>
        /// read data and average it. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private double GetDataAfterEachStep(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double[,] dataAquired=null;
            AnalogMultiChannelReader reader = new AnalogMultiChannelReader(m_triggeredTask.Stream);

            dataAquired = reader.ReadMultiSample(-1);

            if (settings.ChannelsSettings.ActiveChannels.Count != dataAquired.GetLength(0))
            {
                throw new SBJException("Number of data channels doesn't fit the recieved data.");
            }

            return AverageOverOneRawMatrix(dataAquired);
        }

        /// <summary>
        /// Get continuous analog-in task (calibration task)
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Task GetCalibrationTask(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        { 
            //
            // Create the task with its propertites
            //
            TaskProperties taskProperties = new TaskProperties(settings.CalibirationSettings.SampleRate, 
                                                               settings.ChannelsSettings.ActiveChannels);

            return m_daqController.CreateContinuousAITask(taskProperties);
        }
        
        /// <summary>
        /// Open the junction using the stepper motor, while saving the current after each step.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="openCircuitVoltage"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ObtainOpenJunctionByStepperMotorForCalibration(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentOpenCircuit = false;
            bool isTempOpenCircuit = false;
            List<double> rawDataList = new List<double>();

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_stepperMotor.Direction = StepperDirection.UP;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = settings.CalibirationSettings.DelayTime;

            //
            // Open the junction
            //
            while (!isPermanentOpenCircuit)
            {
                //
                // Move up one step and check the voltage afterwards
                //
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(m_stepperMotor.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Get Data after each step and add it to rawDataList.
                //
                rawDataList.Add(GetDataAfterEachStep(settings, worker, e));
                
                //
                // If the junction is open, both current voltage and voltgae after stepping
                // should be smaller than the open circuit threshold.
                //
                isTempOpenCircuit = (currentVoltage < Math.Abs(settings.CalibirationSettings.TriggerVoltage)) &&
                                     (voltageAfterStepping < Math.Abs(settings.CalibirationSettings.TriggerVoltage));

                //
                // If we think we've reached open circuit than wait
                // for 10msec and then check again to verify this is permanent.
                //
                if (isTempOpenCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentOpenCircuit = currentVoltage < Math.Abs(settings.CalibirationSettings.TriggerVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }

            //
            // Assign the aquired data for each channel.
            // First clear all data from previous interation.
            //                
            ClearRawData(settings.ChannelsSettings.ActiveChannels);               
            AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertToMatrix(rawDataList));

            return e.Cancel;
        }
        
        /// <summary>
        /// Close Junction by stepper motor, saves current after each step. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CloseJunctionByStepperMotorForCalibration(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentClosedCircuit = false;
            bool isTempClosedCircuit = false;
            List<double> rawDataList = new List<double>();

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_stepperMotor.Direction = StepperDirection.DOWN;
            m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
            m_stepperMotor.Delay = settings.CalibirationSettings.DelayTime;

            //
            // Close the junction
            //
            while (!isPermanentClosedCircuit)
            {
                //
                // Move up one step and check the voltage afterwards
                //
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(m_stepperMotor.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Get Data after each step and add it to rawDataList.
                //
                rawDataList.Add(GetDataAfterEachStep(settings, worker, e));

                //
                // If the junction is closed, both current voltage and voltgae after stepping
                // should be bigger than the closed circuit threshold.
                //
                isTempClosedCircuit = (currentVoltage > Math.Abs(settings.CalibirationSettings.ShortCircuitVoltage)) &&
                                     (voltageAfterStepping > Math.Abs(settings.CalibirationSettings.ShortCircuitVoltage));

                //
                // If we think we've reached closed circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempClosedCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentClosedCircuit = currentVoltage > Math.Abs(settings.CalibirationSettings.ShortCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }

            //
            // Assign the aquired data for each channel
            //
            ClearRawData(settings.ChannelsSettings.ActiveChannels);               
            AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertToMatrix(rawDataList));    

            return e.Cancel;
        }
        
        /// <summary>
        /// Open Junction by EM, while saving the voltage after each step.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="openCircuitVoltage"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ObtainOpenJunctionByElectroMagnetForCalibration(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentOpenCircuit = false;
            bool isTempOpenCircuit = false;
            List<double> rawDataList = new List<double>();

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set EM direction and delay
            // 
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = settings.ElectromagnetSettings.EMFastDelayTime;
             
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
                // Move up one step.
                // If reached min Voltage, do 200 steps by stepper motor, clear the list that saves the data and keep trying to open the junction...
                //
                if (!m_electroMagnet.MoveSingleStep())
                {
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                    MoveStepsByStepperMotor(StepperDirection.UP, 200);
                    rawDataList = new List<double>();
                }

                //
                // wait the delay time, and check the voltage after stepping. 
                //
                Thread.Sleep(m_electroMagnet.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // if voltage after stepping starts decreasing, switch delay time to the longer (slower) one.  
                //
                if(voltageAfterStepping < 0.9*settings.CalibirationSettings.ShortCircuitVoltage)
                {
                    m_electroMagnet.Delay = settings.ElectromagnetSettings.EMSlowDelayTime;
                }

                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // get data after each step and add it to the rawDataList.
                //
                rawDataList.Add(GetDataAfterEachStep(settings, worker, e));

                //
                // If the junction is open, both current voltage and voltage after stepping
                // should be smaller than the open circuit threshold.
                //
                isTempOpenCircuit = (currentVoltage < Math.Abs(settings.CalibirationSettings.TriggerVoltage)) &&
                                     (voltageAfterStepping < Math.Abs(settings.CalibirationSettings.TriggerVoltage));

                //
                // If we think we've reached open circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempOpenCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentOpenCircuit = currentVoltage < Math.Abs(settings.CalibirationSettings.TriggerVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }

            //
            // Assign the aquired data for each channel.
            // First clear all data from previous interation.
            //                
            ClearRawData(settings.ChannelsSettings.ActiveChannels);
            if (!e.Cancel)
            {
                AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertToMatrix(rawDataList));
            }

            return e.Cancel;
        }
        
        /// <summary>
        /// Closing the junction by the ElectroMagnet, while saving the data. 
        /// Notice: Only one delay-time is applied, the longer (slower) one. 
        /// [Changing the delay in the middle of the trace will be too late when closing the junction]  
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="TriggerVoltage"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CloseJunctionByElectroMagnetForCalibration(CalibrationSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentClosedCircuit = false;
            bool isTempClosedCircuit = false;
            List<double> rawDataList = new List<double>();

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set EM direction and delay
            // 
            m_electroMagnet.Direction = StepperDirection.DOWN;
            m_electroMagnet.Delay = settings.ElectromagnetSettings.EMSlowDelayTime;
            
         
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
                // Move down one step.
                // If reached min Voltage, do 200 steps by stepper motor, clear the list that saves the data and keep trying to open the junction...
                //
                if (!m_electroMagnet.MoveSingleStep())
                {
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                    MoveStepsByStepperMotor(StepperDirection.UP, 200);
                    rawDataList = new List<double>();
                }

                //
                // wait the delay time, and check the voltage after stepping. 
                //
                Thread.Sleep(m_electroMagnet.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Get Data since the last step and add it to rawDataList.
                //
                rawDataList.Add(GetDataAfterEachStep(settings, worker, e));

                //
                // If the junction is closed, both current voltage and voltgae after stepping
                // should be bigger than the closed circuit threshold.
                //
                isTempClosedCircuit = (currentVoltage > Math.Abs(settings.CalibirationSettings.ShortCircuitVoltage)) &&
                                     (voltageAfterStepping > Math.Abs(settings.CalibirationSettings.ShortCircuitVoltage));

                //
                // If we think we've reached closed circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempClosedCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentClosedCircuit = currentVoltage > Math.Abs(settings.CalibirationSettings.ShortCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }

            //
            // Assign the aquired data for each channel
            //
            ClearRawData(settings.ChannelsSettings.ActiveChannels);
            if (!e.Cancel)
            {
                AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, ConvertToMatrix(rawDataList));
            }
            return e.Cancel;
        }
                
        /// <summary>
        /// Obtain open junction by the ElectroMagnet, whithout saving any data. 
        /// </summary>
        /// <param name="openCircuitVoltage"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns>true if the user cancelled the process, false if the process was done successfuly.</returns>
        private bool ObtainOpenJunctionByElectroMagnet(double openCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentOpenCircuit = false;
            bool isTempOpenCircuit = false;
           
            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = m_electroMagnet.MinDelay;

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
                // Move up 5 steps and check the voltage afterwards.
                // If reached to minimum voltage on EM, change the voltage to the initial one and do some steps by the stepper motor. 
                //
                if (!m_electroMagnet.MoveMultipleSteps(5))
                {
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                    MoveStepsByStepperMotor(StepperDirection.UP, 200);
                }
                Thread.Sleep(m_electroMagnet.Delay);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

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
            return e.Cancel;
        }
        
        /// <summary>
        /// Calculate the average of a vector-matrix of type double[1,X]
        /// </summary>
        /// <param name="data">The vector (matrix) to be averaged. </param>
        /// <returns>The average.</returns>
        private double AverageOverOneRawMatrix(double[,] data)
        {
            double average = 0;
            for (int i = 0; i < data.GetLength(1); i++)
            {
                average = average + data[0,i] / data.GetLength(1);
            }
            return average;
        }
        
        /// <summary>
        /// Convert list of double to a 2D matrix of type double[1,length of list]
        /// </summary>
        /// <param name="rawDataList">list to convert</param>
        /// <returns>2D matrix</returns>
        private double[,] ConvertToMatrix(List<double> rawDataList)
        {
            double[,] data = new double[1, rawDataList.Count];

            for (int i = 0; i < rawDataList.Count; i++)
            {
                data[0, i] = rawDataList[i];
            }
            return data;
        }
    }
}
