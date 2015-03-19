using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.VisaNS;
using SBJController.Properties;

namespace SBJController
{
    public class SourceMeter : VisaInstrument
    {
        private double m_bias;
        private const string c_resetCommand = "*RST";
        private const string c_selectVoltageSource = ":SOUR:FUNC VOLT";
        private const string c_setBiasCommand = ":SOUR:VOLT:LEV {0}";
        private const string c_setLocalMode = ":SYST:KEY 23";
        private const string c_setCurrentMeasurementRange = ":SENS:CURR:RANG:AUTO 1";
        private const string c_setCurrentMeasurementCompliance = ":SENS:CURR:PROT {0}";
        private const string c_enableDisplay = ":DISPLAY:ENABLE 1";
        private const string c_outputOn = ":OUTP ON;";
        private const string c_outputOff = ":OUTP OFF;";
        private const string c_autoRange = "SENSe:CURRent:RANGe:AUTO {0}";
        private const string c_setRange = "SENSe:CURRent:RANGe {0}";


        public double Bias
        {
            get { return m_bias; }
            set { m_bias = value; }
        }

        public SourceMeter()
            : base(Settings.Default.SourceMeterAddress)
        {
        }

        new public void Connect()
        {
            base.Connect();
            ResourceManager manager = ResourceManager.GetLocalManager();            
            this.SelectVoltageSource();            
            this.SetCurrentMeasurementAutoRange();
            this.SetCurrentMeasurementCompliance(0.001);
            this.EnableDisplay();
            this.TurnOn();
            this.SetLocalMode();
        }      

        private void SelectVoltageSource()
        {
            Write(c_selectVoltageSource, "Error has occured while trying to set voltage source.");            
        }

        private void SetLocalMode()
        {
            Write(c_setLocalMode, "Error has occured while trying to set current measurement mode.");            
        }

        private void SetCurrentMeasurementAutoRange()
        {
            Write(c_setCurrentMeasurementRange, "Error has occured while trying to set current measurement range.");                       
        }

        private void SetCurrentMeasurementCompliance(double compliance)
        {
            Write(string.Format(c_setCurrentMeasurementCompliance, compliance), "Error has occured while trying to set current measurement compliance.");            
        }

        public void SetBias(double bias, double range, bool isAutoRangeOn)
        {
            Write(String.Format(c_setBiasCommand, bias), "Error has occured while trying to set the bias.");
            if (isAutoRangeOn)
            {
                SetAutoRange(true);
            }
            else
            {
                SetRange(range);
            }
            SetLocalMode();
        }

        public void EnableDisplay()
        {
            Write(c_enableDisplay, "Error has occured while trying to set display on.");
        }
        public void TurnOn()
        {
            Write(c_outputOn, "Error has occured while trying to turn on the source meter.");
        }

        public void TurnOff()
        {
            Write(c_outputOff, "Error has occured while trying to turn off the source meter.");
        }

        public void SetAutoRange(bool autoRangeOn)
        {
            Write(String.Format(c_autoRange, Convert.ToInt32(autoRangeOn)), "Error has occured while trying to set autoRange.");
            SetLocalMode();
        }

        public void SetRange(double range)
        {
            Write(String.Format(c_setRange, range), "Error has occured while trying to set the range.");
            SetLocalMode();
        }
    }
}
