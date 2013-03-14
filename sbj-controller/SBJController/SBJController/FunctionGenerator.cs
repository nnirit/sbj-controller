using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// This class generates a triangle wave, and arrays of constants the same length.
    /// </summary>
    class FunctionGenerator
    {
        #region Private Members
        private double[] m_triangleWave;
        private double[] m_constWave;
        private double[] m_zerosWave;
        #endregion

        #region Properties
        public double[] TriangleWave
        {
            get { return m_triangleWave; }
        }

        public double[] ConstWave
        {
            get { return m_constWave; }
        }

        public double[] ZerosWave
        {
            get { return m_zerosWave; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samplesPerCycle">samples per cycle</param>
        /// <param name="triangleWaveAmplitude">half of the peak-to-peak</param>
        /// <param name="constValue">The value that will fill the const wave</param>
        public FunctionGenerator(int samplesPerCycle, double triangleWaveAmplitude, double constValue)
        {
            m_triangleWave = GenerateTriangleWave(samplesPerCycle, triangleWaveAmplitude);
            m_constWave = GenerateConstWave(samplesPerCycle, constValue);
            m_zerosWave = GenerateConstWave(samplesPerCycle, 0.0000);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generate array of const value
        /// </summary>
        /// <param name="samplesPerCycle">Array length</param>
        /// <param name="constValue">Value to fill in the array</param>
        /// <returns></returns>
        private double[] GenerateConstWave(int samplesPerCycle, double constValue)
        {
            double[] data = new double[samplesPerCycle];

            for (int i = 0; i < samplesPerCycle; i++)
            {
                data[i] = constValue;
            }
            return data;
        }
        
        /// <summary>
        /// Generate one cycle of triangle wave (around zero)
        /// </summary>
        /// <param name="samplesPerCycle">length of the wave</param>
        /// <param name="amplitude">the amplitude of the wave</param>
        /// <returns>array of one cycle of triangle wave</returns>
        private static double[] GenerateTriangleWave(int samplesPerCycle, double amplitude)
        {
            double frequency = 1.0/samplesPerCycle;
            double[] wave = new double[samplesPerCycle];

            for (int i = 0; i < samplesPerCycle; i++)
            {
                //
                // we start with a phase of 90 degrees
                //
                double phase = (90 + 360*frequency*i) % 360;
                double percentPeriod = phase/360;
                double dat = amplitude*4*percentPeriod;

                if (percentPeriod <= 0.25)
                {
                    wave[i] = dat;
                }
                else if (percentPeriod <= 0.75)
                {
                    wave[i] = 2*amplitude - dat;
                }
                else
                {
                    wave[i] = dat - 4*amplitude;
                }
            }
            return wave;
        }
        #endregion
    }
}
