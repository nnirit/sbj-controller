using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.Drawing;
using NationalInstruments.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SBJController.Properties;

namespace SBJController
{
    public partial class SBJControllerMainForm : Form
    {
        #region Private Members
        private SBJController m_sbjController;
        private double m_1G0 = 77.5E-6;
        private double m_maxVoltage = 10.0;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SBJControllerMainForm()
        {           
            InitializeComponent();
            m_sbjController = new SBJController();
            m_sbjController.DataAquired += new SBJController.DataAquiredEventHandler(OnDataAquisition);
            this.bottomPropertyGrid.SelectedObject = new Sample();
            this.topPropertyGrid.SelectedObject = new Sample();
            PopulateChannelsList();
        }
       
        #endregion

        #region UI Event Handlers

        #region ShortCircuit Handlers

        /// <summary>
        /// Fired when short circuit button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shortCircuitButton_CheckedChanged(object sender, EventArgs e)
        {
            if (shortCircuitCheckBoxButton.Checked)
            {
                //
                // Short Circuit request.
                // Verify first that the worker is nor busy otherwise this means that it is a double request.
                //
                if (!obtainShortCircuitBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text and behavior of other related controls
                    //
                    shortCircuitCheckBoxButton.Text = "Stop";
                    startStopCheckBoxButton.Enabled = false;
                    moveUpCheckBoxButton.Enabled = false;

                    //
                    // Do work async
                    //
                    obtainShortCircuitBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start Short Circuit operation." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                //
                // If we reached here that means that we were requested to stop the short circuit.
                //
                if (obtainShortCircuitBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    obtainShortCircuitBackgroundWorker.CancelAsync();
                }
            }
        }

        /// <summary>
        /// The short circuit async thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obtainShortCircuitBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            m_sbjController.TryObtainShortCircuit((double)shortCircuitVoltageNumericUpDown.Value, worker, e);
        }

        /// <summary>
        /// The callback for the short circuit operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obtainSHortCircuitBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // We've reached short circuit or we were requested to stop
            // So let's bring back the appearance of the UI.
            //
            shortCircuitCheckBoxButton.Text = "Short Circuit";
            shortCircuitCheckBoxButton.Checked = false;
            startStopCheckBoxButton.Enabled = true;
            moveUpCheckBoxButton.Enabled = true;
        }
        #endregion

        #region StartStopDataAcquisition Handlers

        /// <summary>
        /// Fired when the Start button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startStopButton_CheckedChanged(object sender, EventArgs e)
        {
            if (startStopCheckBoxButton.Checked)
            {
                //
                // We were requested to start data acquisition so we must verify
                // first that the worker is free for doing the job.
                //
                if (!aquireDataBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text and UI appearance
                    //
                    startStopCheckBoxButton.Text = "Stop";
                    shortCircuitCheckBoxButton.Enabled = false;
                    moveUpCheckBoxButton.Enabled = false;
                    generalSettingsPanel.Enabled = false;
                    laserSettingsPanel.Enabled = false;
                    lockInPanel.Enabled = false;
                    electroMagnetSettingsPanel.Enabled = false;
                    channelsSettingsPanel.Enabled = false;
                    aquireDataBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start data aquisition operation." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                //
                // We were requested to stop data acquisition process
                //
                if (aquireDataBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    aquireDataBackgroundWorker.CancelAsync();
                }
                if (m_sbjController.Task != null)
                {
                    m_sbjController.Task.Control(TaskAction.Abort);
                }
                m_sbjController.QuitJunctionOpenningOperation = true;
                m_sbjController.StepperMotor.Shutdown();
            }
        }

        /// <summary>
        /// The callback for the data acquisition background worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aquireDataBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // We've done taking data so we must bring the UI appearance
            //
            startStopCheckBoxButton.Text = "Start";
            startStopCheckBoxButton.Checked = false;
            shortCircuitCheckBoxButton.Enabled = true;
            moveUpCheckBoxButton.Enabled = true;
            generalSettingsPanel.Enabled = true;
            laserSettingsPanel.Enabled = true;
            lockInPanel.Enabled = true;
            channelsSettingsPanel.Enabled = true;
            electroMagnetSettingsPanel.Enabled = true;
        }

        /// <summary>
        /// The aquireDataBackgroundWorker main thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aquireDataBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            m_sbjController.AquireData(GetSBJControllerSettings(), worker, e);
        }
        #endregion

        #region Stepper Up Handlers

        /// <summary>
        /// Fired when the stepper is requested to move up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.moveUpCheckBoxButton.Checked)
            {
                if (!stepperUpBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    this.moveUpCheckBoxButton.Text = "Stop";
                    this.shortCircuitCheckBoxButton.Enabled = false;
                    this.startStopCheckBoxButton.Enabled = false;
                    this.stepperUpBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start move stepper up." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                this.stepperUpBackgroundWorker.CancelAsync();
            }
        }

        /// <summary>
        /// stepperUpBackgroundWorker main async thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stepperUpBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            m_sbjController.MoveStepperUp(worker, e);
        }

        private void stepperUpBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Bring back the appearance
            //
            moveUpCheckBoxButton.Text = "Stepper Up";
            this.shortCircuitCheckBoxButton.Enabled = true;
            this.startStopCheckBoxButton.Enabled = true;
        }
        #endregion

        #region Fix Bias Handlers
        private void fixBiasCheckBoxButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fixBiasCheckBoxButton.Checked)
            {
                if (!fixBiasBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    this.fixBiasCheckBoxButton.Text = "Stop";
                    this.shortCircuitCheckBoxButton.Enabled = false;
                    this.startStopCheckBoxButton.Enabled = false;
                    this.moveUpCheckBoxButton.Enabled = false;
                    this.fixBiasBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start bias fixing." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                this.fixBiasBackgroundWorker.CancelAsync();
            }
        }

        private void fixBiasBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            m_sbjController.FixBias((double)shortCircuitVoltageNumericUpDown.Value,(double)biasNumericEdit.Value, worker, e);
        }

        private void fixBiasBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // Bring back the appearance
            //
            fixBiasCheckBoxButton.Text = "Fix Bias";
            fixBiasCheckBoxButton.Checked = false;
            this.shortCircuitCheckBoxButton.Enabled = true;
            this.startStopCheckBoxButton.Enabled = true;
            this.moveUpCheckBoxButton.Enabled = true;
            if (!e.Cancelled)
            {
                this.biasErrorNumericEdit.Value = ((Bias)e.Result).Error;
                this.biasNumericEdit.Value = (double)this.biasNumericEdit.Value * ((Bias)e.Result).Sign;
                this.biasErrorLabel.ForeColor = Color.Black;

            }
            m_sbjController.SourceMeter.SetBias(this.biasNumericEdit.Value + this.biasErrorNumericEdit.Value);
        }
        #endregion

        #region ElectroMagnet Tab Events
        /// <summary>
        /// Enable or disable the ElectroMagnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableElectroMagnetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //
            // Update the appearance of other UI related parameters.
            //
            this.emFastDelayTimeLabel.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emFastDelayTimeNumericUpDown.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emSlowDelayTimeLabel.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emSlowDelayTimeNumericUpDown.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emShortCircuitDelayTimeLabel.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emShortCircuitDelayTimeNumericUpDown.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emHoldOnToConductanceRangeCheckBox.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emSkipFirstCycleByStepperMotorCheckBox.Enabled = this.enableElectroMagnetCheckBox.Checked;
            this.emHoldOnMaxConductanceLabel.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMaxConductanceNumericEdit.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMaxVoltageLabel.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMaxVoltageNumericEdit.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMinConductanceLabel.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMinConductanceNumericEdit.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMinVoltageLabel.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
            this.emHoldOnMinVoltageNumericEdit.Enabled = (this.enableElectroMagnetCheckBox.Checked && this.emHoldOnToConductanceRangeCheckBox.Checked);
        }

        private void holdOnToConductanceRangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.emHoldOnMaxConductanceLabel.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMaxConductanceNumericEdit.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMaxVoltageLabel.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMaxVoltageNumericEdit.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMinConductanceLabel.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMinConductanceNumericEdit.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMinVoltageLabel.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
            this.emHoldOnMinVoltageNumericEdit.Enabled = this.emHoldOnToConductanceRangeCheckBox.Checked;
        }

        private void emHoldOnMaxConductanceNumericEdit_AfterChangeValue(object sender, AfterChangeNumericValueEventArgs e)
        {
            this.emHoldOnMaxVoltageNumericEdit.Value = GetVoltageFromConductnace(this.emHoldOnMaxConductanceNumericEdit.Value);
        }

        private void emHoldOnMinConductanceNumericEdit_AfterChangeValue(object sender, AfterChangeNumericValueEventArgs e)
        {
            this.emHoldOnMinVoltageNumericEdit.Value = GetVoltageFromConductnace(this.emHoldOnMinConductanceNumericEdit.Value);
        }
        #endregion

        #region General Controls' Events
        /// <summary>
        /// On gain changed. Make sure amplifier is updated accordingly and other UI parameters as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gainComboBox_ValueChanged(object sender, EventArgs e)
        {
            int gainPower = int.Parse(this.gainComboBox.Text);
            m_sbjController.ChangeGain(gainPower);
            this.triggerConductanceNumericEdit.Value = GetTriggerConductance();
            this.triggerVoltageNumericEdit.Value = -this.triggerConductanceNumericEdit.Value * m_1G0 * Math.Abs(this.biasNumericEdit.Value) * Math.Pow(10, gainPower);            
            this.emHoldOnMaxConductanceNumericEdit.Value = 100 * GetTriggerConductance();
            this.emHoldOnMinConductanceNumericEdit.Value = 50 * GetTriggerConductance();
        }

        /// <summary>
        /// Update UI parameters and Keithley once bias is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void biasNumericEdit_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        {
            //
            // This event is also fired when the UI loads on start.
            // At that point sbjController is still null and we need to verify this.
            //
            if (m_sbjController != null)
            {
                m_sbjController.SourceMeter.SetBias(this.biasNumericEdit.Value + this.biasErrorNumericEdit.Value);
            }

            //
            // If bias had changed sign it is recommanded to run fix bias again.
            //
            if (e.NewValue * e.OldValue < 0)
            {
                this.biasErrorLabel.ForeColor = Color.Red;
            }
            this.triggerConductanceNumericEdit.Value = GetTriggerConductance();
            this.triggerVoltageNumericEdit.Value = -this.triggerConductanceNumericEdit.Value * m_1G0 * Math.Abs(this.biasNumericEdit.Value) * Math.Pow(10, int.Parse(this.gainComboBox.Text));            
            this.emHoldOnMaxVoltageNumericEdit.Value = GetVoltageFromConductnace(this.emHoldOnMaxConductanceNumericEdit.Value); 
        }

        /// <summary>
        /// Enable or Disable the laser 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableLaserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.enableLaserCheckBox.Checked)
            {
                m_sbjController.Tabor.Connect();
            }
            else
            {
                m_sbjController.Tabor.SetLocalMode();
            }

            //
            // Update the appearance of other UI related parameters.
            //
            this.laserModeComboBox.Enabled = this.enableLaserCheckBox.Checked;
            this.amplitudeNumericUpDown.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeLabel.Enabled = this.enableLaserCheckBox.Checked;            
            laserModeComboBox_SelectedValueChanged(sender, e);
        }
        
        /// <summary>
        /// On tirgger conductance change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void triggerConductanceNumericEdit_AfterChangeValue_1(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        {
            this.triggerVoltageNumericEdit.Value = -this.triggerConductanceNumericEdit.Value * m_1G0 * Math.Abs(this.biasNumericEdit.Value) * Math.Pow(10, int.Parse(this.gainComboBox.Text));
        }

        /// <summary>
        /// Select path for saving files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            string initialPath = this.pathTextBox.Text;
            FolderBrowserDialog folderBroswer = new FolderBrowserDialog();

            //
            // Open the directory that currently written.
            // If doesn't exist just open the broswer from the current running path.
            //
            if (Directory.Exists(this.pathTextBox.Text))
            {
                folderBroswer.SelectedPath = this.pathTextBox.Text;
            }
            else
            {
                folderBroswer.SelectedPath = Environment.CurrentDirectory;
            }
            DialogResult dialogResult = folderBroswer.ShowDialog();

            //
            // Show the selected path.
            //
            if (dialogResult == DialogResult.OK)
            {
                this.pathTextBox.Text = folderBroswer.SelectedPath;
            }
            else
            {
                this.pathTextBox.Text = initialPath;
            }
        }    

        //
        // Save to sample log
        //
        private void saveSamplesParamsButton_Click(object sender, EventArgs e)
        {
            Sample bottom =  (Sample) this.bottomPropertyGrid.SelectedObject;
            Sample top = (Sample) this.topPropertyGrid.SelectedObject;
            m_sbjController.WriteToSamplesLog(bottom, top);
        }

        //
        // Open sample log
        // 
        private void openLogBookButton_Click(object sender, EventArgs e)
        {
            m_sbjController.OpenSamplesLog();
        }

        /// <summary>
        /// Fired when form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBJControllerMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_sbjController.ShutDown();
        }

        /// <summary>
        /// Fired when form first appear.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBJControllerMainForm_Shown(object sender, EventArgs e)
        {
            int gainPower = int.Parse(this.gainComboBox.Text);
            m_sbjController.ChangeGain(gainPower);
            m_sbjController.SourceMeter.SetBias(this.biasNumericEdit.Value + this.biasErrorNumericEdit.Value);
        }

        /// <summary>
        /// Update laser realted UI parameters once laser mode is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void laserModeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.frequencyLabel.Enabled = laserModeComboBox.Text.Equals("Square") && this.enableLaserCheckBox.Checked;
            this.frequencyNumericUpDown.Enabled = laserModeComboBox.Text.Equals("Square") && this.enableLaserCheckBox.Checked;
        }

        /// <summary>
        /// Set Laser amplitude
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void amplitudeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double amplitude = (double)this.amplitudeNumericUpDown.Value;
            m_sbjController.Tabor.SetDcModeAmplitude(amplitude);
        }

        /// <summary>
        /// sutting down the electroMagnet when leaving the controller tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controllerTabControl_Deselected(object sender, TabControlEventArgs e)
        {
            m_sbjController.ControllerTabControlDeselected();
        }

        /// <summary>
        /// On path changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            //
            // The folder in which we save the files has been changed, so we need to set the file number back to zero.
            //            
            this.fileNumberNumericUpDown.Value = 0;
        }

        /// <summary>
        /// On number of samples changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void totalSamplesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            //
            // the total number of samples was changed - the trigger usually should be after 85% of the samples.
            //
            pretriggerSamplesNumericUpDown.Value = (decimal)0.85 * totalSamplesNumericUpDown.Value;
        }

        /// <summary>
        /// On enable lock in 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableLockInCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.enableLockInCheckBox.Checked)
            {
                //
                // Turn on lock in with all its configuration
                //
                m_sbjController.LockIn.Connect();
                m_sbjController.LockIn.SetSensitivity(Double.Parse(this.sensitivityComboBox.Text));
                m_sbjController.LockIn.SetRollOff(int.Parse(this.rollOffComboBox.Text));
                m_sbjController.LockIn.SetTimeConstant(Double.Parse(this.timeConstantComboBox.Text));
            }
            else
            {
                //
                // Set lock in to local mode when unchcked
                //
                m_sbjController.LockIn.SetLocalMode();
            }
        }

        /// <summary>
        /// On sensitivity change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sensitivityComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_sbjController.LockIn.SetSensitivity(Double.Parse(this.sensitivityComboBox.Text));
        }

        /// <summary>
        /// On Time constant change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeConstantComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_sbjController.LockIn.SetTimeConstant(Double.Parse(this.timeConstantComboBox.Text));
        }

        /// <summary>
        /// Set roll off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rollOffComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_sbjController.LockIn.SetRollOff(int.Parse(this.rollOffComboBox.Text));
        }

        /// <summary>
        /// Ignore selection on channels list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channelsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                e.Item.Selected = false;
            }
        }

        /// <summary>
        /// Update UI appearnce when channels are checked \ unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel0ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (channel0CheckBox.Checked)
            {
                UpdateChannelsToDisplayListView();
            }
        }

        /// <summary>
        /// Update UI appearnce when channels are checked \ unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel1ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (channel1CheckBox.Checked)
            {
                UpdateChannelsToDisplayListView();
            }
        }

        /// <summary>
        /// Update UI appearnce when channels are checked \ unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel2ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (channel2CheckBox.Checked)
            {
                UpdateChannelsToDisplayListView();
            }
        }

        /// <summary>
        /// Update UI appearnce when channels are checked \ unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel3ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (channel3CheckBox.Checked)
            {
                UpdateChannelsToDisplayListView();
            }
        }

        /// <summary>
        /// Update the channel to display list appearance
        /// </summary>
        private void UpdateChannelsToDisplayListView()
        {
            foreach (ListViewItem channel in channelsListView.Items)
            {
                UpdateChannelAppearance(channel);
            }
        }

        /// <summary>
        /// Update channels list appearance according to the selected channels to sample
        /// </summary>
        /// <param name="channel"></param>
        private void UpdateChannelAppearance(ListViewItem channel)
        {
            //
            // LockInXYInternalSourceDataChannel is a special cas and doesn't obbey the rules
            //
            if (channel.Name.Equals(typeof(LockInXYInternalSourceDataChannel).Name))
            {
                return;
            }

            //
            // check if channel you wish to display is active
            //
            bool isChannelActive = IsChannelActive(channel.Text);

            //
            // If it is not active mark it in red to let the user know something is wrong
            // 
            if (channel.Checked && !isChannelActive)
            {
                channel.ForeColor = Color.Red;
            }

            //
            // Set the color back to black if channel is inactive
            //
            if (channel.Checked && isChannelActive)
            {
                channel.ForeColor = Color.Black;
            }

            //
            // Set the channel back to black if channle is not active anymore
            //
            if (!channel.Checked)
            {
                channel.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Update channel appearance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channelsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateChannelAppearance(e.Item);
        }

        /// <summary>
        /// Update channels appearance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChannelsToDisplayListView();
        }

        /// <summary>
        /// Update channels appearance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChannelsToDisplayListView();
        }

        /// <summary>
        /// Update channels appearance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChannelsToDisplayListView();
        }

        /// <summary>
        /// Update channels appearance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void channel0CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChannelsToDisplayListView();
        }      

        #endregion
        
        #endregion

        #region Private Methods

        /// <summary>
        /// On data acquisition update the UI with the trace.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataAquisition(object sender, DataAquiredEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DataAquiredEventArgs>(OnDataAquisition), sender, e);
            }
            else
            {
                //
                // Update file number
                //
                this.fileNumberNumericUpDown.Value = e.FileNumber;                

                //
                // Update plot
                //
                this.traceWaveformGraph.ClearData();
                List<IDataChannel> channelsToDisplay = GetChannelsToDisplay(e.PhysicalChannels as List<IDataChannel>);
                double[,] data = GetPhysicalData(channelsToDisplay);
                int numberOfChannels = data.GetLength(0);
                this.traceWaveformGraph.PlotYMultiple(data);
       
                if (numberOfChannels > 1)
                {
                    this.traceWaveformGraph.Plots[1].PointColor = Color.Blue;
                    this.traceWaveformGraph.Plots[1].YAxis = yAxis2;
                    this.traceWaveformGraph.Plots[1].ToolTipsEnabled = true;
                    this.traceWaveformGraph.Plots[1].LineColor = Color.Blue;                    
                }
                
                this.traceWaveformGraph.Caption = string.Format("Trace #{0} at {1}", this.fileNumberNumericUpDown.Value, DateTime.Now.TimeOfDay);        
            }
        }

        private List<IDataChannel> GetChannelsToDisplay(List<IDataChannel> physicalChannels)
        {
            List<IDataChannel> selectedChannels = new List<IDataChannel>();
            foreach (ListViewItem selectedChannel in channelsListView.CheckedItems)
            {
                IDataChannel physicalChannel = physicalChannels.Find(new Predicate<IDataChannel>(x => x.Name.Equals(selectedChannel.Name)));
                if (physicalChannel != null)
                {
                    selectedChannels.Add(physicalChannel);
                }
            }

            return selectedChannels;
        }

        private double[,] GetPhysicalData(IList<IDataChannel> physicalChannels)
        {
            List<double[]> physicalDataAsList = new List<double[]>();
            foreach (var channel in physicalChannels)
            {
                channel.ConvertToPhysicalData();
                physicalDataAsList.AddRange(channel.PhysicalData);
            }            

            return GetDataAsArray(physicalDataAsList);            
        }

        private double[,] GetDataAsArray(List<double[]> physicalDataAsList)
        {
            double[,] dataAsArray = new double[physicalDataAsList.Count, physicalDataAsList[0].Length];
            for (int i = 0; i < physicalDataAsList.Count; i++)
            {
                for (int j = 0; j < physicalDataAsList[i].Length; j++)
                {
                    dataAsArray[i, j] = physicalDataAsList[i][j];
                }
            }

            return dataAsArray;
        }

        /// <summary>
        /// Get settings from UI
        /// </summary>
        /// <returns></returns>
        private SBJControllerSettings GetSBJControllerSettings()
        {
            //
            // Using windows forms one must know that UI controls cannot be accessed from a different thread than
            // the thread the UI was created on. This is why everytime you try and reach a UI control you must
            // first verify that you are in the right thread.
            //
            if (this.InvokeRequired)
            {
                //
                // This tells us that we are not in the safe thread and so this function must be re-invoked
                // from an appropriate thread.
                //
                return (SBJControllerSettings) this.Invoke(new Func<SBJControllerSettings>(() => GetSBJControllerSettings()));
            }
            else
            {
                //
                // Apparently this function was called from a safe thread so just carry on with
                // what we were planning on doing.
                //
                return new SBJControllerSettings(new GeneralSBJControllerSettings(this.biasNumericEdit.Value,
                                                                                  this.biasErrorNumericEdit.Value,
                                                                                  this.gainComboBox.Text,
                                                                                  this.triggerVoltageNumericEdit.Value,
                                                                                  this.triggerConductanceNumericEdit.Value,
                                                                                  (int)this.sampleRateNumericUpDown.Value,
                                                                                  (int)this.totalSamplesNumericUpDown.Value,
                                                                                  (int)this.pretriggerSamplesNumericUpDown.Value,
                                                                                  (int)this.stepperWaitTime1NumericUpDown.Value,
                                                                                  (int)this.stepperWaitTime2NumericUpDown.Value,
                                                                                  this.fileSavingCheckBox.Checked,
                                                                                  this.pathTextBox.Text,
                                                                                  (int)this.fileNumberNumericUpDown.Value,
                                                                                  (int)this.numberOfCyclesnumericUpDown.Value,
                                                                                  (double)this.shortCircuitVoltageNumericUpDown.Value,
                                                                                  (Sample)this.bottomPropertyGrid.SelectedObject,
                                                                                  (Sample)this.bottomPropertyGrid.SelectedObject),
                                                  new LaserSBJControllerSettings(this.enableLaserCheckBox.Checked,
                                                                                 this.laserModeComboBox.SelectedItem.ToString(),
                                                                                 (double)this.amplitudeNumericUpDown.Value,
                                                                                 (int)this.frequencyNumericUpDown.Value),
                                                  new LockInSBJControllerSettings(this.enableLockInCheckBox.Checked,
                                                                                  this.internalSourceLockInCheckBoxcheckBox.Checked,
                                                                                  Double.Parse(this.sensitivityComboBox.Text),
                                                                                  Double.Parse(this.timeConstantComboBox.Text),
                                                                                  Double.Parse(this.rollOffComboBox.Text),
                                                                                  (double)this.lockInAcVoltageNumericEdit.Value,
                                                                                  (int)this.mixerReductionFactorNumericEdit.Value),
                                                  new ElectroMagnetSBJControllerSettings(this.enableElectroMagnetCheckBox.Checked,
                                                                                         (int)this.emShortCircuitDelayTimeNumericUpDown.Value,
                                                                                         (int)this.emFastDelayTimeNumericUpDown.Value,
                                                                                         (int)this.emSlowDelayTimeNumericUpDown.Value,
                                                                                         this.emHoldOnToConductanceRangeCheckBox.Checked,
                                                                                         this.emHoldOnMaxConductanceNumericEdit.Value,
                                                                                         this.emHoldOnMaxVoltageNumericEdit.Value,
                                                                                         this.emHoldOnMinConductanceNumericEdit.Value,
                                                                                         this.emHoldOnMinVoltageNumericEdit.Value,
                                                                                         this.emSkipFirstCycleByStepperMotorCheckBox.Checked),
                                                 new ChannelsSettings(GetActiveChannels()));
                                                                         
            }
        }

        /// <summary>
        /// Get the trigger conductance
        /// </summary>
        /// <returns></returns>
        private double GetTriggerConductance()
        {          
            //
            // Critical conductance is set to 3 order of magnitude below the maximum
            // conductance measured at this gain range
            //
            double maxConductance = m_maxVoltage / Math.Pow(10, int.Parse(this.gainComboBox.Text)) / Math.Abs(this.biasNumericEdit.Value) / m_1G0;
            return maxConductance / 1000;
        }

        /// <summary>
        /// Calculates the voltage suitable for the conductance, using the gain and bias that are set on the UI.
        /// </summary>
        /// <param name="conductance"></param>
        /// <returns></returns>
        private double GetVoltageFromConductnace(double conductance)
        {
            int gainPower = int.Parse(this.gainComboBox.Text);
            return -conductance * m_1G0 * Math.Abs(this.biasNumericEdit.Value) * Math.Pow(10, gainPower);
        }

        private void PopulateChannelsList()
        {
            List<string> channelTypes = new List<string>();
            List<string> complexChannelTypes = new List<string>();
            var typeIDataChannel = typeof(IDataChannel);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeIDataChannel.IsAssignableFrom(type) && !type.IsInterface)
                    {
                        if (type.IsSubclassOf(typeof(SimpleDataChannel)))
                        {
                            channelTypes.Add(type.Name);
                        }
                        else
                        {
                            complexChannelTypes.Add(type.Name);
                        }
                    }
                }
            }                   
            
            this.channel0ComboBox.DataSource = channelTypes;
            this.channel1ComboBox.DataSource = new List<string>(channelTypes);
            this.channel2ComboBox.DataSource = new List<string>(channelTypes);
            this.channel3ComboBox.DataSource = new List<string>(channelTypes);

            this.channel0CheckBox.Text = Settings.Default.DAQPhysicalChannelName0;
            this.channel1CheckBox.Text = Settings.Default.DAQPhysicalChannelName1;
            this.channel2CheckBox.Text = Settings.Default.DAQPhysicalChannelName2;
            this.channel3CheckBox.Text = Settings.Default.DAQPhysicalChannelName3;

            //
            // Also populate the channels in the display list
            //
            List<string> allAvailableChannels = channelTypes;
            allAvailableChannels.AddRange(complexChannelTypes);
            List<ListViewItem> channelsToDisplay = GetChannelsToDisplay(allAvailableChannels);
            channelsListView.Items.AddRange(channelsToDisplay.ToArray());           
            channelsListView.Items[channelsListView.Items.IndexOfKey(typeof(DefaultDataChannel).Name)].Checked = true;            
        }

        private List<ListViewItem> GetChannelsToDisplay(List<string> channelsToDisplay)
        {
            List<ListViewItem> channelsToDisplayAsListView = new List<ListViewItem>();
            foreach (string channel in channelsToDisplay)
            {
                ListViewItem newListViewItem = new ListViewItem(channel);
                newListViewItem.Name = channel;
                channelsToDisplayAsListView.Add(newListViewItem);
            }

            return channelsToDisplayAsListView;
        }

        /// <summary>
        /// Create instances of the desired channels to be sampled from by reflection
        /// </summary>
        /// <returns></returns>
        private List<IDataChannel> GetActiveChannels()
        {
            List<IDataChannel> activeChannels = new List<IDataChannel>();

            DataConvertorSettings dataConvertorSettings =  new DataConvertorSettings(Math.Abs(this.biasNumericEdit.Value), Convert.ToInt32(this.gainComboBox.Text),
                                                                                     this.lockInAcVoltageNumericEdit.Value, Double.Parse(this.sensitivityComboBox.Text));

            if (channel0CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(channel0ComboBox.SelectedValue as string)), 
                                                                          new object[]{channel0CheckBox.Text, dataConvertorSettings}));
            }

            if (channel1CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(channel1ComboBox.SelectedValue as string)),
                                                                          new object[] { channel1CheckBox.Text, dataConvertorSettings }));
            }

            if (channel2CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(channel2ComboBox.SelectedValue as string)),
                                                                          new object[] { channel2CheckBox.Text, dataConvertorSettings }));
            }

            if (channel3CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(channel3ComboBox.SelectedValue as string)),
                                                                          new object[] { channel3CheckBox.Text, dataConvertorSettings }));
            }

            return activeChannels;
        }

        /// <summary>
        /// Get the full type name with its namespace
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private string GetFullTypeName(string typeName)
        {
            return Assembly.GetExecutingAssembly().GetName().Name + "." + typeName;
        }

        /// <summary>
        /// Determines whether a channel is active and is marked as to be samples from
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        private bool IsChannelActive(string channelName)
        {
            if (channel0CheckBox.Checked && channel0ComboBox.Text.Equals(channelName))
            {
                return true;
            }

            if (channel1CheckBox.Checked && channel1ComboBox.Text.Equals(channelName))
            {
                return true;
            }

            if (channel2CheckBox.Checked && channel2ComboBox.Text.Equals(channelName))
            {
                return true;
            }

            if (channel3CheckBox.Checked && channel3ComboBox.Text.Equals(channelName))
            {
                return true;
            }

            return false;
        }        
        #endregion         
    }
}
