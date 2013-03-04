using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// Represnts the arguments for the aquired data
    /// </summary>
    public class DataAquiredEventArgs
    {
        /// <summary>
        /// The active channels
        /// </summary>
        public IList<IDataChannel> PhysicalChannels { get; set; }

         /// <summary>
        /// The file number in which the data was saved
        /// </summary>
        public int FileNumber { get; set; }

        public DataAquiredEventArgs(IList<IDataChannel> physicalChannels, int fileNumber)
        {
            FileNumber = fileNumber;
            PhysicalChannels = physicalChannels;
        }

    }
}
