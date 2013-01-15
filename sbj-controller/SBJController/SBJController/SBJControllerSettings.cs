
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
