using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SBJController
{
    /// <summary>
    /// This class represents laser controller which is operated by the IP drive (Flaxer Card) voltage supply.
    /// </summary>
    public class IODriveLaserController : ILaserController
    {
        private double m_power;
        private byte m_channel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">The channel on the IO Drive card from which the voltage would be supplied</param>
        /// <param name="power">The power to the laser in volts units</param>
        public IODriveLaserController(byte channel, double power)
        {
            if (power > 5)
            {
                throw new SBJException("Error configuring IODrive Laser Controller. Supplied power can not exceed value of 5V.");
            }
            m_power = power;
            m_channel = channel;
        }

        public IODriveLaserController(double power)
        {
            if (power > 5)
            {
                throw new SBJException("Error configuring IODrive Laser Controller. Supplied power can not exceed value of 5V.");
            }
            m_power = power;
            m_channel = 0;
        }

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="channel">The analog out channel on the IO Drove card</param>
        public IODriveLaserController(byte channel)
        {            
            m_channel = channel;
        }

        /// <summary>
        /// Set the amplitude for the laser
        /// </summary>
        /// <param name="newPower"></param>
        public void SetAmplitude(double newPower)
        {
            if (newPower > 5)
            {
                throw new SBJException("Supplied power can not exceed value of 5V.");
            }
            m_power = newPower;
            AnalogOut(m_channel, m_power);
        }

        /// <summary>
        /// Implements ILaserController.SetFrequency
        /// Empty implementation.
        /// </summary>
        /// <param name="frequency"></param>
        public void SetFrequency(double frequency)
        {}

        /// <summary>
        /// Turn off power supply
        /// </summary>
        public void TurnOff()
        {
            AnalogOut(m_channel, 0);
        }     

        /// <summary>
        /// Turn on power supply
        /// </summary>
        public void TurnOn()
        {
            AnalogOut(m_channel, m_power);
        }

        /// <summary>
        /// Shutdown
        /// </summary>
        public void Disconnect()
        {}

        /// <summary>
        /// Connect
        /// </summary>
        public void Connect()
        { }

        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AnalogOut(byte Chan, double Volt);
        #endregion

    }
}
