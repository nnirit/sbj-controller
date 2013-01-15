using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace SBJController
{    
    public class StepperMotor
    {
        #region Static Members

        private static int s_minStepperDelay = 2;
        private static int s_maxAllowedSteps = 500000;
        private static StepperPolarization s_polarization = new StepperPolarization();

        #endregion 

        #region Private Members

        private StepperSteppingMode m_steppingMode;
        private StepperDirection m_direction;
        private int m_currentSteppingIndex; // 0 - 7
        private byte m_currentSteppingPolarization;
        private int m_absolutePosition;
        private int m_delay;
        private const int c_minDelay = 2; // 2 msec

        #endregion;

        #region Properties

        /// <summary>
        /// Stepping mode
        /// </summary>
        public StepperSteppingMode SteppingMode
        {
            get { return m_steppingMode; }
            set { m_steppingMode = value; }
        }

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
            get { return c_minDelay; }
        }

        /// <summary>
        /// The current polarization
        /// </summary>
        public byte CurrentSteppingPolarization
        {
            get { return m_currentSteppingPolarization; }
            private set { m_currentSteppingPolarization = value; }
        }

        #endregion

        #region Constructor

        public StepperMotor()
        {
            SteppingMode = StepperSteppingMode.HALF;
            Direction = StepperDirection.STATIC;
            m_currentSteppingIndex = 0; 
            m_absolutePosition = 0;
            m_delay = s_minStepperDelay;
        }

        #endregion

        #region Public Methods
        public void MoveSingleStep()
        {
            //
            // If we are to step in full step mode, return stepper to an even position
            //
            if ((SteppingMode == StepperSteppingMode.FULL) && (m_currentSteppingIndex % 2 == 1))
            {
                m_currentSteppingIndex = (m_currentSteppingIndex - 1) % 8;               
            }
            
            //
            // Calculate "step length" according to the stepping mode and update absolute position
            //
            int stepAmplitude = (int)m_direction * (int)m_steppingMode;
            m_absolutePosition += stepAmplitude;

            //
            // if we stepped too much pause stepper and throw an exception
            //
            if (Math.Abs(m_absolutePosition) > s_maxAllowedSteps)
            {
                Direction = StepperDirection.STATIC;
                throw new SBJException("Stepper crossed its bounderies");
            }

            
            //
            // Calculate next stepping index between 0-7 
            //
            m_currentSteppingIndex = ((8 + m_currentSteppingIndex + stepAmplitude) % 8);

            // 
            // Polarization update
            //
            m_currentSteppingPolarization = s_polarization.GetPolarization(m_currentSteppingIndex);

            //
            // Now move.
            //
            DigitalOut(m_currentSteppingPolarization);                             
        }

        public void MoveMultipleSteps(int numberOfSteps)
        {
            for (int i = 1; i < numberOfSteps; i++)
            {
                MoveSingleStep();
                Thread.Sleep(m_delay);
            }
        }

        public void Shutdown()
        {
            Direction = StepperDirection.STATIC;
            m_currentSteppingPolarization = 0;
            DigitalOut(m_currentSteppingPolarization);
        }

        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DigitalOut(byte data);
        #endregion

        #endregion 
      
        #region Private Class
        private class StepperPolarization
        {
            private static byte[] s_polarization = new byte[8];

            static StepperPolarization()
            {
                s_polarization[0] = 1 | 1 << 2;
                s_polarization[1] = 1;
                s_polarization[2] = (1) | (1 << 3);
                s_polarization[3] = (1 << 3);
                s_polarization[4] = (1 << 1) | (1 << 3);
                s_polarization[5] = (1 << 1);
                s_polarization[6] = (1 << 1) | (1 << 2);
                s_polarization[7] = (1 << 2);
            }

            public byte GetPolarization(int polarizationIndex)
            {
                return (byte) (s_polarization[polarizationIndex]) ;
            }
        }
        #endregion
    }    
}
