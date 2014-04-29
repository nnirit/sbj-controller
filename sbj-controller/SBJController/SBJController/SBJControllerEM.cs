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
        #region Members
        private ElectroMagnet m_electroMagnet;        
        private const double c_initialEMVoltage = 6.5;
        private delegate bool EMOpenJunctionMethodDelegate(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e);
        private delegate bool EMCloseJunctionMethodDelegate(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e);

        #endregion

        #region Properties
        public ElectroMagnet ElectroMagnet
        {
            get { return m_electroMagnet; }
            set { m_electroMagnet = value; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Open the junction asynchronously by the ElectroMagnet
        /// </summary>
        /// <param name="settings"></param>       
        private void EMBeginOpenJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            EMOpenJunctionMethodDelegate emOpenJunctionDelegate = new EMOpenJunctionMethodDelegate(EMTryOpenJunction);
            AsyncCallback callback = new AsyncCallback(EMEndOpenJunction);
            IAsyncResult asyncResult = emOpenJunctionDelegate.BeginInvoke(settings, worker, e, callback, emOpenJunctionDelegate);
        }

        /// <summary>
        /// Close the junction asynchronously by the ElectroMagnet
        /// </summary>
        /// <param name="settings"></param>       
        private void EMBeginCloseJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            EMCloseJunctionMethodDelegate emCloseJunctionDelegate = new EMCloseJunctionMethodDelegate(EMTryCloseJunction);
            AsyncCallback callback = new AsyncCallback(EMEndCloseJunction);
            IAsyncResult asyncResult = emCloseJunctionDelegate.BeginInvoke(settings, worker, e, callback, emCloseJunctionDelegate);
        }

        /// <summary>
        /// Applies initial voltage on ElectroMagnet if it's about to be used. 
        /// It prevents the situation the EM reaches 0 voltage without the junction opening.
        /// </summary>
        /// <param name="isEMEnabled"></param>
        private void ApplyVoltageOnElectroMagnetIfNeeded(bool isEMEnabled)
        {
            if (isEMEnabled)
            {
                m_electroMagnet.SetVoltage(c_initialEMVoltage);
            }
        }

        /// <summary>
        /// Try open junction by the EM, by calling EMOpenJunction.
        /// if min voltage exceeded without the junction being opened, do a few steps by the stepper motor, then retry EM (recursion).
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool EMTryOpenJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // if the EM reached voltage 0 without opening the junction, 
            // return to higher voltage on EM, do some steps by the stepper motor and retry opening by EM.
            //
            if (!EMOpenJunction(settings, worker, e))
            {
                m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);                
                MoveStepsByStepperMotor(StepperDirection.UP, 20);
                return EMTryOpenJunction(settings, worker, e);
            }
            return true;
        }

        /// <summary>
        /// Try close junction by the EM, by calling EMCloseJunction.
        /// if max voltage exceeded without the junction being closed, do a few steps by the stepper motor, then retry EM (recursion).
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool EMTryCloseJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double initialValue = Math.Abs(AnalogIn(0));
            while (initialValue == 0.0)
            {
                initialValue = Math.Abs(AnalogIn(0));
            }

            //
            // if the EM reached max voltage  without closing the junction, 
            // return to lower voltage on EM, do some steps by the stepper motor and retry opening by EM.
            //
            if (!EMCloseJunction(settings, worker, e))
            {
                double currentValue = Math.Abs(AnalogIn(0));
                m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                if (currentValue > initialValue*2)
                {
                    MoveStepsByStepperMotor(StepperDirection.DOWN, 50);
                }
                else
                {
                    MoveStepsByStepperMotor(StepperDirection.DOWN, 200);
                }
                return EMTryCloseJunction(settings, worker, e);
            }
            return true;
        }

        /// <summary>
        /// Open the junction by the ElectroMagnet.
        /// If min voltage exceeded without the junction being opened, return false. 
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private bool EMOpenJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // Set the direction of the movement
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
                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

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
        /// Close the junction by the ElectroMagnet.
        /// If max voltage exceeded without the junction being closed, return false. 
        /// </summary>
        /// <param name="settings">The settings to be used to open the junction</param>
        private bool EMCloseJunction(SBJControllerSettings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //
            // Set the direction of the movement
            // And configure the first setpper delay (shorter) - faster movement
            //
            m_electroMagnet.Direction = StepperDirection.DOWN;
            m_electroMagnet.Delay = settings.ElectromagnetSettings.EMFastDelayTime;


            //
            // Set false to stop operation flag
            // Keep polling the value until it is abit different than 0.
            // This is a problem of instability with Flaxer's card.
            //
            double initialVoltage = AnalogIn(0);
            while (initialVoltage == 0.0)
            {
                initialVoltage = AnalogIn(0);
            }
            m_quitJunctionClosingOperation = false;
            bool isDelayedChanged = false;    

            //
            // Until we've not been signaled from outer thread to stop we'll continue moving up.
            //
            while (!m_quitJunctionClosingOperation)
            {
                //
                // Cancel the operatin if user asked for
                //
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

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
                // The two numbers must be of the same sign in order for us to compare them
                //
                if (!isDelayedChanged && (currentVoltage * initialVoltage > 0) && (Math.Abs(currentVoltage) > Math.Abs(initialVoltage) * 10))
                {
                    m_electroMagnet.Delay = settings.ElectromagnetSettings.EMSlowDelayTime;
                    isDelayedChanged = true;
                }

                //
                // If hold-on trigger was exceeded, wait 10 ms and check if still true.
                //
                if (settings.ElectromagnetSettings.IsEMHoldOnEnable && Math.Abs(currentVoltage) > Math.Abs(settings.ElectromagnetSettings.EMHoldOnMaxVoltage * 1.1))
                {
                    Thread.Sleep(10);
                    if (Math.Abs(currentVoltage) > Math.Abs(settings.ElectromagnetSettings.EMHoldOnMaxVoltage * 1.1))
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
        /// End junction closing by EM
        /// </summary>
        /// <param name="asyncResult"></param>
        private void EMEndCloseJunction(IAsyncResult asyncResult)
        {
            EMCloseJunctionMethodDelegate emCloseJunctionDelegate = (EMCloseJunctionMethodDelegate)asyncResult.AsyncState;
            emCloseJunctionDelegate.EndInvoke(asyncResult);
        }

        /// <summary>
        /// Try obtain short circuit by ElectroMagnet, by calling EMShortCircuit.
        /// If max EM voltage was exceeded without getting a short circuit, use the stepper motor and retry by recurcion. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool EMTryObtainShortCircuit(int shortCircuitDelayTime, double shortCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            switch (EMShortCircuit(shortCircuitDelayTime, shortCircuitVoltage, worker, e))
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
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                    MoveStepsByStepperMotor(StepperDirection.DOWN, 300);
                    return EMTryObtainShortCircuit(shortCircuitDelayTime, shortCircuitVoltage, worker, e);
            }
            return true;
        }

        /// <summary>
        /// Try obtain open circuit by ElectroMagnet, by calling EMOpenCircuit.
        /// If max min voltage was exceeded without getting an open circuit, use the stepper motor and retry by recurcion. 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool EMTryObtainOpenCircuit(int openCircuitDelayTime, double openCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double initialValue = Math.Abs(AnalogIn(0));
            switch (EMOpenCircuit(openCircuitDelayTime, openCircuitVoltage, worker, e))
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
                    // we've reached the min voltage on the EM without getting open circuit position.
                    // return voltage to zero and get the electrodes closer by the stepper motor, 
                    // then start over again. 
                    //
                    double currentVoltage = Math.Abs(AnalogIn(0));
                    m_electroMagnet.ReachEMVoltageGradually(m_electroMagnet.MinDelay, c_initialEMVoltage);
                    if (currentVoltage < initialValue)
                    {
                        MoveStepsByStepperMotor(StepperDirection.UP, 50);
                    }
                    else
                    {
                        MoveStepsByStepperMotor(StepperDirection.UP, 500);
                    }
                    return EMTryObtainOpenCircuit(openCircuitDelayTime, openCircuitVoltage, worker, e);
            }
            return true;
        }

        /// <summary>
        /// Try and obtain open circut by the ElectroMagnet
        /// </summary>
        /// <param name="shortCircuitVoltage">The voltage indicator for open circuit</param>        
        /// <returns>2 whether min EM voltage was exceeded. 1 whether the operation was cancelled. 0 otherwise</returns>
        private int EMOpenCircuit(int openCircuitDelayTime, double openCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentOpenCircuit = false;
            bool isTempOpenCircuit = false;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set EM direction and delay
            // 
            m_electroMagnet.Direction = StepperDirection.UP;
            m_electroMagnet.Delay = openCircuitDelayTime;

            //
            // Reach to open circuit
            //
            while (!isPermanentOpenCircuit)
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
                // Move up 50 steps and check the voltage afterwords.
                // if the EM exceeded min voltage return 2.
                //
                if (!m_electroMagnet.MoveMultipleSteps(50))
                {
                    return 2;
                }
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If we reach open circuit both current voltage and voltgae after stepping
                // should be smaller than the open circuit threshold
                //
                isTempOpenCircuit = (currentVoltage < Math.Abs(openCircuitVoltage)) &&
                                     (voltageAfterStepping < openCircuitVoltage);

                //
                // If we think we've reached open circuit then wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempOpenCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentOpenCircuit = currentVoltage < Math.Abs(openCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }
            return (e.Cancel ? 1 : 0);
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
                if (!m_electroMagnet.MoveMultipleSteps(50))
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

        /// <summary>
        /// Move steps by stepper motor with minDelay and Full mode, and shut it down afterwards.
        /// </summary>
        /// <param name="stepperDirection"></param>
        /// <param name="numberOfSteps"></param>
        private void MoveStepsByStepperMotor(StepperDirection stepperDirection, int numberOfSteps)
        {
            m_stepperMotor.Direction = stepperDirection;
            m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
            m_stepperMotor.Delay = m_stepperMotor.MinDelay;
            m_stepperMotor.MoveMultipleSteps(numberOfSteps);
            m_stepperMotor.Shutdown();
        }

        /// <summary>
        /// Obtain open circuit by stepper motor (returns when open circuit acheived).
        /// This function does NOT wait for an outside sign! 
        /// </summary>
        /// <param name="openCircuitVoltage">the "trigger" voltage</param>
        /// <param name="worker">background worker</param>
        /// <param name="e">event of backgraound worker</param>
        /// <returns></returns>
        private bool ObtainOpenJunctionByStepperMotor(double openCircuitVoltage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            double voltageAfterStepping;
            bool isPermanentOpenCircuit = false;
            bool isTempOpenCircuit = false;

            //
            // Get current voltage
            //
            double currentVoltage = Math.Abs(AnalogIn(0));

            //
            // Set stepper direction, mode and delay
            // 
            m_stepperMotor.Direction = StepperDirection.UP;
            m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
            m_stepperMotor.Delay = m_stepperMotor.MinDelay;

            //
            // Open the junction
            //
            while (!isPermanentOpenCircuit)
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
                // Move up 5 steps and check the voltage afterwards
                //
                m_stepperMotor.MoveMultipleSteps(5);
                Thread.Sleep(m_stepperMotor.Delay*20);
                voltageAfterStepping = Math.Abs(AnalogIn(0));

                //
                // If the junction is open, both current voltage and voltgae after stepping
                // should be smaller than the open circuit threshold.
                //
                isTempOpenCircuit = (currentVoltage < Math.Abs(openCircuitVoltage)) &&
                                     (voltageAfterStepping < Math.Abs(openCircuitVoltage));

                //
                // If we think we've reached open circuit than wait
                // for 10msec and than check again to verify this is permanent.
                //
                if (isTempOpenCircuit)
                {
                    Thread.Sleep(10);
                    currentVoltage = Math.Abs(AnalogIn(0));
                    isPermanentOpenCircuit = currentVoltage < Math.Abs(openCircuitVoltage);
                }
                else
                {
                    currentVoltage = voltageAfterStepping;
                }
            }
            return e.Cancel;
        }
        #endregion
    }
}
