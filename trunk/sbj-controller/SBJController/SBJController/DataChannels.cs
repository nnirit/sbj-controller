using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    public class DefaultDataChannel : SimpleDataChannel, IDataChannel
    {
        private double m_bias;
        private int m_gain;
        private const double c_1G0 = 77.5E-6;

        public DefaultDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            m_bias = settings.Bias;
            m_gain = settings.Gain;
            Name = "DefaultDataChannel";
        }

        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            return Math.Abs(rawVoltageValue) / Math.Pow(10, m_gain) / m_bias / c_1G0;
        }       
    }

    public class LockInXInternalSourceDataChannel : LockInInternalSourceDataChannel, IDataChannel
    {
        public LockInXInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInXInternalSourceDataChannel";
        }
    }

    public class LockInYInternalSourceDataChannel : LockInInternalSourceDataChannel, IDataChannel
    {
        public LockInYInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInYInternalSourceDataChannel";
        }
    }

    public class LockInRInternalSourceChannel : LockInInternalSourceDataChannel, IDataChannel
    {
        public LockInRInternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            Name = "LockInRInternalSourceChannel";
        }
    }

    public class LockInInternalSourceDataChannel : LockInDataChannel
    {
        private double m_acVoltage;
        private static double s_rmsToPPFactor = Math.Sqrt(2);

        public LockInInternalSourceDataChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings)
        {
            m_acVoltage = settings.ACVoltage;
        }

        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            return base.ConvertVoltageToConductanceValue(rawVoltageValue) / m_acVoltage;
        }
    }

    public class LockInXYInternalSourceDataChannel : ComplexDataChannel, IDataChannel
    {
        private double m_normalizationFactor;
        private int m_gain;
        private static double s_1G0 = 77.5E-6;
        private static double s_rmsToPPFactor = Math.Sqrt(2);
        private double m_acVoltage;

        public LockInXYInternalSourceDataChannel(DataConvertorSettings settings)
            : base(settings)
        {
            m_acVoltage = settings.ACVoltage;
            m_gain = settings.Gain;
            m_normalizationFactor = 10 / settings.Sensitivity;
            Name = "LockInXYInternalSourceDataChannel";
        }
        internal override IList<double[]> ConvertVoltageToConductanceValue(double[] rawVoltageDataX, double[] rawVoltageDataY)     
        {
            double[] conductanceValues = new double[rawVoltageDataX.Length];
            double[] phaseValues = new double[rawVoltageDataX.Length];
            for (int i = 0; i < rawVoltageDataX.Length; i++)
            {
                double xValue = rawVoltageDataX[i];
                double yValue = rawVoltageDataY[i];
                double phase = Math.Atan(yValue / xValue);
                phaseValues[i] = phase * 180 / Math.PI;
                conductanceValues[i] = ConvertVoltageToConductanceValue(rawVoltageDataX[i] / Math.Cos(phase));
            }
            List<double[]> convertedData = new List<double[]>();
            convertedData.Add(conductanceValues);
            //convertedData.Add(phaseValues);
            PhysicalData = convertedData;
            return convertedData;
        }

        internal double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            return rawVoltageValue / Math.Pow(10, m_gain) / s_1G0 / m_normalizationFactor / m_acVoltage;
        }
    }   

    public class LockInRExternalSourceChannel : LockInDataChannel, IDataChannel
    {
        public LockInRExternalSourceChannel(string physicalName, DataConvertorSettings settings)
            : base(physicalName, settings) 
        {
            Name = "LockInRExternalSourceChannel";
        }   
    }

    public class LockInDataChannel : SimpleDataChannel
    {
        private double m_normalizationFactor;
        private int m_gain;
        private static double s_1G0 = 77.5E-6;
        private static double s_rmsToPPFactor = Math.Sqrt(2);

        public LockInDataChannel(string physicalName, DataConvertorSettings settings)
            :base(physicalName, settings)
        {
            m_gain = settings.Gain;
            m_normalizationFactor = (10 / settings.Sensitivity);
        }

        internal override double ConvertVoltageToConductanceValue(double rawVoltageValue)
        {
            return rawVoltageValue / Math.Pow(10, m_gain) / s_1G0 / m_normalizationFactor * s_rmsToPPFactor;
        }
    }
    
    public abstract class SimpleDataChannel : DataChannel
    {
        public SimpleDataChannel(string physicalName, DataConvertorSettings settings)
            :base(physicalName, settings) {}

        public SimpleDataChannel(string physicalName, string name, DataConvertorSettings settings)
            : base(physicalName, name, settings) { }

        public SimpleDataChannel(string physicalName, string name, IList<double[]> rawData, DataConvertorSettings settings)
            : base(physicalName, name, rawData, settings) { }

        public SimpleDataChannel(DataChannel channel, IList<double[]> rawData, DataConvertorSettings settings)
            : base(channel, rawData, settings) { }

        public IList<double[]> ConvertToPhysicalData()
        {
            if (RawData == null)
            {
                throw new SBJException("Cannot convert raw data to physical data. Data was not assigned to channel.");
            }

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

    public abstract class ComplexDataChannel : DataChannel
    {
        public ComplexDataChannel(DataConvertorSettings settings)
            : base(string.Empty,settings) { }

        public ComplexDataChannel(string name, DataConvertorSettings settings)
            : base(string.Empty, name, settings) { }

        public ComplexDataChannel(string name, IList<double[]> rawData, DataConvertorSettings settings)
            : base(string.Empty, name, rawData, settings) { }

        public ComplexDataChannel(DataChannel channel, IList<double[]> rawData, DataConvertorSettings settings)
            : base(channel, rawData, settings) { }

        public IList<double[]> ConvertToPhysicalData()
        {
            if (RawData == null)
            {
                throw new SBJException("Cannot convert raw data to physical data. Data was not assigned to channel.");
            }

            double[] rawVoltageFirstDataSet = RawData[0];
            double[] rawVoltageSecondDataSet = RawData[1];

            IList<double[]> convertedData = new List<double[]>();


            convertedData = ConvertVoltageToConductanceValue(RawData[0], RawData[1]);

            PhysicalData = convertedData;
            return PhysicalData;
        }

        internal abstract IList<double[]> ConvertVoltageToConductanceValue(double[] firstRawDataSet, double[] secondRawDataSet);
    }

    public class DataChannel
    {
        public string PhysicalName  {get; set;}
        public string Name { get; set; }
        public IList<double[]> RawData { get; set; }
        public IList<double[]> PhysicalData { get; set; }
        public DataConvertorSettings DataConvertionSettings { get; set; }


        public DataChannel(string physicalName, DataConvertorSettings settings)
        {
            PhysicalName = physicalName;
            PhysicalData = new List<double[]>();
            RawData = new List<double[]>();
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
        }
    }

    public class DataConvertorSettings
    {
        private double m_bias;
        private int m_gain;
        private double m_acVoltage;
        private double m_sensitivity;

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

        public DataConvertorSettings(double bias, int gain, double acVoltage, double sensitivity)
        {
            m_acVoltage = acVoltage;
            m_bias = bias;
            m_gain = gain;
            m_sensitivity = sensitivity;
        }
    }
}
