using NationalInstruments.DAQmx;
using System.Text;
using SBJController.Properties;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SBJController
{
    /// <summary>
    /// This class represents a controller for all data aquisition operations
    /// </summary>
    class DataAcquisitionController
    {
        #region Members
        private const string c_refTriggeredTask = "RefTriggeredTask";
        private const string c_refContAOTask = "RefContinuousAOTask";
        private const string c_refContAITask = "RefContinuousAITask";
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
            try
            {
                m_daqDeviceType = (DAQDeviceType)Enum.Parse(m_daqDeviceType.GetType(), Settings.Default.DAQDeviceType);
            }
            catch (ArgumentException ex)            
            {
                throw new SBJException(string.Format("The DAQDeviceType {0} is invalid.\n Please change to one of the following: {1}",
                    Settings.Default.DAQDeviceType, GetAvailableDAQDevices(), ex));
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
            AIChannel anaglogChannel = analogInputTask.AIChannels.CreateVoltageChannel(Settings.Default.DAQPhysicalChannelName0, string.Empty,
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
            //
            StringBuilder physicalChannelName = new StringBuilder();

            foreach (var channel in properties.ActiveChannels)
            {
                physicalChannelName.Append(channel.PhysicalName);
                physicalChannelName.Append(",");
            }

            //
            // Remove last ':' from channel name
            //
            physicalChannelName.Remove(physicalChannelName.Length - 1, 1);
           
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
        /// Creates a continuous read task.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task CreateContinuousAITask(TaskProperties properties)
        {
            //
            // Create anaglog input task with the specified name
            //
            Task analogInputTask = new Task(c_refContAITask);

            //
            // Construct the the physical channel name according to the desired inputs.
            //
            StringBuilder physicalChannelName = new StringBuilder();

            foreach (var channel in properties.ActiveChannels)
            {
                physicalChannelName.Append(channel.PhysicalName);
                physicalChannelName.Append(",");
            }

            //
            // Remove last ':' from channel name
            //
            physicalChannelName.Remove(physicalChannelName.Length - 1, 1);

            //
            // Define a voltage channel
            //
            AIChannel anaglogChannel = analogInputTask.AIChannels.CreateVoltageChannel(physicalChannelName.ToString(), string.Empty,
                                                            GetAITerminalConfiguration(),
                                                            -10, 10, AIVoltageUnits.Volts);
            //
            // Configure sampling timing. the buffer size is large enough for 10 minutes reading
            //
            analogInputTask.Timing.ConfigureSampleClock(string.Empty, properties.SampleRate, SampleClockActiveEdge.Rising,
                                                        SampleQuantityMode.ContinuousSamples, (int)properties.SampleRate * 600);
            //
            // Verify the task
            //
            analogInputTask.Control(TaskAction.Verify);
            
            //
            // read the data from the buffer from the last read point.
            //
            analogInputTask.Stream.ReadRelativeTo = ReadRelativeTo.CurrentReadPosition;
            analogInputTask.Stream.ReadOffset = 0;
            
            //
            // no time limitation for the reading process
            //
            analogInputTask.Stream.Timeout = -1;
            
            return analogInputTask;
         }

        /// <summary>
        /// Create continuous Analog Out Task
        /// </summary>
        /// <param name="properties">The properties of the task</param>
        /// <returns>The task to be activated</returns>
        public Task CreateContinuousAOTask(ContinuousAOTaskProperties properties)
        {
            //
            // Create analog output task with the specified name
            //
            Task analogOutputTask = new Task(c_refContAOTask);

            //
            // Define a voltage channel
            //
            AOChannel analogChannel = analogOutputTask.AOChannels.CreateVoltageChannel(Settings.Default.DAQPhysicalChannelName_IVOutput, 
                string.Empty, -properties.Amplitude, properties.Amplitude, AOVoltageUnits.Volts);

            //
            // Configure sampling clock, rate, number of samples
            //
            analogOutputTask.Timing.ConfigureSampleClock(string.Empty, properties.SampleRate, SampleClockActiveEdge.Rising,
                SampleQuantityMode.ContinuousSamples, properties.SamplesPerChannel);
 
            //
            // allow regeneration of the writing process - that means the output will keep working until we stop or change it. 
            //
            analogOutputTask.Stream.WriteRegenerationMode = WriteRegenerationMode.AllowRegeneration;

            //
            // verify task
            //
            analogOutputTask.Control(TaskAction.Verify);

            return analogOutputTask;
        }

        /// <summary>
        /// Create calibration task
        /// </summary>
        /// <param name="properties">The properties relevant for the task</param>
        /// <returns>The task to be activated</returns>
        public Task CreateCalibrationTask(TaskProperties properties)
        {
            Task analogInputTask = new Task();

            analogInputTask.AIChannels.CreateVoltageChannel(Settings.Default.DAQPhysicalChannelName0, string.Empty,
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


        private string GetAvailableDAQDevices()
        {
            StringBuilder daqDevices = new StringBuilder();
            foreach (string deviceName in Enum.GetNames(m_daqDeviceType.GetType()))
            {
                daqDevices.Append(deviceName);
                daqDevices.Append(";");
            }
            return daqDevices.ToString();            
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
        public TriggeredTaskProperties(IList<IDataChannel> activeChannels, double sampleRate, int samplesPerChannel, double triggerLevel, long preTriggerSamples, AnalogEdgeReferenceTriggerSlope triggerSlope) 
            : base(sampleRate, samplesPerChannel, activeChannels)
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
        private IList<IDataChannel> m_activeChannels;
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
        /// The active channels
        /// </summary>
        public IList<IDataChannel> ActiveChannels
        {
            get { return m_activeChannels; }
            private set { m_activeChannels = value; }
        }
        #endregion

        #region Constructor
        public TaskProperties(double sampleRate, int samplesPerChannel, IList<IDataChannel> activeChannels)
        {
            SampleRate = sampleRate;
            SamplesPerChannel = samplesPerChannel;
            ActiveChannels = activeChannels;
        }

        public TaskProperties(double sampleRate, int samplesPerChannel) 
        {
            SampleRate = sampleRate;
            SamplesPerChannel = samplesPerChannel;
        }

        public TaskProperties(double sampleRate, IList<IDataChannel> activeChannels)
        {
            SampleRate = sampleRate;
            ActiveChannels = activeChannels;
        }
        #endregion
    }

    /// <summary>
    /// Represents a class for continuous analog out task properties
    /// </summary>
    class ContinuousAOTaskProperties : TaskProperties
    {
        #region Private Members
        private double m_amplitude;
        #endregion

        #region Properties
        /// <summary>
        /// Wave amplitude (half peak-to-peak)
        /// </summary>
        public double Amplitude
        {
            get { return m_amplitude; }
            private set { m_amplitude = value; }
        }
        #endregion

        #region Constructor
        public ContinuousAOTaskProperties(double sampleRate, int samplesPerChannel, double amplitude)
            : base(sampleRate, samplesPerChannel)
        {
            Amplitude = amplitude;
        }
        #endregion
    }

    #endregion
}
