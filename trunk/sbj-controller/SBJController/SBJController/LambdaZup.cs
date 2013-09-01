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
        private SerialPort m_serialPort;
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
                m_serialPort = new SerialPort(Properties.Settings.Default.LambdaZupAddress);
                m_serialPort.BaudRate = 9600;
                m_serialPort.Parity = Parity.None; 
                m_serialPort.DataBits = 8;
                m_serialPort.StopBits = StopBits.One;
                m_serialPort.Handshake = Handshake.XOnXOff;
                m_serialPort.ReadTimeout = 2000;
                m_serialPort.WriteTimeout = 1000;

                m_serialPort.DtrEnable = true;
                m_serialPort.RtsEnable = true;


                m_serialPort.Open();
                m_serialPort.WriteLine(":ADR01;:MDL?;");
            }
            catch (Exception ex)
            {
                 throw new SBJException("Cannot Connect to LambdaZup device", ex);
            }
        }

        public void TurnOnOutput()
        {
            m_serialPort.Write(c_outputOn);
            Thread.Sleep(1000);
        }

        public void TurnOffOutput()
        {
            m_serialPort.Write(c_outputOff);
            Thread.Sleep(1000);
        }

        public void SetVoltage(double voltage)
        {
            string command = string.Format(c_setVoltageValue, voltage.ToString("000.00"));
            m_serialPort.Write(command);
            Thread.Sleep(1000);
        }
        #endregion
    }
}
