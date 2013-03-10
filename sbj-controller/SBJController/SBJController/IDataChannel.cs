using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// This interface represents a logic interface for a data channel.
    /// </summary>
    public interface IDataChannel
    {
        /// <summary>
        /// The name of the physical port on the DAQ card
        /// </summary>
        string PhysicalName { get; set; }

        /// <summary>
        /// The name of the channel which represents its purpose
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The raw data as retrieved from the DAQ card.
        /// In case this list holds more than 1 member it means that this data channels is virtual.
        /// </summary>
        IList<double[]> RawData { get; set; }

        /// <summary>
        /// The physical data itself with its physical meaning (as oppose to the raw data).
        /// </summary>
        IList<double[]> PhysicalData { get; set; }

        /// <summary>
        /// All the settings which are essential for making the conversion from raw data to physical data.
        /// </summary>
        DataConvertorSettings DataConvertionSettings { get; set; }

        /// <summary>
        /// This method implements the conversion process from the raw data to the physical data.
        /// </summary>
        /// <returns></returns>
        IList<double[]> ConvertToPhysicalData();
    }
}
