namespace SBJController
{
    partial class SBJControllerMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.obtainShortCircuitBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.controllerTabControl = new System.Windows.Forms.TabControl();
            this.dataAquisitionTabPage = new System.Windows.Forms.TabPage();
            this.operateGroupBox = new System.Windows.Forms.GroupBox();
            this.fixBiasCheckBoxButton = new System.Windows.Forms.CheckBox();
            this.moveUpCheckBoxButton = new System.Windows.Forms.CheckBox();
            this.startStopCheckBoxButton = new System.Windows.Forms.CheckBox();
            this.shortCircuitCheckBoxButton = new System.Windows.Forms.CheckBox();
            this.numberOfCyclesnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.numberOfCyclesLabel1 = new System.Windows.Forms.Label();
            this.shortCircuitVoltageNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.shortCircuitVoltageLabel = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.fileSavingCheckBox = new System.Windows.Forms.CheckBox();
            this.fileNumberLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.fileNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.traceWaveformGraph = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot1 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis1 = new NationalInstruments.UI.XAxis();
            this.yAxis1 = new NationalInstruments.UI.YAxis();
            this.yAxis2 = new NationalInstruments.UI.YAxis();
            this.samplePropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.openLogBookButton = new System.Windows.Forms.Button();
            this.saveSamplesParamsButton = new System.Windows.Forms.Button();
            this.samplePropertiesTabControl = new System.Windows.Forms.TabControl();
            this.bottomTabPage = new System.Windows.Forms.TabPage();
            this.bottomPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.TopTabPage = new System.Windows.Forms.TabPage();
            this.topPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.SettingsTabControl = new System.Windows.Forms.TabControl();
            this.generalSettingsTabPage = new System.Windows.Forms.TabPage();
            this.generalSettingsPanel = new System.Windows.Forms.Panel();
            this.biasErrorNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.biasErrorLabel = new System.Windows.Forms.Label();
            this.gainComboBox = new System.Windows.Forms.ComboBox();
            this.stepperWaitTime2NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stepperWaitTime2Label = new System.Windows.Forms.Label();
            this.pretriggerSamplesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pretriggerSamplesLabel = new System.Windows.Forms.Label();
            this.sampleRateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.totalSamplesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.totalSamplesLabel = new System.Windows.Forms.Label();
            this.triggerVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.triggerVoltageLabel = new System.Windows.Forms.Label();
            this.triggerConductanceNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.triggerConductanceLabel = new System.Windows.Forms.Label();
            this.sampleRateLabel = new System.Windows.Forms.Label();
            this.stepperWaitTime1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stepperWaitTime1Label = new System.Windows.Forms.Label();
            this.gainLabel = new System.Windows.Forms.Label();
            this.biasNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.biasLabel = new System.Windows.Forms.Label();
            this.laserSettingsTabPage = new System.Windows.Forms.TabPage();
            this.laserSettingsPanel = new System.Windows.Forms.Panel();
            this.laserModeComboBox = new System.Windows.Forms.ComboBox();
            this.frequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.enableLaserCheckBox = new System.Windows.Forms.CheckBox();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.amplitudeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.laserAmplitudeLabel = new System.Windows.Forms.Label();
            this.LockInSettingsTabPage = new System.Windows.Forms.TabPage();
            this.lockInPanel = new System.Windows.Forms.Panel();
            this.samplePhaseCheckBox = new System.Windows.Forms.CheckBox();
            this.sampleLockInSignalCheckBox = new System.Windows.Forms.CheckBox();
            this.sensitivityNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.sensitivityLabel = new System.Windows.Forms.Label();
            this.ElectroMagnetTabPage = new System.Windows.Forms.TabPage();
            this.electroMagnetSettingsPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.emHoldOnMinVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.emHoldOnMinVoltageLabel = new System.Windows.Forms.Label();
            this.emHoldOnMinConductanceLabel = new System.Windows.Forms.Label();
            this.emHoldOnMinConductanceNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.emHoldOnMaxVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.emHoldOnMaxVoltageLabel = new System.Windows.Forms.Label();
            this.emHoldOnMaxConductanceLabel = new System.Windows.Forms.Label();
            this.emHoldOnMaxConductanceNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.emHoldOnToConductanceRangeCheckBox = new System.Windows.Forms.CheckBox();
            this.emSkipFirstCycleByStepperMotorCheckBox = new System.Windows.Forms.CheckBox();
            this.emShortCircuitDelayTimeLabel = new System.Windows.Forms.Label();
            this.emShortCircuitDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.emSlowDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.emSlowDelayTimeLabel = new System.Windows.Forms.Label();
            this.emFastDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.enableElectroMagnetCheckBox = new System.Windows.Forms.CheckBox();
            this.emFastDelayTimeLabel = new System.Windows.Forms.Label();
            this.controlPanelsTabPage = new System.Windows.Forms.TabPage();
            this.electroMagnetGroupBox = new System.Windows.Forms.GroupBox();
            this.electroMagnetUserControl2 = new ElectroMagnetUserControl();
            this.stepperMotorGroupBox = new System.Windows.Forms.GroupBox();
            this.stepperMotorUserControl1 = new StepperMotorUserControl();
            this.aquireDataBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.stepperUpBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.fixBiasBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.controllerTabControl.SuspendLayout();
            this.dataAquisitionTabPage.SuspendLayout();
            this.operateGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCyclesnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shortCircuitVoltageNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileNumberNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.traceWaveformGraph)).BeginInit();
            this.samplePropertiesGroupBox.SuspendLayout();
            this.samplePropertiesTabControl.SuspendLayout();
            this.bottomTabPage.SuspendLayout();
            this.TopTabPage.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            this.SettingsTabControl.SuspendLayout();
            this.generalSettingsTabPage.SuspendLayout();
            this.generalSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.biasErrorNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepperWaitTime2NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pretriggerSamplesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleRateNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalSamplesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerVoltageNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerConductanceNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepperWaitTime1NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.biasNumericEdit)).BeginInit();
            this.laserSettingsTabPage.SuspendLayout();
            this.laserSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amplitudeNumericUpDown)).BeginInit();
            this.LockInSettingsTabPage.SuspendLayout();
            this.lockInPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensitivityNumericEdit)).BeginInit();
            this.ElectroMagnetTabPage.SuspendLayout();
            this.electroMagnetSettingsPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMinVoltageNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMinConductanceNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMaxVoltageNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMaxConductanceNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emShortCircuitDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emSlowDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emFastDelayTimeNumericUpDown)).BeginInit();
            this.controlPanelsTabPage.SuspendLayout();
            this.electroMagnetGroupBox.SuspendLayout();
            this.stepperMotorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // obtainShortCircuitBackgroundWorker
            // 
            this.obtainShortCircuitBackgroundWorker.WorkerReportsProgress = true;
            this.obtainShortCircuitBackgroundWorker.WorkerSupportsCancellation = true;
            this.obtainShortCircuitBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.obtainShortCircuitBackgroundWorker_DoWork);
            this.obtainShortCircuitBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.obtainSHortCircuitBackgroundWorker_RunWorkerCompleted);
            // 
            // controllerTabControl
            // 
            this.controllerTabControl.Controls.Add(this.dataAquisitionTabPage);
            this.controllerTabControl.Controls.Add(this.controlPanelsTabPage);
            this.controllerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controllerTabControl.Location = new System.Drawing.Point(0, 0);
            this.controllerTabControl.Name = "controllerTabControl";
            this.controllerTabControl.SelectedIndex = 0;
            this.controllerTabControl.Size = new System.Drawing.Size(749, 825);
            this.controllerTabControl.TabIndex = 2;
            this.controllerTabControl.Tag = "";
            this.controllerTabControl.Deselected += new System.Windows.Forms.TabControlEventHandler(this.controllerTabControl_Deselected);
            // 
            // dataAquisitionTabPage
            // 
            this.dataAquisitionTabPage.AutoScroll = true;
            this.dataAquisitionTabPage.Controls.Add(this.operateGroupBox);
            this.dataAquisitionTabPage.Controls.Add(this.traceWaveformGraph);
            this.dataAquisitionTabPage.Controls.Add(this.samplePropertiesGroupBox);
            this.dataAquisitionTabPage.Controls.Add(this.settingsGroupBox);
            this.dataAquisitionTabPage.Location = new System.Drawing.Point(4, 22);
            this.dataAquisitionTabPage.Name = "dataAquisitionTabPage";
            this.dataAquisitionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dataAquisitionTabPage.Size = new System.Drawing.Size(741, 799);
            this.dataAquisitionTabPage.TabIndex = 0;
            this.dataAquisitionTabPage.Text = "DAQ";
            this.dataAquisitionTabPage.UseVisualStyleBackColor = true;
            // 
            // operateGroupBox
            // 
            this.operateGroupBox.AutoSize = true;
            this.operateGroupBox.Controls.Add(this.fixBiasCheckBoxButton);
            this.operateGroupBox.Controls.Add(this.moveUpCheckBoxButton);
            this.operateGroupBox.Controls.Add(this.startStopCheckBoxButton);
            this.operateGroupBox.Controls.Add(this.shortCircuitCheckBoxButton);
            this.operateGroupBox.Controls.Add(this.numberOfCyclesnumericUpDown);
            this.operateGroupBox.Controls.Add(this.numberOfCyclesLabel1);
            this.operateGroupBox.Controls.Add(this.shortCircuitVoltageNumericUpDown);
            this.operateGroupBox.Controls.Add(this.shortCircuitVoltageLabel);
            this.operateGroupBox.Controls.Add(this.pathTextBox);
            this.operateGroupBox.Controls.Add(this.browseButton);
            this.operateGroupBox.Controls.Add(this.fileSavingCheckBox);
            this.operateGroupBox.Controls.Add(this.fileNumberLabel);
            this.operateGroupBox.Controls.Add(this.pathLabel);
            this.operateGroupBox.Controls.Add(this.fileNumberNumericUpDown);
            this.operateGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.operateGroupBox.ForeColor = System.Drawing.Color.Red;
            this.operateGroupBox.Location = new System.Drawing.Point(3, 612);
            this.operateGroupBox.MinimumSize = new System.Drawing.Size(478, 176);
            this.operateGroupBox.Name = "operateGroupBox";
            this.operateGroupBox.Size = new System.Drawing.Size(735, 184);
            this.operateGroupBox.TabIndex = 23;
            this.operateGroupBox.TabStop = false;
            this.operateGroupBox.Text = "Operate";
            // 
            // fixBiasCheckBoxButton
            // 
            this.fixBiasCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.fixBiasCheckBoxButton.AutoSize = true;
            this.fixBiasCheckBoxButton.ForeColor = System.Drawing.Color.Black;
            this.fixBiasCheckBoxButton.Location = new System.Drawing.Point(365, 108);
            this.fixBiasCheckBoxButton.MinimumSize = new System.Drawing.Size(74, 23);
            this.fixBiasCheckBoxButton.Name = "fixBiasCheckBoxButton";
            this.fixBiasCheckBoxButton.Size = new System.Drawing.Size(74, 23);
            this.fixBiasCheckBoxButton.TabIndex = 18;
            this.fixBiasCheckBoxButton.Text = "Fix Bias";
            this.fixBiasCheckBoxButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fixBiasCheckBoxButton.UseVisualStyleBackColor = true;
            this.fixBiasCheckBoxButton.CheckedChanged += new System.EventHandler(this.fixBiasCheckBoxButton_CheckedChanged);
            // 
            // moveUpCheckBoxButton
            // 
            this.moveUpCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.moveUpCheckBoxButton.AutoSize = true;
            this.moveUpCheckBoxButton.ForeColor = System.Drawing.Color.Black;
            this.moveUpCheckBoxButton.Location = new System.Drawing.Point(365, 142);
            this.moveUpCheckBoxButton.MinimumSize = new System.Drawing.Size(74, 23);
            this.moveUpCheckBoxButton.Name = "moveUpCheckBoxButton";
            this.moveUpCheckBoxButton.Size = new System.Drawing.Size(74, 23);
            this.moveUpCheckBoxButton.TabIndex = 17;
            this.moveUpCheckBoxButton.Text = "Stepper Up";
            this.moveUpCheckBoxButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.moveUpCheckBoxButton.UseVisualStyleBackColor = true;
            this.moveUpCheckBoxButton.Click += new System.EventHandler(this.moveUpCheckBox_CheckedChanged);
            // 
            // startStopCheckBoxButton
            // 
            this.startStopCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.startStopCheckBoxButton.AutoSize = true;
            this.startStopCheckBoxButton.ForeColor = System.Drawing.Color.Black;
            this.startStopCheckBoxButton.Location = new System.Drawing.Point(273, 108);
            this.startStopCheckBoxButton.MinimumSize = new System.Drawing.Size(74, 23);
            this.startStopCheckBoxButton.Name = "startStopCheckBoxButton";
            this.startStopCheckBoxButton.Size = new System.Drawing.Size(74, 23);
            this.startStopCheckBoxButton.TabIndex = 16;
            this.startStopCheckBoxButton.Text = "Start";
            this.startStopCheckBoxButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.startStopCheckBoxButton.UseVisualStyleBackColor = true;
            this.startStopCheckBoxButton.Click += new System.EventHandler(this.startStopButton_CheckedChanged);
            // 
            // shortCircuitCheckBoxButton
            // 
            this.shortCircuitCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.shortCircuitCheckBoxButton.AutoSize = true;
            this.shortCircuitCheckBoxButton.ForeColor = System.Drawing.Color.Black;
            this.shortCircuitCheckBoxButton.Location = new System.Drawing.Point(273, 142);
            this.shortCircuitCheckBoxButton.MinimumSize = new System.Drawing.Size(74, 23);
            this.shortCircuitCheckBoxButton.Name = "shortCircuitCheckBoxButton";
            this.shortCircuitCheckBoxButton.Size = new System.Drawing.Size(74, 23);
            this.shortCircuitCheckBoxButton.TabIndex = 15;
            this.shortCircuitCheckBoxButton.Text = "Short Circuit";
            this.shortCircuitCheckBoxButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.shortCircuitCheckBoxButton.UseVisualStyleBackColor = true;
            this.shortCircuitCheckBoxButton.Click += new System.EventHandler(this.shortCircuitButton_CheckedChanged);
            // 
            // numberOfCyclesnumericUpDown
            // 
            this.numberOfCyclesnumericUpDown.Location = new System.Drawing.Point(150, 111);
            this.numberOfCyclesnumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberOfCyclesnumericUpDown.Name = "numberOfCyclesnumericUpDown";
            this.numberOfCyclesnumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.numberOfCyclesnumericUpDown.TabIndex = 14;
            this.numberOfCyclesnumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numberOfCyclesLabel1
            // 
            this.numberOfCyclesLabel1.AutoSize = true;
            this.numberOfCyclesLabel1.ForeColor = System.Drawing.Color.Black;
            this.numberOfCyclesLabel1.Location = new System.Drawing.Point(7, 113);
            this.numberOfCyclesLabel1.Name = "numberOfCyclesLabel1";
            this.numberOfCyclesLabel1.Size = new System.Drawing.Size(90, 13);
            this.numberOfCyclesLabel1.TabIndex = 13;
            this.numberOfCyclesLabel1.Text = "Number of Cycles";
            // 
            // shortCircuitVoltageNumericUpDown
            // 
            this.shortCircuitVoltageNumericUpDown.DecimalPlaces = 1;
            this.shortCircuitVoltageNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.shortCircuitVoltageNumericUpDown.Location = new System.Drawing.Point(150, 145);
            this.shortCircuitVoltageNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.shortCircuitVoltageNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.shortCircuitVoltageNumericUpDown.Name = "shortCircuitVoltageNumericUpDown";
            this.shortCircuitVoltageNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.shortCircuitVoltageNumericUpDown.TabIndex = 12;
            this.shortCircuitVoltageNumericUpDown.Value = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            // 
            // shortCircuitVoltageLabel
            // 
            this.shortCircuitVoltageLabel.AutoSize = true;
            this.shortCircuitVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.shortCircuitVoltageLabel.Location = new System.Drawing.Point(7, 147);
            this.shortCircuitVoltageLabel.Name = "shortCircuitVoltageLabel";
            this.shortCircuitVoltageLabel.Size = new System.Drawing.Size(119, 13);
            this.shortCircuitVoltageLabel.TabIndex = 11;
            this.shortCircuitVoltageLabel.Text = "Short Circuit Voltage [V]";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(39, 46);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(400, 20);
            this.pathTextBox.TabIndex = 9;
            this.pathTextBox.Text = "C:\\sbj\\Measurements";
            this.pathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.ForeColor = System.Drawing.Color.Black;
            this.browseButton.Location = new System.Drawing.Point(450, 44);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(74, 23);
            this.browseButton.TabIndex = 10;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // fileSavingCheckBox
            // 
            this.fileSavingCheckBox.AutoSize = true;
            this.fileSavingCheckBox.ForeColor = System.Drawing.Color.Black;
            this.fileSavingCheckBox.Location = new System.Drawing.Point(10, 23);
            this.fileSavingCheckBox.Name = "fileSavingCheckBox";
            this.fileSavingCheckBox.Size = new System.Drawing.Size(78, 17);
            this.fileSavingCheckBox.TabIndex = 5;
            this.fileSavingCheckBox.Text = "File Saving";
            this.fileSavingCheckBox.UseVisualStyleBackColor = true;
            // 
            // fileNumberLabel
            // 
            this.fileNumberLabel.AutoSize = true;
            this.fileNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.fileNumberLabel.Location = new System.Drawing.Point(7, 79);
            this.fileNumberLabel.Name = "fileNumberLabel";
            this.fileNumberLabel.Size = new System.Drawing.Size(63, 13);
            this.fileNumberLabel.TabIndex = 6;
            this.fileNumberLabel.Text = "File Number";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.ForeColor = System.Drawing.Color.Black;
            this.pathLabel.Location = new System.Drawing.Point(7, 49);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(29, 13);
            this.pathLabel.TabIndex = 8;
            this.pathLabel.Text = "Path";
            // 
            // fileNumberNumericUpDown
            // 
            this.fileNumberNumericUpDown.Location = new System.Drawing.Point(150, 77);
            this.fileNumberNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fileNumberNumericUpDown.Name = "fileNumberNumericUpDown";
            this.fileNumberNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.fileNumberNumericUpDown.TabIndex = 7;
            // 
            // traceWaveformGraph
            // 
            this.traceWaveformGraph.CaptionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.traceWaveformGraph.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traceWaveformGraph.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.traceWaveformGraph.InteractionMode = ((NationalInstruments.UI.GraphInteractionModes)((((((((NationalInstruments.UI.GraphInteractionModes.ZoomX | NationalInstruments.UI.GraphInteractionModes.ZoomY)
                        | NationalInstruments.UI.GraphInteractionModes.ZoomAroundPoint)
                        | NationalInstruments.UI.GraphInteractionModes.PanX)
                        | NationalInstruments.UI.GraphInteractionModes.PanY)
                        | NationalInstruments.UI.GraphInteractionModes.DragCursor)
                        | NationalInstruments.UI.GraphInteractionModes.DragAnnotationCaption)
                        | NationalInstruments.UI.GraphInteractionModes.EditRange)));
            this.traceWaveformGraph.Location = new System.Drawing.Point(153, 16);
            this.traceWaveformGraph.Name = "traceWaveformGraph";
            this.traceWaveformGraph.PlotAreaColor = System.Drawing.Color.LightGray;
            this.traceWaveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.traceWaveformGraph.Size = new System.Drawing.Size(492, 339);
            this.traceWaveformGraph.TabIndex = 21;
            this.traceWaveformGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis1});
            this.traceWaveformGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis1,
            this.yAxis2});
            // 
            // waveformPlot1
            // 
            this.waveformPlot1.LineColor = System.Drawing.Color.Red;
            this.waveformPlot1.ToolTipsEnabled = true;
            this.waveformPlot1.XAxis = this.xAxis1;
            this.waveformPlot1.YAxis = this.yAxis1;
            // 
            // xAxis1
            // 
            this.xAxis1.Caption = "Time [sec E-4]";
            this.xAxis1.Range = new NationalInstruments.UI.Range(0, 10000);
            // 
            // yAxis1
            // 
            this.yAxis1.Caption = "Conductance [G0]";
            // 
            // yAxis2
            // 
            this.yAxis2.Caption = "LockIn Signal";
            this.yAxis2.CaptionPosition = NationalInstruments.UI.YAxisPosition.Right;
            this.yAxis2.Position = NationalInstruments.UI.YAxisPosition.Right;
            this.yAxis2.Range = new NationalInstruments.UI.Range(-10, 10);
            // 
            // samplePropertiesGroupBox
            // 
            this.samplePropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.samplePropertiesGroupBox.AutoSize = true;
            this.samplePropertiesGroupBox.Controls.Add(this.openLogBookButton);
            this.samplePropertiesGroupBox.Controls.Add(this.saveSamplesParamsButton);
            this.samplePropertiesGroupBox.Controls.Add(this.samplePropertiesTabControl);
            this.samplePropertiesGroupBox.ForeColor = System.Drawing.Color.Red;
            this.samplePropertiesGroupBox.Location = new System.Drawing.Point(546, 364);
            this.samplePropertiesGroupBox.Name = "samplePropertiesGroupBox";
            this.samplePropertiesGroupBox.Size = new System.Drawing.Size(183, 247);
            this.samplePropertiesGroupBox.TabIndex = 20;
            this.samplePropertiesGroupBox.TabStop = false;
            this.samplePropertiesGroupBox.Text = "Sample Properties";
            // 
            // openLogBookButton
            // 
            this.openLogBookButton.ForeColor = System.Drawing.Color.Black;
            this.openLogBookButton.Location = new System.Drawing.Point(86, 205);
            this.openLogBookButton.Name = "openLogBookButton";
            this.openLogBookButton.Size = new System.Drawing.Size(74, 23);
            this.openLogBookButton.TabIndex = 19;
            this.openLogBookButton.Text = "Open";
            this.openLogBookButton.UseVisualStyleBackColor = true;
            this.openLogBookButton.Click += new System.EventHandler(this.openLogBookButton_Click);
            // 
            // saveSamplesParamsButton
            // 
            this.saveSamplesParamsButton.ForeColor = System.Drawing.Color.Black;
            this.saveSamplesParamsButton.Location = new System.Drawing.Point(6, 205);
            this.saveSamplesParamsButton.Name = "saveSamplesParamsButton";
            this.saveSamplesParamsButton.Size = new System.Drawing.Size(74, 23);
            this.saveSamplesParamsButton.TabIndex = 18;
            this.saveSamplesParamsButton.Text = "Save";
            this.saveSamplesParamsButton.UseVisualStyleBackColor = true;
            this.saveSamplesParamsButton.Click += new System.EventHandler(this.saveSamplesParamsButton_Click);
            // 
            // samplePropertiesTabControl
            // 
            this.samplePropertiesTabControl.Controls.Add(this.bottomTabPage);
            this.samplePropertiesTabControl.Controls.Add(this.TopTabPage);
            this.samplePropertiesTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.samplePropertiesTabControl.Location = new System.Drawing.Point(3, 16);
            this.samplePropertiesTabControl.MinimumSize = new System.Drawing.Size(150, 0);
            this.samplePropertiesTabControl.Name = "samplePropertiesTabControl";
            this.samplePropertiesTabControl.SelectedIndex = 0;
            this.samplePropertiesTabControl.Size = new System.Drawing.Size(177, 183);
            this.samplePropertiesTabControl.TabIndex = 0;
            // 
            // bottomTabPage
            // 
            this.bottomTabPage.Controls.Add(this.bottomPropertyGrid);
            this.bottomTabPage.Location = new System.Drawing.Point(4, 22);
            this.bottomTabPage.Name = "bottomTabPage";
            this.bottomTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.bottomTabPage.Size = new System.Drawing.Size(169, 157);
            this.bottomTabPage.TabIndex = 0;
            this.bottomTabPage.Text = "Bottom";
            this.bottomTabPage.UseVisualStyleBackColor = true;
            // 
            // bottomPropertyGrid
            // 
            this.bottomPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPropertyGrid.HelpVisible = false;
            this.bottomPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.bottomPropertyGrid.MinimumSize = new System.Drawing.Size(150, 0);
            this.bottomPropertyGrid.Name = "bottomPropertyGrid";
            this.bottomPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.bottomPropertyGrid.Size = new System.Drawing.Size(163, 151);
            this.bottomPropertyGrid.TabIndex = 1;
            // 
            // TopTabPage
            // 
            this.TopTabPage.Controls.Add(this.topPropertyGrid);
            this.TopTabPage.Location = new System.Drawing.Point(4, 22);
            this.TopTabPage.Name = "TopTabPage";
            this.TopTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TopTabPage.Size = new System.Drawing.Size(169, 157);
            this.TopTabPage.TabIndex = 1;
            this.TopTabPage.Text = "Top";
            this.TopTabPage.UseVisualStyleBackColor = true;
            // 
            // topPropertyGrid
            // 
            this.topPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPropertyGrid.HelpVisible = false;
            this.topPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.topPropertyGrid.Name = "topPropertyGrid";
            this.topPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.topPropertyGrid.Size = new System.Drawing.Size(163, 151);
            this.topPropertyGrid.TabIndex = 0;
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.AutoSize = true;
            this.settingsGroupBox.Controls.Add(this.SettingsTabControl);
            this.settingsGroupBox.ForeColor = System.Drawing.Color.Red;
            this.settingsGroupBox.Location = new System.Drawing.Point(6, 364);
            this.settingsGroupBox.MinimumSize = new System.Drawing.Size(521, 200);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(534, 247);
            this.settingsGroupBox.TabIndex = 22;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // SettingsTabControl
            // 
            this.SettingsTabControl.Controls.Add(this.generalSettingsTabPage);
            this.SettingsTabControl.Controls.Add(this.laserSettingsTabPage);
            this.SettingsTabControl.Controls.Add(this.LockInSettingsTabPage);
            this.SettingsTabControl.Controls.Add(this.ElectroMagnetTabPage);
            this.SettingsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTabControl.Location = new System.Drawing.Point(3, 16);
            this.SettingsTabControl.Name = "SettingsTabControl";
            this.SettingsTabControl.SelectedIndex = 0;
            this.SettingsTabControl.Size = new System.Drawing.Size(528, 228);
            this.SettingsTabControl.TabIndex = 17;
            // 
            // generalSettingsTabPage
            // 
            this.generalSettingsTabPage.Controls.Add(this.generalSettingsPanel);
            this.generalSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalSettingsTabPage.Name = "generalSettingsTabPage";
            this.generalSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalSettingsTabPage.Size = new System.Drawing.Size(520, 202);
            this.generalSettingsTabPage.TabIndex = 0;
            this.generalSettingsTabPage.Text = "General";
            this.generalSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // generalSettingsPanel
            // 
            this.generalSettingsPanel.Controls.Add(this.biasErrorNumericEdit);
            this.generalSettingsPanel.Controls.Add(this.biasErrorLabel);
            this.generalSettingsPanel.Controls.Add(this.gainComboBox);
            this.generalSettingsPanel.Controls.Add(this.stepperWaitTime2NumericUpDown);
            this.generalSettingsPanel.Controls.Add(this.stepperWaitTime2Label);
            this.generalSettingsPanel.Controls.Add(this.pretriggerSamplesNumericUpDown);
            this.generalSettingsPanel.Controls.Add(this.pretriggerSamplesLabel);
            this.generalSettingsPanel.Controls.Add(this.sampleRateNumericUpDown);
            this.generalSettingsPanel.Controls.Add(this.totalSamplesNumericUpDown);
            this.generalSettingsPanel.Controls.Add(this.totalSamplesLabel);
            this.generalSettingsPanel.Controls.Add(this.triggerVoltageNumericEdit);
            this.generalSettingsPanel.Controls.Add(this.triggerVoltageLabel);
            this.generalSettingsPanel.Controls.Add(this.triggerConductanceNumericEdit);
            this.generalSettingsPanel.Controls.Add(this.triggerConductanceLabel);
            this.generalSettingsPanel.Controls.Add(this.sampleRateLabel);
            this.generalSettingsPanel.Controls.Add(this.stepperWaitTime1NumericUpDown);
            this.generalSettingsPanel.Controls.Add(this.stepperWaitTime1Label);
            this.generalSettingsPanel.Controls.Add(this.gainLabel);
            this.generalSettingsPanel.Controls.Add(this.biasNumericEdit);
            this.generalSettingsPanel.Controls.Add(this.biasLabel);
            this.generalSettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalSettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.generalSettingsPanel.MinimumSize = new System.Drawing.Size(0, 151);
            this.generalSettingsPanel.Name = "generalSettingsPanel";
            this.generalSettingsPanel.Size = new System.Drawing.Size(514, 196);
            this.generalSettingsPanel.TabIndex = 16;
            // 
            // biasErrorNumericEdit
            // 
            this.biasErrorNumericEdit.CoercionInterval = 0.01;
            this.biasErrorNumericEdit.Enabled = false;
            this.biasErrorNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(4);
            this.biasErrorNumericEdit.Location = new System.Drawing.Point(147, 37);
            this.biasErrorNumericEdit.Name = "biasErrorNumericEdit";
            this.biasErrorNumericEdit.Range = new NationalInstruments.UI.Range(-2, 2);
            this.biasErrorNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.biasErrorNumericEdit.TabIndex = 20;
            // 
            // biasErrorLabel
            // 
            this.biasErrorLabel.AutoSize = true;
            this.biasErrorLabel.ForeColor = System.Drawing.Color.Black;
            this.biasErrorLabel.Location = new System.Drawing.Point(4, 39);
            this.biasErrorLabel.Name = "biasErrorLabel";
            this.biasErrorLabel.Size = new System.Drawing.Size(68, 13);
            this.biasErrorLabel.TabIndex = 19;
            this.biasErrorLabel.Text = "Bias Error [V]";
            // 
            // gainComboBox
            // 
            this.gainComboBox.DisplayMember = "3";
            this.gainComboBox.FormattingEnabled = true;
            this.gainComboBox.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.gainComboBox.Location = new System.Drawing.Point(147, 65);
            this.gainComboBox.Name = "gainComboBox";
            this.gainComboBox.Size = new System.Drawing.Size(75, 21);
            this.gainComboBox.TabIndex = 18;
            this.gainComboBox.Text = "5";
            this.gainComboBox.SelectedIndexChanged += new System.EventHandler(this.gainComboBox_ValueChanged);
            // 
            // stepperWaitTime2NumericUpDown
            // 
            this.stepperWaitTime2NumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.stepperWaitTime2NumericUpDown.Location = new System.Drawing.Point(423, 127);
            this.stepperWaitTime2NumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.stepperWaitTime2NumericUpDown.Name = "stepperWaitTime2NumericUpDown";
            this.stepperWaitTime2NumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.stepperWaitTime2NumericUpDown.TabIndex = 17;
            this.stepperWaitTime2NumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // stepperWaitTime2Label
            // 
            this.stepperWaitTime2Label.AutoSize = true;
            this.stepperWaitTime2Label.ForeColor = System.Drawing.Color.Black;
            this.stepperWaitTime2Label.Location = new System.Drawing.Point(246, 129);
            this.stepperWaitTime2Label.Name = "stepperWaitTime2Label";
            this.stepperWaitTime2Label.Size = new System.Drawing.Size(138, 13);
            this.stepperWaitTime2Label.TabIndex = 16;
            this.stepperWaitTime2Label.Text = "Stepper Wait Time 2 [msec]";
            // 
            // pretriggerSamplesNumericUpDown
            // 
            this.pretriggerSamplesNumericUpDown.Location = new System.Drawing.Point(423, 37);
            this.pretriggerSamplesNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.pretriggerSamplesNumericUpDown.Name = "pretriggerSamplesNumericUpDown";
            this.pretriggerSamplesNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.pretriggerSamplesNumericUpDown.TabIndex = 15;
            this.pretriggerSamplesNumericUpDown.Value = new decimal(new int[] {
            8500,
            0,
            0,
            0});
            // 
            // pretriggerSamplesLabel
            // 
            this.pretriggerSamplesLabel.AutoSize = true;
            this.pretriggerSamplesLabel.ForeColor = System.Drawing.Color.Black;
            this.pretriggerSamplesLabel.Location = new System.Drawing.Point(246, 39);
            this.pretriggerSamplesLabel.Name = "pretriggerSamplesLabel";
            this.pretriggerSamplesLabel.Size = new System.Drawing.Size(98, 13);
            this.pretriggerSamplesLabel.TabIndex = 14;
            this.pretriggerSamplesLabel.Text = "Pre-trigger Samples";
            // 
            // sampleRateNumericUpDown
            // 
            this.sampleRateNumericUpDown.Location = new System.Drawing.Point(423, 7);
            this.sampleRateNumericUpDown.Maximum = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            this.sampleRateNumericUpDown.Name = "sampleRateNumericUpDown";
            this.sampleRateNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.sampleRateNumericUpDown.TabIndex = 7;
            this.sampleRateNumericUpDown.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // totalSamplesNumericUpDown
            // 
            this.totalSamplesNumericUpDown.Location = new System.Drawing.Point(423, 66);
            this.totalSamplesNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.totalSamplesNumericUpDown.Name = "totalSamplesNumericUpDown";
            this.totalSamplesNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.totalSamplesNumericUpDown.TabIndex = 13;
            this.totalSamplesNumericUpDown.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.totalSamplesNumericUpDown.ValueChanged += new System.EventHandler(this.totalSamplesNumericUpDown_ValueChanged);
            // 
            // totalSamplesLabel
            // 
            this.totalSamplesLabel.AutoSize = true;
            this.totalSamplesLabel.ForeColor = System.Drawing.Color.Black;
            this.totalSamplesLabel.Location = new System.Drawing.Point(246, 69);
            this.totalSamplesLabel.Name = "totalSamplesLabel";
            this.totalSamplesLabel.Size = new System.Drawing.Size(74, 13);
            this.totalSamplesLabel.TabIndex = 12;
            this.totalSamplesLabel.Text = "Total Samples";
            // 
            // triggerVoltageNumericEdit
            // 
            this.triggerVoltageNumericEdit.Enabled = false;
            this.triggerVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.triggerVoltageNumericEdit.Location = new System.Drawing.Point(147, 95);
            this.triggerVoltageNumericEdit.Name = "triggerVoltageNumericEdit";
            this.triggerVoltageNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.triggerVoltageNumericEdit.TabIndex = 11;
            this.triggerVoltageNumericEdit.Value = -0.01;
            // 
            // triggerVoltageLabel
            // 
            this.triggerVoltageLabel.AutoSize = true;
            this.triggerVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.triggerVoltageLabel.Location = new System.Drawing.Point(4, 99);
            this.triggerVoltageLabel.Name = "triggerVoltageLabel";
            this.triggerVoltageLabel.Size = new System.Drawing.Size(95, 13);
            this.triggerVoltageLabel.TabIndex = 10;
            this.triggerVoltageLabel.Text = "Trigger Voltage [V]";
            // 
            // triggerConductanceNumericEdit
            // 
            this.triggerConductanceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.triggerConductanceNumericEdit.Location = new System.Drawing.Point(147, 124);
            this.triggerConductanceNumericEdit.Name = "triggerConductanceNumericEdit";
            this.triggerConductanceNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.triggerConductanceNumericEdit.TabIndex = 9;
            this.triggerConductanceNumericEdit.Value = 0.0129;
            this.triggerConductanceNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.triggerConductanceNumericEdit_AfterChangeValue_1);
            // 
            // triggerConductanceLabel
            // 
            this.triggerConductanceLabel.AutoSize = true;
            this.triggerConductanceLabel.ForeColor = System.Drawing.Color.Black;
            this.triggerConductanceLabel.Location = new System.Drawing.Point(4, 129);
            this.triggerConductanceLabel.Name = "triggerConductanceLabel";
            this.triggerConductanceLabel.Size = new System.Drawing.Size(130, 13);
            this.triggerConductanceLabel.TabIndex = 8;
            this.triggerConductanceLabel.Text = "Trigger Conductance [G0]";
            // 
            // sampleRateLabel
            // 
            this.sampleRateLabel.AutoSize = true;
            this.sampleRateLabel.ForeColor = System.Drawing.Color.Black;
            this.sampleRateLabel.Location = new System.Drawing.Point(246, 9);
            this.sampleRateLabel.Name = "sampleRateLabel";
            this.sampleRateLabel.Size = new System.Drawing.Size(90, 13);
            this.sampleRateLabel.TabIndex = 6;
            this.sampleRateLabel.Text = "Sample Rate [Hz]";
            // 
            // stepperWaitTime1NumericUpDown
            // 
            this.stepperWaitTime1NumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.stepperWaitTime1NumericUpDown.Location = new System.Drawing.Point(423, 97);
            this.stepperWaitTime1NumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.stepperWaitTime1NumericUpDown.Name = "stepperWaitTime1NumericUpDown";
            this.stepperWaitTime1NumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.stepperWaitTime1NumericUpDown.TabIndex = 5;
            this.stepperWaitTime1NumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // stepperWaitTime1Label
            // 
            this.stepperWaitTime1Label.AutoSize = true;
            this.stepperWaitTime1Label.ForeColor = System.Drawing.Color.Black;
            this.stepperWaitTime1Label.Location = new System.Drawing.Point(246, 99);
            this.stepperWaitTime1Label.Name = "stepperWaitTime1Label";
            this.stepperWaitTime1Label.Size = new System.Drawing.Size(138, 13);
            this.stepperWaitTime1Label.TabIndex = 4;
            this.stepperWaitTime1Label.Text = "Stepper Wait Time 1 [msec]";
            // 
            // gainLabel
            // 
            this.gainLabel.AutoSize = true;
            this.gainLabel.ForeColor = System.Drawing.Color.Black;
            this.gainLabel.Location = new System.Drawing.Point(4, 69);
            this.gainLabel.Name = "gainLabel";
            this.gainLabel.Size = new System.Drawing.Size(62, 13);
            this.gainLabel.TabIndex = 2;
            this.gainLabel.Text = "Gain Power";
            // 
            // biasNumericEdit
            // 
            this.biasNumericEdit.CoercionInterval = 0.01;
            this.biasNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(3);
            this.biasNumericEdit.Location = new System.Drawing.Point(147, 7);
            this.biasNumericEdit.Name = "biasNumericEdit";
            this.biasNumericEdit.Range = new NationalInstruments.UI.Range(-2, 2);
            this.biasNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.biasNumericEdit.TabIndex = 1;
            this.biasNumericEdit.Value = 0.1;
            this.biasNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.biasNumericEdit_AfterChangeValue);
            // 
            // biasLabel
            // 
            this.biasLabel.AutoSize = true;
            this.biasLabel.ForeColor = System.Drawing.Color.Black;
            this.biasLabel.Location = new System.Drawing.Point(4, 9);
            this.biasLabel.Name = "biasLabel";
            this.biasLabel.Size = new System.Drawing.Size(43, 13);
            this.biasLabel.TabIndex = 0;
            this.biasLabel.Text = "Bias [V]";
            // 
            // laserSettingsTabPage
            // 
            this.laserSettingsTabPage.Controls.Add(this.laserSettingsPanel);
            this.laserSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.laserSettingsTabPage.Name = "laserSettingsTabPage";
            this.laserSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.laserSettingsTabPage.Size = new System.Drawing.Size(520, 202);
            this.laserSettingsTabPage.TabIndex = 1;
            this.laserSettingsTabPage.Text = "Laser";
            this.laserSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // laserSettingsPanel
            // 
            this.laserSettingsPanel.Controls.Add(this.laserModeComboBox);
            this.laserSettingsPanel.Controls.Add(this.frequencyNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.enableLaserCheckBox);
            this.laserSettingsPanel.Controls.Add(this.frequencyLabel);
            this.laserSettingsPanel.Controls.Add(this.amplitudeNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeLabel);
            this.laserSettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laserSettingsPanel.ForeColor = System.Drawing.Color.Black;
            this.laserSettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.laserSettingsPanel.MinimumSize = new System.Drawing.Size(0, 89);
            this.laserSettingsPanel.Name = "laserSettingsPanel";
            this.laserSettingsPanel.Size = new System.Drawing.Size(514, 196);
            this.laserSettingsPanel.TabIndex = 5;
            // 
            // laserModeComboBox
            // 
            this.laserModeComboBox.Enabled = false;
            this.laserModeComboBox.FormattingEnabled = true;
            this.laserModeComboBox.Items.AddRange(new object[] {
            "DC",
            "Square"});
            this.laserModeComboBox.Location = new System.Drawing.Point(147, 4);
            this.laserModeComboBox.Name = "laserModeComboBox";
            this.laserModeComboBox.Size = new System.Drawing.Size(75, 21);
            this.laserModeComboBox.TabIndex = 3;
            this.laserModeComboBox.Text = "DC";
            this.laserModeComboBox.SelectedValueChanged += new System.EventHandler(this.laserModeComboBox_SelectedValueChanged);
            // 
            // frequencyNumericUpDown
            // 
            this.frequencyNumericUpDown.Enabled = false;
            this.frequencyNumericUpDown.Location = new System.Drawing.Point(147, 72);
            this.frequencyNumericUpDown.Name = "frequencyNumericUpDown";
            this.frequencyNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.frequencyNumericUpDown.TabIndex = 5;
            this.frequencyNumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // enableLaserCheckBox
            // 
            this.enableLaserCheckBox.AutoSize = true;
            this.enableLaserCheckBox.ForeColor = System.Drawing.Color.Black;
            this.enableLaserCheckBox.Location = new System.Drawing.Point(7, 6);
            this.enableLaserCheckBox.Name = "enableLaserCheckBox";
            this.enableLaserCheckBox.Size = new System.Drawing.Size(88, 17);
            this.enableLaserCheckBox.TabIndex = 2;
            this.enableLaserCheckBox.Text = "Enable Laser";
            this.enableLaserCheckBox.UseVisualStyleBackColor = true;
            this.enableLaserCheckBox.CheckedChanged += new System.EventHandler(this.enableLaserCheckBox_CheckedChanged);
            // 
            // frequencyLabel
            // 
            this.frequencyLabel.AutoSize = true;
            this.frequencyLabel.Enabled = false;
            this.frequencyLabel.Location = new System.Drawing.Point(4, 74);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(79, 13);
            this.frequencyLabel.TabIndex = 4;
            this.frequencyLabel.Text = "Frequency [Hz]";
            // 
            // amplitudeNumericUpDown
            // 
            this.amplitudeNumericUpDown.DecimalPlaces = 3;
            this.amplitudeNumericUpDown.Enabled = false;
            this.amplitudeNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.amplitudeNumericUpDown.Location = new System.Drawing.Point(147, 40);
            this.amplitudeNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.amplitudeNumericUpDown.Name = "amplitudeNumericUpDown";
            this.amplitudeNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.amplitudeNumericUpDown.TabIndex = 3;
            this.amplitudeNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.amplitudeNumericUpDown.ValueChanged += new System.EventHandler(this.amplitudeNumericUpDown_ValueChanged);
            // 
            // laserAmplitudeLabel
            // 
            this.laserAmplitudeLabel.AutoSize = true;
            this.laserAmplitudeLabel.Enabled = false;
            this.laserAmplitudeLabel.ForeColor = System.Drawing.Color.Black;
            this.laserAmplitudeLabel.Location = new System.Drawing.Point(4, 42);
            this.laserAmplitudeLabel.Name = "laserAmplitudeLabel";
            this.laserAmplitudeLabel.Size = new System.Drawing.Size(69, 13);
            this.laserAmplitudeLabel.TabIndex = 2;
            this.laserAmplitudeLabel.Text = "Amplitude [V]";
            // 
            // LockInSettingsTabPage
            // 
            this.LockInSettingsTabPage.Controls.Add(this.lockInPanel);
            this.LockInSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.LockInSettingsTabPage.Name = "LockInSettingsTabPage";
            this.LockInSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LockInSettingsTabPage.Size = new System.Drawing.Size(520, 202);
            this.LockInSettingsTabPage.TabIndex = 2;
            this.LockInSettingsTabPage.Text = "LockIn";
            this.LockInSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // lockInPanel
            // 
            this.lockInPanel.Controls.Add(this.samplePhaseCheckBox);
            this.lockInPanel.Controls.Add(this.sampleLockInSignalCheckBox);
            this.lockInPanel.Controls.Add(this.sensitivityNumericEdit);
            this.lockInPanel.Controls.Add(this.sensitivityLabel);
            this.lockInPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lockInPanel.Location = new System.Drawing.Point(3, 3);
            this.lockInPanel.Name = "lockInPanel";
            this.lockInPanel.Size = new System.Drawing.Size(514, 196);
            this.lockInPanel.TabIndex = 0;
            // 
            // samplePhaseCheckBox
            // 
            this.samplePhaseCheckBox.AutoSize = true;
            this.samplePhaseCheckBox.ForeColor = System.Drawing.Color.Black;
            this.samplePhaseCheckBox.Location = new System.Drawing.Point(3, 30);
            this.samplePhaseCheckBox.Name = "samplePhaseCheckBox";
            this.samplePhaseCheckBox.Size = new System.Drawing.Size(126, 17);
            this.samplePhaseCheckBox.TabIndex = 5;
            this.samplePhaseCheckBox.Text = "Sample Phase Signal";
            this.samplePhaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // sampleLockInSignalCheckBox
            // 
            this.sampleLockInSignalCheckBox.AutoSize = true;
            this.sampleLockInSignalCheckBox.ForeColor = System.Drawing.Color.Black;
            this.sampleLockInSignalCheckBox.Location = new System.Drawing.Point(3, 3);
            this.sampleLockInSignalCheckBox.Name = "sampleLockInSignalCheckBox";
            this.sampleLockInSignalCheckBox.Size = new System.Drawing.Size(129, 17);
            this.sampleLockInSignalCheckBox.TabIndex = 4;
            this.sampleLockInSignalCheckBox.Text = "Sample LockIn Signal";
            this.sampleLockInSignalCheckBox.UseVisualStyleBackColor = true;
            // 
            // sensitivityNumericEdit
            // 
            this.sensitivityNumericEdit.CoercionInterval = 0.01;
            this.sensitivityNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.sensitivityNumericEdit.Location = new System.Drawing.Point(90, 53);
            this.sensitivityNumericEdit.Name = "sensitivityNumericEdit";
            this.sensitivityNumericEdit.Range = new NationalInstruments.UI.Range(2E-09, 1);
            this.sensitivityNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.sensitivityNumericEdit.TabIndex = 3;
            this.sensitivityNumericEdit.Value = 0.5;
            // 
            // sensitivityLabel
            // 
            this.sensitivityLabel.AutoSize = true;
            this.sensitivityLabel.ForeColor = System.Drawing.Color.Black;
            this.sensitivityLabel.Location = new System.Drawing.Point(3, 57);
            this.sensitivityLabel.Name = "sensitivityLabel";
            this.sensitivityLabel.Size = new System.Drawing.Size(70, 13);
            this.sensitivityLabel.TabIndex = 2;
            this.sensitivityLabel.Text = "Sensitivity [V]";
            // 
            // ElectroMagnetTabPage
            // 
            this.ElectroMagnetTabPage.Controls.Add(this.electroMagnetSettingsPanel);
            this.ElectroMagnetTabPage.Location = new System.Drawing.Point(4, 22);
            this.ElectroMagnetTabPage.Name = "ElectroMagnetTabPage";
            this.ElectroMagnetTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ElectroMagnetTabPage.Size = new System.Drawing.Size(520, 202);
            this.ElectroMagnetTabPage.TabIndex = 3;
            this.ElectroMagnetTabPage.Text = "ElectroMagnet";
            this.ElectroMagnetTabPage.UseVisualStyleBackColor = true;
            // 
            // electroMagnetSettingsPanel
            // 
            this.electroMagnetSettingsPanel.Controls.Add(this.groupBox1);
            this.electroMagnetSettingsPanel.Controls.Add(this.emSkipFirstCycleByStepperMotorCheckBox);
            this.electroMagnetSettingsPanel.Controls.Add(this.emShortCircuitDelayTimeLabel);
            this.electroMagnetSettingsPanel.Controls.Add(this.emShortCircuitDelayTimeNumericUpDown);
            this.electroMagnetSettingsPanel.Controls.Add(this.emSlowDelayTimeNumericUpDown);
            this.electroMagnetSettingsPanel.Controls.Add(this.emSlowDelayTimeLabel);
            this.electroMagnetSettingsPanel.Controls.Add(this.emFastDelayTimeNumericUpDown);
            this.electroMagnetSettingsPanel.Controls.Add(this.enableElectroMagnetCheckBox);
            this.electroMagnetSettingsPanel.Controls.Add(this.emFastDelayTimeLabel);
            this.electroMagnetSettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.electroMagnetSettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.electroMagnetSettingsPanel.Name = "electroMagnetSettingsPanel";
            this.electroMagnetSettingsPanel.Size = new System.Drawing.Size(514, 196);
            this.electroMagnetSettingsPanel.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.emHoldOnMinVoltageNumericEdit);
            this.groupBox1.Controls.Add(this.emHoldOnMinVoltageLabel);
            this.groupBox1.Controls.Add(this.emHoldOnMinConductanceLabel);
            this.groupBox1.Controls.Add(this.emHoldOnMinConductanceNumericEdit);
            this.groupBox1.Controls.Add(this.emHoldOnMaxVoltageNumericEdit);
            this.groupBox1.Controls.Add(this.emHoldOnMaxVoltageLabel);
            this.groupBox1.Controls.Add(this.emHoldOnMaxConductanceLabel);
            this.groupBox1.Controls.Add(this.emHoldOnMaxConductanceNumericEdit);
            this.groupBox1.Controls.Add(this.emHoldOnToConductanceRangeCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(269, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 172);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // emHoldOnMinVoltageNumericEdit
            // 
            this.emHoldOnMinVoltageNumericEdit.BackColor = System.Drawing.SystemColors.Control;
            this.emHoldOnMinVoltageNumericEdit.CoercionInterval = 0.001;
            this.emHoldOnMinVoltageNumericEdit.Enabled = false;
            this.emHoldOnMinVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2);
            this.emHoldOnMinVoltageNumericEdit.InteractionMode = NationalInstruments.UI.NumericEditInteractionModes.Indicator;
            this.emHoldOnMinVoltageNumericEdit.Location = new System.Drawing.Point(145, 138);
            this.emHoldOnMinVoltageNumericEdit.Name = "emHoldOnMinVoltageNumericEdit";
            this.emHoldOnMinVoltageNumericEdit.Size = new System.Drawing.Size(77, 20);
            this.emHoldOnMinVoltageNumericEdit.TabIndex = 17;
            // 
            // emHoldOnMinVoltageLabel
            // 
            this.emHoldOnMinVoltageLabel.AutoSize = true;
            this.emHoldOnMinVoltageLabel.Enabled = false;
            this.emHoldOnMinVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.emHoldOnMinVoltageLabel.Location = new System.Drawing.Point(9, 141);
            this.emHoldOnMinVoltageLabel.Name = "emHoldOnMinVoltageLabel";
            this.emHoldOnMinVoltageLabel.Size = new System.Drawing.Size(79, 13);
            this.emHoldOnMinVoltageLabel.TabIndex = 16;
            this.emHoldOnMinVoltageLabel.Text = "Min Voltage [V]";
            // 
            // emHoldOnMinConductanceLabel
            // 
            this.emHoldOnMinConductanceLabel.AutoSize = true;
            this.emHoldOnMinConductanceLabel.Enabled = false;
            this.emHoldOnMinConductanceLabel.ForeColor = System.Drawing.Color.Black;
            this.emHoldOnMinConductanceLabel.Location = new System.Drawing.Point(9, 111);
            this.emHoldOnMinConductanceLabel.Name = "emHoldOnMinConductanceLabel";
            this.emHoldOnMinConductanceLabel.Size = new System.Drawing.Size(114, 13);
            this.emHoldOnMinConductanceLabel.TabIndex = 15;
            this.emHoldOnMinConductanceLabel.Text = "Min Conductance [G0]";
            // 
            // emHoldOnMinConductanceNumericEdit
            // 
            this.emHoldOnMinConductanceNumericEdit.CoercionInterval = 0.1;
            this.emHoldOnMinConductanceNumericEdit.Enabled = false;
            this.emHoldOnMinConductanceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2);
            this.emHoldOnMinConductanceNumericEdit.Location = new System.Drawing.Point(145, 109);
            this.emHoldOnMinConductanceNumericEdit.Name = "emHoldOnMinConductanceNumericEdit";
            this.emHoldOnMinConductanceNumericEdit.Range = new NationalInstruments.UI.Range(0, 20);
            this.emHoldOnMinConductanceNumericEdit.Size = new System.Drawing.Size(77, 20);
            this.emHoldOnMinConductanceNumericEdit.TabIndex = 14;
            this.emHoldOnMinConductanceNumericEdit.Value = 0.7;
            this.emHoldOnMinConductanceNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.emHoldOnMinConductanceNumericEdit_AfterChangeValue);
            // 
            // emHoldOnMaxVoltageNumericEdit
            // 
            this.emHoldOnMaxVoltageNumericEdit.BackColor = System.Drawing.SystemColors.Control;
            this.emHoldOnMaxVoltageNumericEdit.CoercionInterval = 0.001;
            this.emHoldOnMaxVoltageNumericEdit.Enabled = false;
            this.emHoldOnMaxVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2);
            this.emHoldOnMaxVoltageNumericEdit.InteractionMode = NationalInstruments.UI.NumericEditInteractionModes.Indicator;
            this.emHoldOnMaxVoltageNumericEdit.Location = new System.Drawing.Point(145, 78);
            this.emHoldOnMaxVoltageNumericEdit.Name = "emHoldOnMaxVoltageNumericEdit";
            this.emHoldOnMaxVoltageNumericEdit.Size = new System.Drawing.Size(77, 20);
            this.emHoldOnMaxVoltageNumericEdit.TabIndex = 12;
            // 
            // emHoldOnMaxVoltageLabel
            // 
            this.emHoldOnMaxVoltageLabel.AutoSize = true;
            this.emHoldOnMaxVoltageLabel.Enabled = false;
            this.emHoldOnMaxVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.emHoldOnMaxVoltageLabel.Location = new System.Drawing.Point(9, 81);
            this.emHoldOnMaxVoltageLabel.Name = "emHoldOnMaxVoltageLabel";
            this.emHoldOnMaxVoltageLabel.Size = new System.Drawing.Size(82, 13);
            this.emHoldOnMaxVoltageLabel.TabIndex = 11;
            this.emHoldOnMaxVoltageLabel.Text = "Max Voltage [V]";
            // 
            // emHoldOnMaxConductanceLabel
            // 
            this.emHoldOnMaxConductanceLabel.AutoSize = true;
            this.emHoldOnMaxConductanceLabel.Enabled = false;
            this.emHoldOnMaxConductanceLabel.ForeColor = System.Drawing.Color.Black;
            this.emHoldOnMaxConductanceLabel.Location = new System.Drawing.Point(9, 51);
            this.emHoldOnMaxConductanceLabel.Name = "emHoldOnMaxConductanceLabel";
            this.emHoldOnMaxConductanceLabel.Size = new System.Drawing.Size(117, 13);
            this.emHoldOnMaxConductanceLabel.TabIndex = 10;
            this.emHoldOnMaxConductanceLabel.Text = "Max Conductance [G0]";
            // 
            // emHoldOnMaxConductanceNumericEdit
            // 
            this.emHoldOnMaxConductanceNumericEdit.CoercionInterval = 0.1;
            this.emHoldOnMaxConductanceNumericEdit.Enabled = false;
            this.emHoldOnMaxConductanceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2);
            this.emHoldOnMaxConductanceNumericEdit.Location = new System.Drawing.Point(145, 49);
            this.emHoldOnMaxConductanceNumericEdit.Name = "emHoldOnMaxConductanceNumericEdit";
            this.emHoldOnMaxConductanceNumericEdit.Range = new NationalInstruments.UI.Range(0, 20);
            this.emHoldOnMaxConductanceNumericEdit.Size = new System.Drawing.Size(77, 20);
            this.emHoldOnMaxConductanceNumericEdit.TabIndex = 9;
            this.emHoldOnMaxConductanceNumericEdit.Value = 1.3;
            this.emHoldOnMaxConductanceNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.emHoldOnMaxConductanceNumericEdit_AfterChangeValue);
            // 
            // emHoldOnToConductanceRangeCheckBox
            // 
            this.emHoldOnToConductanceRangeCheckBox.AutoSize = true;
            this.emHoldOnToConductanceRangeCheckBox.Enabled = false;
            this.emHoldOnToConductanceRangeCheckBox.ForeColor = System.Drawing.Color.Black;
            this.emHoldOnToConductanceRangeCheckBox.Location = new System.Drawing.Point(12, 19);
            this.emHoldOnToConductanceRangeCheckBox.Name = "emHoldOnToConductanceRangeCheckBox";
            this.emHoldOnToConductanceRangeCheckBox.Size = new System.Drawing.Size(179, 17);
            this.emHoldOnToConductanceRangeCheckBox.TabIndex = 8;
            this.emHoldOnToConductanceRangeCheckBox.Text = "Hold On to Conductance Range";
            this.emHoldOnToConductanceRangeCheckBox.UseVisualStyleBackColor = true;
            this.emHoldOnToConductanceRangeCheckBox.CheckedChanged += new System.EventHandler(this.holdOnToConductanceRangeCheckBox_CheckedChanged);
            // 
            // emSkipFirstCycleByStepperMotorCheckBox
            // 
            this.emSkipFirstCycleByStepperMotorCheckBox.AutoSize = true;
            this.emSkipFirstCycleByStepperMotorCheckBox.Enabled = false;
            this.emSkipFirstCycleByStepperMotorCheckBox.ForeColor = System.Drawing.Color.Black;
            this.emSkipFirstCycleByStepperMotorCheckBox.Location = new System.Drawing.Point(8, 164);
            this.emSkipFirstCycleByStepperMotorCheckBox.Name = "emSkipFirstCycleByStepperMotorCheckBox";
            this.emSkipFirstCycleByStepperMotorCheckBox.Size = new System.Drawing.Size(182, 17);
            this.emSkipFirstCycleByStepperMotorCheckBox.TabIndex = 13;
            this.emSkipFirstCycleByStepperMotorCheckBox.Text = "Skip First Cycle by Stepper Motor";
            this.emSkipFirstCycleByStepperMotorCheckBox.UseVisualStyleBackColor = true;
            // 
            // emShortCircuitDelayTimeLabel
            // 
            this.emShortCircuitDelayTimeLabel.AutoSize = true;
            this.emShortCircuitDelayTimeLabel.Enabled = false;
            this.emShortCircuitDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.emShortCircuitDelayTimeLabel.Location = new System.Drawing.Point(11, 41);
            this.emShortCircuitDelayTimeLabel.Name = "emShortCircuitDelayTimeLabel";
            this.emShortCircuitDelayTimeLabel.Size = new System.Drawing.Size(142, 13);
            this.emShortCircuitDelayTimeLabel.TabIndex = 7;
            this.emShortCircuitDelayTimeLabel.Text = "Short Circuit Delay Time [ms]";
            // 
            // emShortCircuitDelayTimeNumericUpDown
            // 
            this.emShortCircuitDelayTimeNumericUpDown.Enabled = false;
            this.emShortCircuitDelayTimeNumericUpDown.Location = new System.Drawing.Point(162, 38);
            this.emShortCircuitDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.emShortCircuitDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.emShortCircuitDelayTimeNumericUpDown.Name = "emShortCircuitDelayTimeNumericUpDown";
            this.emShortCircuitDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.emShortCircuitDelayTimeNumericUpDown.TabIndex = 6;
            this.emShortCircuitDelayTimeNumericUpDown.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // emSlowDelayTimeNumericUpDown
            // 
            this.emSlowDelayTimeNumericUpDown.Enabled = false;
            this.emSlowDelayTimeNumericUpDown.Location = new System.Drawing.Point(162, 101);
            this.emSlowDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.emSlowDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.emSlowDelayTimeNumericUpDown.Name = "emSlowDelayTimeNumericUpDown";
            this.emSlowDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.emSlowDelayTimeNumericUpDown.TabIndex = 5;
            this.emSlowDelayTimeNumericUpDown.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // emSlowDelayTimeLabel
            // 
            this.emSlowDelayTimeLabel.AutoSize = true;
            this.emSlowDelayTimeLabel.Enabled = false;
            this.emSlowDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.emSlowDelayTimeLabel.Location = new System.Drawing.Point(11, 103);
            this.emSlowDelayTimeLabel.Name = "emSlowDelayTimeLabel";
            this.emSlowDelayTimeLabel.Size = new System.Drawing.Size(108, 13);
            this.emSlowDelayTimeLabel.TabIndex = 4;
            this.emSlowDelayTimeLabel.Text = "Slow Delay Time [ms]";
            // 
            // emFastDelayTimeNumericUpDown
            // 
            this.emFastDelayTimeNumericUpDown.Enabled = false;
            this.emFastDelayTimeNumericUpDown.Location = new System.Drawing.Point(162, 70);
            this.emFastDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.emFastDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.emFastDelayTimeNumericUpDown.Name = "emFastDelayTimeNumericUpDown";
            this.emFastDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.emFastDelayTimeNumericUpDown.TabIndex = 3;
            this.emFastDelayTimeNumericUpDown.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // enableElectroMagnetCheckBox
            // 
            this.enableElectroMagnetCheckBox.AutoSize = true;
            this.enableElectroMagnetCheckBox.ForeColor = System.Drawing.Color.Black;
            this.enableElectroMagnetCheckBox.Location = new System.Drawing.Point(7, 6);
            this.enableElectroMagnetCheckBox.Name = "enableElectroMagnetCheckBox";
            this.enableElectroMagnetCheckBox.Size = new System.Drawing.Size(131, 17);
            this.enableElectroMagnetCheckBox.TabIndex = 0;
            this.enableElectroMagnetCheckBox.Text = "Enable ElectroMagnet";
            this.enableElectroMagnetCheckBox.UseVisualStyleBackColor = true;
            this.enableElectroMagnetCheckBox.CheckedChanged += new System.EventHandler(this.enableElectroMagnetCheckBox_CheckedChanged);
            // 
            // emFastDelayTimeLabel
            // 
            this.emFastDelayTimeLabel.AutoSize = true;
            this.emFastDelayTimeLabel.Enabled = false;
            this.emFastDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.emFastDelayTimeLabel.Location = new System.Drawing.Point(11, 72);
            this.emFastDelayTimeLabel.Name = "emFastDelayTimeLabel";
            this.emFastDelayTimeLabel.Size = new System.Drawing.Size(105, 13);
            this.emFastDelayTimeLabel.TabIndex = 2;
            this.emFastDelayTimeLabel.Text = "Fast Delay Time [ms]";
            // 
            // controlPanelsTabPage
            // 
            this.controlPanelsTabPage.AutoScroll = true;
            this.controlPanelsTabPage.Controls.Add(this.electroMagnetGroupBox);
            this.controlPanelsTabPage.Controls.Add(this.stepperMotorGroupBox);
            this.controlPanelsTabPage.Location = new System.Drawing.Point(4, 22);
            this.controlPanelsTabPage.Name = "controlPanelsTabPage";
            this.controlPanelsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.controlPanelsTabPage.Size = new System.Drawing.Size(741, 799);
            this.controlPanelsTabPage.TabIndex = 1;
            this.controlPanelsTabPage.Text = "Control Panels";
            this.controlPanelsTabPage.UseVisualStyleBackColor = true;
            // 
            // electroMagnetGroupBox
            // 
            this.electroMagnetGroupBox.Controls.Add(this.electroMagnetUserControl2);
            this.electroMagnetGroupBox.Location = new System.Drawing.Point(13, 183);
            this.electroMagnetGroupBox.Name = "electroMagnetGroupBox";
            this.electroMagnetGroupBox.Size = new System.Drawing.Size(281, 278);
            this.electroMagnetGroupBox.TabIndex = 2;
            this.electroMagnetGroupBox.TabStop = false;
            this.electroMagnetGroupBox.Text = "ElectroMagnet";
            // 
            // electroMagnetUserControl2
            // 
            this.electroMagnetUserControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.electroMagnetUserControl2.Location = new System.Drawing.Point(3, 16);
            this.electroMagnetUserControl2.Name = "electroMagnetUserControl2";
            this.electroMagnetUserControl2.Size = new System.Drawing.Size(275, 259);
            this.electroMagnetUserControl2.TabIndex = 0;
            // 
            // stepperMotorGroupBox
            // 
            this.stepperMotorGroupBox.Controls.Add(this.stepperMotorUserControl1);
            this.stepperMotorGroupBox.Location = new System.Drawing.Point(12, 6);
            this.stepperMotorGroupBox.Name = "stepperMotorGroupBox";
            this.stepperMotorGroupBox.Size = new System.Drawing.Size(282, 170);
            this.stepperMotorGroupBox.TabIndex = 1;
            this.stepperMotorGroupBox.TabStop = false;
            this.stepperMotorGroupBox.Text = "Stepper Motor";
            // 
            // stepperMotorUserControl1
            // 
            this.stepperMotorUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepperMotorUserControl1.Location = new System.Drawing.Point(3, 16);
            this.stepperMotorUserControl1.Name = "stepperMotorUserControl1";
            this.stepperMotorUserControl1.Size = new System.Drawing.Size(276, 151);
            this.stepperMotorUserControl1.TabIndex = 0;
            // 
            // aquireDataBackgroundWorker
            // 
            this.aquireDataBackgroundWorker.WorkerReportsProgress = true;
            this.aquireDataBackgroundWorker.WorkerSupportsCancellation = true;
            this.aquireDataBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.aquireDataBackgroundWorker_DoWork);
            this.aquireDataBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.aquireDataBackgroundWorker_RunWorkerCompleted);
            // 
            // stepperUpBackgroundWorker
            // 
            this.stepperUpBackgroundWorker.WorkerReportsProgress = true;
            this.stepperUpBackgroundWorker.WorkerSupportsCancellation = true;
            this.stepperUpBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.stepperUpBackgroundWorker_DoWork);
            this.stepperUpBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.stepperUpBackgroundWorker_RunWorkerCompleted);
            // 
            // fixBiasBackgroundWorker
            // 
            this.fixBiasBackgroundWorker.WorkerSupportsCancellation = true;
            this.fixBiasBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fixBiasBackgroundWorker_DoWork);
            this.fixBiasBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fixBiasBackgroundWorker_RunWorkerCompleted);
            // 
            // SBJControllerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(749, 825);
            this.Controls.Add(this.controllerTabControl);
            this.Name = "SBJControllerMainForm";
            this.Text = "SBJControllerMainForm";
            this.Shown += new System.EventHandler(this.SBJControllerMainForm_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SBJControllerMainForm_FormClosed);
            this.controllerTabControl.ResumeLayout(false);
            this.dataAquisitionTabPage.ResumeLayout(false);
            this.dataAquisitionTabPage.PerformLayout();
            this.operateGroupBox.ResumeLayout(false);
            this.operateGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCyclesnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shortCircuitVoltageNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileNumberNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.traceWaveformGraph)).EndInit();
            this.samplePropertiesGroupBox.ResumeLayout(false);
            this.samplePropertiesTabControl.ResumeLayout(false);
            this.bottomTabPage.ResumeLayout(false);
            this.TopTabPage.ResumeLayout(false);
            this.settingsGroupBox.ResumeLayout(false);
            this.SettingsTabControl.ResumeLayout(false);
            this.generalSettingsTabPage.ResumeLayout(false);
            this.generalSettingsPanel.ResumeLayout(false);
            this.generalSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.biasErrorNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepperWaitTime2NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pretriggerSamplesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampleRateNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalSamplesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerVoltageNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.triggerConductanceNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stepperWaitTime1NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.biasNumericEdit)).EndInit();
            this.laserSettingsTabPage.ResumeLayout(false);
            this.laserSettingsPanel.ResumeLayout(false);
            this.laserSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amplitudeNumericUpDown)).EndInit();
            this.LockInSettingsTabPage.ResumeLayout(false);
            this.lockInPanel.ResumeLayout(false);
            this.lockInPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensitivityNumericEdit)).EndInit();
            this.ElectroMagnetTabPage.ResumeLayout(false);
            this.electroMagnetSettingsPanel.ResumeLayout(false);
            this.electroMagnetSettingsPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMinVoltageNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMinConductanceNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMaxVoltageNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emHoldOnMaxConductanceNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emShortCircuitDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emSlowDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emFastDelayTimeNumericUpDown)).EndInit();
            this.controlPanelsTabPage.ResumeLayout(false);
            this.electroMagnetGroupBox.ResumeLayout(false);
            this.stepperMotorGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker obtainShortCircuitBackgroundWorker;
        private System.Windows.Forms.TabControl controllerTabControl;
        private System.Windows.Forms.TabPage dataAquisitionTabPage;
        private System.Windows.Forms.TabPage controlPanelsTabPage;
        private System.ComponentModel.BackgroundWorker aquireDataBackgroundWorker;
        private System.ComponentModel.BackgroundWorker stepperUpBackgroundWorker;
        private System.Windows.Forms.GroupBox operateGroupBox;
        private System.Windows.Forms.Button saveSamplesParamsButton;
        private System.Windows.Forms.CheckBox moveUpCheckBoxButton;
        private System.Windows.Forms.CheckBox startStopCheckBoxButton;
        private System.Windows.Forms.CheckBox shortCircuitCheckBoxButton;
        private System.Windows.Forms.NumericUpDown numberOfCyclesnumericUpDown;
        private System.Windows.Forms.Label numberOfCyclesLabel1;
        private System.Windows.Forms.NumericUpDown shortCircuitVoltageNumericUpDown;
        private System.Windows.Forms.Label shortCircuitVoltageLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.CheckBox fileSavingCheckBox;
        private System.Windows.Forms.Label fileNumberLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.NumericUpDown fileNumberNumericUpDown;
        private NationalInstruments.UI.WindowsForms.WaveformGraph traceWaveformGraph;
        private NationalInstruments.UI.WaveformPlot waveformPlot1;
        private NationalInstruments.UI.XAxis xAxis1;
        private NationalInstruments.UI.YAxis yAxis1;
        private System.Windows.Forms.GroupBox samplePropertiesGroupBox;
        private System.Windows.Forms.TabControl samplePropertiesTabControl;
        private System.Windows.Forms.TabPage bottomTabPage;
        private System.Windows.Forms.PropertyGrid bottomPropertyGrid;
        private System.Windows.Forms.TabPage TopTabPage;
        private System.Windows.Forms.PropertyGrid topPropertyGrid;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Panel laserSettingsPanel;
        private System.Windows.Forms.ComboBox laserModeComboBox;
        private System.Windows.Forms.NumericUpDown frequencyNumericUpDown;
        private System.Windows.Forms.CheckBox enableLaserCheckBox;
        private System.Windows.Forms.Label frequencyLabel;
        private System.Windows.Forms.NumericUpDown amplitudeNumericUpDown;
        private System.Windows.Forms.Label laserAmplitudeLabel;
        private System.Windows.Forms.Panel generalSettingsPanel;
        private System.Windows.Forms.NumericUpDown stepperWaitTime2NumericUpDown;
        private System.Windows.Forms.Label stepperWaitTime2Label;
        private System.Windows.Forms.NumericUpDown pretriggerSamplesNumericUpDown;
        private System.Windows.Forms.Label pretriggerSamplesLabel;
        private System.Windows.Forms.NumericUpDown sampleRateNumericUpDown;
        private System.Windows.Forms.NumericUpDown totalSamplesNumericUpDown;
        private System.Windows.Forms.Label totalSamplesLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit triggerVoltageNumericEdit;
        private System.Windows.Forms.Label triggerVoltageLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit triggerConductanceNumericEdit;
        private System.Windows.Forms.Label triggerConductanceLabel;
        private System.Windows.Forms.Label sampleRateLabel;
        private System.Windows.Forms.NumericUpDown stepperWaitTime1NumericUpDown;
        private System.Windows.Forms.Label stepperWaitTime1Label;
        private System.Windows.Forms.Label gainLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit biasNumericEdit;
        private System.Windows.Forms.Label biasLabel;
        private System.Windows.Forms.Button openLogBookButton;
        private System.Windows.Forms.ComboBox gainComboBox;
        private NationalInstruments.UI.YAxis yAxis2;
        private System.Windows.Forms.TabControl SettingsTabControl;
        private System.Windows.Forms.TabPage generalSettingsTabPage;
        private System.Windows.Forms.TabPage laserSettingsTabPage;
        private System.Windows.Forms.TabPage LockInSettingsTabPage;
        private System.Windows.Forms.Panel lockInPanel;
        private NationalInstruments.UI.WindowsForms.NumericEdit sensitivityNumericEdit;
        private System.Windows.Forms.Label sensitivityLabel;
        private System.Windows.Forms.CheckBox sampleLockInSignalCheckBox;
        private System.Windows.Forms.CheckBox samplePhaseCheckBox;
        private System.Windows.Forms.TabPage ElectroMagnetTabPage;
        private System.Windows.Forms.CheckBox enableElectroMagnetCheckBox;
        private System.Windows.Forms.GroupBox stepperMotorGroupBox;
        private System.Windows.Forms.GroupBox electroMagnetGroupBox;
        private System.Windows.Forms.Panel electroMagnetSettingsPanel;
        private System.Windows.Forms.Label emFastDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown emFastDelayTimeNumericUpDown;
        private System.Windows.Forms.Label emSlowDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown emSlowDelayTimeNumericUpDown;
        private System.Windows.Forms.Label emShortCircuitDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown emShortCircuitDelayTimeNumericUpDown;
        private System.Windows.Forms.Label emHoldOnMaxConductanceLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit emHoldOnMaxConductanceNumericEdit;
        private System.Windows.Forms.CheckBox emHoldOnToConductanceRangeCheckBox;
        private NationalInstruments.UI.WindowsForms.NumericEdit emHoldOnMaxVoltageNumericEdit;
        private System.Windows.Forms.Label emHoldOnMaxVoltageLabel;
        private System.Windows.Forms.CheckBox emSkipFirstCycleByStepperMotorCheckBox;
        private NationalInstruments.UI.WindowsForms.NumericEdit emHoldOnMinVoltageNumericEdit;
        private System.Windows.Forms.Label emHoldOnMinVoltageLabel;
        private System.Windows.Forms.Label emHoldOnMinConductanceLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit emHoldOnMinConductanceNumericEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private ElectroMagnetUserControl electroMagnetUserControl2;
        private StepperMotorUserControl stepperMotorUserControl1;
        private System.Windows.Forms.Label biasErrorLabel;
        private System.Windows.Forms.CheckBox fixBiasCheckBoxButton;
        private System.ComponentModel.BackgroundWorker fixBiasBackgroundWorker;
        private NationalInstruments.UI.WindowsForms.NumericEdit biasErrorNumericEdit;
                         
    }
}