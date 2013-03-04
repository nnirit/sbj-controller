using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    public interface IDataChannel
    {
        string PhysicalName { get; set; }
        string Name { get; set; }
        IList<double[]> RawData { get; set; }
        IList<double[]> PhysicalData { get; set; }
        DataConvertorSettings DataConvertionSettings { get; set; }



        IList<double[]> ConvertToPhysicalData();
    }
}
