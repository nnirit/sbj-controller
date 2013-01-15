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
        /// The data that was received
        /// </summary>
        public double[,] Data { get; set; }

         /// <summary>
        /// The file number in which the data was saved
        /// </summary>
        public int FileNumber { get; set; }

        public DataAquiredEventArgs(double[,] data, int fileNumber)
        {
            Data = data;
            FileNumber = fileNumber;
        }

    }
}
