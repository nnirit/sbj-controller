using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// Interface for optional laser controllers- Tabor or IoDrive
    /// </summary>
    public interface ILaserController
    {
        void TurnOff();
        void TurnOn();
        void Connect();
        void Disconnect();
        void SetAmplitude(double amplitude);
    }
}
