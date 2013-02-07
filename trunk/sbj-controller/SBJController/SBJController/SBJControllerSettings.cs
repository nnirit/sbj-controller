
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

        public SBJControllerSettings(GeneralSBJControllerSettings generalSettings,
                                     LaserSBJControllerSettings laserSettings,
                                     LockInSBJControllerSettings lockInSettings,
                                     ElectroMagnetSBJControllerSettings electromagnetSettings)
        {
            GeneralSettings = generalSettings;
            LaserSettings = laserSettings;
            LockInSettings = lockInSettings;
            ElectromagnetSettings = electromagnetSettings;
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
                                     string path, int currentFileNumber, int totalNUmberOfCycles, 
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
            Path = path;
            CurrentFileNumber = currentFileNumber;
            TotalNumberOfCycles = totalNUmberOfCycles;
            ShortCircuitVoltage = shourtCircuitVoltage;
            Bottom = bottom;
            Top = top;
        }
    
    }

    /// <summary>
    /// This class represents the laser settinngs in the UI
    /// </summary>
    public class LaserSBJControllerSettings
    {
        public bool IsLaserOn { get; set; }
        public string LaserMode { get; set; }
        public double LaserAmplitude { get; set; }
        public int LaserFrequency { get; set; }

        public LaserSBJControllerSettings(bool isLaserOn, string laserMode, double laserAmplitude, int laserFrequency)
        {
            IsLaserOn = isLaserOn;
            LaserMode = laserMode;
            LaserAmplitude = laserAmplitude;
            LaserFrequency = laserFrequency;
        }
    }

    /// <summary>
    /// This class represents the general lockin in the UI
    /// </summary>
    public class LockInSBJControllerSettings
    {
        public bool IsLockInSignalEnable { get; set; }
        public bool IsLockInPhaseSignalEnable { get; set; }
        public double LockInSensitivity { get; set; }  
        
        public  LockInSBJControllerSettings(bool isLockInSignalEnable, bool isLockInPhaseSignalEnable, double lockInSensitivity)
        {
            IsLockInSignalEnable = isLockInSignalEnable;
            IsLockInPhaseSignalEnable = isLockInPhaseSignalEnable;
            LockInSensitivity = lockInSensitivity;
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
    }
}
