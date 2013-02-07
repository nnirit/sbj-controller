using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.Drawing;
using NationalInstruments.UI;

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
            m_sbjController.FixBias((double)shortCircuitVoltageNumericUpDown.Value, worker, e);
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
                this.biasErrorNumericEdit.Value = (double)e.Result;
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
            this.triggerVoltageNumericEdit.Value = -this.triggerConductanceNumericEdit.Value * m_1G0 * this.biasNumericEdit.Value * Math.Pow(10, gainPower);            
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
            this.triggerConductanceNumericEdit.Value = GetTriggerConductance();
            this.triggerVoltageNumericEdit.Value = -this.triggerConductanceNumericEdit.Value * m_1G0 * this.biasNumericEdit.Value * Math.Pow(10, int.Parse(this.gainComboBox.Text));            
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
            this.triggerVoltageNumericEdit.Value = -this.triggerConductanceNumericEdit.Value * m_1G0 * this.biasNumericEdit.Value * Math.Pow(10, int.Parse(this.gainComboBox.Text));
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

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            //
            // The folder in which we save the files has been changed, so we need to set the file number back to zero.
            //            
            this.fileNumberNumericUpDown.Value = 0;
        }

        private void totalSamplesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            //
            // the total number of samples was changed - the trigger usually should be after 85% of the samples.
            //
            pretriggerSamplesNumericUpDown.Value = (decimal)0.85 * totalSamplesNumericUpDown.Value;
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
                this.waveformPlot1.ClearData();
                double[,] data = GetDataAsConductionValues(e.Data);
                int numberOfChannels = data.GetLength(0);
                this.traceWaveformGraph.PlotYMultiple(data);
       
                if (numberOfChannels > 1)
                {
                    this.traceWaveformGraph.Plots[1].PointColor = Color.Blue;
                    this.traceWaveformGraph.Plots[1].YAxis = yAxis2;
                    this.traceWaveformGraph.Plots[1].ToolTipsEnabled = true;
                    this.traceWaveformGraph.Plots[1].LineColor = Color.Blue;
                    if (numberOfChannels > 2)
                    {
                        //this.traceWaveformGraph.Plots[2].YAxis = yAxis1;
                        //this.traceWaveformGraph.Plots[2].ToolTipsEnabled = true;
                        //this.traceWaveformGraph.Plots[2].LineColor = Color.Black;
                    }
                }
                
                this.traceWaveformGraph.Caption = string.Format("Trace #{0} at {1}", this.fileNumberNumericUpDown.Value, DateTime.Now.TimeOfDay);        
            }
        }

        private double[,] GetDataAsConductionValues(double[,] rawData)
        {
            int numberOfChannels = rawData.GetLength(0);
            int numberOfDataPoints = rawData.GetLength(1);
            double[,] data = new double[numberOfChannels, numberOfDataPoints];
            int gain = Int32.Parse(this.gainComboBox.Text);
            double bias = this.biasNumericEdit.Value;
           
            for (int i = 0; i < numberOfDataPoints; i++)
            {
                double rawDataPoint = Math.Abs(rawData[0, i]);
                data[0, i] = rawDataPoint / Math.Pow(10, gain) / bias / m_1G0;

                if (numberOfChannels > 1)
                {
                    //
                    // The lock in siganl is normalized to the range of [0 10] so that at overload when
                    // maximum sensitivity value is detected, the out out is 10V.
                    //
                    double normalizationFactor = (10 / this.sensitivityNumericEdit.Value);
                    double lockInSignalDataPoint = rawData[1, i];
                    data[1, i] = lockInSignalDataPoint / Math.Pow(10, gain) / bias / m_1G0 / normalizationFactor;

                    if (numberOfChannels > 2)
                    {
                        double lockInPhaseDataPoint = rawData[2, i];
                        data[2, i] = lockInPhaseDataPoint;
                    }
                }
            }
            return data;
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
                                                  new LockInSBJControllerSettings(this.sampleLockInSignalCheckBox.Checked,
                                                                                  this.samplePhaseCheckBox.Checked,
                                                                                  this.sensitivityNumericEdit.Value),
                                                  new ElectroMagnetSBJControllerSettings(this.enableElectroMagnetCheckBox.Checked,
                                                                                         (int)this.emShortCircuitDelayTimeNumericUpDown.Value,
                                                                                         (int)this.emFastDelayTimeNumericUpDown.Value,
                                                                                         (int)this.emSlowDelayTimeNumericUpDown.Value,
                                                                                         this.emHoldOnToConductanceRangeCheckBox.Checked,
                                                                                         this.emHoldOnMaxConductanceNumericEdit.Value,
                                                                                         this.emHoldOnMaxVoltageNumericEdit.Value,
                                                                                         this.emHoldOnMinConductanceNumericEdit.Value,
                                                                                         this.emHoldOnMinVoltageNumericEdit.Value,
                                                                                         this.emSkipFirstCycleByStepperMotorCheckBox.Checked));
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
            double maxConductance = m_maxVoltage / Math.Pow(10, int.Parse(this.gainComboBox.Text)) / this.biasNumericEdit.Value / m_1G0;
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
            return -conductance * m_1G0 * this.biasNumericEdit.Value * Math.Pow(10, gainPower);
        }
        #endregion

        
    }
}
