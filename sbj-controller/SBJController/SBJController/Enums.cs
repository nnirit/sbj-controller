using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SBJController
{
    /// <summary>
    /// Stepper direction enum
    /// </summary>
    public enum StepperDirection
    {
        UP = 1,
        DOWN = -1,
        STATIC = 0
    }

    /// <summary>
    /// Stepping mode
    /// </summary>
    public enum StepperSteppingMode
    {
        HALF = 1,
        FULL = 2
    }

    /// <summary>
    /// DAQ device type enum
    /// </summary>
    public enum DAQDeviceType
    {
        PCI4461 = 1,
        PCI4474 = 2,
        PCI4472 = 3
    }

    /// <summary>
    /// Stepping device enum
    /// </summary>
    public enum SteppingDevice
    {
        StepperMotor = 1,
        ElectroMagnet = 2
    }

    public enum CalibrationMeasurementType
    {
        OpenJunction = 1,
        CloseJunction = 2,
        BothOpenAndClose = 3
    }
}