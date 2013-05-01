using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// This class is an attribute that marks channels that should be used by the main DAQ tab
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DAQAttribute : System.Attribute
    {
        public DAQAttribute()
        {
        }
    }

    /// <summary>
    /// This class is an attribute that marks channels that should be used by the IV tab
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class IVAttribute : System.Attribute 
    {
        public IVAttribute()
        { 
        }
    }

    /// <summary>
    /// This class is an attribute that marks channels that should be used by the Calibration tab
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class CalibrationAttribute : System.Attribute
    {
        public CalibrationAttribute()
        {
        }
    }

}
