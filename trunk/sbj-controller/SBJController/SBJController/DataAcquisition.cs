using NationalInstruments.DAQmx;
using System.Text;
using SBJController.Properties;
using System;

namespace SBJController
{
    /// <summary>
    /// This class represents a controller for all data aquisition operations
    /// </summary>
    class DataAcquisitionController
    {
        #region Members
        private const string c_refTriggeredTask = "RefTriggeredTask";
        private const string c_startTriggeredTask = "StartTriggeredTask";
        private DAQDeviceType m_daqDeviceType;

        #endregion 

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DataAcquisitionController()
        {
            //
            // convert the DAQDeviceType given by the settings to its enum representation.
            //
            if (!Enum.TryParse(Settings.Default.DAQDeviceType, out m_daqDeviceType))
            {
                throw new SBJException(string.Format("The DAQDeviceType {0} is invalid.\n Please change to one of the following: {1}",
                    Settings.Default.DAQDeviceType));
            }
        }
        #endregion 

        #region Public Methods
        /// <summary>
        /// Create DAQ Task with trigger
        /// </summary>
        /// <param name="properties">The properties of the task</param>
        /// <returns>The task to be activated</returns>
        public Task CreateTriggeredTask(TriggeredTaskProperties properties)
        {
            //
            // Create anaglog input task with the specified name
            //
            Task analogInputTask = new Task(c_refTriggeredTask);

            //
            // Define a voltage channel
            //
            AIChannel anaglogChannel = analogInputTask.AIChannels.CreateVoltageChannel(Settings.Default.DAQPhysicalChannelName1, string.Empty,
                                                            GetAITerminalConfiguration(), 
                                                            -10, 10, AIVoltageUnits.Volts);
            //
            // Configure sampling rate
            //
            analogInputTask.Timing.ConfigureSampleClock(string.Empty, properties.SampleRate, 
                                                        SampleClockActiveEdge.Falling, 
                                                        SampleQuantityMode.FiniteSamples,
                                                        properties.SamplesPerChannel);

            //
            // Configure the trigger
            //
            analogInputTask.Triggers.ReferenceTrigger.ConfigureAnalogEdgeTrigger(anaglogChannel.VirtualName, 
                                                                                 properties.TriggerSlope,
                                                                                 properties.TriggerLevel,
                                                                                 properties.PreTriggerSamples);
            
            analogInputTask.Stream.ReadRelativeTo = ReadRelativeTo.FirstPretriggerSample;
            analogInputTask.Stream.ReadOffset = 0;
            analogInputTask.Stream.Timeout= 600000;
            analogInputTask.Stream.ReadAllAvailableSamples = false;                      
                
            return analogInputTask;
        }

        public Task CreateMultipleChannelsTriggeredTask(TriggeredTaskProperties properties)
        {
            //
            // Create anaglog input task with the specified name
            //
            Task analogInputTask = new Task(c_refTriggeredTask);

            //
            // Construct the the physical channel name according to the desired inputs.
            // First channel is always monitor.
            //
            StringBuilder physicalChannelName = new StringBuilder(Settings.Default.DAQPhysicalChannelName1);

            //
            // If we also have lock in signal than add a channel
            //
            if (properties.SampleLockInSignal)
            {
                physicalChannelName.Append(":");
                physicalChannelName.Append(Settings.Default.DAQPhysicalChannelName2);
            }

            //
            // If we also monitor the lock in phase signal than add another channel.
            //
            if (properties.SampleLockInPhaseSignal)
            {
                physicalChannelName.Append(":");
                physicalChannelName.Append(Settings.Default.DAQPhysicalChannelName3);
            }
            
            //
            // Define a voltage channel
            //
            AIChannel anaglogChannel = analogInputTask.AIChannels.CreateVoltageChannel(physicalChannelName.ToString(), string.Empty,
                                                            GetAITerminalConfiguration(),
                                                            -10, 10, AIVoltageUnits.Volts);
            //
            // Configure sampling rate
            //
            analogInputTask.Timing.ConfigureSampleClock(string.Empty, properties.SampleRate,
                                                        SampleClockActiveEdge.Falling,
                                                        SampleQuantityMode.FiniteSamples,
                                                        properties.SamplesPerChannel);

            //
            // Configure the trigger according to channel 1
            //
            analogInputTask.Triggers.ReferenceTrigger.ConfigureAnalogEdgeTrigger(analogInputTask.AIChannels[0].VirtualName,
                                                                                 properties.TriggerSlope,
                                                                                 properties.TriggerLevel,
                                                                                 properties.PreTriggerSamples);

                                                                          

            analogInputTask.Stream.ReadRelativeTo = ReadRelativeTo.FirstPretriggerSample;
            analogInputTask.Stream.ReadOffset = 0;
            analogInputTask.Stream.Timeout = 600000;
            analogInputTask.Stream.ReadAllAvailableSamples = false;

            return analogInputTask;
          
        }

        /// <summary>
        /// Create calibration task
        /// </summary>
        /// <param name="properties">The properties relevant for teh task</param>
        /// <returns>The task to be activated</returns>
        public Task CreateCalibrationTask(TaskProperties properties)
        {
            Task analogInputTask = new Task();

            analogInputTask.AIChannels.CreateVoltageChannel(Settings.Default.DAQPhysicalChannelName1, string.Empty,
                                                             GetAITerminalConfiguration(),
                                                             -10, 0, AIVoltageUnits.Volts);

            analogInputTask.Timing.ConfigureSampleClock(string.Empty, properties.SampleRate,
                                                        SampleClockActiveEdge.Rising, 
                                                        SampleQuantityMode.ContinuousSamples, 
                                                        properties.SamplesPerChannel);

            analogInputTask.Timing.SampleTimingType = SampleTimingType.SampleClock;

            return analogInputTask;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// choosing configuration for a voltage channel.
        /// NI447x support only pseudodifferential, NI446x support both differnetial and pseudo.
        /// </summary>
        /// <returns>the suitable AITerminalConfiguration</returns>
        private AITerminalConfiguration GetAITerminalConfiguration()
        {
            AITerminalConfiguration terminalConfiguration =
                (m_daqDeviceType == DAQDeviceType.PCI4461) ? AITerminalConfiguration.Differential : AITerminalConfiguration.Pseudodifferential;
            
            return terminalConfiguration;
        }

        #endregion

    }

    #region TaskProperties Class
    /// <summary>
    /// Represents a class for triggered task properties
    /// </summary>
    class TriggeredTaskProperties : TaskProperties
    {
        #region Private Members
        private double m_triggerLevel;
        private long m_preTriggerSamples;        
        private AnalogEdgeReferenceTriggerSlope m_triggerSlope;
        #endregion

        #region Properties
        /// <summary>
        /// The trigger level in voltage
        /// </summary>
        public double TriggerLevel
        {
            get { return m_triggerLevel; }
            private set { m_triggerLevel = value; }
        }        

        /// <summary>
        /// Number of samples to aquire before the trigger
        /// </summary>
        public long PreTriggerSamples
        {
            get { return m_preTriggerSamples; }
            private set { m_preTriggerSamples = value; }
        }

        public AnalogEdgeReferenceTriggerSlope TriggerSlope
        {
            get { return m_triggerSlope; }
            private set { m_triggerSlope = value; }
        }
        #endregion

        #region Constructor
        public TriggeredTaskProperties(bool sampleLockInSignal, bool sampleLockInPhaseSignal, double sampleRate, int samplesPerChannel, double triggerLevel, long preTriggerSamples, AnalogEdgeReferenceTriggerSlope triggerSlope) 
            : base(sampleRate, samplesPerChannel, sampleLockInSignal, sampleLockInPhaseSignal)
        {
            TriggerLevel = triggerLevel;
            PreTriggerSamples = preTriggerSamples;
            TriggerSlope = triggerSlope;
        }
        #endregion
    }    

    class TaskProperties
    {
        #region Private members
        private double m_sampleRate;
        private int m_samplesPerChannel;
        private bool m_sampleLockInSignal;
        private bool m_sampleLockInPhaseSignal;
        #endregion

        #region Properties
        /// <summary>
        /// The sampling rate in Hz
        /// </summary>
        public double SampleRate
        {
            get { return m_sampleRate; }
            private set { m_sampleRate = value; }
        }
        
        /// <summary>
        /// Number of samples to aquire per channel
        /// </summary>
        public int SamplesPerChannel
        {
            get { return m_samplesPerChannel; }
            private set { m_samplesPerChannel = value; }
        }

        /// <summary>
        /// Indicates whether lock in signal should be monitored.
        /// </summary>
        public bool SampleLockInSignal
        {
            get { return m_sampleLockInSignal; }
            private set { m_sampleLockInSignal = value; }
        }

        /// <summary>
        /// Indicates whether lock in phase signal should be monitored.
        /// </summary>
        public bool SampleLockInPhaseSignal
        {
            get { return m_sampleLockInPhaseSignal; }
            private set { m_sampleLockInPhaseSignal = value; }
        }
        #endregion

        #region Constructor
        public TaskProperties(double sampleRate, int samplesPerChannel, bool sampleLockInSignal, bool sampleLockInPhaseSignal)
        {
            SampleRate = sampleRate;
            SamplesPerChannel = samplesPerChannel;
            SampleLockInSignal = sampleLockInSignal;
            SampleLockInPhaseSignal = sampleLockInPhaseSignal;
        }
        #endregion
    }
    #endregion
}
