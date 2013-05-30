using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    class IVDataHandle
    {
        #region Private Members
        private const double c_1G0 = 77.5E-6;
        private int m_samplesPerIVCycle;
        private double[] m_voltageRawData;
        private double[] m_junctionRawData;
        private IList<int> m_peakIndices;
        private IList<IList<double[]>> m_ivCyclesData;
        private int m_gain;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="junctionRawData">the raw data from the junction input</param>
        /// <param name="voltageRawData">the raw data from the monitor input</param>
        /// <param name="samplesPerCycle"></param>
        /// <param name="gain"></param>
        public IVDataHandle(double[] junctionRawData, double[] voltageRawData, int samplesPerCycle, int gain)
        {
            m_samplesPerIVCycle = samplesPerCycle;
            m_voltageRawData = voltageRawData;
            m_junctionRawData = junctionRawData;
            m_gain = gain;
        }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Get one cycle of IV (current-voltage) out of the raw data of a full trace. 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns>2 row list, 1st row is the current measured, 2nd row is the applied voltage</returns>
        private IList<double[]> GetOneIVCycleData(int startIndex, int endIndex)
        {
            IList<double[]> oneCycleData = new List<double[]>();
            oneCycleData.Add(new double[endIndex-startIndex]);
            oneCycleData.Add(new double[endIndex-startIndex]);

            //
            // run over all junction and voltage dots and arrange them in the double arrays inside the list
            // the junction data is converted from Voltage to Current 
            //
            for (int i = startIndex; i < endIndex; i++)
            {
                oneCycleData[0][i - startIndex] = - m_junctionRawData[i] / Math.Pow(10, m_gain);
                oneCycleData[1][i - startIndex] = m_voltageRawData[i];
            }
            return oneCycleData;
        }

        /// <summary>
        /// This function receive an array of triangle wave and returns the indices of the peaks.
        /// </summary>
        /// <returns>the indices of the peaks</returns>
        private IList<int> GetPeaksIndices()
        {
            double[] diff = new double[m_voltageRawData.Length - 1];
            IList<int> peakIndices = new List<int>();

            for (int i = 0; i < (m_voltageRawData.Length - 1); i++)
            {
                diff[i] = m_voltageRawData[i + 1] - m_voltageRawData[i];
            }

            for (int i = 0; i < (diff.Length - 1); i++)
            {
                //
                // each time the derivative goes from positive to negative - we have a maximum peak. 
                //
                if (diff[i] > 0 && diff[i + 1] <= 0)
                {
                    if (peakIndices.Count == 0)
                    {
                        peakIndices.Add(i + 1);
                        continue;
                    }

                    //
                    // checks if the distance between the current peak and the last one is reasonable
                    //
                    if ((i + 1) - peakIndices.Last() < m_samplesPerIVCycle * 1.03 && (i + 1) - peakIndices.Last() > m_samplesPerIVCycle * 0.97)
                    {
                        //
                        // distance is reasonable; let's add the new dot.
                        //
                        peakIndices.Add(i + 1);
                    }
                    else
                    {
                        //
                        // if not, delete last item and put the current one. 
                        //
                        peakIndices[peakIndices.Count - 1] = i + 1;
                    }

                    //
                    // let's check if the peak is in the overload part of the trace. 
                    // if it is, than we might don't want to save it. 
                    //
                    if (Math.Abs(m_junctionRawData[peakIndices.Last()]) > 9.9)
                    {
                        //
                        // if we already have items in the index list - continue (we are in the middle of the trace)
                        // if we this is one of the first indices in the list, delete everything.
                        //
                        if (peakIndices.Count() < 3)
                        {
                            peakIndices.Clear();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return peakIndices;
        }

        /// <summary>
        /// Converts current data (as it arrives from the m_ivCyclesData) to conductance.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="voltage"></param>
        /// <returns></returns>
        private double ConvertCurrentToConductanceValue(double currentValue, double voltage)
        {
            return currentValue / c_1G0 / voltage;
        }

        /// <summary>
        /// gets a list of all the iv cycles from one full trace
        /// </summary>
        /// <returns></returns>
        public IList<IList<double[]>> GetIVCycles()
        {
            //
            // all the data will be arranged in this list.
            //
            m_ivCyclesData = new List<IList<double[]>>();

            //
            // get the indices of the peaks
            //
            m_peakIndices = GetPeaksIndices();

            //
            // run over all the indices of the peaks in the trace and chop the data
            //
            for (int i = 0; i < m_peakIndices.Count - 1; i++)
            {
                //
                // add the current iv cycle to the list
                //
                m_ivCyclesData.Add(GetOneIVCycleData(m_peakIndices[i], m_peakIndices[i + 1]));  
            }
            return m_ivCyclesData;
        }

        /// <summary>
        /// Get a trace (conductance values vs. index) of the measurements made only on a certain voltage value.
        /// </summary>
        /// <param name="requestedVoltage">the voltage at wich to get the trace. MUST BE SMALLER THAN APPLIED VOLTAGE AMPLITUDE!</param>
        /// <returns>a list, 1st row contains the junction data, 2nd row the respective indices (plot 1st row vs. 2nd) </returns>
        public List<double[]> GetCertainVoltageTrace(double requestedVoltage)
        {
            List<double[]> traceData = new List<double[]>();
            traceData.Add(new double[m_ivCyclesData.Count]);
            //traceData.Add(new double[m_ivCyclesData.Count*2]);

            //
            // run over all IV cycles
            //
            for (int i = 0; i < m_ivCyclesData.Count - 1; i++)
            {
                double[] thisCycleJunctionData = m_ivCyclesData[i][0];
                double[] thisCycleVoltageData = m_ivCyclesData[i][1];
                
                //
                // the indexes of the requested voltage in this cycle
                //
                int index1 = 0;
                //int index2 = 0;

                for (int j = 0; j < thisCycleVoltageData.Length; j++)
                {
                    //
                    // save the index of the first time we go beneath the requested voltage
                    //
                    if (index1 == 0 && (thisCycleVoltageData[j] < requestedVoltage))
                    {
                        index1 = j;
                    }

                    //
                    // checking backwards from the end of the trace: when is the first time we go beneath requested voltage
                    //
                    //if (index2 == 0 && (thisCycleVoltageData[thisCycleVoltageData.Length - j - 1] < requestedVoltage))
                    //{
                    //    index2 = thisCycleVoltageData.Length - j - 1;
                    //}
                }

                //
                // adds the junction measurement in the indexes we found 
                //
                traceData[0][i] = ConvertCurrentToConductanceValue(thisCycleJunctionData[index1], requestedVoltage);
                //traceData[0][2 * i + 1] = ConvertCurrentToConductanceValue(thisCycleJunctionData[index2], requestedVoltage);

                //
                // add the absolute approximated index of the data 
                // (we know only the exact index inside the current IV cycle, and we approximate that the 
                // previous cycles were of samplesPerCycle length)
                //
                //traceData[1][2 * i] = m_samplesPerIVCycle * i + index1;
                //traceData[1][2 * i + 1] = m_samplesPerIVCycle * i + index2;
            }
            return traceData;
        }

        #endregion
    }
}
