using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.NI4882;
using NationalInstruments.VisaNS;
using SBJController.Properties;

namespace SBJController
{
    public class Tabor : VisaInstrument
    {  
        private const string c_dcModeCommand = "FUNCtion:SHAPe DC";
        private const string c_squareModeCommand = "FUNCtion:SHAPe SQUare";
        private const string c_dcAmplitudeCommand = "DC {0}";
        private const string c_squareAmplitudeCommand = "VOLTage {0}";
        private const string c_squareFrequencyCommand = "FREQuency {0}";
        private const string c_turnOffCommand = "OUTPut 0";
        private const string c_turnOnCommand = "OUTPut 1";
        private const string c_localCommand = "SYSTem: LOCal"; 
        
        public Tabor() : base (Settings.Default.TaborAddress)
        {
        }

        new public void Connect()
        {
            base.Connect();
        }

        public void SetDCMode()
        {
            this.Write(c_dcModeCommand, "Error occured while trying to set DC mode.");           
        }

        public void SetSquareMode()
        {
            Write(c_squareModeCommand, "Error occured while trying to set square mode.");            
        }

        public void SetDcModeAmplitude(double voltAmplitude)
        {
            Write(String.Format(c_dcAmplitudeCommand, voltAmplitude), "Error occured while trying to set DC mode amplitude.");           
        }

        public void SetSquareModeAmplitude(double voltAmplitude)
        {
            Write(String.Format(c_squareAmplitudeCommand, voltAmplitude), "Error occured while trying to set Square mode amplitude.");            
        }

        public void SetSquareModeFrequency(int frequency)
        {
            Write(String.Format(c_squareFrequencyCommand, frequency), "Error occured while trying to set Square mode frequency.");            
        }

        public void SetLocalMode()
        {
            Write(c_localCommand, "Error occured while trying to set local mode.");
        }

        public void TurnOff()
        {
            Write(c_turnOffCommand, "Error occured while trying to turn off Tabor.");            
        }

        public void TurnOn()
        {
            Write(c_turnOnCommand, "Error occured while trying to turn on Tabor.");            
        }       
    }
}
