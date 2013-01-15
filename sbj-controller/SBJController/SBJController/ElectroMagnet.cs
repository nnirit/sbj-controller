using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace SBJController
{
    class ElectroMagnet
    {
        #region Static Members

        private static byte s_analogOutChannel = 0;
        private static int s_minEMDelay = 2;
        private static double s_voltInterval = 0.0048828125; //5/1024;
        private static double s_maxEMVoltage = 9.995;         //Maximal output voltage for the EM (10*2047/2048)
        private static double s_minEMVoltage = 0;        
        
        #endregion

        #region Private Members

        private StepperDirection m_direction;
        private double m_currentEMVoltage;
        private int m_delay;
        //private double m_voltInterval;

        #endregion;

        #region Properties

        /// <summary>
        /// Direction
        /// </summary>
        public StepperDirection Direction
        {
            get { return m_direction; }
            set { m_direction = value; }
        }

        /// <summary>
        /// Delay between each step
        /// </summary>
        public int Delay
        {
            get { return m_delay; }
            set { m_delay = value; }
        }

        /// <summary>
        /// Min Delay
        /// </summary>
        public int MinDelay
        {
            get { return s_minEMDelay; }
        }

        /// <summary>
        /// The current voltage
        /// </summary>
        public double CurrentEMVoltage
        {
            get { return m_currentEMVoltage; }
            private set { m_currentEMVoltage = value; }
        }

        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ElectroMagnet()
        {
            Direction = StepperDirection.STATIC;
            m_currentEMVoltage = 0; 
            m_delay = s_minEMDelay;
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Calculate and applies next step voltage.
        /// </summary>
        /// <returns>returns 0 if next step voltage was applied, 
        /// returns -1 if max voltage was exceeded and last voltage remained. </returns>
        public int MoveSingleStep()
        {
            double oldVoltage = m_currentEMVoltage;
            
            //
            //Calculate next voltage to apply.
            //            
            m_currentEMVoltage += s_voltInterval*(int)m_direction;            
            
            //
            // if exceedes max or min voltage, pause stepper and return -1.
            // else: move and return 0. 
            //
            if ( (Math.Abs(m_currentEMVoltage) > s_maxEMVoltage) || (m_currentEMVoltage < s_minEMVoltage) )
            {
                Direction = StepperDirection.STATIC;
                m_currentEMVoltage = oldVoltage;
                return -1;
            }
            else
            {
                AnalogOut(s_analogOutChannel, m_currentEMVoltage);
                return 0;
            }
        }

        /// <summary>
        /// Move Multiple Steps
        /// </summary>
        /// <param name="numberOfSteps"></param>
        /// <returns>return 0 if done successfully, -1 if exceeded max EM voltage</returns>
        public int MoveMultipleSteps(int numberOfSteps)
        {
            for (int i = 1; i < numberOfSteps; i++)
            {
                //
                //if EM voltage exceeded max value return -1
                //
                if (MoveSingleStep() == -1)
                {
                    return -1;
                }
                Thread.Sleep(m_delay);
            }
            
            return 0;
        }

        public void SetVoltage(double voltage)
        {
            m_currentEMVoltage = voltage;
            AnalogOut(s_analogOutChannel, m_currentEMVoltage);
        }

        public void Shutdown()
        {
            m_direction = StepperDirection.STATIC;
            this.SetVoltage(0);
        }

        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AnalogOut(byte Chan, double Volt);
        #endregion
        
        #endregion

    }
}
