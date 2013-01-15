using System;
using NationalInstruments.VisaNS;

namespace SBJController
{
    /// <summary>
    /// This class represents an amplifier
    /// </summary>
    public class Amplifier : VisaInstrument
    {
        #region Members
        private int m_gainPower;
        private const string c_resourceName = "GPIB0::22::INSTR";
        private const string c_changeGainCommand = "R{0}X";
        #endregion

        #region Properties
        /// <summary>
        /// The power of the current gain.
        /// </summary>
        public int Gain
        {
            get { return m_gainPower; }
            set { m_gainPower = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Amplifier() : base (c_resourceName)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        public void Connect()
        {
            base.Connect(c_resourceName);
        }
        /// <summary>
        /// Set gain power to new value
        /// </summary>
        /// <param name="newGainValue">Gain new value in the range of 3 to 10</param>
        /// <exception cref="SBJException"></exception>
        public void ChangeGain(int newGainValue)
        {
            //
            // Verify gain is in the right range
            //
            if ((newGainValue < 3) || (newGainValue > 10))
            {
                throw new SBJException("Gain power must be in the range of 3 to 10");
            }

            //
            // Set new value for the gain
            //
            Write(String.Format(c_changeGainCommand, newGainValue), "Error has occured while trying to modify gain.");            
        }
        #endregion
    }
}
