using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NationalInstruments.DAQmx;
using System.Windows.Forms;
using System.Threading;

namespace SBJController
{
    public partial class SBJController
    {
        #region Private Members
        private const double c_preAmpOffset = 0.0;
        private Task m_outputTask;
        private Task m_ivInputTask;
        private FunctionGenerator m_functionGenerator;
        private AnalogSingleChannelWriter writer;
        private AnalogMultiChannelReader reader;
        private delegate bool IV_EMOpenJunctionMethodDelegate(IVSettings settings);
        #endregion

        #region Properties
        public Task OutputTask
        {
            get { return m_outputTask; }
            set { m_outputTask = value; }
        }

        public Task IVInputTask
        {
            get { return m_ivInputTask; }
            set { m_ivInputTask = value; }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Start a constant output by the DAQ device. This function starts a task named "OutputTask",
        /// Don't forget to stop it when it's no longer needed.
        /// </summary>
        /// <param name="voltage">the requested voltage to apply</param>
        public void StartConstantOutputTask(double voltage)
        {
            int samplesPerChannel = 2500;
            int sampleRate = 2500;

            //
            // generate output array 
            //
            m_functionGenerator = new FunctionGenerator(samplesPerChannel, voltage, voltage);
            
            //
            // get the properties required for the output task
            //
            ContinuousAOTaskProperties outputProperties = new ContinuousAOTaskProperties(sampleRate, samplesPerChannel, voltage);
            
            //
            // create the output task
            //
            m_outputTask = m_daqController.CreateContinuousAOTask(outputProperties);

            //
            // finds the on-board buffer length for the output task
            //
            long onBoardBufferLength = m_outputTask.Stream.Buffer.OutputOnBoardBufferSize;

            //
            // create the writer of the output task
            //
            writer = new AnalogSingleChannelWriter(m_outputTask.Stream);

            //
            // write static voltage
            //
            writer.WriteMultiSample(true, m_functionGenerator.ConstWave);
        }

        /// <summary>
        /// Do Cycles of closing and opening the junction, while measuring IVs
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        public void IV_AcquireData(IVSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool isCancelled = false;
            int finalFileNumber = settings.IVGeneralSettings.CurrentFileNumber;
            double[,] dataAcquired;
            List<IDataChannel> physicalChannels = new List<IDataChannel>();

            if (settings.IVSteppingMethodSettings.SteppingDevice != SteppingDevice.ElectroMagnet)
            {
                MessageBox.Show("Please enable the ElectroMagnet in order to acquire IV curves.");
                return;
            }

            //
            // Save this run settings if desired
            //
            SaveSettingsIfNeeded(settings, settings.IVGeneralSettings.IsFileSavingRequired, settings.IVGeneralSettings.Path);

            //
            // apply initial voltage on the EM if needed
            //
            ApplyVoltageOnElectroMagnetIfNeeded(settings.IVSteppingMethodSettings.SteppingDevice == SteppingDevice.ElectroMagnet);

            
            //
            // create the input and output tasks
            //
            m_ivInputTask = GetContinuousAITask(settings);
            m_outputTask = GetContinuousAOTask(settings);

            //
            // initiate writer for the output and set initial bias
            //
            InitiateOutputWriter(m_outputTask, settings);
            
            //
            // Main loop for data aquisition
            //
            for (int i = 0; i < settings.IVGeneralSettings.TotalNumberOfCycles; i++)
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
                // if we use EM, and we are asked to skip the first cycle (that is done by the stepper motor), 
                // move on to the next cycle.
                //
                if (i == 0 && settings.IVSteppingMethodSettings.SteppingDevice == SteppingDevice.ElectroMagnet 
                            && settings.IVSteppingMethodSettings.IsEMSkipFirstCycleEnable)
                {
                    m_stepperMotor.Shutdown();
                    continue;
                }

                //
                // Change the gain power to 5 before reaching contact
                // to ensure full contact current
                //
                m_amplifier.ChangeGain(5);

                //
                // Reach to contact before we start openning the junction
                // If we use EM and we're after the first cycle, use the EM.
                // If user asked to stop than exit
                //
                isCancelled = (settings.IVSteppingMethodSettings.SteppingDevice == SteppingDevice.ElectroMagnet && i > 0) ?
                               EMTryObtainShortCircuit(settings.IVSteppingMethodSettings.EMShortCircuitDelayTime, settings.IVGeneralSettings.ShortCircuitVoltage, worker, e) :
                               TryObtainShortCircuit(settings.IVGeneralSettings.ShortCircuitVoltage, worker, e);
                if (isCancelled)
                {
                    break;
                }

                //
                // Configure the gain to the desired one before strating the measurement.
                // And also this is the time to switch the laser on.
                //
                int gainPower;
                Int32.TryParse(settings.IVGeneralSettings.Gain, out gainPower);
                m_amplifier.ChangeGain(gainPower);

                //
                // Start openning the junction.
                // If EM is enabled and we're after the first cycle, use the EM.
                //
                if (settings.IVSteppingMethodSettings.SteppingDevice == SteppingDevice.ElectroMagnet)
                {
                    if (i == 0)
                    {
                        //
                        // we are on the first cycle and wish to open the junction by the stepper motor.
                        //
                        ObtainOpenJunctionByStepperMotor(settings.IVGeneralSettings.TriggerVoltage, worker, e);

                        //
                        // from now on we will be using the electroMagnet, so lets turn the stepper motor off and move to the next cycle
                        //
                        m_stepperMotor.Shutdown();
                        continue;
                    }
                    else
                    {
                        //
                        // we set the votlage to triangle wave and then open the junction by the EM
                        //
                        writer.BeginWriteMultiSample(false, m_functionGenerator.TriangleWave, null, null);
                        IV_EMBeginOpenJunction(settings);
                    }
                }
                else
                {
                    //
                    // we set the voltage to triangle wave and then open the junction by stepper motor
                    //
                    writer.BeginWriteMultiSample(false, m_functionGenerator.TriangleWave, null, null);
                    //TODO: Add opening function by stepper motor 
                }

                //
                // Start the input task.
                //
                try
                {
                    m_ivInputTask.Start();
                }
                catch (DaqException ex)
                {
                    throw new SBJException("Error occured when tryin to start DAQ input task", ex);
                }

                //
                // start reading continuously. 
                // when the junction is opened, the opening thread will change m_quitJuncctionOpeningOperation to true.
                // set dataAquired to null otherwise it saves last cycle's data. 
                //
                reader = new AnalogMultiChannelReader(m_ivInputTask.Stream);
                dataAcquired = null;
                try
                {
                    while (!m_quitJunctionOpenningOperation)
                    {
                        dataAcquired = reader.ReadMultiSample(-1);
                    }

                    if (dataAcquired != null)
                    {
                        if (settings.ChannelsSettings.ActiveChannels.Count != dataAcquired.GetLength(0))
                        {
                            throw new SBJException("Number of data channels doesn't fit the recieved data.");
                        }
                    }
                }
                catch (DaqException)
                {
                    //
                    // Probably timeout.
                    // Ignore this cycle and rerun.
                    //
                    m_ivInputTask.Stop();
                    continue;
                }

                //
                // At this point the reader has returned with all the data and we can stop the input task.
                //
                m_ivInputTask.Stop();

                //
                // if we didn't acquire any data, there's no need to save anything.
                //
                if (dataAcquired == null)
                {
                    continue;
                }

                //
                // insert raw data into active channels
                //
                AssignRawDataToChannels(settings.ChannelsSettings.ActiveChannels, dataAcquired);

                //
                // physical channel will include both simple and complex channels. 
                // 
                physicalChannels = GetChannelsForDisplay(settings.ChannelsSettings.ActiveChannels);

                //
                // calculate the physical data for each channel
                //
                GetPhysicalData(physicalChannels);

                //
                // the IV acquisition is done, we need to return the output to constant voltage for the next cycle
                //
                writer.BeginWriteMultiSample(false, m_functionGenerator.ConstWave, null, null);

                // 
                // Increase file number by one
                // Save data if needed
                //
                finalFileNumber++;
                if (settings.IVGeneralSettings.IsFileSavingRequired)
                {
                    finalFileNumber = SaveData(settings.IVGeneralSettings.Path, settings.ChannelsSettings.ActiveChannels, physicalChannels, finalFileNumber);
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
            if (settings.IVSteppingMethodSettings.SteppingDevice == SteppingDevice.ElectroMagnet)
            {
                m_electroMagnet.Shutdown();
            }
            m_ivInputTask.Dispose();
            m_ivInputTask = null;
            m_outputTask.Dispose();
            m_outputTask = null;
            m_stepperMotor.Shutdown();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Open the junction asynchronously by the ElectroMagnet for IV measurements
        /// </summary>
        /// <param name="settings"></param>
        private void IV_EMBeginOpenJunction(IVSettings settings)
        {
            IV_EMOpenJunctionMethodDelegate emOpenJunctionDelegate = new IV_EMOpenJunctionMethodDelegate(IV_EMTryOpenJunction);
            AsyncCallback callback = new AsyncCallback(IV_EMEndOpenJunction);
            IAsyncResult asyncResult = emOpenJunctionDelegate.BeginInvoke(settings, callback, emOpenJunctionDelegate);
        }

        /// <summary>
        /// Try open junction by the EM, by calling IV_EMOpenJunction.
        /// if min voltage exceeded without the junction being opened, do a few steps by the stepper motor, then retry EM (recursion).
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool IV_EMTryOpenJunction(IVSettings settings)
        {
            //
            // if the EM reached voltage 0 without opening the junction, 
            // return to higher voltage on EM, do some steps by the stepper motor and retry opening by EM.
            //
            if (!IV_EMOpenJunction(settings))
            {
                m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                MoveStepsByStepperMotor(StepperDirection.UP, 200);
                return IV_EMTryOpenJunction(settings);
            }
            return true;
        }

        /// <summary>
        /// Open the junction by the ElectroMagnet for IV acquisition.
        /// If min voltage exceeded without the junction being opened, return false. 
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private bool IV_EMOpenJunction(IVSettings settings)
        {
            int underTriggerCounts = 0;
            int numOfSteps = 0;
            double changeRateTrigger = 9.9;
            int rateTriggerCounts = 0;

            //
            // Set the direction of the movement
            // And configure the first setpper delay (shorter) - faster movement
            //
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = settings.IVSteppingMethodSettings.EMFastDelayTime;

            //
            // Read the initial voltgae before we've done anything
            //
            double initialVoltage = AnalogIn(0);
            m_quitJunctionOpenningOperation = false;

            //
            // we'll moving up until this function figures the junction is open. then it signals it
            // by changing the value of m_quitJunctionOpeningOperatin to TRUE.
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
                numOfSteps++;
                double currentVoltage = AnalogIn(0);

                //
                // after some steps, slower the rate
                //
                if (numOfSteps == 100)
                {
                    m_electroMagnet.Delay = (int)(0.2 * settings.IVSteppingMethodSettings.EMSlowDelayTime + 0.8 * settings.IVSteppingMethodSettings.EMFastDelayTime);
                }

                ////
                //// after more steps, slower the rate more
                ////
                //if (numOfSteps == 200)
                //{
                //    m_electroMagnet.Delay = (int)settings.ElectromagnetSettings.EMSlowDelayTime;
                //}

                //
                // if we didn't switch to slowest rate yet, and we made more than 100 steps
                //
                if ((rateTriggerCounts >= 0) && (numOfSteps > 100))
                {
                    //
                    // check if the voltage is under the rate trigger. Count how many times in a row it happens.
                    //
                    if (Math.Abs(currentVoltage) < changeRateTrigger)
                    {
                        rateTriggerCounts++;
                    }
                    else
                    {
                        rateTriggerCounts=0;
                    }

                    //
                    // if we are under the trigger for some steps in a row, switch to the slowest rate.
                    //
                    if (rateTriggerCounts > 8)
                    {
                        m_electroMagnet.Delay = (int)settings.IVSteppingMethodSettings.EMSlowDelayTime;
                        rateTriggerCounts = -1;
                    }
                }


                //
                // check if the voltage is under the trigger. Count how many times in a row it happens.
                // correct the trigger by the preamp offset
                //
                if (numOfSteps > 200 && Math.Abs(currentVoltage) < (Math.Abs(settings.IVGeneralSettings.TriggerVoltage)+c_preAmpOffset))
                {
                    underTriggerCounts++;
                }
                else
                {
                    underTriggerCounts = 0;
                }

                //
                // if we are under the trigger for some steps in a row, the junction is open.
                //
                if (underTriggerCounts > 4)
                {
                     m_quitJunctionOpenningOperation = true;
                }
                Thread.Sleep(m_electroMagnet.Delay);
            }
            return true;
        }

        /// <summary>
        /// End junction openning with IV by EM
        /// </summary>
        /// <param name="asyncResult"></param>
        private void IV_EMEndOpenJunction(IAsyncResult asyncResult)
        {
            EMOpenJunctionMethodDelegate emOpenJunctionDelegate = (EMOpenJunctionMethodDelegate)asyncResult.AsyncState;
            emOpenJunctionDelegate.EndInvoke(asyncResult);
        }

        /// <summary>
        /// Get Continuous Analog Input Task
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private Task GetContinuousAITask(IVSettings settings)
        {
            //
            // get the properties required for the input task
            //
            TaskProperties inputTaskProperties = new TaskProperties(settings.IVGeneralSettings.SampleRate, 
                                                                    settings.ChannelsSettings.ActiveChannels);

            //
            // return the input task
            //
            return m_daqController.CreateContinuousAITask(inputTaskProperties);
        }

        /// <summary>
        /// Get Continuous Analog Output Task
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private Task GetContinuousAOTask(IVSettings settings)
        {
            //
            // get the properties required for the output task
            //
            ContinuousAOTaskProperties outputProperties = new ContinuousAOTaskProperties(settings.IVGeneralSettings.OutputUpdateRate,
                                                                                         settings.IVGeneralSettings.SamplesPerCycle,
                                                                                         settings.IVGeneralSettings.VoltageAmplitude);
            //
            // return the output task
            //
            return m_daqController.CreateContinuousAOTask(outputProperties);
        }

        /// <summary>
        /// Initiate the output task writer, and apply an initial constant bias
        /// </summary>
        /// <param name="task">The output task that the writer will belong to</param>
        /// <param name="settings">The UI settings</param>
        private void InitiateOutputWriter(Task task, IVSettings settings)
        {
            //
            // generate output arrays
            //
            m_functionGenerator = new FunctionGenerator(settings.IVGeneralSettings.SamplesPerCycle,
                                                        settings.IVGeneralSettings.VoltageAmplitude,
                                                        settings.IVGeneralSettings.VoltageAmplitude);

            //
            // create the writer of the output task
            //
            writer = new AnalogSingleChannelWriter(m_outputTask.Stream);

            //
            // write static voltage
            //
            writer.WriteMultiSample(true, m_functionGenerator.ConstWave);
        }
        #endregion
    }
}
