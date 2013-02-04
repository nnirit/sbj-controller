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
    public partial class ElectroMagnetUserControl : UserControl
    {
        private ElectroMagnet m_electroMagnet;
           
        public ElectroMagnetUserControl()
        {
            InitializeComponent();
            m_electroMagnet = new ElectroMagnet();
            m_electroMagnet.Direction = EMDirectionSwitch.Value ? StepperDirection.UP : StepperDirection.DOWN; 
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
                    
                    //
                    //make buttons unable
                    //
                    runStepsCheckBox.Enabled = false;
                    EMDirectionSwitch.Enabled = false;
                    manualVoltageNumericEdit.Enabled = false;
                    delayNumericUpDown.Enabled = false;
                    numberOfStepsNumericUpDown.Enabled = false;
                    manualVoltageCheckBox.Enabled = false;
                       
                    //
                    //run background worker
                    //
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
            int i = 0;
            int delayTime = (int)this.delayNumericUpDown.Value;
            BackgroundWorker worker = sender as BackgroundWorker;

            //
            //In case the user changed manually the voltage to be negative, ask them to turn it back to positive. 
            //else: run.
            //
            if (m_electroMagnet.CurrentEMVoltage < 0)
            {
                MessageBox.Show("Please set the voltage to a positive value.");
            }
            else
            {
                while ((!worker.CancellationPending) && (m_electroMagnet.Direction != StepperDirection.STATIC))
                {
                    if (!m_electroMagnet.MoveSingleStep())
                    {
                        break;
                    }
                    this.EMVoltageMeter.Value = m_electroMagnet.CurrentEMVoltage;
                    Thread.Sleep(delayTime);
                    i++;
                }
            }       
        }

        private void runStepsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runStepsCheckBox.Checked)
            {
                if (!runStepsBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    runStepsCheckBox.Text = "Stop";

                    //
                    //make buttons unable
                    //
                    runContinuouslyCheckBox.Enabled = false;
                    EMDirectionSwitch.Enabled = false;
                    manualVoltageNumericEdit.Enabled = false;
                    delayNumericUpDown.Enabled = false;
                    numberOfStepsNumericUpDown.Enabled = false;
                    manualVoltageCheckBox.Enabled = false;

                    //
                    //run background worker
                    //
                    runStepsBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start running." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                if (runStepsBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    runStepsBackgroundWorker.CancelAsync();
                }
            }
        }

        private void runStepsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            int totalNumberOfSteps = (int)this.numberOfStepsNumericUpDown.Value;
            int delayTime = (int)this.delayNumericUpDown.Value;
            BackgroundWorker worker = sender as BackgroundWorker;

            //
            //In case the user changed manually the voltage to be negative, ask them to turn it back to positive. 
            //else: run.
            //
            if (m_electroMagnet.CurrentEMVoltage < 0)
            {
                MessageBox.Show("Please set the voltage to a positive value.");
            }
            else
            {
                while ((!worker.CancellationPending) && (i < totalNumberOfSteps) && (m_electroMagnet.Direction != StepperDirection.STATIC))
                {
                    if (!m_electroMagnet.MoveSingleStep())
                    {
                        break;
                    }
                    this.EMVoltageMeter.Value = m_electroMagnet.CurrentEMVoltage;
                    Thread.Sleep(delayTime);
                    i++;
                }
            }
        }

        private void runStepsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runStepsCheckBox.Text = "Run Steps";
            runStepsCheckBox.Checked = false;
            manualVoltageNumericEdit.Value = m_electroMagnet.CurrentEMVoltage;
            
            //
            //enable buttons   
            //
            runContinuouslyCheckBox.Enabled = true;
            EMDirectionSwitch.Enabled = true;
            manualVoltageNumericEdit.Enabled = manualVoltageCheckBox.Checked ? true : false;
            delayNumericUpDown.Enabled = true;
            numberOfStepsNumericUpDown.Enabled = true;
            manualVoltageCheckBox.Enabled = true;
        }

        private void runContinuouslyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runContinuouslyCheckBox.Text = "Run Continuously";
            runContinuouslyCheckBox.Checked = false;
            manualVoltageNumericEdit.Value = m_electroMagnet.CurrentEMVoltage;
            
            //
            //enable buttons
            //
            runStepsCheckBox.Enabled = true;
            EMDirectionSwitch.Enabled = true;
            manualVoltageNumericEdit.Enabled = manualVoltageCheckBox.Checked ? true : false;
            delayNumericUpDown.Enabled = true;
            numberOfStepsNumericUpDown.Enabled = true;
            manualVoltageCheckBox.Enabled = true;
        }

        private void EMDirectionSwitch_StateChanged(object sender, NationalInstruments.UI.ActionEventArgs e)
        {
            m_electroMagnet.Direction = EMDirectionSwitch.Value ? StepperDirection.UP : StepperDirection.DOWN;
        }

        private void manualVoltageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (manualVoltageCheckBox.Checked)
            {
                manualVoltageNumericEdit.Enabled = true;
            }
            else
            {                               
                manualVoltageNumericEdit.Enabled = false;         
            }
        }

        private void manualVoltageNumericEdit_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        {
            m_electroMagnet.SetVoltage(e.NewValue);
            EMVoltageMeter.Value = m_electroMagnet.CurrentEMVoltage;
        }
    } 
}
