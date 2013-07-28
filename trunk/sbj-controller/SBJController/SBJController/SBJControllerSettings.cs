
using System.Collections.Generic;
using System.Text;
using System;
namespace SBJController
{
    /// <summary>
    /// This class represents all the required data memebers of the UI
    /// </summary>
    public class SBJControllerSettings
    {
        public GeneralSBJControllerSettings GeneralSettings { get; set; }
        public LaserSBJControllerSettings LaserSettings { get; set; }
        public LockInSBJControllerSettings LockInSettings { get; set; }
        public ElectroMagnetSBJControllerSettings ElectromagnetSettings { get; set; }
        public LambdaZupSBJControllerSettings LambdaZupSettings { get; set; }
        public ChannelsSettings ChannelsSettings { get; set; }
        
        public SBJControllerSettings(GeneralSBJControllerSettings generalSettings,
                                     LaserSBJControllerSettings laserSettings,
                                     LockInSBJControllerSettings lockInSettings,
                                     ElectroMagnetSBJControllerSettings electromagnetSettings,
                                     LambdaZupSBJControllerSettings lambdaZupSettings,
                                     ChannelsSettings channelsSettings)
        {
            GeneralSettings = generalSettings;
            LaserSettings = laserSettings;
            LockInSettings = lockInSettings;
            ElectromagnetSettings = electromagnetSettings;
            LambdaZupSettings = lambdaZupSettings;     
            ChannelsSettings = channelsSettings;
        }
    }

    /// <summary>
    /// This class represents the general settinngs in the UI
    /// </summary>
    public class GeneralSBJControllerSettings 
    {
        public double Bias { get; set; }
        public double BiasError { get; set; }
        public string Gain { get; set; }
        public double TriggerVoltage { get; set; }
        public double TriggerConductance { get; set; }
        public int SampleRate { get; set; }
        public int TotalSamples { get; set; }
        public int PretriggerSamples { get; set; }
        public int StepperWaitTime1 { get; set; }
        public int StepperWaitTime2 { get; set; }
        public bool IsFileSavingRequired { get; set; }
        public bool UseKeithley { get; set; }
        public string Path { get; set; }
        public int CurrentFileNumber { get; set; }
        public int TotalNumberOfCycles { get; set; }
        public double ShortCircuitVoltage { get; set; }
        public Sample Bottom { get; set; }
        public Sample Top { get; set; }

        public GeneralSBJControllerSettings (double bias, double biasError, string gain, double triggerVoltage,
                                     double triggerConductance, int sampleRate,
                                     int totalSamples, int pretriggerSamples,
                                     int stepperWaitTime1, int stepperWaitTime2, bool isFileSavingRequired, 
                                     bool useKeithley, string path, int currentFileNumber, int totalNUmberOfCycles, 
                                     double shourtCircuitVoltage,Sample bottom, Sample top)
        {
            Bias = bias;
            BiasError = biasError;
            Gain = gain;
            TriggerConductance = triggerConductance;
            TriggerVoltage = triggerVoltage;
            SampleRate = sampleRate;
            TotalSamples = totalSamples;
            PretriggerSamples = pretriggerSamples;
            StepperWaitTime1 = stepperWaitTime1;
            StepperWaitTime2 = stepperWaitTime2;
            IsFileSavingRequired = isFileSavingRequired;
            UseKeithley = useKeithley;
            Path = path;
            CurrentFileNumber = currentFileNumber;
            TotalNumberOfCycles = totalNUmberOfCycles;
            ShortCircuitVoltage = shourtCircuitVoltage;
            Bottom = bottom;
            Top = top;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("General Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString() + Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }

    /// <summary>
    /// This class represents the laser settinngs in the UI
    /// </summary>
    public class LaserSBJControllerSettings
    {
        public bool IsLaserOn { get; set; }
        public bool IsFirstEOMOn { get; set; }
        public bool IsSecondEOMOn { get; set; }
        public bool IsChopperOn { get; set; }
        public string LaserMode { get; set; }
        public double LaserAmplitudeVolts { get; set; }
        public double LaserAmplitudeMiliWatt { get; set; }
        public double LaserAmplitudeOnSample { get; set; }
        public int LaserFrequency { get; set; }
        public int ExternalModulationFrequency { get; set; }
        public int FirstEOMFrequency { get; set; }
        public int SecondEOMFrequency { get; set; }
        public string EOMConfiguration { get; set; }

        public LaserSBJControllerSettings(bool isLaserOn, string laserMode, double laserAmplitudeVolts, double laserAmplitudeMiliWatt,
            double laserAmplitudeOnSample, int laserFrequency, bool isFirstEomON, bool isSecondEomON, bool isChopperON, string eomConfiguration, 
            int externalModulationFrequency, int firstEOMFrequency, int secondEOMFrequency)
        {
            IsLaserOn = isLaserOn;
            LaserMode = laserMode;
            LaserAmplitudeVolts = laserAmplitudeVolts;
            LaserAmplitudeMiliWatt = laserAmplitudeMiliWatt;
            LaserAmplitudeOnSample = laserAmplitudeOnSample;
            LaserFrequency = laserFrequency;
            IsFirstEOMOn = isFirstEomON;
            IsSecondEOMOn = isSecondEomON;
            EOMConfiguration = eomConfiguration;
            IsChopperOn = isChopperON;
            ExternalModulationFrequency = externalModulationFrequency;
            FirstEOMFrequency = firstEOMFrequency;
            SecondEOMFrequency = secondEOMFrequency;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("Laser Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString() + Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }

    /// <summary>
    /// This class represents the general lockin in the UI
    /// </summary>
    public class LockInSBJControllerSettings
    {
        public bool IsLockInSignalEnable { get; set; }
        public bool IsLockInInternalEnable { get; set; }
        public double LockInSensitivity { get; set; }
        public double LockInTimeConstant { get; set; }
        public double LockInRollOff { get; set; }
        public double LockInAcVoltage { get; set; }
        public int LockInMixerReductionFactor { get; set; }


        public LockInSBJControllerSettings(bool isLockInSignalEnable, bool isLockInInternalEnable, double lockInSensitivity, double lockInTimeConstant, double lockInRollOff, double lockInAcVoltage, int lockInMixerReductionFactor)
        {
            IsLockInSignalEnable = isLockInSignalEnable;
            IsLockInInternalEnable = isLockInInternalEnable;
            LockInSensitivity = lockInSensitivity;
            LockInAcVoltage = lockInAcVoltage;
            LockInTimeConstant = lockInTimeConstant;
            LockInRollOff = lockInRollOff;
            LockInMixerReductionFactor = lockInMixerReductionFactor;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("LockIn Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString() + Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }

    /// <summary>
    /// This class represents the electromagnet settinngs in the UI
    /// </summary>
    public class ElectroMagnetSBJControllerSettings
    {
        public bool IsEMEnable { get; set; }
        public int EMShortCircuitDelayTime { get; set; }
        public int EMFastDelayTime { get; set; }
        public int EMSlowDelayTime { get; set; }
        public bool IsEMHoldOnEnable { get; set; }
        public double EMHoldOnMaxConductance { get; set; }
        public double EMHoldOnMaxVoltage { get; set; }
        public double EMHoldOnMinConductance { get; set; }
        public double EMHoldOnMinVoltage { get; set; }
        public bool IsEMSkipFirstCycleEnable { get; set; }

        public ElectroMagnetSBJControllerSettings (bool isEMEnable, int emShortCircuitDelayTime, 
                                                   int emFastDelayTime, int emSlowDelayTime, 
                                                   bool isEMHoldOnEnable, double emHoldOnMaxConductance, 
                                                   double emHoldOnMaxVoltage, double emHoldOnMinConductance, 
                                                   double emHoldOnMinVoltage, bool isEMSkipFirstCycleEnable)
        {
            IsEMEnable = isEMEnable;
            EMShortCircuitDelayTime = emShortCircuitDelayTime;
            EMFastDelayTime = emFastDelayTime;
            EMSlowDelayTime = emSlowDelayTime;
            IsEMHoldOnEnable = isEMHoldOnEnable;
            EMHoldOnMaxConductance = emHoldOnMaxConductance;
            EMHoldOnMaxVoltage = emHoldOnMaxVoltage;
            EMHoldOnMinConductance = emHoldOnMinConductance;
            EMHoldOnMinVoltage = emHoldOnMinVoltage;
            IsEMSkipFirstCycleEnable = isEMSkipFirstCycleEnable;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("Electromagnet Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString()+ Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }

    /// <summary>
    /// This class represents the general lockin in the UI
    /// </summary>
    public class LambdaZupSBJControllerSettings
    {
        public bool IsLambdaZupEnable { get; set; }
        public double OutputVoltage { get; set; }


        public LambdaZupSBJControllerSettings(bool isLambdaZupEnable, double outputVoltage)
        {
            IsLambdaZupEnable = isLambdaZupEnable;
            OutputVoltage = outputVoltage;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("LambdaZup Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString() + Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }

    /// <summary>
    /// This class represents the channels settinngs in the UI
    /// </summary>
    public class ChannelsSettings
    {
        private IList<IDataChannel> m_activeChannels;

        public IList<IDataChannel> ActiveChannels
        {
            get { return m_activeChannels; }
        }

        public ChannelsSettings(IList<IDataChannel> channels)
        {
            m_activeChannels = channels;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("Channels Settings:" + Environment.NewLine);
            foreach (var dataChannel in ActiveChannels)
            {
                toStringResult.Append(dataChannel.PhysicalName + ":\t" + dataChannel.Name + Environment.NewLine);                
            }
            return toStringResult.ToString();
        }
    }

    #region IV
    /// <summary>
    /// This class represents the IV settings in the UI
    /// </summary>
    public class IVSettings
    {
        public IVGeneralSettings IVGeneralSettings { get; set; }
        public IVSteppingMethodSettings IVSteppingMethodSettings { get; set; }
        public ChannelsSettings ChannelsSettings { get; set; }

        public IVSettings(IVGeneralSettings ivGeneralSettings,
                            IVSteppingMethodSettings ivSteppingMethodSettings,
                            ChannelsSettings channelsSettings)
        {
            IVGeneralSettings = ivGeneralSettings;
            IVSteppingMethodSettings = ivSteppingMethodSettings;                        
            ChannelsSettings = channelsSettings;
        }
    }
  
    /// <summary>
    /// This class combines all the general settings on the IV tab
    /// </summary>
    public class IVGeneralSettings 
    {
        public double VoltageAmplitude { get; set; }
        public int SamplesPerCycle { get; set; }
        public double OutputUpdateDelay { get; set; }
        public int OutputUpdateRate { get; set; }
        public double VoltageForDisplayedTrace { get; set; }
        public double TimeForOneIVCycle { get; set; }
        public string Gain { get; set; }
        public double TriggerVoltage { get; set; }
        public double TriggerConductance { get; set; }
        public int SampleRate { get; set; }
        public bool IsFileSavingRequired { get; set; }
        public string Path { get; set; }
        public int CurrentFileNumber { get; set; }
        public int TotalNumberOfCycles { get; set; }
        public double ShortCircuitVoltage { get; set; }
        public Sample Bottom { get; set; }
        public Sample Top { get; set; }

        public IVGeneralSettings (double voltageAmplitude, int samplesPerCycle, 
                                     double outputUpdateDelay, int outputUpdateRate, 
                                     double voltageForDisplayedTrace, double timeForOneIVCycle,
                                     string gain, double triggerVoltage,
                                     double triggerConductance, int sampleRate, bool isFileSavingRequired, 
                                     string path, int currentFileNumber, int totalNUmberOfCycles, 
                                     double shortCircuitVoltage,Sample bottom, Sample top)
        {
            VoltageAmplitude = voltageAmplitude;
            SamplesPerCycle = samplesPerCycle;
            OutputUpdateDelay = outputUpdateDelay;
            OutputUpdateRate = outputUpdateRate;
            VoltageForDisplayedTrace = voltageForDisplayedTrace;
            TimeForOneIVCycle = timeForOneIVCycle;
            Gain = gain;
            TriggerConductance = triggerConductance;
            TriggerVoltage = triggerVoltage;
            SampleRate = sampleRate;
            IsFileSavingRequired = isFileSavingRequired;
            Path = path;
            CurrentFileNumber = currentFileNumber;
            TotalNumberOfCycles = totalNUmberOfCycles;
            ShortCircuitVoltage = shortCircuitVoltage;
            Bottom = bottom;
            Top = top;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("IV General Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString() + Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }

    /// <summary>
    /// This class combines the settings of the stepping method on the IV tab
    /// </summary>
    public class IVSteppingMethodSettings
    {
        public SteppingDevice SteppingDevice { get; set; }
        public int StepperMotorWaitTime1 { get; set; }
        public int StepperMotorWaitTime2 { get; set; }
        public int EMShortCircuitDelayTime { get; set; }
        public int EMFastDelayTime { get; set; }
        public int EMSlowDelayTime { get; set; }
        public bool IsEMSkipFirstCycleEnable { get; set; }

        public IVSteppingMethodSettings(SteppingDevice steppingDevice, 
                                            int stepperMotorWaitTime1, int stepperMotorWaitTime2, 
                                            int emShortCircuitDelayTime, int emFastDelayTime, 
                                            int emSlowDelayTime, bool isEMSkipFirstCycleEnable)
        {
            SteppingDevice = steppingDevice;
            StepperMotorWaitTime1 = stepperMotorWaitTime1;
            StepperMotorWaitTime2 = stepperMotorWaitTime2; 
            EMShortCircuitDelayTime = emShortCircuitDelayTime;
            EMFastDelayTime = emFastDelayTime;
            EMSlowDelayTime = emSlowDelayTime;
            IsEMSkipFirstCycleEnable = isEMSkipFirstCycleEnable;
        }

        public override string ToString()
        {
            StringBuilder toStringResult = new StringBuilder("IV Stepping Method Settings:" + Environment.NewLine);
            foreach (var property in this.GetType().GetProperties())
            {
                toStringResult.Append(property.Name + ":\t" + property.GetValue(this, null).ToString()+ Environment.NewLine);
            }
            return toStringResult.ToString();
        }
    }
    #endregion

    #region Calibration
    public class CalibrationSettings
    {
        public CalibrationGeneralSettings CalibirationSettings { get; set; }
        public ElectroMagnetSBJControllerSettings ElectromagnetSettings { get; set; }
        public ChannelsSettings ChannelsSettings { get; set; }

        public CalibrationSettings(CalibrationGeneralSettings calibirationSettings,
                                                   ElectroMagnetSBJControllerSettings electromagnetSettings,
                                                   ChannelsSettings channelsSettings)
        {
            CalibirationSettings = calibirationSettings;
            ElectromagnetSettings = electromagnetSettings;
            ChannelsSettings = channelsSettings;
        }
    }

    public class CalibrationGeneralSettings
    {
        public double Bias { get; set; }
        public string Gain { get; set; }
        public double TriggerVoltage { get; set; }
        public double TriggerConductance { get; set; }
        public int SampleRate { get; set; }
        public bool IsFileSavingRequired { get; set; }
        public string Path { get; set; }
        public int CurrentFileNumber { get; set; }
        public int TotalNumberOfCycles { get; set; }
        public double ShortCircuitVoltage { get; set; }
        public bool EnableElectroMagnet { get; set; }
        public bool UseKeithley { get; set; }
        public int DelayTime { get; set; }
        public CalibrationMeasurementType MeasurementType { get; set; }

        public CalibrationGeneralSettings(double bias, string gain, double triggerVoltage,
                                                 double triggerConductance, bool isFileSavingRequired,
                                                 int sampleRate, string path, int currentFileNumber,
                                                 int totalNUmberOfCycles, double shourtCircuitVoltage,
                                                 bool enableElectroMagnet, bool useKeithley, int delayTime,
                                                 CalibrationMeasurementType measurementType)
        {
            Bias = bias;
            Gain = gain;
            TriggerConductance = triggerConductance;
            TriggerVoltage = triggerVoltage;
            SampleRate = sampleRate;
            IsFileSavingRequired = isFileSavingRequired;
            Path = path;
            CurrentFileNumber = currentFileNumber;
            TotalNumberOfCycles = totalNUmberOfCycles;
            ShortCircuitVoltage = shourtCircuitVoltage;
            DelayTime = delayTime;
            EnableElectroMagnet = enableElectroMagnet;
            UseKeithley = useKeithley;
            MeasurementType = measurementType;
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
    #endregion
}
