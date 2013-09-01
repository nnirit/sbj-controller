using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.Drawing;
using NationalInstruments.UI;
using NationalInstruments.UI.WindowsForms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SBJController.Properties;
using System.Runtime.InteropServices;
using System.Linq;

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
            setParallelPortMode(1);
            InitializeComponent();
            m_sbjController = new SBJController();
            m_sbjController.DataAquired += new SBJController.DataAquiredEventHandler(OnDataAquisition);
            this.bottomPropertyGrid.SelectedObject = new Sample();
            this.topPropertyGrid.SelectedObject = new Sample();
            PopulateChannelsLists();
            TryConnectToKeithly();
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
            ShortCircuitButtonFunction(this.shortCircuitCheckBoxButton.Checked);
            //if (shortCircuitCheckBoxButton.Checked)
            //{
            //    //
            //    // Short Circuit request.
            //    // Verify first that the worker is nor busy otherwise this means that it is a double request.
            //    //
            //    if (!obtainShortCircuitBackgroundWorker.IsBusy)
            //    {
            //        //
            //        // Change button text and behavior of other related controls
            //        //
            //        shortCircuitCheckBoxButton.Text = "Stop";
            //        startStopCheckBoxButton.Enabled = false;
            //        manualStartCheckBoxButton.Enabled = false;
            //        moveUpCheckBoxButton.Enabled = false;
            //        fixBiasCheckBoxButton.Enabled = false;

            //        //
            //        // Do work async
            //        //
            //        obtainShortCircuitBackgroundWorker.RunWorkerAsync();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Can not start Short Circuit operation." + Environment.NewLine + "Please try again in few seconds.");
            //    }
            //}
            //else
            //{
            //    //
            //    // If we reached here that means that we were requested to stop the short circuit.
            //    //
            //    if (obtainShortCircuitBackgroundWorker.WorkerSupportsCancellation == true)
            //    {
            //        obtainShortCircuitBackgroundWorker.CancelAsync();
            //    }
            //}
        }

        private void ShortCircuitButtonFunction(bool isShortCircuitButtonChecked)
        {
            if (isShortCircuitButtonChecked)
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
                    ivShortCircuitCheckBox.Text = "Stop";
                    calibrationShortCircuitCheckBox.Text = "Stop";

                    startStopCheckBoxButton.Enabled = false;
                    ivStartStopCheckBox.Enabled = false;
                    calibrationStartStopCheckBox.Enabled = false;

                    moveUpCheckBoxButton.Enabled = false;
                    ivStepperUpCheckBox.Enabled = false;
                    calibrationStepperUpCheckBox.Enabled = false;

                    manualStartCheckBoxButton.Enabled = false;
                    
                    fixBiasCheckBoxButton.Enabled = false;

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
            
            //
            // if we don't use the keithley for bias,  we need to apply it through the DAQ device
            //
            m_sbjController.ApplyVoltageIfNeeded(this.useKeithleyCheckBox.Checked, this.biasNumericEdit.Value, this.biasErrorNumericEdit.Value);

            m_sbjController.TryObtainShortCircuit((double)shortCircuitVoltageNumericUpDown.Value, worker, e);
        }

        /// <summary>
        /// The callback for the short circuit operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void obtainShortCircuitBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // We've reached short circuit or we were requested to stop
            // So let's bring back the appearance of the UI.
            //
            shortCircuitCheckBoxButton.Text = "Short Circuit";
            ivShortCircuitCheckBox.Text = "Short Circuit";
            calibrationShortCircuitCheckBox.Text = "Short Circuit";

            shortCircuitCheckBoxButton.Checked = false;
            ivShortCircuitCheckBox.Checked = false;
            calibrationShortCircuitCheckBox.Checked = false;

            startStopCheckBoxButton.Enabled = true;
            ivStartStopCheckBox.Enabled = true;
            calibrationStartStopCheckBox.Enabled = true;

            moveUpCheckBoxButton.Enabled = true;
            ivStepperUpCheckBox.Enabled = true;
            calibrationStepperUpCheckBox.Enabled = true;

            manualStartCheckBoxButton.Enabled = true;
            fixBiasCheckBoxButton.Enabled = true;

            //
            // if we applied the bias by the DAQ device, we need to stop the task. 
            //
            m_sbjController.StopApplyingVoltageIfNeeded();
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
                    manualStartCheckBoxButton.Enabled = false;
                    moveUpCheckBoxButton.Enabled = false;
                    shortCircuitCheckBoxButton.Enabled = false;
                    fixBiasCheckBoxButton.Enabled = false;
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
                    try
                    {
                        m_sbjController.Task.Control(TaskAction.Abort);
                    }
                    catch(Exception)
                    {
                        //probably the task was already disposed. go on, no need for exception.
                    }
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
            manualStartCheckBoxButton.Enabled = true;
            shortCircuitCheckBoxButton.Enabled = true;
            fixBiasCheckBoxButton.Enabled = true;
            moveUpCheckBoxButton.Enabled = true;
            generalSettingsPanel.Enabled = true;
            laserSettingsPanel.Enabled = true;
            lockInPanel.Enabled = true;
            channelsSettingsPanel.Enabled = true;
            electroMagnetSettingsPanel.Enabled = true;

            //
            // if we applied the bias by the DAQ device, we need to stop the task. 
            //
            m_sbjController.StopApplyingVoltageIfNeeded();
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
            StepperUpButtonFunction(this.moveUpCheckBoxButton.Checked);
        }

        /// <summary>
        /// according to the status (checked/unchecked) of the Stepper Up button, this function starts or stops
        /// the stepping-up process
        /// </summary>
        /// <param name="isStepperUpButtonChecked"></param>
        private void StepperUpButtonFunction(bool isStepperUpButtonChecked)
        {
            if (isStepperUpButtonChecked)
            {
                if (!stepperUpBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    this.moveUpCheckBoxButton.Text = "Stop";
                    this.ivStepperUpCheckBox.Text = "Stop";
                    this.calibrationStepperUpCheckBox.Text = "Stop";

                    //
                    // update other buttons to be unavailable
                    //
                    this.shortCircuitCheckBoxButton.Enabled = false;
                    this.ivShortCircuitCheckBox.Enabled = false;
                    this.calibrationShortCircuitCheckBox.Enabled = false;

                    this.startStopCheckBoxButton.Enabled = false;
                    this.ivStartStopCheckBox.Enabled = false;
                    this.calibrationStartStopCheckBox.Enabled = false;

                    this.manualStartCheckBoxButton.Enabled = false;

                    //
                    //start the background worker that does the stepping-up process
                    //
                    this.stepperUpBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start move stepper up." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                //
                // stop the stepping up process
                //
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
            this.moveUpCheckBoxButton.Text = "Stepper Up";
            this.ivStepperUpCheckBox.Text = "Stepper Up";
            this.calibrationStepperUpCheckBox.Text = "Stepper Up";

            this.shortCircuitCheckBoxButton.Enabled = true;
            this.ivShortCircuitCheckBox.Enabled = true;
            this.calibrationShortCircuitCheckBox.Enabled = true;

            this.startStopCheckBoxButton.Enabled = true;
            this.ivStartStopCheckBox.Enabled = true;
            this.calibrationStartStopCheckBox.Enabled = true;

            this.manualStartCheckBoxButton.Enabled = true;
        }
        #endregion

        #region Fix Bias Handlers
        private void fixBiasCheckBoxButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fixBiasCheckBoxButton.Checked)
            {
                if (!this.useKeithleyCheckBox.Checked)
                {
                    try
                    {
                        m_sbjController.SourceMeter.Connect();
                        m_sbjController.SourceMeter.SetBias(this.biasNumericEdit.Value + this.biasErrorNumericEdit.Value);
                    }
                    catch (SBJException ex)
                    {
                        //
                        // the keithley doesn't connect.
                        //
                        throw new SBJException("Error occured when tryin to start the keithley", ex);
                    }
                }
                if (!fixBiasBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text
                    //
                    this.fixBiasCheckBoxButton.Text = "Stop";
                    this.shortCircuitCheckBoxButton.Enabled = false;
                    this.startStopCheckBoxButton.Enabled = false;
                    this.moveUpCheckBoxButton.Enabled = false;
                    this.manualStartCheckBoxButton.Enabled = false;
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
            this.manualStartCheckBoxButton.Enabled = true;
            if (!e.Cancelled)
            {
                this.biasErrorNumericEdit.Value = ((Bias)e.Result).Error;
                this.biasNumericEdit.Value = (double)this.biasNumericEdit.Value * ((Bias)e.Result).Sign;
                this.biasErrorLabel.ForeColor = Color.Black;

            }
            m_sbjController.SourceMeter.SetBias(this.biasNumericEdit.Value + this.biasErrorNumericEdit.Value);
        }
        #endregion

        #region IV Cycles Handlers

        /// <summary>
        /// Fired when the Start IV button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivStartStopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ivStartStopCheckBox.Checked)
            {
                //
                // We were requested to start data acquisition so we must verify
                // first that the worker is free for doing the job.
                //
                if (!ivCyclesBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text and UI appearance
                    //
                    this.ivStartStopCheckBox.Text = "Stop";
                    this.ivShortCircuitCheckBox.Enabled = false;
                    this.ivStepperUpCheckBox.Enabled = false;
                    this.ivGeneralSettingsPanel.Enabled = false;
                    this.ivSteppingMethodPanel.Enabled = false;
                    this.ivChannelsPanel.Enabled = false;
                    ivCyclesBackgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Can not start IV cycles operation." + Environment.NewLine + "Please try again in few seconds.");
                }
            }
            else
            {
                //
                // We were requested to stop data acquisition process
                //
                if (ivCyclesBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    ivCyclesBackgroundWorker.CancelAsync();
                }
                if (m_sbjController.IVInputTask != null)
                {
                    m_sbjController.IVInputTask.Control(TaskAction.Abort);
                }
                if (m_sbjController.OutputTask != null)
                {
                    m_sbjController.OutputTask.Control(TaskAction.Abort);
                }
                m_sbjController.QuitJunctionOpenningOperation = true;
                m_sbjController.StepperMotor.Shutdown();
        }
        }

        /// <summary>
        /// The ivCyclesBackgroundWorker main thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivCyclesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            m_sbjController.IV_AcquireData(GetIVSettings(), worker, e);
        }

        /// <summary>
        /// The callback for the IV cycles background worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivCyclesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // We've done taking data so we must bring back the UI appearance
            //
            this.ivStartStopCheckBox.Text = "Start";
            this.ivStartStopCheckBox.Checked = false;
            this.ivShortCircuitCheckBox.Enabled = true;
            this.ivStepperUpCheckBox.Enabled = true;
            this.ivGeneralSettingsPanel.Enabled = true;
            this.ivSteppingMethodPanel.Enabled = true;
            this.ivChannelsPanel.Enabled = true;
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
            //
            // Update the appearance of other UI related parameters.
            //            
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
            GainUpdate(this.biasNumericEdit.Value, int.Parse(this.gainComboBox.Text), 
                        this.triggerConductanceNumericEdit, this.triggerVoltageNumericEdit);

            this.emHoldOnMaxConductanceNumericEdit.Value = 100 * this.triggerConductanceNumericEdit.Value;
            this.emHoldOnMinConductanceNumericEdit.Value = 50 * this.triggerConductanceNumericEdit.Value;
        }

        /// <summary>
        /// Update UI parameters and Keithley once bias is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void biasNumericEdit_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        {
            BiasUpdate(this.biasNumericEdit.Value, int.Parse(this.gainComboBox.Text), this.useKeithleyCheckBox.Checked, 
                        this.triggerConductanceNumericEdit, this.triggerVoltageNumericEdit);

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
                if (this.laserModeComboBox.Text.Equals("IODrive"))
                {
                    m_sbjController.LaserController = new IODriveLaserController(Settings.Default.IODriveOutputAddress);
                }
                else
                {
                    TaborModel laserModel = (TaborModel)Enum.Parse(typeof(TaborModel), Settings.Default.TaborLaserModel);
                    string laserAddress = Settings.Default["Tabor" + Settings.Default.TaborLaserModel + "Address"].ToString();                    
                    m_sbjController.LaserController = new TaborLaserController(laserModel, laserAddress);                
                }
                m_sbjController.LaserController.Connect();                
            }
            else
            {
                m_sbjController.LaserController.Disconnect();
            }

            //
            // Update the appearance of other UI related parameters.
            //
            this.laserModeComboBox.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeNumericUpDown.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeLabel.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeOnSampleNumericUpDown.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeOnSampleLabel.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeWNumericUpDown.Enabled = this.enableLaserCheckBox.Checked;
            this.laserAmplitudeWLabel.Enabled = this.enableLaserCheckBox.Checked;            
            this.enableFirstEOMcheckBox.Enabled = this.enableLaserCheckBox.Checked;
            this.enableSecondEOMCheckBox.Enabled = this.enableLaserCheckBox.Checked;
            this.enableChopperCheckBox.Enabled = this.enableLaserCheckBox.Checked;
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
            BrowseButtonFunction(this.pathTextBox);
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
        }

        /// <summary>
        /// Update laser realted UI parameters once laser mode is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void laserModeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (laserModeComboBox.Text.Equals("IODrive")) 
            {
                //
                // Assign the correct type of laser control if haven't been done before
                //
                if ((m_sbjController.LaserController as IODriveLaserController) == null)
                {
                    m_sbjController.LaserController = new IODriveLaserController(Settings.Default.IODriveOutputAddress);             
                }

                //
                // Set the amplitude
                //
                m_sbjController.LaserController.SetAmplitude((double)this.laserAmplitudeNumericUpDown.Value);
            }
            else
            {
                //
                // All othe types of wave forms require Tabor as the laser controller
                //
                if ((m_sbjController.LaserController as TaborLaserController) == null)
                {
                    TaborModel laserModel = (TaborModel)Enum.Parse(typeof(TaborModel), Settings.Default.TaborLaserModel);
                    string laserAddress = Settings.Default["Tabor" + Settings.Default.TaborLaserModel + "Address"].ToString();                    
                    m_sbjController.LaserController = new TaborLaserController(laserModel, laserAddress);  
                }

                if (laserModeComboBox.Text.Equals("Square"))
                {
                    (m_sbjController.LaserController as TaborLaserController).SetSquareMode((double)this.frequencyNumericUpDown.Value, (double)this.laserAmplitudeNumericUpDown.Value);                    
                }

                if (laserModeComboBox.Text.Equals("DC"))
                {
                    (m_sbjController.LaserController as TaborLaserController).SetDCMode((double)this.laserAmplitudeNumericUpDown.Value);
                }
            }            
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
            double amplitude = (double)this.laserAmplitudeNumericUpDown.Value;
            m_sbjController.LaserController.SetAmplitude(amplitude);            
        }

        /// <summary>
        /// Change the frequency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frequencyNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double frequency = (double)this.frequencyNumericUpDown.Value;
            m_sbjController.LaserController.SetFrequency(frequency);
        }

        private void firstEOMFrequencyNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double frequency = (double)this.firstEOMFrequencyNumericUpDown.Value;
            m_sbjController.TaborFirstEOM.SetFrequency(frequency);
        }

        private void secondEOMFrequencyNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double frequency = (double)this.secondEOMFrequencyNumericUpDown.Value;
            m_sbjController.TaborSecondEOM.SetFrequency(frequency);
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
            // LockInXYInternalSourceDataChannel and IVProcessed are special cases and doesn't obbey the rules
            //
            if (channel.Name.Equals(typeof(LockInXYInternalSourceDataChannel).Name) ||
                channel.Name.Equals(typeof(IVProcessedDataChannel).Name))
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

        /// <summary>
        /// Connect to the keithley if the use keithley checkBox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useKeithleyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.useKeithleyCheckBox.Checked)
            {
                //
                // if we checked the use keithley box on the DAQ tab, we need to check it also on the calibration tab.
                //
                this.calibrationKeithleyCheckBox.Checked = true;

                //
                // Connect to the keithley and set bias.
                //
                m_sbjController.SourceMeter.Connect();
                m_sbjController.SourceMeter.SetBias(this.biasNumericEdit.Value + this.biasErrorNumericEdit.Value);
            }
        }

        /// <summary>
        /// Open the folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFolderButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.pathTextBox.Text);
            }

        /// <summary>
        /// On IV Amplitude change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivVoltageAmplitudeNumericEdit_AfterChangeValue(object sender, AfterChangeNumericValueEventArgs e)
        {
            //
            // sets the voltage in which we display the trace to be whithing the boundries of +-amplitude.
            //
            if (Math.Abs(this.ivVoltageForTraceNumericEdit.Value) > Math.Abs(this.ivVoltageAmplitudeNumericEdit.Value))
            {
                this.ivVoltageForTraceNumericEdit.Value = (this.ivVoltageForTraceNumericEdit.Value > 0) ? this.ivVoltageAmplitudeNumericEdit.Value : (-this.ivVoltageAmplitudeNumericEdit.Value);
            }
        }

        private void manualStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (manualStartCheckBoxButton.Checked)
            {
                //
                // We were requested to start data acquisition so we must verify
                // first that the worker is free for doing the job.
                //
                if (!manualStartBackgroundWorker.IsBusy)
                {
                    //
                    // Change button text and UI appearance
                    //
                    manualStartCheckBoxButton.Text = "Stop";
                    startStopCheckBoxButton.Enabled = false;
                    shortCircuitCheckBoxButton.Enabled = false;
                    fixBiasCheckBoxButton.Enabled = false;
                    moveUpCheckBoxButton.Enabled = false;
                    generalSettingsPanel.Enabled = false;
                    laserSettingsPanel.Enabled = false;
                    lockInPanel.Enabled = false;
                    electroMagnetSettingsPanel.Enabled = false;
                    channelsSettingsPanel.Enabled = false;
                    manualStartBackgroundWorker.RunWorkerAsync();
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
                if (manualStartBackgroundWorker.WorkerSupportsCancellation == true)
                {
                    manualStartBackgroundWorker.CancelAsync();
                }
                m_sbjController.QuitJunctionOpenningOperation = true;
                m_sbjController.StepperMotor.Shutdown();
            }
        }

        private void manualStartBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            m_sbjController.AquireDataManually(GetSBJControllerSettings(), worker, e);
        }

        private void manualStartBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // We've done taking data so we must bring the UI appearance
            //
            manualStartCheckBoxButton.Text = "Manual Start";
            manualStartCheckBoxButton.Checked = false;
            startStopCheckBoxButton.Enabled = true;
            fixBiasCheckBoxButton.Enabled = true;
            shortCircuitCheckBoxButton.Enabled = true;
            moveUpCheckBoxButton.Enabled = true;
            generalSettingsPanel.Enabled = true;
            laserSettingsPanel.Enabled = true;
            lockInPanel.Enabled = true;
            channelsSettingsPanel.Enabled = true;
            electroMagnetSettingsPanel.Enabled = true;

            //
            // if we applied the bias by the DAQ device, we need to stop the task. 
            //
            m_sbjController.StopApplyingVoltageIfNeeded();
        }

        private void useLambdaZupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.useLambdaZupCheckBox.Checked)
            {
                try
                {
                    m_sbjController.LambdaZup.Connect();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot connect to LambdaZup.");
                }
            }
        }

        private void enableEOMcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (m_sbjController.TaborFirstEOM == null)
            {
                TaborModel eomModel = (TaborModel)Enum.Parse(typeof(TaborModel), Settings.Default.TaborFirstEOMModel);
                string eomAddress = Settings.Default["Tabor" + Settings.Default.TaborFirstEOMModel + "Address"].ToString();
                m_sbjController.TaborFirstEOM = new TaborEOMController(eomModel, eomAddress);
            }

            if (this.enableFirstEOMcheckBox.Checked)
            {
                m_sbjController.TaborFirstEOM.Connect();
                m_sbjController.TaborFirstEOM.SetSinusoidMode((double)this.firstEOMFrequencyNumericUpDown.Value);
            }
            else
            {
                m_sbjController.TaborFirstEOM.Disconnect();
            }

            this.firstEOMFrequencyLabel.Enabled = this.enableFirstEOMcheckBox.Checked;
            this.firstEOMFrequencyNumericUpDown.Enabled = this.enableFirstEOMcheckBox.Checked;
        }
        
        private void enableSecondEOMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (m_sbjController.TaborSecondEOM == null)
            {
                TaborModel eomModel = (TaborModel)Enum.Parse(typeof(TaborModel), Settings.Default.TaborSecondEOMModel);
                string eomAddress = Settings.Default["Tabor" + Settings.Default.TaborSecondEOMModel + "Address"].ToString();
                m_sbjController.TaborSecondEOM = new TaborEOMController(eomModel, eomAddress);
            }
            
            if (this.enableSecondEOMCheckBox.Checked)
            {
                m_sbjController.TaborSecondEOM.Connect();
                m_sbjController.TaborSecondEOM.SetSinusoidMode((double)this.secondEOMFrequencyNumericUpDown.Value);
            }
            else
            {
                m_sbjController.TaborSecondEOM.Disconnect();
            }

            this.secondEOMFrequencyLabel.Enabled = this.enableSecondEOMCheckBox.Checked;
            this.secondEOMFrequencyNumericUpDown.Enabled = this.enableSecondEOMCheckBox.Checked;
            this.eomCOnfigurationComboBox.Enabled = this.enableSecondEOMCheckBox.Checked;
        }

        private void enableChopperCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.externalFrequencyLabel.Enabled = this.enableChopperCheckBox.Checked;
            this.externalFrequencyNumericUpDown.Enabled = this.enableChopperCheckBox.Checked;
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
                // Check the attribute of the first channel received (all of the channels received are suppose to be of the same attribute). 
                // This will help us decide which UI Tab should be updated.
                //
                if (e.DataChannels[0].GetType().GetCustomAttributes(false)[0] is DAQAttribute)
                {
                    GraphAndFileNumberUpdate(e.DataChannels as List<IDataChannel>, this.channelsListView, this.traceWaveformGraph, e.FileNumber, this.fileNumberNumericUpDown);
                }
                else if (e.DataChannels[0].GetType().GetCustomAttributes(false)[0] is IVAttribute)
                {
                    GraphAndFileNumberUpdate(e.DataChannels as List<IDataChannel>, this.ivChannelsListView, this.ivWaveformGraph, e.FileNumber, this.ivFileNumberNumericUpDown);
                }
                else
                {
                    GraphAndFileNumberUpdate(e.DataChannels as List<IDataChannel>, this.calibrationChannelsListView, this.calibrationWaveformGraph, e.FileNumber, this.calibrationCycleNumberNumericUpDown);
                }
            }
        }

        /// <summary>
        /// Update the plot on the graph and the current file number, on the UI.
        /// </summary>
        /// <param name="dataChannels">The channels received from the event, and contains the data that should be plotted on the graph.</param>
        /// <param name="relevantChannelsListView">The relevant ListView on the correct tab.</param>
        /// <param name="waveformGraph">The waveform graph from the UI that should be updated.</param>
        /// <param name="fileNumber">The current file number.</param>
        /// <param name="fileNumberNumericButton">The button from the UI that shows the current file number.</param>
        private void GraphAndFileNumberUpdate(List<IDataChannel> dataChannels, ListView relevantChannelsListView, WaveformGraph waveformGraph, int fileNumber, NumericUpDown fileNumberNumericButton)
        {
            //
            // Retreive the channels to be dispalyed according to the user's choice.
            //
            List<IDataChannel> channelsToDisplay = GetChannelsToDisplay(dataChannels, relevantChannelsListView);

            //
            // Convert the raw data to phyical one
            //
            double[,] data = GetPhysicalData(channelsToDisplay);
            
            //
            // Update file number
            //
            fileNumberNumericButton.Value = fileNumber;
            
            //
            // Clear the last plot
            //
            waveformGraph.ClearData();

            //
            // Update plot
            //
            int numberOfChannels = data.GetLength(0);
            waveformGraph.PlotYMultiple(data);

            if (numberOfChannels > 1)
            {
                waveformGraph.Plots[1].PointColor = Color.Blue;
                waveformGraph.Plots[1].YAxis = yAxis2;
                waveformGraph.Plots[1].ToolTipsEnabled = true;
                waveformGraph.Plots[1].LineColor = Color.Blue;
            }
            
            waveformGraph.Caption = string.Format("Trace #{0} at {1}", fileNumber, DateTime.Now.TimeOfDay);
        }

        /// <summary>
        /// Get the channels to display on the UI
        /// </summary>
        /// <param name="physicalChannels">The possible available channels</param>
        /// <returns></returns>
        private List<IDataChannel> GetChannelsToDisplay(List<IDataChannel> dataChannels, ListView relevantChannelsListView)
        {
            List<IDataChannel> selectedChannels = new List<IDataChannel>();

            //
            // Only dispaly channels that were chosen to be displayed
            // 
            foreach (ListViewItem selectedChannel in relevantChannelsListView.CheckedItems)
            {
                //
                // Find the desired channel in the available channel list.
                // Only add it to the list if it was found.
                //
                IDataChannel physicalChannel = dataChannels.Find(new Predicate<IDataChannel>(x => x.Name.Equals(selectedChannel.Name)));

                if (physicalChannel != null)
                {
                    selectedChannels.Add(physicalChannel);
                }
            }

            return selectedChannels;
        }

        /// <summary>
        /// Get the physical data out of the available data channels
        /// </summary>
        /// <param name="physicalChannels"></param>
        /// <returns></returns>
        private double[,] GetPhysicalData(IList<IDataChannel> dataChannels)
        {
            List<double[]> physicalDataAsList = new List<double[]>();
            foreach (var channel in dataChannels)
            {
                physicalDataAsList.AddRange(channel.PhysicalData);
            }            

            return GetDataAsArray(physicalDataAsList);            
        }

        /// <summary>
        /// Converts the data to a matrix of elements in order to be displayed later
        /// </summary>
        /// <param name="physicalDataAsList">A list of data sets for each channel</param>
        /// <returns></returns>
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
                                                                                  this.useKeithleyCheckBox.Checked,
                                                                                  this.pathTextBox.Text,
                                                                                  (int)this.fileNumberNumericUpDown.Value,
                                                                                  (int)this.numberOfCyclesnumericUpDown.Value,
                                                                                  (double)this.shortCircuitVoltageNumericUpDown.Value,
                                                                                  (Sample)this.bottomPropertyGrid.SelectedObject,
                                                                                  (Sample)this.bottomPropertyGrid.SelectedObject),
                                                  new LaserSBJControllerSettings(this.enableLaserCheckBox.Checked,
                                                                                 this.laserModeComboBox.SelectedItem.ToString(),
                                                                                 (double)this.laserAmplitudeNumericUpDown.Value,
                                                                                 (double)this.laserAmplitudeWNumericUpDown.Value,
                                                                                 (double)this.laserAmplitudeOnSampleNumericUpDown.Value,
                                                                                 (int)this.frequencyNumericUpDown.Value,
                                                                                 this.enableFirstEOMcheckBox.Checked,
                                                                                 this.enableSecondEOMCheckBox.Checked,
                                                                                 this.enableChopperCheckBox.Checked,
                                                                                 this.eomCOnfigurationComboBox.Text,
                                                                                 (int)this.externalFrequencyNumericUpDown.Value,
                                                                                 (int)this.firstEOMFrequencyNumericUpDown.Value,
                                                                                 (int)this.secondEOMFrequencyNumericUpDown.Value),
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
                                                  new LambdaZupSBJControllerSettings(this.useLambdaZupCheckBox.Checked,
                                                                                     this.lambdaZupOutputVoltageNumericEdit.Value),
                                                  new ChannelsSettings(GetActiveChannels()));
                                                                         

            }
        }

        /// <summary>
        /// Get the trigger conductance
        /// </summary>
        /// <returns></returns>
        private double GetTriggerConductance(int gain, double bias)
        {          
            //
            // Critical conductance is set to 3 order of magnitude below the maximum
            // conductance measured at this gain range
            //
            double maxConductance = m_maxVoltage / Math.Pow(10, gain) / Math.Abs(bias) / m_1G0;
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

        /// <summary>
        /// Populate the channles list in the UI
        /// </summary>
        private void PopulateChannelsLists()
        {
            PopulateChannelsListOnDAQTab();
            PopulateChannelsListOnIVTab();
            PopulateChannelsListOnCalibrationTab();
        }

        /// <summary>
        /// Populate channels list on thw DAQ tab on the UI
        /// </summary>
        private void PopulateChannelsListOnDAQTab()
        {
            //
            // Get lists of the relevant channel types
            //
            ChannelTypeLists channelTypeLists = PopulateChannelTypeLists(typeof(DAQAttribute));

            //
            // In the channles tab only assign the simple data channels
            //
            this.channel0ComboBox.DataSource = channelTypeLists.Simple;
            this.channel1ComboBox.DataSource = new List<string>(channelTypeLists.Simple);
            this.channel2ComboBox.DataSource = new List<string>(channelTypeLists.Simple);
            this.channel3ComboBox.DataSource = new List<string>(channelTypeLists.Simple);

            
            this.channel0CheckBox.Text = Settings.Default.DAQPhysicalChannelName0;
            this.channel1CheckBox.Text = Settings.Default.DAQPhysicalChannelName1;
            this.channel2CheckBox.Text = Settings.Default.DAQPhysicalChannelName2;
            this.channel3CheckBox.Text = Settings.Default.DAQPhysicalChannelName3;

            //
            // Also populate the channels in the display list
            //
            List<string> allAvailableChannels = channelTypeLists.Simple;
            allAvailableChannels.AddRange(channelTypeLists.Complex);
            List<ListViewItem> channelsToDisplay = GetChannelsToDisplay(allAvailableChannels);
            channelsListView.Items.AddRange(channelsToDisplay.ToArray());
            channelsListView.Items[channelsListView.Items.IndexOfKey(typeof(DefaultDataChannel).Name)].Checked = true;            
        }

        /// <summary>
        /// Populate channels list on the Calibration tab on the UI
        /// </summary>
        private void PopulateChannelsListOnCalibrationTab()
        {
            //
            // Get lists of the relevant channel types
            //
            ChannelTypeLists channelTypeLists = PopulateChannelTypeLists(typeof(CalibrationAttribute));

            //
            // In the channles tab only assign the simple data channels
            //
            this.calibrationChannel1ComboBox.DataSource = channelTypeLists.Simple;
            this.calibrationChannel1CheckBox.Text = Settings.Default.DAQPhysicalChannelName0;
            
            //
            // Also populate the channels in the display list
            //
            List<string> allAvailableChannels = channelTypeLists.Simple;
            allAvailableChannels.AddRange(channelTypeLists.Complex);
            List<ListViewItem> channelsToDisplay = GetChannelsToDisplay(allAvailableChannels);
            calibrationChannelsListView.Items.AddRange(channelsToDisplay.ToArray());
            calibrationChannelsListView.Items[calibrationChannelsListView.Items.IndexOfKey(typeof(CalibrationDataChannel).Name)].Checked = true;
        }

        /// <summary>
        /// Populate channels list on thw IV tab on the UI
        /// </summary>
        private void PopulateChannelsListOnIVTab()
        {
            //
            // Get lists of the relevant channel types
            //
            ChannelTypeLists channelTypeLists = PopulateChannelTypeLists(typeof(IVAttribute));

            //
            // In the channles tab only assign the simple data channels
            //
            this.ivChannel0ComboBox.DataSource = channelTypeLists.Simple;
            this.ivChannel1ComboBox.DataSource = new List<string>(channelTypeLists.Simple);
            this.ivChannel2ComboBox.DataSource = new List<string>(channelTypeLists.Simple);
            this.ivChannel3ComboBox.DataSource = new List<string>(channelTypeLists.Simple);
            
            //Set the right initial value for the first comboBox
            this.ivChannel0ComboBox.SelectedItem = typeof(IVInputDataChannel).Name;

            this.ivChannel0CheckBox.Text = Settings.Default.DAQPhysicalChannelName0;
            this.ivChannel1CheckBox.Text = Settings.Default.DAQPhysicalChannelName1;
            this.ivChannel2CheckBox.Text = Settings.Default.DAQPhysicalChannelName2;
            this.ivChannel3CheckBox.Text = Settings.Default.DAQPhysicalChannelName3;

            //
            // Also populate the channels in the display list
            //
            List<string> allAvailableChannels = channelTypeLists.Simple;
            allAvailableChannels.AddRange(channelTypeLists.Complex);
            List<ListViewItem> channelsToDisplay = GetChannelsToDisplay(allAvailableChannels);
            ivChannelsListView.Items.AddRange(channelsToDisplay.ToArray());
            ivChannelsListView.Items[ivChannelsListView.Items.IndexOfKey(typeof(IVProcessedDataChannel).Name)].Checked = true;
        }

        /// <summary>
        /// Get lists of all the channel types that are marked with the input attribute.
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns>a class that contains 2 lists of channel types, one of the simple channels and one of the complex.</returns>
        private ChannelTypeLists PopulateChannelTypeLists(Type attributeType)
        {
            List<string> channelTypes = new List<string>();
            List<string> complexChannelTypes = new List<string>();
            var typeIDataChannel = typeof(IDataChannel);

            //
            // A possible data channel is only one which ipmlements IDataChannel
            // Take only these ones and sort to Simple and Complex data channels lists.
            //
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeIDataChannel.IsAssignableFrom(type) && !type.IsInterface)
                    {
                        if (type.GetCustomAttributes(attributeType, false).Length > 0)
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
            }

            return (new ChannelTypeLists(channelTypes, complexChannelTypes));
        }

        /// <summary>
        /// Creats a list view for the channels
        /// </summary>
        /// <param name="channelsToDisplay"></param>
        /// <returns></returns>        
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
                                                                                     this.lockInAcVoltageNumericEdit.Value, Double.Parse(this.sensitivityComboBox.Text), 
                                                                                     (int)this.ivSamplesPerCycleNumericEdit.Value);
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
        /// Create instances of the desired channels to be sampled from by reflection for the IV tab
        /// </summary>
        /// <returns></returns>
        private List<IDataChannel> GetIVActiveChannels()
        {
            List<IDataChannel> activeChannels = new List<IDataChannel>();

            DataConvertorSettings dataConvertorSettings = new DataConvertorSettings(Math.Abs(this.ivVoltageForTraceNumericEdit.Value), Convert.ToInt32(this.ivGainPoweComboBox.Text),
                                                                                     this.lockInAcVoltageNumericEdit.Value, Double.Parse(this.sensitivityComboBox.Text),
                                                                                     (int)this.ivSamplesPerCycleNumericEdit.Value);
            if (ivChannel0CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(ivChannel0ComboBox.SelectedValue as string)),
                                                                          new object[] { ivChannel0CheckBox.Text, dataConvertorSettings }));
            }

            if (ivChannel1CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(ivChannel1ComboBox.SelectedValue as string)),
                                                                          new object[] { ivChannel1CheckBox.Text, dataConvertorSettings }));
            }

            if (ivChannel2CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(ivChannel2ComboBox.SelectedValue as string)),
                                                                          new object[] { ivChannel2CheckBox.Text, dataConvertorSettings }));
            }

            if (ivChannel3CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(ivChannel3ComboBox.SelectedValue as string)),
                                                                          new object[] { ivChannel3CheckBox.Text, dataConvertorSettings }));
            }

            return activeChannels;
        }

        /// <summary>
        /// Create instances of the desired channels to be sampled from by reflection for the calibration tab
        /// </summary>
        /// <returns></returns>
        private List<IDataChannel> GetCalibrationActiveChannels()
        {
            List<IDataChannel> activeChannels = new List<IDataChannel>();

            DataConvertorSettings dataConvertorSettings = new DataConvertorSettings(Math.Abs(this.calibrationBiasNumericEdit.Value), Convert.ToInt32(this.calibrationGainPowerComboBox.Text),
                                                                                     this.lockInAcVoltageNumericEdit.Value, Double.Parse(this.sensitivityComboBox.Text),
                                                                                     (int)this.ivSamplesPerCycleNumericEdit.Value);
            if (calibrationChannel1CheckBox.Checked)
            {
                activeChannels.Add((IDataChannel)Activator.CreateInstance(Type.GetType(GetFullTypeName(calibrationChannel1ComboBox.SelectedValue as string)),
                                                                          new object[] { calibrationChannel1CheckBox.Text, dataConvertorSettings }));
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

        /// <summary>
        /// Select path for saving file
        /// </summary>
        /// <param name="relevantPathTextBox">the active path text box</param>
        private void BrowseButtonFunction(TextBox relevantPathTextBox)
        {
            string initialPath = relevantPathTextBox.Text;
            FolderBrowserDialog folderBroswer = new FolderBrowserDialog();

            //
            // Open the directory that currently written.
            // If doesn't exist just open the broswer from the current running path.
            //
            if (Directory.Exists(relevantPathTextBox.Text))
            {
                folderBroswer.SelectedPath = relevantPathTextBox.Text;
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
                relevantPathTextBox.Text = folderBroswer.SelectedPath;
            }
            else
            {
                relevantPathTextBox.Text = initialPath;
            }
        }

        /// <summary>
        /// updates the bias on the keithley if needed, and the trigger values on the UI
        /// </summary>
        /// <param name="isKeithleyUsed"></param>
        /// <param name="triggerConductance"></param>
        /// <param name="triggerVoltage"></param>
        private void BiasUpdate(double bias, int gainPower, bool isKeithleyUsed, NumericEdit triggerConductance, NumericEdit triggerVoltage)
        {
            //
            // This event is also fired when the UI loads on start.
            // At that point sbjController is still null and we need to verify this.
            //
            if (m_sbjController != null && isKeithleyUsed)
            {
                m_sbjController.SourceMeter.SetBias(bias);
            }

            //
            // update the trigger values
            //
            triggerConductance.Value = GetTriggerConductance(gainPower, bias);
            triggerVoltage.Value = -triggerConductance.Value * m_1G0 * Math.Abs(bias) * Math.Pow(10, gainPower);
        }

        /// <summary>
        /// updates the gain on the amplifier, and the trigger values on the UI
        /// </summary>
        /// <param name="isKeithleyUsed"></param>
        /// <param name="triggerConductance"></param>
        /// <param name="triggerVoltage"></param>
        private void GainUpdate(double bias, int gainPower, NumericEdit triggerConductance, NumericEdit triggerVoltage)
        {
            //
            // update gain power on the amplifier
            //
            m_sbjController.ChangeGain(gainPower);

            //
            // update the trigger values
            //
            triggerConductance.Value = GetTriggerConductance(gainPower, bias);
            triggerVoltage.Value = -triggerConductance.Value * m_1G0 * Math.Abs(bias) * Math.Pow(10, gainPower);
        }

        /// <summary>
        /// Tries to connect to the keithley. If failed, uncheck the "use keithly" checkBox. 
        /// </summary>
        private void TryConnectToKeithly()
        {
            try
            {
                m_sbjController.SourceMeter.Connect();
            }
            catch (SBJException)
            {
                //
                // the keithley doesn't connect. 
                //
                this.useKeithleyCheckBox.Checked = false;
                this.calibrationKeithleyCheckBox.Checked = false;
            }
        }
       
        #endregion            

        #region Calibration Tab

        private void StartStopCalibrationcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (calibrationStartStopCheckBox.Checked)
            {
                //
                // We were requested to start data acquisition so we must verify
                // first that the worker is free for doing the job.
                //
                if (!calibrationBackGroundWorker.IsBusy)
                {
                    //
                    // Change button text and UI appearance
                    //
                    calibrationStartStopCheckBox.Text = "Stop";
                    calibrationShortCircuitCheckBox.Enabled = false;
                    calibrationStepperUpCheckBox.Enabled = false;
                    generalSettingsPanel.Enabled = false;
                    calibrationBackGroundWorker.RunWorkerAsync();
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
                if (calibrationBackGroundWorker.WorkerSupportsCancellation == true)
                {
                    calibrationBackGroundWorker.CancelAsync();
                }
                if (m_sbjController.Task != null)
                {
                    try
                    {
                        m_sbjController.Task.Control(TaskAction.Abort);
                    }
                    catch(Exception)
                    {
                    }
                }
                m_sbjController.QuitJunctionOpenningOperation = true;
                m_sbjController.StepperMotor.Shutdown();
            }
        }

        private void calibrationBackGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // We've done taking data so we must bring the UI appearance
            //
            calibrationStartStopCheckBox.Text = "Start";
            calibrationStartStopCheckBox.Checked = false;
            calibrationShortCircuitCheckBox.Enabled = true;
            calibrationStepperUpCheckBox.Enabled = true;
            generalSettingsPanel.Enabled = true;

            //
            // if we applied the bias by the DAQ device, we need to stop the task. 
            //
            m_sbjController.StopApplyingVoltageIfNeeded();
        }

        private void calibrationBackGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            m_sbjController.AquireCalibrationData(GetCalibrationSettings(), worker, e);
        }

        private CalibrationSettings GetCalibrationSettings()
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
                return (CalibrationSettings)this.Invoke(new Func<CalibrationSettings>(() => GetCalibrationSettings()));
            }
            else
            {
                //
                // Apparently this function was called from a safe thread so just carry on with
                // what we were planning on doing.
                //
                return new CalibrationSettings(new CalibrationGeneralSettings(this.calibrationBiasNumericEdit.Value,
                                                                                                    this.calibrationGainPowerComboBox.Text,
                                                                                                    this.calibrationTriggerVoltageNumericEdit.Value,
                                                                                                    this.calibrationTriggerConductanceNumericEdit.Value,
                                                                                                    this.calibrationSavingFilesCheckBox.Checked,
                                                                                                    (int)this.calibrationSampleRateNumericEdit.Value,
                                                                                                    this.calibrationPathTextBox.Text,
                                                                                                    (int)this.calibrationCycleNumberNumericUpDown.Value,
                                                                                                    (int)this.calibrationNumberOfCyclesNumericUpDown.Value,
                                                                                                    (double)this.calibrationShortCircuitVoltageumericUpDown.Value,
                                                                                                    this.calibrationEnableElectroMagnetCheckBox.Checked,
                                                                                                    this.calibrationKeithleyCheckBox.Checked,
                                                                                                    (int)this.calibrationDelayTimeNumericUpDown.Value,
                                                                                                    GetCalibrationMeasurementType()),
                                                               new ElectroMagnetSBJControllerSettings(this.calibrationEnableElectroMagnetCheckBox.Checked,
                                                                                                    (int)this.calibrationEMShortCircuitDelayTimeNumericUpDown.Value,
                                                                                                    (int)this.calibrationEMFastDelayTimeNumericUpDown.Value,
                                                                                                    (int)this.calibrationEMSlowDelayTimeNumericUpDown.Value,
                                                                                                    this.emHoldOnToConductanceRangeCheckBox.Checked,
                                                                                                    this.emHoldOnMaxConductanceNumericEdit.Value,
                                                                                                    this.emHoldOnMaxVoltageNumericEdit.Value,
                                                                                                    this.emHoldOnMinConductanceNumericEdit.Value,
                                                                                                    this.emHoldOnMinVoltageNumericEdit.Value,
                                                                                                    this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Checked),
                                                               new ChannelsSettings(GetCalibrationActiveChannels()));

            }
        }

        private CalibrationMeasurementType GetCalibrationMeasurementType()
        {
            return (CalibrationMeasurementType)Enum.Parse(typeof(CalibrationMeasurementType), this.calibrationMeasurementTypeComboBox.Text);
        }

        /// <summary>
        /// Update UI parameters once bias is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationbiasNumericEdit_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        {
            BiasUpdate(this.calibrationBiasNumericEdit.Value, int.Parse(this.calibrationGainPowerComboBox.Text), this.calibrationKeithleyCheckBox.Checked,
                        this.calibrationTriggerConductanceNumericEdit, this.calibrationTriggerVoltageNumericEdit);
        }
       
        /// <summary>
        /// On gain changed. Make sure amplifier is updated accordingly and other UI parameters as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationGainComboBox_ValueChanged(object sender, EventArgs e)
        {
            GainUpdate(this.calibrationBiasNumericEdit.Value, int.Parse(this.calibrationGainPowerComboBox.Text),
                        this.calibrationTriggerConductanceNumericEdit, this.calibrationTriggerVoltageNumericEdit);
        }
        
        /// <summary>
        /// On tirgger conductance change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationTriggerConductanceNumericEdit_AfterChangeValue(object sender, NationalInstruments.UI.AfterChangeNumericValueEventArgs e)
        {
            this.calibrationTriggerVoltageNumericEdit.Value = -this.calibrationTriggerConductanceNumericEdit.Value * m_1G0 * Math.Abs(this.calibrationBiasNumericEdit.Value) * Math.Pow(10, int.Parse(this.calibrationGainPowerComboBox.Text));
        }
             
        /// <summary>
        /// Enable or disable the ElectroMagnet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationEnableElectroMagnetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //
            // Update the appearance of other UI related parameters.
            //
            this.calibrationEMFastDelayTimeLabel.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
            this.calibrationEMFastDelayTimeNumericUpDown.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
            this.calibrationEMSlowDelayTimeLabel.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
            this.calibrationEMSlowDelayTimeNumericUpDown.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
            this.calibrationEMShortCircuitDelayTimeLabel.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Enabled = this.calibrationEnableElectroMagnetCheckBox.Checked;
        }
        
        /// <summary>
        /// Update UI appearnce when channels are checked \ unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationChannel1ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (calibrationChannel1CheckBox.Checked)
            {
                UpdateChannelsToDisplayListView();
            }
        }
        
        /// <summary>
        /// Update channels appearance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationChannel1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChannelsToDisplayListView();
        }
      
        /// <summary>
        /// On path changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationPathTextBox_TextChanged(object sender, EventArgs e)
        {
            //
            // The folder in which we save the files has been changed, so we need to set the file number back to zero.
            //            
            this.calibrationCycleNumberNumericUpDown.Value = 0;
        }
        
        /// <summary>
        /// Select path for saving files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationBrowseButton_Click(object sender, EventArgs e)
        {
            BrowseButtonFunction(this.calibrationPathTextBox);
        }

        /// <summary>
        /// Fired when short circuit button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationShortCircuitButton_CheckedChanged(object sender, EventArgs e)
        {
            ShortCircuitButtonFunction(this.calibrationShortCircuitCheckBox.Checked);
        }
        
        /// <summary>
        /// Open the folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationOpenFolderButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.calibrationPathTextBox.Text);
        }
        
        /// <summary>
        /// Connect to the keithley if the use keithley checkBox is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibrationUseKeithleyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.calibrationKeithleyCheckBox.Checked)
            {
                m_sbjController.SourceMeter.Connect();
                m_sbjController.SourceMeter.SetBias(this.calibrationBiasNumericEdit.Value);
            }
        }

        private void calibrationStepperUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            StepperUpButtonFunction(this.calibrationStepperUpCheckBox.Checked);
        }
    
        #region Native Dll
        [DllImport("IODrive2007.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void setParallelPortMode(int mode);
        #endregion
    

        #endregion

        #region IV Tab
        /// <summary>
        /// Select path for saving files on the IV Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivBrowseButton_Click(object sender, EventArgs e)
        {
            BrowseButtonFunction(this.ivPathTextBox);
    }

        /// <summary>
        /// Open the folder on from the path on the IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivOpenFolderButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.ivPathTextBox.Text);
        }

        /// <summary>
        /// choose if to use the stepper motor on the IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivStepperMotorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ivStepperMotorRadioButton.Checked)
            {
                this.ivElectroMagnetRadioButton.Checked = false;
                this.ivElectroMagnetGroupBox.Enabled = false;
                this.ivStepperMotorGroupBox.Enabled = true;
            }
            }

        /// <summary>
        /// chosse if to use the electromagnet on the IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivElectroMagnetRadioButton_CheckedChanged(object sender, EventArgs e)
            {
            if (this.ivElectroMagnetRadioButton.Checked)
            {
                this.ivStepperMotorRadioButton.Checked = false;
                this.ivElectroMagnetGroupBox.Enabled = true;
                this.ivStepperMotorGroupBox.Enabled = false;
            }
        }

        /// <summary>
        /// on path changed in the IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivPathTextBox_TextChanged(object sender, EventArgs e)
        {
            //
            // The folder in which we save the files has been changed, so we need to set the file number back to zero.
            //            
            this.fileNumberNumericUpDown.Value = 0;
            }

        /// <summary>
        /// On gain changed on IV tab. 
        /// Make sure amplifier is updated accordingly and other UI parameters as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivGainPoweComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GainUpdate(this.ivVoltageForTraceNumericEdit.Value, int.Parse(this.ivGainPoweComboBox.Text),
                        this.ivTriggerConductanceNumericEdit, this.ivTriggerVoltageNumericEdit);
        }

        /// <summary>
        /// On trigger conductance change on IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivTriggerConductanceNumericEdit_AfterChangeValue(object sender, AfterChangeNumericValueEventArgs e)
        {
            this.ivTriggerVoltageNumericEdit.Value = -this.ivTriggerConductanceNumericEdit.Value * m_1G0 * Math.Abs(this.ivVoltageForTraceNumericEdit.Value) * Math.Pow(10, int.Parse(this.ivGainPoweComboBox.Text));
            }

        /// <summary>
        /// On samples per cycle change on IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivSamplesPerCycleNumericEdit_AfterChangeValue(object sender, AfterChangeNumericValueEventArgs e)
        {
            this.ivTimeOfOneIVCycleNumericEdit.Value = this.ivOutputUpdateDelayNumericEdit.Value * this.ivSamplesPerCycleNumericEdit.Value;
            }

        /// <summary>
        /// On Output update delay change on IV tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ivOutputUpdateDelayNumericEdit_AfterChangeValue(object sender, AfterChangeNumericValueEventArgs e)
        {
            this.ivTimeOfOneIVCycleNumericEdit.Value = this.ivOutputUpdateDelayNumericEdit.Value * this.ivSamplesPerCycleNumericEdit.Value;
            this.ivOutputUpdateRateNumericEdit.Value = 1000 / e.NewValue;
            }

        private void ivStepperUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            StepperUpButtonFunction(this.ivStepperUpCheckBox.Checked);
        }

        private void ivShortCircuitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShortCircuitButtonFunction(this.ivShortCircuitCheckBox.Checked);
        }

        /// <summary>
        /// IV Tab - get settings from UI
        /// </summary>
        /// <returns></returns>
        private IVSettings GetIVSettings()
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
                return (IVSettings)this.Invoke(new Func<IVSettings>(() => GetIVSettings()));
            }
            else
            {
                //
                // Apparently this function was called from a safe thread so just carry on with
                // what we were planning on doing.
                //
                return new IVSettings(new IVGeneralSettings(this.ivVoltageAmplitudeNumericEdit.Value, 
                                                            (int)this.ivSamplesPerCycleNumericEdit.Value, 
                                                            this.ivOutputUpdateDelayNumericEdit.Value, 
                                                            (int)this.ivOutputUpdateRateNumericEdit.Value, 
                                                            this.ivVoltageForTraceNumericEdit.Value, 
                                                            this.ivTimeOfOneIVCycleNumericEdit.Value, 
                                                            this.ivGainPoweComboBox.Text, 
                                                            this.ivTriggerVoltageNumericEdit.Value, 
                                                            this.ivTriggerConductanceNumericEdit.Value, 
                                                            (int)this.ivInputSampleRateNumericUpDown.Value, 
                                                            this.ivFileSavingCheckBox.Checked, 
                                                            this.ivPathTextBox.Text, 
                                                            (int)this.ivFileNumberNumericUpDown.Value, 
                                                            (int)this.ivNumberOfCyclesNumericUpDown.Value, 
                                                            (double)this.ivShortCircuitVoltageNumericUpDown.Value,
                                                            (Sample)this.bottomPropertyGrid.SelectedObject,
                                                            (Sample)this.bottomPropertyGrid.SelectedObject), 
                                new IVSteppingMethodSettings(GetSteppingDevice(), 
                                                            (int)this.ivStepperDelayTime1NumericUpDown.Value, 
                                                            (int)this.ivStepperDelayTime2NumericUpDown.Value, 
                                                            (int)this.ivEMShortCircuitDelayTimeNumericUpDown.Value, 
                                                            (int)this.ivEMFastDelayTimeNumericUpDown.Value, 
                                                            (int)this.ivEMSlowDelayTimeNumericUpDown.Value, 
                                                            this.ivEMSkipStepperMotorCheckBox.Checked),
                                new ChannelsSettings(GetIVActiveChannels()));
            }
        }

        /// <summary>
        /// On IV tab: return what is the requested stepping device
        /// </summary>
        /// <returns></returns>
        private SteppingDevice GetSteppingDevice()
        {
            if (this.ivStepperMotorRadioButton.Checked)
            {
                return SteppingDevice.StepperMotor;
            }
            else
            {
                return SteppingDevice.ElectroMagnet;
            }
        }        
        #endregion    
    }
}
