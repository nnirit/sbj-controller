using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace SBJController
{
    public partial class SBJController
    {
        /// <summary>
        /// Open the junction asynchronously by the ElectroMagnet
        /// </summary>
        /// <param name="settings"></param>       
        private void EMBeginOpenJunction(SBJControllerSettings settings)
        {
            EMOpenJunctionMethodDelegate emOpenJunctionDelegate = new EMOpenJunctionMethodDelegate(EMTryOpenJunction);
            AsyncCallback callback = new AsyncCallback(EMEndOpenJunction);
            IAsyncResult asyncResult = emOpenJunctionDelegate.BeginInvoke(settings, callback, emOpenJunctionDelegate);
        }

        //TODO: Add Summary here
        private void ApplyVoltageOnElectroMagnetIfNeeded(bool isEMEnabled)
        {
            if (isEMEnabled)
            {
                //TODO: Move 6.5 to constants
                m_electroMagnet.SetVoltage(6.5);
            }
        }

        /// <summary>
        /// Try open junction by the EM, by calling EMOpenJunction.
        /// if min voltage exceeded without the junction being opened, do a few steps by the stepper motor, then retry EM (recursion).
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool EMTryOpenJunction(SBJControllerSettings settings)
        {
            //
            // if the EM reached voltage 0 without opening the junction, 
            // return to higher voltage on EM, do some steps by the stepper motor and retry opening by EM.
            //
            if (!EMOpenJunction(settings))
            {
                m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, 6.5);
                m_stepperMotor.Direction = StepperDirection.UP;
                m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
                m_stepperMotor.Delay = m_stepperMotor.MinDelay;
                m_stepperMotor.MoveMultipleSteps(100);
                m_stepperMotor.Shutdown();
                return EMTryOpenJunction(settings);
            }
            return true;
        }

        /// <summary>
        /// Open the junction by the ElectroMagnet.
        /// If min voltage exceeded without the junction being opened, return false. 
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private bool EMOpenJunction(SBJControllerSettings settings)
        {
            //
            // Set the direction of the movement and stepping mode
            // And configure the first setpper delay (shorter) - faster movement
            //
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = settings.ElectromagnetSettings.EMFastDelayTime;

            //
            // Read the initial voltgae before we've done anything
            //
            double initialVoltage = AnalogIn(0);
            bool isDelayedChanged = false;
            m_quitJunctionOpenningOperation = false;

            //
            // Until we've not been signaled from outer thread to stop we'll continue moving up.
            //
            while (!m_quitJunctionOpenningOperation)
            {
                if (!m_electroMagnet.MoveSingleStep())
                {
                    //
                    // if min Votlage on EM was exceeded return false.
                    //
                    return false;
                }
                double currentVoltage = AnalogIn(0);

                //
                // If voltgae had been changed in 0.0001% then switch to slow mode
                // Note that voltage can be negative so we must take the absoulute value
                //
                if (!isDelayedChanged && (Math.Abs(currentVoltage) < Math.Abs(initialVoltage) * 0.9999))
                {
                    m_electroMagnet.Delay = settings.ElectromagnetSettings.EMSlowDelayTime;
                    isDelayedChanged = true;
                }

                //
                // If hold-on trigger was exceeded, wait 10 ms and check if still true.
                //
                if (settings.ElectromagnetSettings.IsEMHoldOnEnable && Math.Abs(currentVoltage) < Math.Abs(settings.ElectromagnetSettings.EMHoldOnMaxVoltage * 1.1))
                {
                    Thread.Sleep(10);
                    if (Math.Abs(currentVoltage) < Math.Abs(settings.ElectromagnetSettings.EMHoldOnMaxVoltage * 1.1))
                    {
                        //
                        // trigger was truely exceeded. try to hold on to a certain junction voltage.
                        //
                        if (!StabilizeJunction(currentVoltage, settings.ElectromagnetSettings.EMHoldOnMaxVoltage, settings.ElectromagnetSettings.EMHoldOnMinVoltage))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                Thread.Sleep(m_electroMagnet.Delay);
            }
            return true;
        }

        /// <summary>
        /// A feedback loop that stabilizes the junction's voltage between maxVoltage and minVoltage.
        /// </summary>
        /// <param name="currentVoltage"></param>
        /// <param name="triggerVoltage"></param>
        /// <returns>false if min or max voltage was exceeded, true if the process has been stopped from another thread.</returns>
        private bool StabilizeJunction(double currentVoltage, double maxVoltage, double minVoltage)
        {
            double deviation = 0;
            double oldDev1 = 0, oldDev2 = 0, oldDev3 = 0, oldDev4 = 0, oldDev5 = 0;
            double diff = 0;

            //
            // The moment we entered this function, try to stop the junction from closing by opening a little.
            //
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = 5;
            m_electroMagnet.MoveMultipleSteps(5);

            //
            // this loop runs until the thread is stopped from the background, 
            // or until one of the limits of the EM voltage is exceeded.
            //
            while (!m_quitJunctionOpenningOperation)
            {
                currentVoltage = AnalogIn(0);
                if (currentVoltage > 10)
                {
                    currentVoltage = 10;
                }
                if (currentVoltage < -10)
                {
                    currentVoltage = -10;
                }
                //
                // save older deviations
                //
                oldDev5 = oldDev4;
                oldDev4 = oldDev3;
                oldDev3 = oldDev2;
                oldDev2 = oldDev1;
                oldDev1 = deviation;
                deviation = 0;

                //
                // if voltage at the junction is too low, move the electrodes one step closer
                //
                if (Math.Abs(currentVoltage) < Math.Abs(minVoltage))
                {
                    //
                    // calculate the deviation from target voltage. deviation is negative. 
                    //
                    deviation = Math.Abs(currentVoltage) - Math.Abs(minVoltage);

                    //
                    // calculate the derivative in order to know the slop of the trace. we average on 3 dots. 
                    //
                    diff = 0.333 * (oldDev2 + oldDev1 + deviation - oldDev5 - oldDev4 - oldDev3);

                    m_electroMagnet.Direction = StepperDirection.DOWN;
                    if (!m_electroMagnet.MoveSingleStep())
                    {
                        //
                        // if max Votlage on EM was exceeded return false.
                        //
                        return false;
                    }
                }

                //
                // if voltage at the junction is too high, open the junction one step
                //
                if (Math.Abs(currentVoltage) > Math.Abs(maxVoltage))
                {
                    //
                    // calculate the deviation from target voltage
                    //
                    deviation = Math.Abs(currentVoltage) - Math.Abs(maxVoltage);

                    //
                    // calculate the derivative in order to know the slop of the trace.
                    // if it's negative its good since we want the voltage to go down. 
                    // so we take the minus of diff.
                    //
                    diff = -0.333 * (oldDev2 + oldDev1 + deviation - oldDev5 - oldDev4 - oldDev3);

                    m_electroMagnet.Direction = StepperDirection.UP;
                    if (!m_electroMagnet.MoveSingleStep())
                    {
                        //
                        // if min Votlage on EM was exceeded return false.
                        //
                        return false;
                    }
                }

                //
                // deviation is between 0-10. that means: time delay between 13-55 ms. 
                // the larger the deviation, the faster we move (=> shorter time delay) 
                // the values of diff can change between (+10) - (-10). if it's positive we're good and want to step slower. if negative - we want to step faster.
                //
                Thread.Sleep((int)(diff + 5 + 100 / (Math.Abs(deviation) + 2)));
            }
            return true;
        }

        /// <summary>
        /// End junction openning by EM
        /// </summary>
        /// <param name="asyncResult"></param>
        private void EMEndOpenJunction(IAsyncResult asyncResult)
        {
            EMOpenJunctionMethodDelegate emOpenJunctionDelegate = (EMOpenJunctionMethodDelegate)asyncResult.AsyncState;
            emOpenJunctionDelegate.EndInvoke(asyncResult);
        }

        /// <summary>
        /// Try obtain short circuit by ElectroMagnet, by calling EMShortCircuit.
        /// If max EM voltage was exceeded without getting a short circuit, use the stepper motor and retry by recurcion. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool EMTryObtainShortCircuit(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            switch (EMShortCircuit(settings.ElectromagnetSettings.EMShortCircuitDelayTime, settings.GeneralSettings.ShortCircuitVoltage, worker, e))
            {
                case 0:
                    //
                    // short contact succeeded. 
                    //
                    return false;

                case 1:
                    //
                    // the proccess was cancelled by the user.
                    //
                    return true;

                case 2:
                    //
                    // we've reached the max voltage on the EM without getting to contact.
                    // return voltage to zero and get the electrodes closer by the stepper motor, 
                    // then start over again. 
                    //
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, 6.5);
                    m_stepperMotor.Direction = StepperDirection.DOWN;
                    m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
                    m_stepperMotor.Delay = m_stepperMotor.MinDelay;
                    m_stepperMotor.MoveMultipleSteps(100);
                    m_stepperMotor.Shutdown();
                    return EMTryObtainShortCircuit(settings, worker, e);
            }
            return true;
        }

        /// <summary>
        /// Try and obtain short circut by the ElectroMagnet
        /// </summary>
        /// <param name="shortCircuitVoltage">The voltage indicator for short circuit</param>        
        /// <returns>2 whether max EM voltage was exceeded. 1 whether the operation was cancelled. 0 otherwise</returns>
        private int EMShortCircuit(int shortCircuitDelayTime, double shortCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentShortCircuit = false;
            bool isTempShortCircuit = false;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set EM direction and delay
            // 
            m_electroMagnet.Direction = StepperDirection.DOWN;
            m_electroMagnet.Delay = shortCircuitDelayTime;

            //
            // Reach to contact
            //
            while (!isPermanentShortCircuit)
            {
                //
                // If the backgroundworker requested cancellation - exit
                //
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                //
                // Move down 10 steps and check the voltage afterwords.
                // if the EM exceeded max voltage return 2.
                //
                if (!m_electroMagnet.MoveMultipleSteps(10))
                {
                    return 2;
                }
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If we reach contact both current voltage and voltgae after stepping
                // should be smaller than the short circuit threshold (since voltage is negative)
                //
                isTempShortCircuit = (currentVoltage > Math.Abs(shortCircuitVoltage)) &&
                                     (voltageAfterStepping > shortCircuitVoltage);

                //
                // If we think we've reached short circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempShortCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentShortCircuit = currentVoltage > Math.Abs(shortCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }
            return (e.Cancel ? 1 : 0);
        }
  
    }
}
