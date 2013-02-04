
namespace SBJController
{
    /// <summary>
    /// This class represents all the required data memebers of the UI
    /// </summary>
    public class SBJControllerSettings
    {
        public double Bias {get; set;}
        public string Gain { get; set; }
        public double TriggerVoltage { get; set; }
        public double TriggerConductance { get; set; }
        public int SampleRate { get; set; }
        public int TotalSamples { get; set; }
        public int PretriggerSamples { get; set; }
        public int StepperWaitTime1 { get; set; }
        public int StepperWaitTime2 { get; set; }
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
        public bool IsLaserOn { get; set; }
        public string LaserMode { get; set; }
        public int LaserAmplitude { get; set; }
        public int LaserFrequency { get; set; }
        public bool IsLockInSignalEnable { get; set; }
        public bool IsLockInPhaseSignalEnable { get; set; }
        public double LockInSensitivity { get; set; }        
        public bool IsFileSavingRequired { get; set; }
        public string Path { get; set; }
        public int CurrentFileNumber { get; set; }
        public int TotalNumberOfCycles { get; set; }
        public double ShortCircuitVoltage { get; set; }
        public Sample Bottom { get; set; }
        public Sample Top { get; set; }

        public SBJControllerSettings(double bias, string gain, double triggerVoltage,
                                     double triggerConductance, int sampleRate,
                                     int totalSamples, int pretriggerSamples,
                                     int stepperWaitTime1, int stepperWaitTime2,
                                     bool isEMEnable, int emShortCircuitDelayTime, int emFastDelayTime, int emSlowDelayTime, 
                                     bool isEMHoldOnEnable, double emHoldOnMaxConductance, double emHoldOnMaxVoltage,
                                     double emHoldOnMinConductance, double emHoldOnMinVoltage, bool isEMSkipFirstCycleEnable, 
                                     bool isLaserOn, string laserMode,int laserAmplitude,
                                     int laserFrequency, bool isLockInSignalEnable, bool isLockInPhaseSignalEnable, double lockInSensitivity,
                                     bool isFileSavingRequired, string path, int currentFileNumber, 
                                     int totalNUmberOfCycles, double shourtCircuitVoltage,
                                     Sample bottom, Sample top)
        {
            Bias = bias;
            Gain = gain;
            TriggerConductance = triggerConductance;
            TriggerVoltage = triggerVoltage;
            SampleRate = sampleRate;
            TotalSamples = totalSamples;
            PretriggerSamples = pretriggerSamples;
            StepperWaitTime1 = stepperWaitTime1;
            StepperWaitTime2 = stepperWaitTime2;
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
            IsLaserOn = isLaserOn;
            LaserMode = laserMode;
            LaserAmplitude = laserAmplitude;
            LaserFrequency = laserFrequency;
            IsLockInSignalEnable = isLockInSignalEnable;
            IsLockInPhaseSignalEnable = isLockInPhaseSignalEnable;
            LockInSensitivity = lockInSensitivity;
            IsFileSavingRequired = isFileSavingRequired;
            Path = path;
            CurrentFileNumber = currentFileNumber;
            TotalNumberOfCycles = totalNUmberOfCycles;
            ShortCircuitVoltage = shourtCircuitVoltage;
            Bottom = bottom;
            Top = top;
        }
    }
}
