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
    [DAQAttribute()]
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
        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            if (DataConvertionSettings.Bias > 0)
            {
                return Math.Abs(rawVoltageValue) / Math.Pow(10, DataConvertionSettings.Gain) / DataConvertionSettings.Bias / c_1G0;
            }
            else
            {
                //
                // We will never get here since DataConvertionSettings.Bias always takes the abs value.
                // See 'GetActiveChannels()' Method.
                //
                return rawVoltageValue;
            }
        }       
    }

    /// <summary>
    /// Photodetector data channel.
    /// This channel is used to sample data points fron the silicon diode.
    /// </summary>
    [DAQAttribute()]
    public class PhotodetectorDataChannel : SimpleDataChannel, IDataChannel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical channels on the PCI board. e.g: Dev1\AI0</param>
        /// <param name="settings"></param>
        public PhotodetectorDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "PhotodetectorDataChannel";
        }

        /// <summary>
        /// Override. 
        /// Converts the voltage to intensity units.
        /// TODO: Add conversion.
        /// </summary>
        /// <param name="rawVoltageValue"></param>
        /// <returns></returns>
        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            return rawVoltageValue;
        }
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency id dictated by the lock in itself.
    /// X equals: X = Vsig*cos(theta), where theta is the phase of the lockin and Vsig is proportional to the 
    /// physical signal from the measurement unit and it is the signal to be interperted.
    /// </summary>
    [DAQAttribute()]
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
    [DAQAttribute()]
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
    [DAQAttribute()]
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
        private static double s_1G0 = 77.5E-6;
        private static double s_rmsToPPFactor = Math.Sqrt(2);

        public LockInInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings) {}

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="rawVoltageValue">One raw data point to be converted to physicak data.</param>
        /// <returns>The physical data representation of the input data point.</returns>
        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            //
            // When the lock in is internally referenced, the measured signal is in the form of:
            // Vsig ~= (dI/dV)|Vdc  * Vac.
            // We must divide in the ac voltage applied internally from the lock - in in order to extract the
            // the physical data. We also change to G0 units.
            // Since the AC voltage from the mixer is also rms, we need to convert it to p-p.
            //
            return base.ConvertVoltageToPhysicalValue(rawVoltageValue) / (DataConvertionSettings.ACVoltage * s_rmsToPPFactor) / s_1G0;
        }
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency is dictated by the lock in itself.
    /// The XY data channel is a complex one which means that in order to extract the physical data
    /// from the measurement one must have both X and Y data channels. These channels complete one another
    /// and only by having both data sets we can take out the signal itself.
    /// </summary>
    [DAQAttribute()]
    public class LockInXYInternalSourceDataChannel : LockInXYSourceDataChannel, IDataChannel
    {
        private static double s_1G0 = 77.5E-6;
        private static double s_rmsToPPFactor = Math.Sqrt(2);

        public LockInXYInternalSourceDataChannel(DataConvertorSettings settings)
            : base(settings)
        {           
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
        internal override IList<double[]> ConvertVoltageToPhysicalValue(double[] rawVoltageDataX, double[] rawVoltageDataY)     
        {
            IList<double[]> data = GetData(rawVoltageDataX, rawVoltageDataY);
            double[] normalizedCurrentValues = data[0];
            double[] phaseValues = data[1];
            double[] normalizedConductanceValues = new double[normalizedCurrentValues.Length];
            for (int i = 0; i < normalizedCurrentValues.Length; i++)
            {
                normalizedConductanceValues[i] = ConvertVoltageToConductanceValue(normalizedCurrentValues[i]);
            }

            List<double[]> convertedData = new List<double[]>();
            convertedData.Add(normalizedConductanceValues);
            convertedData.Add(phaseValues);
            PhysicalData = convertedData;
            return convertedData;
        }

        /// <summary>
        /// Convert to physical data
        /// </summary>
        /// <param name="rawVoltageValue"></param>
        /// <returns></returns>
        internal double ConvertVoltageToConductanceValue(double currentValue)
        {
            return currentValue / (DataConvertionSettings.ACVoltage * s_rmsToPPFactor)/ s_1G0;
        }
    }

    [DAQAttribute()]
    public class LockInXYExtrenalSourceDataChannel : LockInXYSourceDataChannel, IDataChannel
    {
        private static double s_1G0 = 77.5E-6;

        public LockInXYExtrenalSourceDataChannel(DataConvertorSettings settings)
            : base(settings)
        {
            Name = "LockInXYExtrenalSourceDataChannel";
        }

        /// <summary>
        /// Override.
        /// Converts the raw data to physical one.
        /// Since this is a complex data channel we need both data set to retrieve the physical data.
        /// </summary>
        /// <param name="rawVoltageDataX">The X data set</param>
        /// <param name="rawVoltageDataY">The Y data set</param>
        /// <returns></returns>
        internal override IList<double[]> ConvertVoltageToPhysicalValue(double[] rawVoltageDataX, double[] rawVoltageDataY)
        {
            IList<double[]> data = GetData(rawVoltageDataX, rawVoltageDataY);
            double[] normalizedCurrentValues = data[0];
            double[] phaseValues = data[1];
            double[] normalizedConductanceValues = new double[normalizedCurrentValues.Length];
            for (int i = 0; i < normalizedCurrentValues.Length; i++)
            {
                normalizedConductanceValues[i] = ConvertVoltageToConductanceValue(normalizedCurrentValues[i]);
            }

            List<double[]> convertedData = new List<double[]>();
            convertedData.Add(normalizedConductanceValues);
            convertedData.Add(phaseValues);
            PhysicalData = convertedData;
            return convertedData;
        }

        /// <summary>
        /// Convert to physical data
        /// </summary>
        /// <param name="rawVoltageValue"></param>
        /// <returns></returns>
        internal double ConvertVoltageToConductanceValue(double currentValue)
        {
            return currentValue / DataConvertionSettings.Bias / s_1G0;
        }
    }   

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency is dictated by the lock in itself.
    /// The XY data channel is a complex one which means that in order to extract the physical data
    /// from the measurement one must have both X and Y data channels. These channels complete one another
    /// and only by having both data sets we can take out the signal itself.
    /// </summary>
    public abstract class LockInXYSourceDataChannel : ComplexDataChannel
    {
        /// <summary>
        /// The signal from the lock in is alwyas in the range of 0-10V
        /// so the measured signal, Vsig, is always proportinal to the range.
        /// That is: the sensetivity value which is the upper voltage to be measured will be scaled 
        /// and appeared as 10V.
        /// </summary>
        private double m_normalizationFactor;
        private static double s_rmsToPPFactor = Math.Sqrt(2);

        public LockInXYSourceDataChannel(DataConvertorSettings settings)
            : base(settings)
        {
            // Calculate the scaling factor in the voltage 
            // by dividing the maximum of the lock-in, 10V, withe upper limit of the measurement.
            m_normalizationFactor = 10 / settings.Sensitivity;
            Name = "LockInXYSourceDataChannel";
        }

        /// <summary>
        /// Override.
        /// Converts the raw data to physical one.
        /// Since this is a complex data channel we need both data set to retrieve the physical data.
        /// </summary>
        /// <param name="rawVoltageDataX">The X data set</param>
        /// <param name="rawVoltageDataY">The Y data set</param>
        /// <returns></returns>
        internal IList<double[]> GetData(double[] rawVoltageDataX, double[] rawVoltageDataY)
        {
            double[] normalizedCurrentValues = new double[rawVoltageDataX.Length];
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
                // We also normalize the result according to the sensitivity factor and change from rms ro peak-peak.
                // The result is current units since we divide in the gain.
                //
                normalizedCurrentValues[i] = rawVoltageDataX[i] / Math.Cos(phase) / Math.Pow(10, DataConvertionSettings.Gain) / m_normalizationFactor * s_rmsToPPFactor;
            }

            List<double[]> convertedData = new List<double[]>();
            convertedData.Add(normalizedCurrentValues);
            convertedData.Add(phaseValues);
            return convertedData;
        }
        internal override abstract IList<double[]> ConvertVoltageToPhysicalValue(double[] firstRawDataSet, double[] secondRawDataSet);
    }

    /// <summary>
    /// This class represents the data channel aquired from the lock in when it is internal referenced.
    /// Internal referenced means that the reference frequency is dictated by the lock in itself.
    /// </summary>
    public class LockInExternalSourceDataChannel : LockInDataChannel
    {
        private static double s_1G0 = 77.5E-6;

        public LockInExternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings) { }

        /// <summary>
        /// Override.
        /// </summary>
        /// <param name="rawVoltageValue">One raw data point to be converted to physicak data.</param>
        /// <returns>The physical data representation of the input data point.</returns>
        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            //
            // When the lock in is externally referenced we measure a signal in [V].
            // In order to convert it to conductance units we first converts it to normalized (sensitivity + rms)
            // current units [I] and then need to divide with the bias and get the units in G0.
     
            return base.ConvertVoltageToPhysicalValue(rawVoltageValue) / DataConvertionSettings.Bias / s_1G0;
        }
    }

    /// <summary>
    /// This class represents a lock in data channel which is externally referenced.
    /// Externally referenced means that the varying signal to be locked on is not given by the lock in.
    /// R means: R = sqrt(X^2 +Y^2). The sampled lock in can be the R itself (instead of separately sampling X and Y).
    /// Please note that in that case the refresh time of the lock in for that channel is only 512Hz.
    /// Sampling X and Y separatley improve accuracy as these channels are updated more frequently.
    /// </summary>
    [DAQAttribute()]
    public class LockInRExternalSourceChannel : LockInExternalSourceDataChannel, IDataChannel
    {
        public LockInRExternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings) 
        {
            Name = "LockInRExternalSourceChannel";
        }   
    }

    [DAQAttribute()]
    public class LockInXExternalSourceChannel : LockInExternalSourceDataChannel, IDataChannel
    {
        public LockInXExternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInXExternalSourceChannel";
        }
    }

    [DAQAttribute()]
    public class LockInYExternalSourceChannel : LockInExternalSourceDataChannel, IDataChannel
    {
        public LockInYExternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInYExternalSourceChannel";
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

        public LockInDataChannel(string physicalName, DataConvertorSettings settings)
            :base(physicalName, settings)
        {
            // Calculate the scaling factor in the voltage 
            // by dividing the maximum of the lock-in, 10V, withe upper limit of the measurement.
            m_normalizationFactor = (10 / settings.Sensitivity);
        }

        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            //
            // We convert it to  normalized current [I] units:
            // Divide with the gain results current and then we normalized it by the sensitivity factor and the rms.
            //
            return rawVoltageValue / Math.Pow(10, DataConvertionSettings.Gain) / m_normalizationFactor * s_rmsToPPFactor;
        }
    }

    /// <summary>
    /// Represents the IV data after processing. 
    /// The Physical Data contains the trace constructed by a certain voltage measurements.
    /// The additional data is the I-V cycles and will be saved but not displayed by the UI
    /// </summary>
    [IVAttribute()]
    public class IVProcessedDataChannel : ComplexDataChannel, IDataChannel
    {
        public IVProcessedDataChannel(DataConvertorSettings settings)
            : base(settings)
        {
            Name = "IVProcessedDataChannel";
        }

        //
        // this function sets the right values in physical data and additional data.
        //
        internal override IList<double[]> ConvertVoltageToPhysicalValue(double[] junctionData, double[] voltageData)
        {
            //
            // get an instance of the class that has the functions that does all the data processing for the iv measurements.
            //
            IVDataHandle ivDataHandle = new IVDataHandle(junctionData, voltageData, 
                                                         base.DataConvertionSettings.SamplesPerIVCycle, 
                                                         base.DataConvertionSettings.Gain);

            //
            // set AdditionalData to be the IV cycles data
            // If we are in static mode (no stpper movement) then 
            // we allow IV to be taken even at over load.
            // If IVs are taken for each trace we would like to ignore the overload level.
            //
            bool ignoreOverload = !base.DataConvertionSettings.IsStaticIV;
            base.AdditionalData = ivDataHandle.GetIVCycles(ignoreOverload);
           
            if (!base.DataConvertionSettings.IsStaticIV)
            {
                //
                // set the physical data to be a trace at a certain voltage
                //
                base.PhysicalData = ivDataHandle.GetCertainVoltageTrace(base.DataConvertionSettings.Bias);
            }
            else
            {
                //
                // If the code got here then we are in static mode IV
                // and we only set the physical data to be the first set of results from the additional data.
                //
                base.PhysicalData = this.AdditionalData.First();
            }
            
            return PhysicalData;
        }
    }
    
    /// <summary>
    /// This class represents the IV input monitor channel (reads directly the output voltage)
    /// </summary>
    [IVAttribute(), DAQAttribute()]
    public class InputVoltageMonitorChannel : SimpleDataChannel, IDataChannel
    {
        public InputVoltageMonitorChannel(string PhysicalName, DataConvertorSettings settings)
            : base(PhysicalName, settings)
        {
            Name = "InputVoltageMonitorChannel";
        }

        //
        // no need to modify the raw data, so just return with it.
        //
        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            return rawVoltageValue;
        }
    }

    /// <summary>
    /// This class represents the IV input data channel
    /// </summary>
    [IVAttribute()]
    public class IVInputDataChannel : SimpleDataChannel, IDataChannel
    {
        public IVInputDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "IVInputDataChannel";
        }

        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
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
            // If RawData has more than one data set, then append them all.
            List<double> flatList = new List<double>();
            for (int i = 0; i < RawData.Count; i++)
            {
                flatList.AddRange(RawData[i]);
            }
            double[] conductanceValues = new double[flatList.Count];
            for (int i = 0; i < flatList.Count; i++)
            {
                conductanceValues[i] = ConvertVoltageToPhysicalValue(flatList[i]);
            }
            PhysicalData.Clear();
            PhysicalData.Add(conductanceValues);
            return PhysicalData;
        }

        internal abstract double ConvertVoltageToPhysicalValue(double p);        
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

            convertedData = ConvertVoltageToPhysicalValue(RawData[0], RawData[1]);

            PhysicalData = convertedData;
            return PhysicalData;
        }

        internal abstract IList<double[]> ConvertVoltageToPhysicalValue(double[] firstRawDataSet, double[] secondRawDataSet);
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
        private bool m_isStaticIV;

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

        /// <summary>
        /// Specify whether the IV measurement is taken without moving the motor (static)
        /// Or (when it is set to false) the IV is taken while aquiring traces.
        /// </summary>
        public bool IsStaticIV
        {
            get { return m_isStaticIV; }
        }

        public DataConvertorSettings(double bias, int gain, double acVoltage, double sensitivity, int samplesPerIVCycle, bool isStaticIV)
        {
            m_acVoltage = acVoltage;
            m_bias = bias;
            m_gain = gain;
            m_sensitivity = sensitivity;
            m_samplesPerIVCycle = samplesPerIVCycle;
            m_isStaticIV = isStaticIV;
        }
    }

    [CalibrationAttribute()]
    public class CalibrationDataChannel : SimpleDataChannel, IDataChannel
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="physicalName">The physical channels on the PCI board. e.g: Dev1\AI0</param>
        /// <param name="settings"></param>
        public CalibrationDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "CalibrationDataChannel";
        }

        internal override double ConvertVoltageToPhysicalValue(double rawVoltageValue)
        {
            return Math.Abs(rawVoltageValue) / Math.Pow(10, DataConvertionSettings.Gain);
        }
    }

    /// <summary>
    /// This class represents 2 lists of channel types, one that holds only simple channel types and one for complex channel types. 
    /// </summary>
    public class ChannelTypeLists
    {
        public List<string> Simple { get; set; }
        public List<string> Complex { get; set; }

        public ChannelTypeLists(List<string> simpleChannelTypes, List<string> complexChannelTypes)
        {
            Simple = simpleChannelTypes;
            Complex = complexChannelTypes;
        }
    }
}
