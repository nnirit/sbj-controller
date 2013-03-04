using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBJController.Properties;

namespace SBJController
{
    public class LockIn : VisaInstrument
    {
        private static Dictionary<double, int> sensitivitiesRepository = new Dictionary<double, int>(27);
        private static Dictionary<double, int> timeConstantsRepository = new Dictionary<double, int>(20);
        private static Dictionary<int, int> rollOffRepository = new Dictionary<int, int>(4);
        private const string c_setSensitivityCommand = "SENS {0}";
        private const string c_setTimeConstantCommand = "OFLT {0}";
        private const string c_setRollOffCommand = "OFSL {0}";
        private const string c_setLocalModeCommand = "LOCL 0";


        static LockIn()
        {
            PopulateSensitivitiesRepositort();
            PopulateTimeConstantsRepository();
            PopulateRollOffRepository();

        }

        private static void PopulateRollOffRepository()
        {
            rollOffRepository.Add(6, 0);
            rollOffRepository.Add(12, 1);
            rollOffRepository.Add(18, 2);
            rollOffRepository.Add(24, 3);
        }

        private static void PopulateTimeConstantsRepository()
        {
            sensitivitiesRepository.Add(2E-9, 0);
            sensitivitiesRepository.Add(5E-9, 1);
            sensitivitiesRepository.Add(10E-9, 2);
            sensitivitiesRepository.Add(20E-9, 3);
            sensitivitiesRepository.Add(50E-9, 4);
            sensitivitiesRepository.Add(100E-9, 5);
            sensitivitiesRepository.Add(200E-9, 6);
            sensitivitiesRepository.Add(500E-9, 7);
            sensitivitiesRepository.Add(1E-6, 8);
            sensitivitiesRepository.Add(2E-6, 9);
            sensitivitiesRepository.Add(5E-6, 10);
            sensitivitiesRepository.Add(10E-6, 11);
            sensitivitiesRepository.Add(20E-6, 12);
            sensitivitiesRepository.Add(50E-6, 13);
            sensitivitiesRepository.Add(100E-6, 14);
            sensitivitiesRepository.Add(200E-6, 15);
            sensitivitiesRepository.Add(500E-6, 16);
            sensitivitiesRepository.Add(1E-3, 17);
            sensitivitiesRepository.Add(2E-3, 18);
            sensitivitiesRepository.Add(5E-3, 19);
            sensitivitiesRepository.Add(10E-3, 20);
            sensitivitiesRepository.Add(20E-3, 21);
            sensitivitiesRepository.Add(50E-3, 22);
            sensitivitiesRepository.Add(100E-3, 23);
            sensitivitiesRepository.Add(200E-3, 24);
            sensitivitiesRepository.Add(500E-3, 25);
            sensitivitiesRepository.Add(1, 26);
        }

        private static void PopulateSensitivitiesRepositort()
        {
            timeConstantsRepository.Add(10E-6, 0);
            timeConstantsRepository.Add(30E-6, 1);
            timeConstantsRepository.Add(100E-6, 2);
            timeConstantsRepository.Add(300E-6, 3);
            timeConstantsRepository.Add(1E-3, 4);
            timeConstantsRepository.Add(3E-3, 5);
            timeConstantsRepository.Add(10E-3, 6);
            timeConstantsRepository.Add(30E-3, 7);
            timeConstantsRepository.Add(100E-3, 8);
            timeConstantsRepository.Add(300E-3, 9);
            timeConstantsRepository.Add(1, 10);
            timeConstantsRepository.Add(3, 11);
            timeConstantsRepository.Add(10, 12);
            timeConstantsRepository.Add(30, 13);
            timeConstantsRepository.Add(100, 14);
            timeConstantsRepository.Add(300, 15);
            timeConstantsRepository.Add(1E+3, 16);
            timeConstantsRepository.Add(3E+3, 17);
            timeConstantsRepository.Add(10E+3, 18);
            timeConstantsRepository.Add(30E+3, 19);
        }

        public LockIn()
            : base(Settings.Default.LockInAddress)
        {
        }

        public void SetSensitivity(double sensitivityLevel)
        {
            int sensitivityIndex = 0;
            if (sensitivitiesRepository.TryGetValue(sensitivityLevel, out sensitivityIndex))
            {
                Write(string.Format(c_setSensitivityCommand, sensitivityIndex), "Error occured while trying to set sensitivity value.");
            }
            else
            {
                throw new SBJException(string.Format("Sensitivity value {0} is not valid.", sensitivityLevel));
            }
        }

        public void SetTimeConstant(double timeConstantLevel)
        {
            int timeConstantIndex = 0;
            if (timeConstantsRepository.TryGetValue(timeConstantLevel, out timeConstantIndex))
            {
                Write(string.Format(c_setTimeConstantCommand, timeConstantIndex), "Error occured while trying to set time constant value.");
            }
            else
            {
                throw new SBJException(string.Format("Time constant value {0} is not valid.", timeConstantLevel));
            }
        }

        public void SetRollOff(int rollOffLevel)
        {
            int rollOffIndex = 0;
            if (rollOffRepository.TryGetValue(rollOffLevel, out rollOffIndex))
            {
                Write(string.Format(c_setRollOffCommand, rollOffIndex), "Error occured while trying to set time constant value.");
            }
            else
            {
                throw new SBJException(string.Format("Roll off value {0} is not valid.", rollOffLevel));
            }
        }

        public void SetLocalMode()
        {
            Write(c_setLocalModeCommand, "Error occured while trying to set local mode.");
        }
    }
}
