using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.NI4882;
using NationalInstruments.VisaNS;
using SBJController.Properties;

namespace SBJController
{
    /// <summary>
    /// This class represents the Tabor instrumnet
    /// </summary>
    public class TaborController : VisaInstrument
    {  
        protected static Dictionary<TaborModel, Dictionary<string, string>> s_commandsRepository;

        //
        // WW2571 Commands
        //
        private const string c_ww2571DcModeCommand = "FUNCtion:SHAPe DC;:DC:AMPLitude {0}";
        private const string c_ww2571SquareModeCommand = "FUNCtion:SHAPe SQUare;:SOURce:FREQuency {0};:SOURce:VOLTage {1}";
        private const string c_ww2571SinusoidModeCommand = "FUNCtion:SHAPe SINusoid;:SOURce:FREQuency {0};:SOURce:VOLTage {1}";
        private const string c_ww2571SetAmplitudeCommand = "SOURce:VOLTage:LEVel:AMPLitude {0};:DC:AMPLitude {0}";

        //
        // WW5061Commands
        //
        private const string c_ww5061SinusoidModeCommand = ":SOUR:APPL:SIN {0},{1},0,0";
        private const string c_ww5061DCModeCommand = ":SOUR:APPL:DC;:SOURce:VOLTage {0}";
        private const string c_ww5061SqaureModeCommand = ":SOUR:APPL:SQUare {0},{1},0,50";
        private const string c_ww5061SetAmplitudeCommand = "SOURce:VOLTage {0}";

        //
        // Common Commands
        //
        private const string c_turnOffCommand = "OUTPut 0";
        private const string c_turnOnCommand = "OUTPut 1";
        private const string c_localCommand = "SYSTem: LOCal";
        private const string c_setFrequencyCommand = "SOURce:FREQuency {0}";

        //
        // Commands 
        //
        protected const string c_setDCMode = "SetDCMode";
        protected const string c_setSqaureMode = "SetSqaureMode";
        protected const string c_setSinusoidMode = "SetSinusoidMode";
        protected const string c_setFrequency = "SetFrequency";
        protected const string c_setAmplitude = "SetAmplitude";
        protected const string c_turnOff = "TurnOff";
        protected const string c_turnOn = "TurnOn";
        protected const string c_local = "Local";
        
        //
        // Non static members
        //
        protected TaborModel m_taborModel;
        protected string m_address;

        /// <summary>
        /// Static constructor
        /// </summary>
        static TaborController()
        {
            PopulateCommandsRepository();
        }

        /// <summary>
        /// Populate commands repository according to Tabor's model.
        /// </summary>
        private static void PopulateCommandsRepository()
        {
            s_commandsRepository = new Dictionary<TaborModel, Dictionary<string, string>>();
            Dictionary<string, string> commandsWW2571 = new Dictionary<string, string>();
            commandsWW2571.Add(c_turnOff, c_turnOffCommand);
            commandsWW2571.Add(c_turnOn, c_turnOnCommand);
            commandsWW2571.Add(c_setDCMode, c_ww2571DcModeCommand);
            commandsWW2571.Add(c_setSqaureMode, c_ww2571SquareModeCommand);
            commandsWW2571.Add(c_setSinusoidMode, c_ww2571SinusoidModeCommand);
            commandsWW2571.Add(c_setAmplitude, c_ww2571SetAmplitudeCommand);
            commandsWW2571.Add(c_setFrequency, c_setFrequencyCommand);
            commandsWW2571.Add(c_local, c_localCommand);

            Dictionary<string, string> commandsWW5061 = new Dictionary<string, string>();
            commandsWW5061.Add(c_turnOff, c_turnOffCommand);
            commandsWW5061.Add(c_turnOn, c_turnOnCommand);
            commandsWW5061.Add(c_setDCMode, c_ww5061DCModeCommand);
            commandsWW5061.Add(c_setSqaureMode, c_ww5061SqaureModeCommand);
            commandsWW5061.Add(c_setSinusoidMode, c_ww5061SinusoidModeCommand);
            commandsWW5061.Add(c_setAmplitude, c_ww5061SetAmplitudeCommand);
            commandsWW5061.Add(c_setFrequency, c_setFrequencyCommand);
            commandsWW5061.Add(c_local, c_localCommand);

            s_commandsRepository.Add(TaborModel.WW2571, commandsWW2571);
            s_commandsRepository.Add(TaborModel.WW5061, commandsWW5061);
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The function generator model - WW2571 \ WW5061</param>
        /// <param name="address">The GPIB address</param>
        public TaborController(TaborModel model, string address) : base (address)
        {
            m_address = address;
            m_taborModel = model;
        }

        /// <summary>
        /// Connect to the device
        /// </summary>
        new public void Connect()
        {
            base.Connect();
        }       

        public void Disconnect()
        {
            string command = s_commandsRepository[m_taborModel][c_local];
            Write(command, "Error occured while trying to set local mode.");
        }

        public void TurnOff()
        {
            string command = s_commandsRepository[m_taborModel][c_turnOff];
            Write(command, "Error occured while trying to turn off Tabor.");            
        }

        public void TurnOn()
        {
            string command = s_commandsRepository[m_taborModel][c_turnOn];
            Write(command, "Error occured while trying to turn on Tabor.");            
        }

        public void SetFrequency(double frequency)
        {
            string command = s_commandsRepository[m_taborModel][c_setFrequency];
            Write(string.Format(command, frequency), "Error occured while trying to change frequency."); 
        }
    }

    /// <summary>
    /// This class represents the Tabor as the laser controller
    /// </summary>
    public class TaborLaserController : TaborController, ILaserController
    {
        public TaborLaserController(TaborModel model, string address)
            : base(model, address)
        { }

        public void SetDCMode(double amplitude)
        {
            string command = s_commandsRepository[m_taborModel][c_setDCMode];
            this.Write(string.Format(command, amplitude), "Error occured while trying to set DC mode.");
        }

        public void SetSquareMode(double frequency, double amplitude)
        {
            string command = s_commandsRepository[m_taborModel][c_setSqaureMode];
            Write(string.Format(command, frequency, amplitude), "Error occured while trying to set square mode.");
        }

        public void SetAmplitude(double amplitude)
        {
            string command = s_commandsRepository[m_taborModel][c_setAmplitude];
            Write(string.Format(command, amplitude), "Error occured while trying to set the new amplitude.");
        } 
    }

    /// <summary>
    /// This class represents the Tabor as the controller for the Electro Optical Modulator.
    /// </summary>
    public class TaborEOMController : TaborController
    {
        //
        // EOM only works with 10V amplitude.
        //
        private const double c_amplitude = 10.0;

        public TaborEOMController(TaborModel model, string address)
            : base(model, address)
        { }

        public void SetSinusoidMode(double frequency)
        {
            string command = s_commandsRepository[m_taborModel][c_setSinusoidMode];
            Write(string.Format(command, frequency, c_amplitude), "Error occured while trying to set sinusoid mode.");            
        } 
    }
}