using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// The default data channel.
    /// This channel is used to sample data points fron the PCI card direcly.
    /// </summary>
    public class DefaultDataChannel : SimpleDataChannel, IDataChannel
    {
        private const double c_1G0 = 77.5E-6;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical channels on the PCI board. e.g: Dev1\AI0</param>
        /// <param name="settings"></param>
        public DefaultDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "DefaultDataChannel";
        } 

        /// <summary>
        /// Override. 
        /// Converts the voltage to conduction units in G0.
        /// </summary>
        /// <param name="rawVoltageValue"></param>
        /// <returns></returns>
        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {           
            return Math.Abs(rawVoltageValue) / Math.Pow(10, DataConvertionSettings.Gain) / DataConvertionSettings.Bias / c_1G0;
        }       
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency id dictated by the lock in itself.
    /// X equals: X = Vsig*cos(theta), where theta is the phase of the lockin and Vsig is proportional to the 
    /// physical signal from the measurement unit and it is the signal to be interperted.
    /// </summary>
    public class LockInXInternalSourceDataChannel : LockInInternalSourceDataChannel, IDataChannel
    {
        /// <summary>
        /// Cond\structor
        /// </summary>
        /// <param name="physicalName"></param>
        /// <param name="settings"></param>
        public LockInXInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInXInternalSourceDataChannel";
        }
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency id dictated by the lock in itself.
    /// Y equals: Y = Vsig*sin(theta), where theta is the phase of the lockin and Vsig is proportional to the 
    /// physical signal from the measurement unit and it is the signal to be interperted.
    /// </summary>
    public class LockInYInternalSourceDataChannel : LockInInternalSourceDataChannel, IDataChannel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName"></param>
        /// <param name="settings"></param>
        public LockInYInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInYInternalSourceDataChannel";
        }
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency id dictated by the lock in itself.
    /// R means: R = sqrt(X^2 +Y^2). The sampled lock in can be the R itself (instead of separately sampling X and Y).
    /// Please note that in that case the refresh time of the lock in for that channel is only 512Hz.
    /// Sampling X and Y separatley improve accuracy as these channels are updated more frequently.
    /// </summary>
    public class LockInRInternalSourceChannel : LockInInternalSourceDataChannel, IDataChannel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName"></param>
        /// <param name="settings"></param>
        public LockInRInternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInRInternalSourceChannel";
        }
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency is dictated by the lock in itself.
    /// </summary>
    public class LockInInternalSourceDataChannel : LockInDataChannel
    {
        public LockInInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings) {}

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="rawVoltageValue">One raw data point to be converted to physicak data.</param>
        /// <returns>The physical data representation of the input data point.</returns>
        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            //
            // When the lock in is internally referenced, the measured signal is in the form of:
            // Vsig ~= (dI/dV)|Vdc  * Vac.
            // We must divide in the ac voltage applied internally from the lock - in in order to extract the
            // the physical data.
            //
            return base.ConvertVoltageToConductanceValue(rawVoltageValue) / DataConvertionSettings.ACVoltage;
        }
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency is dictated by the lock in itself.
    /// The XY data channel is a complex one which means that in order to extract the physical data
    /// from the measurement one must have both X and Y data channels. These channels complete one another
    /// and only by having both data sets we can take out the signal itself.
    /// </summary>
    public class LockInXYInternalSourceDataChannel : ComplexDataChannel, IDataChannel
    {
        /// <summary>
        /// The signal from the lock in is alwyas in the range of 0-10V
        /// so the measured signal, Vsig, is always proportinal to the range.
        /// That is: the sensetivity value which is the upper voltage to be measured will be scaled 
        /// and appeared as 10V.
        /// </summary>
        private double m_normalizationFactor;
       
        private static double s_1G0 = 77.5E-6;

        public LockInXYInternalSourceDataChannel(DataConvertorSettings settings)
            : base(settings)
        {
            // Calculate the scaling factor in the voltage 
            // by dividing the maximum of the lock-in, 10V, withe upper limit of the measurement.
            m_normalizationFactor = 10 / settings.Sensitivity;
            Name = "LockInXYInternalSourceDataChannel";
        }

        /// <summary>
        /// Override.
        /// Converts the raw data to physical one.
        /// Since this is a complex data channel we need both data set to retrieve the physical data.
        /// </summary>
        /// <param name="rawVoltageDataX">The X data set</param>
        /// <param name="rawVoltageDataY">The Y data set</param>
        /// <returns></returns>
        internal override IList<double[]> ConvertVoltageToConductanceValue(double[] rawVoltageDataX, double[] rawVoltageDataY)     
        {
            double[] conductanceValues = new double[rawVoltageDataX.Length];
            double[] phaseValues = new double[rawVoltageDataX.Length];
            for (int i = 0; i < rawVoltageDataX.Length; i++)
            {
                double xValue = rawVoltageDataX[i];
                double yValue = rawVoltageDataY[i];
                double phase = Math.Atan(yValue / xValue);

                //
                // Convert from radians to degrees
                //
                phaseValues[i] = phase * 180 / Math.PI;

                //
                // After extracting the phase, the Vsig can be calculated by the X point
                // as X = Vsig * cos(phase).
                //
                conductanceValues[i] = ConvertVoltageToConductanceValue(rawVoltageDataX[i] / Math.Cos(phase));
            }

            List<double[]> convertedData = new List<double[]>();
            convertedData.Add(conductanceValues);
            convertedData.Add(phaseValues);
            PhysicalData = convertedData;
            return convertedData;
        }

        /// <summary>
        /// Convert to physical data
        /// </summary>
        /// <param name="rawVoltageValue"></param>
        /// <returns></returns>
        internal double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            //
            // rawVoltageValue is proportional to ~= (dI/dV)|Vdc * Vac
            //
            return rawVoltageValue / Math.Pow(10, DataConvertionSettings.Gain) / s_1G0 / m_normalizationFactor / DataConvertionSettings.ACVoltage;
        }
    }   

    /// <summary>
    /// This class represents a lock in data channel which is externally referenced.
    /// Externally referenced means that the varying signal to be locked on is not given by the lock in.
    /// R means: R = sqrt(X^2 +Y^2). The sampled lock in can be the R itself (instead of separately sampling X and Y).
    /// Please note that in that case the refresh time of the lock in for that channel is only 512Hz.
    /// Sampling X and Y separatley improve accuracy as these channels are updated more frequently.
    /// </summary>
    public class LockInRExternalSourceChannel : LockInDataChannel, IDataChannel
    {
        public LockInRExternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings) 
        {
            Name = "LockInRExternalSourceChannel";
        }   
    }

    /// <summary>
    /// The general lock in data channel.
    /// </summary>
    public class LockInDataChannel : SimpleDataChannel
    {
        /// <summary>
        /// The signal from the lock in is alwyas in the range of 0-10V
        /// so the measured signal, Vsig, is always proportinal to the range.
        /// That is: the sensetivity value which is the upper voltage to be measured will be scaled 
        /// and appeared as 10V.
        /// </summary>
        private double m_normalizationFactor;

        /// <summary>
        /// RMS to peak-to-peak ratio.
        /// The lock in signal is always an RMS value.
        /// This must be converted to peak-to-peak value if we want the amplitude of our signal.
        /// </summary>
        private static double s_rmsToPPFactor = Math.Sqrt(2);

        private static double s_1G0 = 77.5E-6;

        public LockInDataChannel(string physicalName, DataConvertorSettings settings)
            :base(physicalName, settings)
        {
            // Calculate the scaling factor in the voltage 
            // by dividing the maximum of the lock-in, 10V, withe upper limit of the measurement.
            m_normalizationFactor = (10 / settings.Sensitivity);
        }

        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            return rawVoltageValue / Math.Pow(10, DataConvertionSettings.Gain) / s_1G0 / m_normalizationFactor * s_rmsToPPFactor;
        }
    }

    /// <summary>
    /// Represents the IV data after processing. 
    /// The Physical Data contains the trace constructed by a certain voltage measurements.
    /// The additional data is the I-V cycles and will be saved but not displayed by the UI
    /// </summary>
    public class IVProcessedDataChannel : ComplexDataChannel, IDataChannel
    {
        private static double s_1G0 = 77.5E-6;
        public IVProcessedDataChannel(DataConvertorSettings settings)
            : base(settings)
        {
            Name = "IVProcessedDataChannel";
        }

        //
        // this function sets the right values in physical data and additional data.
        //
        internal override IList<double[]> ConvertVoltageToConductanceValue(double[] junctionData, double[] voltageData)
        {
            //
            // get an instance of the class that has the functions that does all the data processing for the iv measurements.
            //
            IVDataHandle ivDataHandle = new IVDataHandle(junctionData, voltageData, 
                                                         base.DataConvertionSettings.SamplesPerIVCycle, 
                                                         base.DataConvertionSettings.Gain);

            //
            // set AdditionalData to be the IV cycles data
            //
            base.AdditionalData = ivDataHandle.GetIVCycles();

            //
            // set the physical data to be a trace at a certain voltage
            //
            base.PhysicalData = ivDataHandle.GetCertainVoltageTrace(base.DataConvertionSettings.IVTraceVoltage);
            
            return PhysicalData;
        }
    }
    
    /// <summary>
    /// This class represents the IV input monitor channel (reads directly the output voltage)
    /// </summary>
    public class IVInputMonitorChannel : SimpleDataChannel, IDataChannel
    {
        public IVInputMonitorChannel(string PhysicalName, DataConvertorSettings settings)
            : base(PhysicalName, settings)
        {
            Name = "IVInputMonitorChannel";
        }

        //
        // no need to modify the raw data, so just return with it.
        //
        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            return rawVoltageValue;
        }
    }

    /// <summary>
    /// This class represents the IV input data channel
    /// </summary>
    public class IVInputDataChannel : SimpleDataChannel, IDataChannel
    {
        public IVInputDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "IVInputDataChannel";
        }

        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            //
            // calculates the current (not the conductance since the bias was oscilating).
            //
            return rawVoltageValue / Math.Pow(10, base.DataConvertionSettings.Gain);
        }        

    }
    
    /// <summary>
    /// This class represents a simple data channel.
    /// A simple data channel is a channel which its raw data can be independentally convert to
    /// physical data without dependency on any other data source.
    /// </summary>
    public abstract class SimpleDataChannel : DataChannel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical channel name of the board. e.g Dev1\AI0</param>
        /// <param name="settings">General settings required for the conversion of the raw data to physical data</param>
        public SimpleDataChannel(string physicalName, DataConvertorSettings settings)
            :base(physicalName, settings) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical channel name of the board. e.g Dev1\AI0</param>
        /// <param name="name">The virtual name of the channel</param>
        /// <param name="settings">General settings required for the conversion of the raw data to physical data</param>
        public SimpleDataChannel(string physicalName, string name, DataConvertorSettings settings)
            : base(physicalName, name, settings) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical channel name of the board. e.g Dev1\AI0</param>
        /// <param name="name">The virtual name of the channel</param>
        /// <param name="rawData">The raw data aquired in the channel</param>
        /// <param name="settings">General settings required for the conversion of the raw data to physical data</param>
        public SimpleDataChannel(string physicalName, string name, IList<double[]> rawData, DataConvertorSettings settings)
            : base(physicalName, name, rawData, settings) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">The data channle to serve as the source for the creation of this data channel.</param>
        /// <param name="rawData">The raw data aquired in the channel</param>
        /// <param name="settings">General settings required for the conversion of the raw data to physical data</param>
        public SimpleDataChannel(DataChannel channel, IList<double[]> rawData, DataConvertorSettings settings)
            : base(channel, rawData, settings) { }

        /// <summary>
        /// The implementation of the IDataChannel contract
        /// Converts the data channel to physical data
        /// </summary>
        /// <returns></returns>
        public IList<double[]> ConvertToPhysicalData()
        {
            if (RawData == null)
            {
                throw new SBJException("Cannot convert raw data to physical data. Data was not assigned to channel.");
            }

            //
            // Since this is a simple data channel, we are only concerned with the first
            // data set in the RawData collection.
            //
            double[] rawVoltageData = RawData[0];
            double[] conductanceValues = new double[rawVoltageData.Length];
            for (int i = 0; i < rawVoltageData.Length; i++)
            {
                conductanceValues[i] = ConvertVoltageToConductanceValue(rawVoltageData[i]);
            }
            PhysicalData.Clear();
            PhysicalData.Add(conductanceValues);
            return PhysicalData;
        }

        internal abstract double ConvertVoltageToConductanceValue(double p);        
    }

    /// <summary>
    /// This class represents a complex data channle.
    /// A complex data channel is channels which needs at least 2 data sources in order
    /// to conver its raw data to physical meaningful one.
    /// </summary>
    public abstract class ComplexDataChannel : DataChannel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">The general settings required for the conversion from raw data to physical one.</param>
        public ComplexDataChannel(DataConvertorSettings settings)
            : base(string.Empty,settings) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The virtual name of the channel</param>
        /// <param name="settings">The general settings required for the conversion from raw data to physical one.</param>
        public ComplexDataChannel(string name, DataConvertorSettings settings)
            : base(string.Empty, name, settings) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The virtual name of the channel</param>
        /// <param name="rawData">The raw data aquired for this data channel</param>
        /// <param name="settings">The general settings required for the conversion from raw data to physical one.</param>
        public ComplexDataChannel(string name, IList<double[]> rawData, DataConvertorSettings settings)
            : base(string.Empty, name, rawData, settings) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">The data channle to serve as the source for the creation of this data channel.</param>
        /// <param name="rawData">The raw data aquired in the channel</param>
        /// <param name="settings">General settings required for the conversion of the raw data to physical data</param>
        public ComplexDataChannel(DataChannel channel, IList<double[]> rawData, DataConvertorSettings settings)
            : base(channel, rawData, settings) { }

        /// <summary>
        /// Implements IDataChannel contract
        /// </summary>
        /// <returns></returns>
        public IList<double[]> ConvertToPhysicalData()
        {
            if (RawData == null)
            {
                throw new SBJException("Cannot convert raw data to physical data. Data was not assigned to channel.");
            }

            //
            // Since this is a complex data channel, we need both data sets to
            // convert to physical meaningful information.
            //
            double[] rawVoltageFirstDataSet = RawData[0];
            double[] rawVoltageSecondDataSet = RawData[1];

            IList<double[]> convertedData = new List<double[]>();

            //TODO: change the name of this function to something like "GetPhysicalAndAdditionalData"
            convertedData = ConvertVoltageToConductanceValue(RawData[0], RawData[1]);

            PhysicalData = convertedData;
            return PhysicalData;
        }

        internal abstract IList<double[]> ConvertVoltageToConductanceValue(double[] firstRawDataSet, double[] secondRawDataSet);
    }

    /// <summary>
    /// The high hierarchy data channel class and the most general one
    /// </summary>
    public class DataChannel
    {
        /// <summary>
        /// The physical name of the channel as appear on the DAQ card.
        /// E.g: Dev1\A01
        /// </summary>
        public string PhysicalName  {get; set;}

        /// <summary>
        /// The virtual name of the data channel
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The raw data aquired for the data channel
        /// </summary>
        public IList<double[]> RawData { get; set; }

        /// <summary>
        /// The physical data of the channel.
        /// Physical data is the real meaningful data after the conversion of the raw data
        /// </summary>
        public IList<double[]> PhysicalData { get; set; }
        public IList<IList<double[]>> AdditionalData { get; set; }

        /// <summary>
        /// A set of general settings which are required for the conversion processfrom raw data to physical data
        /// </summary>
        public DataConvertorSettings DataConvertionSettings { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical name of the channel as apear on the DAQ card</param>
        /// <param name="settings">DataConvertorSettings</param>
        public DataChannel(string physicalName, DataConvertorSettings settings)
        {
            PhysicalName = physicalName;
            PhysicalData = new List<double[]>();
            RawData = new List<double[]>();
            AdditionalData = new List<IList<double[]>>();
            DataConvertionSettings = settings;
        }

        public DataChannel(string physicalName, string name, DataConvertorSettings settings)
            : this(physicalName, settings)
        {            
            Name = name;
        }


        public DataChannel(string physicalName, string name, IList<double[]> rawData, DataConvertorSettings settings)
            :this(physicalName, name,settings)
        {
            RawData = rawData;
        }

        public DataChannel(DataChannel channel, IList<double[]> rawData, DataConvertorSettings settings)
        {
            PhysicalName = channel.PhysicalName;
            Name = channel.Name;
            RawData = rawData;
            DataConvertionSettings = settings;
            AdditionalData = new List<IList<double[]>>();
        }
    }

    /// <summary>
    /// A repository for general settings required for all data channels types for their conversion process
    /// </summary>
    public class DataConvertorSettings
    {
        private double m_bias;
        private int m_gain;
        private double m_acVoltage;
        private double m_sensitivity;
        private int m_samplesPerIVCycle;
        private double m_ivTraceVoltage;

        public double Bias
        {
            get { return m_bias; }
        }

        public int Gain
        {
            get { return m_gain; }
        }

        public double ACVoltage
        {
            get { return m_acVoltage; }
        }

        public double Sensitivity
        {
            get { return m_sensitivity; }
        }

        public int SamplesPerIVCycle
        {
            get { return m_samplesPerIVCycle; }
        }

        public double IVTraceVoltage
        {
            get { return m_ivTraceVoltage; }
        }

        public DataConvertorSettings(double bias, int gain, double acVoltage, double sensitivity, int samplesPerIVCycle, double ivTraceVoltage)
        {
            m_acVoltage = acVoltage;
            m_bias = bias;
            m_gain = gain;
            m_sensitivity = sensitivity;
            m_samplesPerIVCycle = samplesPerIVCycle;
            m_ivTraceVoltage = ivTraceVoltage;
        }
    }
}
