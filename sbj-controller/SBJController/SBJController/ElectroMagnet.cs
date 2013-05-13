using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace SBJController
{
    public  class ElectroMagnet
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

        #endregion

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
        /// <returns>returns true if next step voltage was applied, 
        /// returns false if max voltage was exceeded and last voltage remained. </returns>
        public bool MoveSingleStep()
        {
            double oldVoltage = m_currentEMVoltage;
            
            //
            //Calculate next voltage to apply. 
            //the higher the voltage, the more we push the junction (up the voltage->down direction)
            //            
            m_currentEMVoltage -= s_voltInterval*(int)m_direction;            
            
            //
            // if exceedes max or min voltage, pause stepper and return false.
            // else: move and return true. 
            //
            if ( (Math.Abs(m_currentEMVoltage) > s_maxEMVoltage) || (m_currentEMVoltage < s_minEMVoltage) )
            {
                m_direction = StepperDirection.STATIC;
                m_currentEMVoltage = oldVoltage;
                return false;
            }
            else
            {
                AnalogOut(s_analogOutChannel, m_currentEMVoltage);
                return true;
            }
        }

        /// <summary>
        /// Move Multiple Steps
        /// </summary>
        /// <param name="numberOfSteps"></param>
        /// <returns>return true if done successfully, false if exceeded max EM voltage</returns>
        public bool MoveMultipleSteps(int numberOfSteps)
        {
            for (int i = 1; i < numberOfSteps; i++)
            {
                //
                //if EM voltage exceeded max value return false
                //
                if (!MoveSingleStep())
                {
                    return false;
                }
                Thread.Sleep(m_delay);
            }            
            return true;
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

        /// <summary>
        /// Reach target voltage on EM.
        /// </summary>
        /// <param name="targetVoltage"></param>
        public void ReachEMVoltageGradually(int delayTime, double targetVoltage)
        {
            //
            // if the current voltage is different than the target
            //
            if (m_currentEMVoltage != targetVoltage)
            {
                //
                // choose direction of motion according to where is the current voltage relative to the target
                // notice: increase in voltage causes DOWN direction motion
                //
                m_direction = ((m_currentEMVoltage - targetVoltage) > 0) ? StepperDirection.UP : StepperDirection.DOWN;
                
                //
                // move until the difference between the current voltage and the target is below zero
                //
                if ((m_currentEMVoltage - targetVoltage) > 0)
                {
                    while ((m_currentEMVoltage - targetVoltage) > 0)
                    {
                        //
                        //if EM voltage exceeded max value - break. 
                        //
                        if (!MoveSingleStep())
                        {
                            return;
                        }
                        Thread.Sleep(delayTime);
                    }
                }
                else
                {
                    while ((m_currentEMVoltage - targetVoltage) < 0)
                    {
                        //
                        //if EM voltage exceeded max value - break.  
                        //
                        if (!MoveSingleStep())
                        {
                            return;
                        }
                        Thread.Sleep(delayTime);
                    }
                }
            }
        }

        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AnalogOut(byte Chan, double Volt);
        #endregion
        
        #endregion

    }
}
