using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    class SBJException : Exception
    {
        public SBJException()
            : base()
        {
        }

        public SBJException(string exception)
            : base (exception)
        {            
        }
        
        public SBJException(string exception, Exception innerException)
            : base(exception, innerException)
        {
        }
    }
}
