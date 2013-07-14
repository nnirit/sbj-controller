using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;


namespace SBJController
{
    /// <summary>
    /// This class is responsible for the LambdaZup device, using RS232 interface.
    /// </summary>
    public class LambdaZup
    {
        #region Members
        private SerialPort mySerialPort;
        private const string c_outputOn = ":OUT1;";
        private const string c_outputOff = ":OUT0;";
        private const string c_setVoltageValue = ":VOL{0};";
        #endregion

        #region Constructor
        public LambdaZup()
        {}
        #endregion

        #region Methods
        public void Connect()
        {
            try
            {
                mySerialPort = new SerialPort(Properties.Settings.Default.LambdaZupAddress);
                mySerialPort.BaudRate = 9600;
                mySerialPort.Parity = Parity.None; 
                mySerialPort.DataBits = 8;
                mySerialPort.StopBits = StopBits.One;
                mySerialPort.Handshake = Handshake.XOnXOff;
                mySerialPort.ReadTimeout = 2000;
                mySerialPort.WriteTimeout = 1000;

                mySerialPort.DtrEnable = true;
                mySerialPort.RtsEnable = true;


                mySerialPort.Open();
                mySerialPort.WriteLine(":ADR01;:MDL?;");
            }
            catch (Exception ex)
            {
                 throw new SBJException("Cannot Connect to LambdaZup device", ex);
            }
        }

        public void TurnOnOutput()
        {
            mySerialPort.Write(c_outputOn);
            Thread.Sleep(1000);
        }

        public void TurnOffOutput()
        {
            mySerialPort.Write(c_outputOff);
            Thread.Sleep(1000);
        }

        public void SetVoltage(double voltage)
        {
            string command = string.Format(c_setVoltageValue, voltage.ToString("000.00"));
            mySerialPort.Write(command);
            Thread.Sleep(1000);
        }
        #endregion
    }
}
