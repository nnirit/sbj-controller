using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SBJController
{
    public partial class StepperMotorUserControl : UserControl
    {
        private StepperMotor m_stepperMotor;

        public StepperMotorUserControl()
        {
            InitializeComponent();
            m_stepperMotor = new StepperMotor();
            if (fullStepRadioButton.Checked)
            {
                m_stepperMotor.SteppingMode = StepperSteppingMode.FULL;
            }
            else
            {
                if (halfStepRadioButton.Checked)
                {
                    m_stepperMotor.SteppingMode = StepperSteppingMode.HALF;
                }
            }
            if (stepperDirectionSwitch.Value == true)
            {
                m_stepperMotor.Direction = StepperDirection.UP;
            }
            else
            {
                m_stepperMotor.Direction = StepperDirection.DOWN;
            }
        }

        private void runContinuouslyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runContinuouslyCheckBox.Checked)
            {
                if (!runContinuouslyBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    runContinuouslyCheckBox.Text = "Stop";
                    runCheckBox.Enabled = false;
                    stepperDirectionSwitch.Enabled = false;
                    halfStepRadioButton.Enabled = false;
                    fullStepRadioButton.Enabled = false;
                    runContinuouslyBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start running." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                if (runContinuouslyBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    runContinuouslyBackgroundWorker.CancelAsync();
                }
            }
        }
              
        private void runContinuouslyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!worker.CancellationPending)
            {
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(2);
            }
        }

        private void runContinuouslyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runContinuouslyCheckBox.Text = "Run Continuously";
            runCheckBox.Enabled = true;
            stepperDirectionSwitch.Enabled = true;
            halfStepRadioButton.Enabled = true;
            fullStepRadioButton.Enabled = true;
        }

        private void stepperDirectionSwitch_StateChanged(object sender, NationalInstruments.UI.ActionEventArgs e)
        {
            m_stepperMotor.Direction = stepperDirectionSwitch.Value ? StepperDirection.UP : StepperDirection.DOWN;
        }

        private void halfStepRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            m_stepperMotor.SteppingMode = halfStepRadioButton.Checked ? StepperSteppingMode.HALF : StepperSteppingMode.FULL;
        }

        private void fullStepRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            m_stepperMotor.SteppingMode = fullStepRadioButton.Checked ? StepperSteppingMode.FULL : StepperSteppingMode.HALF;
        }

        private void runCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runCheckBox.Checked)
            {
                if (!runContinuouslyBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    runCheckBox.Text = "Stop";
                    runContinuouslyCheckBox.Enabled = false;
                    stepperDirectionSwitch.Enabled = false;
                    halfStepRadioButton.Enabled = false;
                    fullStepRadioButton.Enabled = false;
                    runBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start running." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                if (runBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    runBackgroundWorker.CancelAsync();
                }
            }
        }

        private void runBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            int totalNumberOfSteps = (int) this.numberOfStepsNumericUpDown.Value;
            BackgroundWorker worker = sender as BackgroundWorker;
            while ((!worker.CancellationPending) && (i < totalNumberOfSteps))
            {
                m_stepperMotor.MoveSingleStep();
                Thread.Sleep(2);
                i++;
            }            
        }

        private void runBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runCheckBox.Text = "Run";           
            runContinuouslyCheckBox.Enabled = true;
            stepperDirectionSwitch.Enabled = true;
            halfStepRadioButton.Enabled = true;
            fullStepRadioButton.Enabled = true;
            runCheckBox.Checked = false;
        }
    }
}
