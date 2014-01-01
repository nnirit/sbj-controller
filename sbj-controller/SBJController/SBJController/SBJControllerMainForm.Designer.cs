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
            this.plotGroupBox = new System.Windows.Forms.GroupBox();
            this.channelsListView = new System.Windows.Forms.ListView();
            this.operateGroupBox = new System.Windows.Forms.GroupBox();
            this.continuousSamplingCheckBox = new System.Windows.Forms.CheckBox();
            this.manualStartCheckBoxButton = new System.Windows.Forms.CheckBox();
            this.openFolderButton = new System.Windows.Forms.Button();
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
            this.useKeithleyCheckBox = new System.Windows.Forms.CheckBox();
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
            this.eomCOnfigurationComboBox = new System.Windows.Forms.ComboBox();
            this.secondEOMFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.secondEOMFrequencyLabel = new System.Windows.Forms.Label();
            this.enableSecondEOMCheckBox = new System.Windows.Forms.CheckBox();
            this.firstEOMFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.firstEOMFrequencyLabel = new System.Windows.Forms.Label();
            this.enableChopperCheckBox = new System.Windows.Forms.CheckBox();
            this.externalFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.externalFrequencyLabel = new System.Windows.Forms.Label();
            this.enableFirstEOMcheckBox = new System.Windows.Forms.CheckBox();
            this.laserAmplitudeOnSampleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.laserAmplitudeOnSampleLabel = new System.Windows.Forms.Label();
            this.laserAmplitudeWNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.laserAmplitudeWLabel = new System.Windows.Forms.Label();
            this.laserModeComboBox = new System.Windows.Forms.ComboBox();
            this.frequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.enableLaserCheckBox = new System.Windows.Forms.CheckBox();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.laserAmplitudeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.laserAmplitudeLabel = new System.Windows.Forms.Label();
            this.LockInSettingsTabPage = new System.Windows.Forms.TabPage();
            this.lockInPanel = new System.Windows.Forms.Panel();
            this.rollOffComboBox = new System.Windows.Forms.ComboBox();
            this.rollOffLabel = new System.Windows.Forms.Label();
            this.enableLockInCheckBox = new System.Windows.Forms.CheckBox();
            this.timeConstantComboBox = new System.Windows.Forms.ComboBox();
            this.timeConstantLabel = new System.Windows.Forms.Label();
            this.sensitivityComboBox = new System.Windows.Forms.ComboBox();
            this.internalSourceLockInGroupBox = new System.Windows.Forms.GroupBox();
            this.mixerReductionFactorNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.acVoltageReductionFactorLabel = new System.Windows.Forms.Label();
            this.internalSourceLockInCheckBoxcheckBox = new System.Windows.Forms.CheckBox();
            this.lockInAcVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.lockInAcVoltageLabel = new System.Windows.Forms.Label();
            this.lockInSensitivityLabel = new System.Windows.Forms.Label();
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
            this.externalEMTabPage = new System.Windows.Forms.TabPage();
            this.externalEMpanel = new System.Windows.Forms.Panel();
            this.lambdaZupOutputVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.lambdaZupOutputVoltageLabel = new System.Windows.Forms.Label();
            this.useLambdaZupCheckBox = new System.Windows.Forms.CheckBox();
            this.channelsConfigurationTabPage = new System.Windows.Forms.TabPage();
            this.channelsSettingsPanel = new System.Windows.Forms.Panel();
            this.channel1ComboBox = new System.Windows.Forms.ComboBox();
            this.channel3ComboBox = new System.Windows.Forms.ComboBox();
            this.channel0CheckBox = new System.Windows.Forms.CheckBox();
            this.channel3CheckBox = new System.Windows.Forms.CheckBox();
            this.channel0ComboBox = new System.Windows.Forms.ComboBox();
            this.channel2ComboBox = new System.Windows.Forms.ComboBox();
            this.channel1CheckBox = new System.Windows.Forms.CheckBox();
            this.channel2CheckBox = new System.Windows.Forms.CheckBox();
            this.ivAcquisition = new System.Windows.Forms.TabPage();
            this.ivOperateGroupBox = new System.Windows.Forms.GroupBox();
            this.ivOpenFolderButton = new System.Windows.Forms.Button();
            this.ivStepperUpCheckBox = new System.Windows.Forms.CheckBox();
            this.ivStartStopCheckBox = new System.Windows.Forms.CheckBox();
            this.ivShortCircuitCheckBox = new System.Windows.Forms.CheckBox();
            this.ivNumberOfCyclesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivNumberOfCyclesLlabel = new System.Windows.Forms.Label();
            this.ivShortCircuitVoltageNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivShortCircuitVoltageLabel = new System.Windows.Forms.Label();
            this.ivPathTextBox = new System.Windows.Forms.TextBox();
            this.ivBrowseButton = new System.Windows.Forms.Button();
            this.ivFileSavingCheckBox = new System.Windows.Forms.CheckBox();
            this.ivFileNumberLabel = new System.Windows.Forms.Label();
            this.ivPathLabel = new System.Windows.Forms.Label();
            this.ivFileNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivPlotGroupBox = new System.Windows.Forms.GroupBox();
            this.ivChannelsListView = new System.Windows.Forms.ListView();
            this.ivWaveformGraph = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.ivWaveformPlot = new NationalInstruments.UI.WaveformPlot();
            this.ivXAxis = new NationalInstruments.UI.XAxis();
            this.ivYAxis = new NationalInstruments.UI.YAxis();
            this.ivSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.ivGeneralTabControl = new System.Windows.Forms.TabControl();
            this.ivGeneralSettingsTabPage = new System.Windows.Forms.TabPage();
            this.ivGeneralSettingsPanel = new System.Windows.Forms.Panel();
            this.ivVoltageForTraceNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivVoltageForTraceLabel = new System.Windows.Forms.Label();
            this.ivTimeOfOneIVCycleNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivTimeOfIVCycleLabel = new System.Windows.Forms.Label();
            this.ivOutputUpdateDelayLabel = new System.Windows.Forms.Label();
            this.ivOutputUpdateRateNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivSamplesPerCycleLabel = new System.Windows.Forms.Label();
            this.ivVoltageAmplitudeLabel = new System.Windows.Forms.Label();
            this.ivOutputUpdateRateLabel = new System.Windows.Forms.Label();
            this.ivVoltageAmplitudeNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivOutputUpdateDelayNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivSamplesPerCycleNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivGainPoweComboBox = new System.Windows.Forms.ComboBox();
            this.ivInputSampleRateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivTriggerVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivTriggerVoltageLabel = new System.Windows.Forms.Label();
            this.ivTriggerConductanceNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.ivTriggerConductanceLabel = new System.Windows.Forms.Label();
            this.ivInputSampleRateLabel = new System.Windows.Forms.Label();
            this.ivGainPowerLabel = new System.Windows.Forms.Label();
            this.ivSteppingMethodTabPage = new System.Windows.Forms.TabPage();
            this.ivSteppingMethodPanel = new System.Windows.Forms.Panel();
            this.ivElectroMagnetRadioButton = new System.Windows.Forms.RadioButton();
            this.ivElectroMagnetGroupBox = new System.Windows.Forms.GroupBox();
            this.ivEMSkipStepperMotorCheckBox = new System.Windows.Forms.CheckBox();
            this.ivEMShortCircuitDelayTimeLabel = new System.Windows.Forms.Label();
            this.ivEMShortCircuitDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivEMSlowDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivEMSlowDelayTimeLabel = new System.Windows.Forms.Label();
            this.ivEMFastDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivEMFastDelayTimeLabel = new System.Windows.Forms.Label();
            this.ivStepperMotorRadioButton = new System.Windows.Forms.RadioButton();
            this.ivStepperMotorGroupBox = new System.Windows.Forms.GroupBox();
            this.ivStepperDelayTime2NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivStepperDelayTime1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivStepperDelayTime2Label = new System.Windows.Forms.Label();
            this.ivStepperDelayTime1Label = new System.Windows.Forms.Label();
            this.ivChannelsTabPage = new System.Windows.Forms.TabPage();
            this.ivChannelsPanel = new System.Windows.Forms.Panel();
            this.ivChannel1ComboBox = new System.Windows.Forms.ComboBox();
            this.ivChannel3ComboBox = new System.Windows.Forms.ComboBox();
            this.ivChannel0CheckBox = new System.Windows.Forms.CheckBox();
            this.ivChannel3CheckBox = new System.Windows.Forms.CheckBox();
            this.ivChannel0ComboBox = new System.Windows.Forms.ComboBox();
            this.ivChannel2ComboBox = new System.Windows.Forms.ComboBox();
            this.ivChannel1CheckBox = new System.Windows.Forms.CheckBox();
            this.ivChannel2CheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationTabPage = new System.Windows.Forms.TabPage();
            this.calibrationPlotGroupBox = new System.Windows.Forms.GroupBox();
            this.calibrationChannelsListView = new System.Windows.Forms.ListView();
            this.calibrationWaveformGraph = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.calibrationWaveformPlot = new NationalInstruments.UI.WaveformPlot();
            this.calibrationXAxis = new NationalInstruments.UI.XAxis();
            this.calibrationYAxis = new NationalInstruments.UI.YAxis();
            this.calibrationSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.calibrationSettingsTabControl = new System.Windows.Forms.TabControl();
            this.CalibrationGeneralTabPage = new System.Windows.Forms.TabPage();
            this.calibrationMeasurementTypeComboBox = new System.Windows.Forms.ComboBox();
            this.calibrationMeasurementsTypeLabel = new System.Windows.Forms.Label();
            this.calibrationDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.calibrationDelayTimeLabel = new System.Windows.Forms.Label();
            this.calibrationKeithleyCheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationSampleRateNumericEdit = new System.Windows.Forms.NumericUpDown();
            this.calibrationSampleRateLabel = new System.Windows.Forms.Label();
            this.calibrationTriggerVoltagrLabel = new System.Windows.Forms.Label();
            this.calibrationGainPowerComboBox = new System.Windows.Forms.ComboBox();
            this.calibrationBiasNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.calibrationGainPowerLabel = new System.Windows.Forms.Label();
            this.calibrationBiasLabel = new System.Windows.Forms.Label();
            this.calibrationTriggerVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.calibrationTriggerConductanceNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.calibrationTriggerConductanceLabel = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.calibrationElectroMagnetPanel = new System.Windows.Forms.Panel();
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationEMShortCircuitDelayTimeLabel = new System.Windows.Forms.Label();
            this.calibrationEMShortCircuitDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.calibrationEMSlowDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.calibrationEMSlowDelayTimeLabel = new System.Windows.Forms.Label();
            this.calibrationEMFastDelayTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.calibrationEMFastDelayTimeLabel = new System.Windows.Forms.Label();
            this.calibrationEnableElectroMagnetCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.calibrationChannelsPanel = new System.Windows.Forms.Panel();
            this.calibrationChannel1CheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationChannel1ComboBox = new System.Windows.Forms.ComboBox();
            this.calibrationOperateGroupBox = new System.Windows.Forms.GroupBox();
            this.calibrationOpenFolderButton = new System.Windows.Forms.Button();
            this.calibrationStepperUpCheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationStartStopCheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationShortCircuitCheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationNumberOfCyclesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.calibrationNumberOfCyclesLabel = new System.Windows.Forms.Label();
            this.calibrationShortCircuitVoltageumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.calibrationShortCircuitVoltageLabel = new System.Windows.Forms.Label();
            this.calibrationPathTextBox = new System.Windows.Forms.TextBox();
            this.calibrationBrowseButton = new System.Windows.Forms.Button();
            this.calibrationSavingFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.calibrationFileNumberLabel = new System.Windows.Forms.Label();
            this.calibrationPathLabel = new System.Windows.Forms.Label();
            this.calibrationCycleNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.controlPanelsTabPage = new System.Windows.Forms.TabPage();
            this.electroMagnetGroupBox = new System.Windows.Forms.GroupBox();
            this.electroMagnetUserControl1 = new ElectroMagnetUserControl();
            this.stepperMotorGroupBox = new System.Windows.Forms.GroupBox();
            this.stepperMotorUserControl2 = new StepperMotorUserControl();
            this.aquireDataBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.stepperUpBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.fixBiasBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ivCyclesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.calibrationBackGroundWorker = new System.ComponentModel.BackgroundWorker();
            this.manualStartBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.continuousSamplingBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.useDefaultGainCheckBox = new System.Windows.Forms.CheckBox();
            this.controllerTabControl.SuspendLayout();
            this.dataAquisitionTabPage.SuspendLayout();
            this.plotGroupBox.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.secondEOMFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstEOMFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.externalFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laserAmplitudeOnSampleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laserAmplitudeWNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laserAmplitudeNumericUpDown)).BeginInit();
            this.LockInSettingsTabPage.SuspendLayout();
            this.lockInPanel.SuspendLayout();
            this.internalSourceLockInGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mixerReductionFactorNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lockInAcVoltageNumericEdit)).BeginInit();
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
            this.externalEMTabPage.SuspendLayout();
            this.externalEMpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lambdaZupOutputVoltageNumericEdit)).BeginInit();
            this.channelsConfigurationTabPage.SuspendLayout();
            this.channelsSettingsPanel.SuspendLayout();
            this.ivAcquisition.SuspendLayout();
            this.ivOperateGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivNumberOfCyclesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivShortCircuitVoltageNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivFileNumberNumericUpDown)).BeginInit();
            this.ivPlotGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivWaveformGraph)).BeginInit();
            this.ivSettingsGroupBox.SuspendLayout();
            this.ivGeneralTabControl.SuspendLayout();
            this.ivGeneralSettingsTabPage.SuspendLayout();
            this.ivGeneralSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivVoltageForTraceNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivTimeOfOneIVCycleNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivOutputUpdateRateNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivVoltageAmplitudeNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivOutputUpdateDelayNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivSamplesPerCycleNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivInputSampleRateNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivTriggerVoltageNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivTriggerConductanceNumericEdit)).BeginInit();
            this.ivSteppingMethodTabPage.SuspendLayout();
            this.ivSteppingMethodPanel.SuspendLayout();
            this.ivElectroMagnetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivEMShortCircuitDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivEMSlowDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivEMFastDelayTimeNumericUpDown)).BeginInit();
            this.ivStepperMotorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivStepperDelayTime2NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivStepperDelayTime1NumericUpDown)).BeginInit();
            this.ivChannelsTabPage.SuspendLayout();
            this.ivChannelsPanel.SuspendLayout();
            this.calibrationTabPage.SuspendLayout();
            this.calibrationPlotGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationWaveformGraph)).BeginInit();
            this.calibrationSettingsGroupBox.SuspendLayout();
            this.calibrationSettingsTabControl.SuspendLayout();
            this.CalibrationGeneralTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationSampleRateNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationBiasNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationTriggerVoltageNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationTriggerConductanceNumericEdit)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.calibrationElectroMagnetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationEMShortCircuitDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationEMSlowDelayTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationEMFastDelayTimeNumericUpDown)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.calibrationChannelsPanel.SuspendLayout();
            this.calibrationOperateGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationNumberOfCyclesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationShortCircuitVoltageumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationCycleNumberNumericUpDown)).BeginInit();
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
            this.obtainShortCircuitBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.obtainShortCircuitBackgroundWorker_RunWorkerCompleted);
            // 
            // controllerTabControl
            // 
            this.controllerTabControl.Controls.Add(this.dataAquisitionTabPage);
            this.controllerTabControl.Controls.Add(this.ivAcquisition);
            this.controllerTabControl.Controls.Add(this.calibrationTabPage);
            this.controllerTabControl.Controls.Add(this.controlPanelsTabPage);
            this.controllerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controllerTabControl.Location = new System.Drawing.Point(0, 0);
            this.controllerTabControl.Name = "controllerTabControl";
            this.controllerTabControl.SelectedIndex = 0;
            this.controllerTabControl.Size = new System.Drawing.Size(787, 825);
            this.controllerTabControl.TabIndex = 2;
            this.controllerTabControl.Tag = "";
            // 
            // dataAquisitionTabPage
            // 
            this.dataAquisitionTabPage.AutoScroll = true;
            this.dataAquisitionTabPage.Controls.Add(this.plotGroupBox);
            this.dataAquisitionTabPage.Controls.Add(this.operateGroupBox);
            this.dataAquisitionTabPage.Controls.Add(this.traceWaveformGraph);
            this.dataAquisitionTabPage.Controls.Add(this.samplePropertiesGroupBox);
            this.dataAquisitionTabPage.Controls.Add(this.settingsGroupBox);
            this.dataAquisitionTabPage.Location = new System.Drawing.Point(4, 22);
            this.dataAquisitionTabPage.Name = "dataAquisitionTabPage";
            this.dataAquisitionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dataAquisitionTabPage.Size = new System.Drawing.Size(779, 799);
            this.dataAquisitionTabPage.TabIndex = 0;
            this.dataAquisitionTabPage.Text = "DAQ";
            this.dataAquisitionTabPage.UseVisualStyleBackColor = true;
            // 
            // plotGroupBox
            // 
            this.plotGroupBox.Controls.Add(this.channelsListView);
            this.plotGroupBox.ForeColor = System.Drawing.Color.Red;
            this.plotGroupBox.Location = new System.Drawing.Point(494, 16);
            this.plotGroupBox.Name = "plotGroupBox";
            this.plotGroupBox.Size = new System.Drawing.Size(235, 339);
            this.plotGroupBox.TabIndex = 25;
            this.plotGroupBox.TabStop = false;
            this.plotGroupBox.Text = "Plots";
            // 
            // channelsListView
            // 
            this.channelsListView.CheckBoxes = true;
            this.channelsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelsListView.Location = new System.Drawing.Point(3, 16);
            this.channelsListView.Name = "channelsListView";
            this.channelsListView.Size = new System.Drawing.Size(229, 320);
            this.channelsListView.TabIndex = 25;
            this.channelsListView.UseCompatibleStateImageBehavior = false;
            this.channelsListView.View = System.Windows.Forms.View.List;
            this.channelsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.channelsListView_ItemChecked);
            this.channelsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.channelsListView_ItemSelectionChanged);
            // 
            // operateGroupBox
            // 
            this.operateGroupBox.AutoSize = true;
            this.operateGroupBox.Controls.Add(this.useDefaultGainCheckBox);
            this.operateGroupBox.Controls.Add(this.continuousSamplingCheckBox);
            this.operateGroupBox.Controls.Add(this.manualStartCheckBoxButton);
            this.operateGroupBox.Controls.Add(this.openFolderButton);
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
            this.operateGroupBox.Location = new System.Drawing.Point(3, 620);
            this.operateGroupBox.MinimumSize = new System.Drawing.Size(478, 176);
            this.operateGroupBox.Name = "operateGroupBox";
            this.operateGroupBox.Size = new System.Drawing.Size(773, 176);
            this.operateGroupBox.TabIndex = 23;
            this.operateGroupBox.TabStop = false;
            this.operateGroupBox.Text = "Operate";
            // 
            // continuousSamplingCheckBox
            // 
            this.continuousSamplingCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.continuousSamplingCheckBox.AutoSize = true;
            this.continuousSamplingCheckBox.ForeColor = System.Drawing.Color.Black;
            this.continuousSamplingCheckBox.Location = new System.Drawing.Point(616, 79);
            this.continuousSamplingCheckBox.MinimumSize = new System.Drawing.Size(77, 23);
            this.continuousSamplingCheckBox.Name = "continuousSamplingCheckBox";
            this.continuousSamplingCheckBox.Size = new System.Drawing.Size(77, 23);
            this.continuousSamplingCheckBox.TabIndex = 31;
            this.continuousSamplingCheckBox.Text = "Continuous";
            this.continuousSamplingCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.continuousSamplingCheckBox.UseVisualStyleBackColor = true;
            this.continuousSamplingCheckBox.CheckedChanged += new System.EventHandler(this.continuousSamplingCheckBox_CheckedChanged);
            // 
            // manualStartCheckBoxButton
            // 
            this.manualStartCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.manualStartCheckBoxButton.AutoSize = true;
            this.manualStartCheckBoxButton.ForeColor = System.Drawing.Color.Black;
            this.manualStartCheckBoxButton.Location = new System.Drawing.Point(450, 113);
            this.manualStartCheckBoxButton.MinimumSize = new System.Drawing.Size(77, 23);
            this.manualStartCheckBoxButton.Name = "manualStartCheckBoxButton";
            this.manualStartCheckBoxButton.Size = new System.Drawing.Size(77, 23);
            this.manualStartCheckBoxButton.TabIndex = 30;
            this.manualStartCheckBoxButton.Text = "Manual Start";
            this.manualStartCheckBoxButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.manualStartCheckBoxButton.UseVisualStyleBackColor = true;
            this.manualStartCheckBoxButton.CheckedChanged += new System.EventHandler(this.manualStartCheckBox_CheckedChanged);
            // 
            // openFolderButton
            // 
            this.openFolderButton.ForeColor = System.Drawing.Color.Black;
            this.openFolderButton.Location = new System.Drawing.Point(533, 44);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(77, 23);
            this.openFolderButton.TabIndex = 29;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // fixBiasCheckBoxButton
            // 
            this.fixBiasCheckBoxButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.fixBiasCheckBoxButton.AutoSize = true;
            this.fixBiasCheckBoxButton.ForeColor = System.Drawing.Color.Black;
            this.fixBiasCheckBoxButton.Location = new System.Drawing.Point(616, 113);
            this.fixBiasCheckBoxButton.MinimumSize = new System.Drawing.Size(77, 23);
            this.fixBiasCheckBoxButton.Name = "fixBiasCheckBoxButton";
            this.fixBiasCheckBoxButton.Size = new System.Drawing.Size(77, 23);
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
            this.moveUpCheckBoxButton.Location = new System.Drawing.Point(533, 113);
            this.moveUpCheckBoxButton.MinimumSize = new System.Drawing.Size(77, 23);
            this.moveUpCheckBoxButton.Name = "moveUpCheckBoxButton";
            this.moveUpCheckBoxButton.Size = new System.Drawing.Size(77, 23);
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
            this.startStopCheckBoxButton.Location = new System.Drawing.Point(450, 79);
            this.startStopCheckBoxButton.MinimumSize = new System.Drawing.Size(77, 23);
            this.startStopCheckBoxButton.Name = "startStopCheckBoxButton";
            this.startStopCheckBoxButton.Size = new System.Drawing.Size(77, 23);
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
            this.shortCircuitCheckBoxButton.Location = new System.Drawing.Point(533, 79);
            this.shortCircuitCheckBoxButton.MinimumSize = new System.Drawing.Size(77, 23);
            this.shortCircuitCheckBoxButton.Name = "shortCircuitCheckBoxButton";
            this.shortCircuitCheckBoxButton.Size = new System.Drawing.Size(77, 23);
            this.shortCircuitCheckBoxButton.TabIndex = 15;
            this.shortCircuitCheckBoxButton.Text = "Short Circuit";
            this.shortCircuitCheckBoxButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.shortCircuitCheckBoxButton.UseVisualStyleBackColor = true;
            this.shortCircuitCheckBoxButton.Click += new System.EventHandler(this.shortCircuitButton_CheckedChanged);
            // 
            // numberOfCyclesnumericUpDown
            // 
            this.numberOfCyclesnumericUpDown.Location = new System.Drawing.Point(364, 77);
            this.numberOfCyclesnumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberOfCyclesnumericUpDown.Name = "numberOfCyclesnumericUpDown";
            this.numberOfCyclesnumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.numberOfCyclesnumericUpDown.TabIndex = 14;
            this.numberOfCyclesnumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numberOfCyclesLabel1
            // 
            this.numberOfCyclesLabel1.AutoSize = true;
            this.numberOfCyclesLabel1.ForeColor = System.Drawing.Color.Black;
            this.numberOfCyclesLabel1.Location = new System.Drawing.Point(250, 79);
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
            this.shortCircuitVoltageNumericUpDown.Location = new System.Drawing.Point(150, 116);
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
            this.shortCircuitVoltageLabel.Location = new System.Drawing.Point(3, 118);
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
            this.browseButton.Size = new System.Drawing.Size(77, 23);
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
            this.traceWaveformGraph.Location = new System.Drawing.Point(21, 16);
            this.traceWaveformGraph.Name = "traceWaveformGraph";
            this.traceWaveformGraph.PlotAreaColor = System.Drawing.Color.LightGray;
            this.traceWaveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.traceWaveformGraph.Size = new System.Drawing.Size(467, 339);
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
            this.samplePropertiesGroupBox.Location = new System.Drawing.Point(553, 364);
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
            this.settingsGroupBox.Location = new System.Drawing.Point(13, 364);
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
            this.SettingsTabControl.Controls.Add(this.externalEMTabPage);
            this.SettingsTabControl.Controls.Add(this.channelsConfigurationTabPage);
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
            this.generalSettingsPanel.Controls.Add(this.useKeithleyCheckBox);
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
            // useKeithleyCheckBox
            // 
            this.useKeithleyCheckBox.AutoSize = true;
            this.useKeithleyCheckBox.Checked = true;
            this.useKeithleyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useKeithleyCheckBox.ForeColor = System.Drawing.Color.Black;
            this.useKeithleyCheckBox.Location = new System.Drawing.Point(7, 6);
            this.useKeithleyCheckBox.Name = "useKeithleyCheckBox";
            this.useKeithleyCheckBox.Size = new System.Drawing.Size(159, 17);
            this.useKeithleyCheckBox.TabIndex = 21;
            this.useKeithleyCheckBox.Text = "Use Keithley as Bias Source";
            this.useKeithleyCheckBox.UseVisualStyleBackColor = true;
            this.useKeithleyCheckBox.CheckedChanged += new System.EventHandler(this.useKeithleyCheckBox_CheckedChanged);
            // 
            // biasErrorNumericEdit
            // 
            this.biasErrorNumericEdit.CoercionInterval = 0.01;
            this.biasErrorNumericEdit.Enabled = false;
            this.biasErrorNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(4);
            this.biasErrorNumericEdit.Location = new System.Drawing.Point(147, 59);
            this.biasErrorNumericEdit.Name = "biasErrorNumericEdit";
            this.biasErrorNumericEdit.Range = new NationalInstruments.UI.Range(-2, 2);
            this.biasErrorNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.biasErrorNumericEdit.TabIndex = 20;
            // 
            // biasErrorLabel
            // 
            this.biasErrorLabel.AutoSize = true;
            this.biasErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.biasErrorLabel.Location = new System.Drawing.Point(4, 61);
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
            this.gainComboBox.Location = new System.Drawing.Point(147, 87);
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
            this.stepperWaitTime2NumericUpDown.Location = new System.Drawing.Point(423, 149);
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
            this.stepperWaitTime2Label.Location = new System.Drawing.Point(246, 151);
            this.stepperWaitTime2Label.Name = "stepperWaitTime2Label";
            this.stepperWaitTime2Label.Size = new System.Drawing.Size(138, 13);
            this.stepperWaitTime2Label.TabIndex = 16;
            this.stepperWaitTime2Label.Text = "Stepper Wait Time 2 [msec]";
            // 
            // pretriggerSamplesNumericUpDown
            // 
            this.pretriggerSamplesNumericUpDown.Location = new System.Drawing.Point(423, 59);
            this.pretriggerSamplesNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
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
            this.pretriggerSamplesLabel.Location = new System.Drawing.Point(246, 61);
            this.pretriggerSamplesLabel.Name = "pretriggerSamplesLabel";
            this.pretriggerSamplesLabel.Size = new System.Drawing.Size(98, 13);
            this.pretriggerSamplesLabel.TabIndex = 14;
            this.pretriggerSamplesLabel.Text = "Pre-trigger Samples";
            // 
            // sampleRateNumericUpDown
            // 
            this.sampleRateNumericUpDown.Location = new System.Drawing.Point(423, 29);
            this.sampleRateNumericUpDown.Maximum = new decimal(new int[] {
            100000,
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
            this.totalSamplesNumericUpDown.Location = new System.Drawing.Point(423, 88);
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
            this.totalSamplesLabel.Location = new System.Drawing.Point(246, 91);
            this.totalSamplesLabel.Name = "totalSamplesLabel";
            this.totalSamplesLabel.Size = new System.Drawing.Size(74, 13);
            this.totalSamplesLabel.TabIndex = 12;
            this.totalSamplesLabel.Text = "Total Samples";
            // 
            // triggerVoltageNumericEdit
            // 
            this.triggerVoltageNumericEdit.Enabled = false;
            this.triggerVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.triggerVoltageNumericEdit.Location = new System.Drawing.Point(147, 117);
            this.triggerVoltageNumericEdit.Name = "triggerVoltageNumericEdit";
            this.triggerVoltageNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.triggerVoltageNumericEdit.TabIndex = 11;
            this.triggerVoltageNumericEdit.Value = -0.01;
            // 
            // triggerVoltageLabel
            // 
            this.triggerVoltageLabel.AutoSize = true;
            this.triggerVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.triggerVoltageLabel.Location = new System.Drawing.Point(4, 121);
            this.triggerVoltageLabel.Name = "triggerVoltageLabel";
            this.triggerVoltageLabel.Size = new System.Drawing.Size(95, 13);
            this.triggerVoltageLabel.TabIndex = 10;
            this.triggerVoltageLabel.Text = "Trigger Voltage [V]";
            // 
            // triggerConductanceNumericEdit
            // 
            this.triggerConductanceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.triggerConductanceNumericEdit.Location = new System.Drawing.Point(147, 146);
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
            this.triggerConductanceLabel.Location = new System.Drawing.Point(4, 151);
            this.triggerConductanceLabel.Name = "triggerConductanceLabel";
            this.triggerConductanceLabel.Size = new System.Drawing.Size(130, 13);
            this.triggerConductanceLabel.TabIndex = 8;
            this.triggerConductanceLabel.Text = "Trigger Conductance [G0]";
            // 
            // sampleRateLabel
            // 
            this.sampleRateLabel.AutoSize = true;
            this.sampleRateLabel.ForeColor = System.Drawing.Color.Black;
            this.sampleRateLabel.Location = new System.Drawing.Point(246, 31);
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
            this.stepperWaitTime1NumericUpDown.Location = new System.Drawing.Point(423, 119);
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
            this.stepperWaitTime1Label.Location = new System.Drawing.Point(246, 121);
            this.stepperWaitTime1Label.Name = "stepperWaitTime1Label";
            this.stepperWaitTime1Label.Size = new System.Drawing.Size(138, 13);
            this.stepperWaitTime1Label.TabIndex = 4;
            this.stepperWaitTime1Label.Text = "Stepper Wait Time 1 [msec]";
            // 
            // gainLabel
            // 
            this.gainLabel.AutoSize = true;
            this.gainLabel.ForeColor = System.Drawing.Color.Black;
            this.gainLabel.Location = new System.Drawing.Point(4, 91);
            this.gainLabel.Name = "gainLabel";
            this.gainLabel.Size = new System.Drawing.Size(62, 13);
            this.gainLabel.TabIndex = 2;
            this.gainLabel.Text = "Gain Power";
            // 
            // biasNumericEdit
            // 
            this.biasNumericEdit.CoercionInterval = 0.01;
            this.biasNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(3);
            this.biasNumericEdit.Location = new System.Drawing.Point(147, 29);
            this.biasNumericEdit.Name = "biasNumericEdit";
            this.biasNumericEdit.Range = new NationalInstruments.UI.Range(-4, 4);
            this.biasNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.biasNumericEdit.TabIndex = 1;
            this.biasNumericEdit.Value = 0.1;
            this.biasNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.biasNumericEdit_AfterChangeValue);
            // 
            // biasLabel
            // 
            this.biasLabel.AutoSize = true;
            this.biasLabel.ForeColor = System.Drawing.Color.Black;
            this.biasLabel.Location = new System.Drawing.Point(4, 31);
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
            this.laserSettingsPanel.Controls.Add(this.eomCOnfigurationComboBox);
            this.laserSettingsPanel.Controls.Add(this.secondEOMFrequencyNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.secondEOMFrequencyLabel);
            this.laserSettingsPanel.Controls.Add(this.enableSecondEOMCheckBox);
            this.laserSettingsPanel.Controls.Add(this.firstEOMFrequencyNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.firstEOMFrequencyLabel);
            this.laserSettingsPanel.Controls.Add(this.enableChopperCheckBox);
            this.laserSettingsPanel.Controls.Add(this.externalFrequencyNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.externalFrequencyLabel);
            this.laserSettingsPanel.Controls.Add(this.enableFirstEOMcheckBox);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeOnSampleNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeOnSampleLabel);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeWNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeWLabel);
            this.laserSettingsPanel.Controls.Add(this.laserModeComboBox);
            this.laserSettingsPanel.Controls.Add(this.frequencyNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.enableLaserCheckBox);
            this.laserSettingsPanel.Controls.Add(this.frequencyLabel);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeNumericUpDown);
            this.laserSettingsPanel.Controls.Add(this.laserAmplitudeLabel);
            this.laserSettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laserSettingsPanel.ForeColor = System.Drawing.Color.Black;
            this.laserSettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.laserSettingsPanel.MinimumSize = new System.Drawing.Size(0, 89);
            this.laserSettingsPanel.Name = "laserSettingsPanel";
            this.laserSettingsPanel.Size = new System.Drawing.Size(514, 196);
            this.laserSettingsPanel.TabIndex = 5;
            // 
            // eomCOnfigurationComboBox
            // 
            this.eomCOnfigurationComboBox.DisplayMember = "1";
            this.eomCOnfigurationComboBox.Enabled = false;
            this.eomCOnfigurationComboBox.FormattingEnabled = true;
            this.eomCOnfigurationComboBox.Items.AddRange(new object[] {
            "Parallel",
            "Series"});
            this.eomCOnfigurationComboBox.Location = new System.Drawing.Point(409, 68);
            this.eomCOnfigurationComboBox.Name = "eomCOnfigurationComboBox";
            this.eomCOnfigurationComboBox.Size = new System.Drawing.Size(85, 21);
            this.eomCOnfigurationComboBox.TabIndex = 19;
            this.eomCOnfigurationComboBox.Text = "Series";
            // 
            // secondEOMFrequencyNumericUpDown
            // 
            this.secondEOMFrequencyNumericUpDown.Enabled = false;
            this.secondEOMFrequencyNumericUpDown.Location = new System.Drawing.Point(409, 102);
            this.secondEOMFrequencyNumericUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.secondEOMFrequencyNumericUpDown.Name = "secondEOMFrequencyNumericUpDown";
            this.secondEOMFrequencyNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.secondEOMFrequencyNumericUpDown.TabIndex = 18;
            this.secondEOMFrequencyNumericUpDown.ThousandsSeparator = true;
            this.secondEOMFrequencyNumericUpDown.Value = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.secondEOMFrequencyNumericUpDown.ValueChanged += new System.EventHandler(this.secondEOMFrequencyNumericUpDown_ValueChanged);
            // 
            // secondEOMFrequencyLabel
            // 
            this.secondEOMFrequencyLabel.AutoSize = true;
            this.secondEOMFrequencyLabel.Enabled = false;
            this.secondEOMFrequencyLabel.Location = new System.Drawing.Point(298, 104);
            this.secondEOMFrequencyLabel.Name = "secondEOMFrequencyLabel";
            this.secondEOMFrequencyLabel.Size = new System.Drawing.Size(79, 13);
            this.secondEOMFrequencyLabel.TabIndex = 17;
            this.secondEOMFrequencyLabel.Text = "Frequency [Hz]";
            // 
            // enableSecondEOMCheckBox
            // 
            this.enableSecondEOMCheckBox.AutoSize = true;
            this.enableSecondEOMCheckBox.Enabled = false;
            this.enableSecondEOMCheckBox.ForeColor = System.Drawing.Color.Black;
            this.enableSecondEOMCheckBox.Location = new System.Drawing.Point(298, 70);
            this.enableSecondEOMCheckBox.Name = "enableSecondEOMCheckBox";
            this.enableSecondEOMCheckBox.Size = new System.Drawing.Size(86, 17);
            this.enableSecondEOMCheckBox.TabIndex = 16;
            this.enableSecondEOMCheckBox.Text = "Enable EOM";
            this.enableSecondEOMCheckBox.UseVisualStyleBackColor = true;
            this.enableSecondEOMCheckBox.CheckedChanged += new System.EventHandler(this.enableSecondEOMCheckBox_CheckedChanged);
            // 
            // firstEOMFrequencyNumericUpDown
            // 
            this.firstEOMFrequencyNumericUpDown.Enabled = false;
            this.firstEOMFrequencyNumericUpDown.Location = new System.Drawing.Point(409, 31);
            this.firstEOMFrequencyNumericUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.firstEOMFrequencyNumericUpDown.Name = "firstEOMFrequencyNumericUpDown";
            this.firstEOMFrequencyNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.firstEOMFrequencyNumericUpDown.TabIndex = 15;
            this.firstEOMFrequencyNumericUpDown.ThousandsSeparator = true;
            this.firstEOMFrequencyNumericUpDown.Value = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.firstEOMFrequencyNumericUpDown.ValueChanged += new System.EventHandler(this.firstEOMFrequencyNumericUpDown_ValueChanged);
            // 
            // firstEOMFrequencyLabel
            // 
            this.firstEOMFrequencyLabel.AutoSize = true;
            this.firstEOMFrequencyLabel.Enabled = false;
            this.firstEOMFrequencyLabel.Location = new System.Drawing.Point(298, 33);
            this.firstEOMFrequencyLabel.Name = "firstEOMFrequencyLabel";
            this.firstEOMFrequencyLabel.Size = new System.Drawing.Size(79, 13);
            this.firstEOMFrequencyLabel.TabIndex = 14;
            this.firstEOMFrequencyLabel.Text = "Frequency [Hz]";
            // 
            // enableChopperCheckBox
            // 
            this.enableChopperCheckBox.AutoSize = true;
            this.enableChopperCheckBox.Enabled = false;
            this.enableChopperCheckBox.ForeColor = System.Drawing.Color.Black;
            this.enableChopperCheckBox.Location = new System.Drawing.Point(298, 140);
            this.enableChopperCheckBox.Name = "enableChopperCheckBox";
            this.enableChopperCheckBox.Size = new System.Drawing.Size(102, 17);
            this.enableChopperCheckBox.TabIndex = 13;
            this.enableChopperCheckBox.Text = "Enable Chopper";
            this.enableChopperCheckBox.UseVisualStyleBackColor = true;
            this.enableChopperCheckBox.CheckedChanged += new System.EventHandler(this.enableChopperCheckBox_CheckedChanged);
            // 
            // externalFrequencyNumericUpDown
            // 
            this.externalFrequencyNumericUpDown.Enabled = false;
            this.externalFrequencyNumericUpDown.Location = new System.Drawing.Point(409, 167);
            this.externalFrequencyNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.externalFrequencyNumericUpDown.Name = "externalFrequencyNumericUpDown";
            this.externalFrequencyNumericUpDown.Size = new System.Drawing.Size(85, 20);
            this.externalFrequencyNumericUpDown.TabIndex = 12;
            this.externalFrequencyNumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // externalFrequencyLabel
            // 
            this.externalFrequencyLabel.AutoSize = true;
            this.externalFrequencyLabel.Enabled = false;
            this.externalFrequencyLabel.Location = new System.Drawing.Point(298, 168);
            this.externalFrequencyLabel.Name = "externalFrequencyLabel";
            this.externalFrequencyLabel.Size = new System.Drawing.Size(79, 13);
            this.externalFrequencyLabel.TabIndex = 11;
            this.externalFrequencyLabel.Text = "Frequency [Hz]";
            // 
            // enableFirstEOMcheckBox
            // 
            this.enableFirstEOMcheckBox.AutoSize = true;
            this.enableFirstEOMcheckBox.Enabled = false;
            this.enableFirstEOMcheckBox.ForeColor = System.Drawing.Color.Black;
            this.enableFirstEOMcheckBox.Location = new System.Drawing.Point(298, 6);
            this.enableFirstEOMcheckBox.Name = "enableFirstEOMcheckBox";
            this.enableFirstEOMcheckBox.Size = new System.Drawing.Size(86, 17);
            this.enableFirstEOMcheckBox.TabIndex = 10;
            this.enableFirstEOMcheckBox.Text = "Enable EOM";
            this.enableFirstEOMcheckBox.UseVisualStyleBackColor = true;
            this.enableFirstEOMcheckBox.CheckedChanged += new System.EventHandler(this.enableEOMcheckBox_CheckedChanged);
            // 
            // laserAmplitudeOnSampleNumericUpDown
            // 
            this.laserAmplitudeOnSampleNumericUpDown.DecimalPlaces = 3;
            this.laserAmplitudeOnSampleNumericUpDown.Enabled = false;
            this.laserAmplitudeOnSampleNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.laserAmplitudeOnSampleNumericUpDown.Location = new System.Drawing.Point(161, 98);
            this.laserAmplitudeOnSampleNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.laserAmplitudeOnSampleNumericUpDown.Name = "laserAmplitudeOnSampleNumericUpDown";
            this.laserAmplitudeOnSampleNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.laserAmplitudeOnSampleNumericUpDown.TabIndex = 9;
            this.laserAmplitudeOnSampleNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // laserAmplitudeOnSampleLabel
            // 
            this.laserAmplitudeOnSampleLabel.AutoSize = true;
            this.laserAmplitudeOnSampleLabel.Enabled = false;
            this.laserAmplitudeOnSampleLabel.ForeColor = System.Drawing.Color.Black;
            this.laserAmplitudeOnSampleLabel.Location = new System.Drawing.Point(4, 100);
            this.laserAmplitudeOnSampleLabel.Name = "laserAmplitudeOnSampleLabel";
            this.laserAmplitudeOnSampleLabel.Size = new System.Drawing.Size(134, 13);
            this.laserAmplitudeOnSampleLabel.TabIndex = 8;
            this.laserAmplitudeOnSampleLabel.Text = "Amplitude on Sample [mW]";
            // 
            // laserAmplitudeWNumericUpDown
            // 
            this.laserAmplitudeWNumericUpDown.DecimalPlaces = 3;
            this.laserAmplitudeWNumericUpDown.Enabled = false;
            this.laserAmplitudeWNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.laserAmplitudeWNumericUpDown.Location = new System.Drawing.Point(161, 67);
            this.laserAmplitudeWNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.laserAmplitudeWNumericUpDown.Name = "laserAmplitudeWNumericUpDown";
            this.laserAmplitudeWNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.laserAmplitudeWNumericUpDown.TabIndex = 7;
            this.laserAmplitudeWNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // laserAmplitudeWLabel
            // 
            this.laserAmplitudeWLabel.AutoSize = true;
            this.laserAmplitudeWLabel.Enabled = false;
            this.laserAmplitudeWLabel.ForeColor = System.Drawing.Color.Black;
            this.laserAmplitudeWLabel.Location = new System.Drawing.Point(4, 70);
            this.laserAmplitudeWLabel.Name = "laserAmplitudeWLabel";
            this.laserAmplitudeWLabel.Size = new System.Drawing.Size(81, 13);
            this.laserAmplitudeWLabel.TabIndex = 6;
            this.laserAmplitudeWLabel.Text = "Amplitude [mW]";
            // 
            // laserModeComboBox
            // 
            this.laserModeComboBox.Enabled = false;
            this.laserModeComboBox.FormattingEnabled = true;
            this.laserModeComboBox.Items.AddRange(new object[] {
            "IODrive",
            "DC",
            "Square"});
            this.laserModeComboBox.Location = new System.Drawing.Point(161, 4);
            this.laserModeComboBox.Name = "laserModeComboBox";
            this.laserModeComboBox.Size = new System.Drawing.Size(75, 21);
            this.laserModeComboBox.TabIndex = 3;
            this.laserModeComboBox.Text = "DC";
            this.laserModeComboBox.SelectedValueChanged += new System.EventHandler(this.laserModeComboBox_SelectedValueChanged);
            // 
            // frequencyNumericUpDown
            // 
            this.frequencyNumericUpDown.Enabled = false;
            this.frequencyNumericUpDown.Location = new System.Drawing.Point(161, 129);
            this.frequencyNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.frequencyNumericUpDown.Name = "frequencyNumericUpDown";
            this.frequencyNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.frequencyNumericUpDown.TabIndex = 5;
            this.frequencyNumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.frequencyNumericUpDown.ValueChanged += new System.EventHandler(this.frequencyNumericUpDown_ValueChanged);
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
            this.frequencyLabel.Location = new System.Drawing.Point(4, 130);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(79, 13);
            this.frequencyLabel.TabIndex = 4;
            this.frequencyLabel.Text = "Frequency [Hz]";
            // 
            // laserAmplitudeNumericUpDown
            // 
            this.laserAmplitudeNumericUpDown.DecimalPlaces = 3;
            this.laserAmplitudeNumericUpDown.Enabled = false;
            this.laserAmplitudeNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.laserAmplitudeNumericUpDown.Location = new System.Drawing.Point(161, 38);
            this.laserAmplitudeNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.laserAmplitudeNumericUpDown.Name = "laserAmplitudeNumericUpDown";
            this.laserAmplitudeNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.laserAmplitudeNumericUpDown.TabIndex = 3;
            this.laserAmplitudeNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.laserAmplitudeNumericUpDown.ValueChanged += new System.EventHandler(this.amplitudeNumericUpDown_ValueChanged);
            // 
            // laserAmplitudeLabel
            // 
            this.laserAmplitudeLabel.AutoSize = true;
            this.laserAmplitudeLabel.Enabled = false;
            this.laserAmplitudeLabel.ForeColor = System.Drawing.Color.Black;
            this.laserAmplitudeLabel.Location = new System.Drawing.Point(4, 40);
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
            this.lockInPanel.Controls.Add(this.rollOffComboBox);
            this.lockInPanel.Controls.Add(this.rollOffLabel);
            this.lockInPanel.Controls.Add(this.enableLockInCheckBox);
            this.lockInPanel.Controls.Add(this.timeConstantComboBox);
            this.lockInPanel.Controls.Add(this.timeConstantLabel);
            this.lockInPanel.Controls.Add(this.sensitivityComboBox);
            this.lockInPanel.Controls.Add(this.internalSourceLockInGroupBox);
            this.lockInPanel.Controls.Add(this.lockInSensitivityLabel);
            this.lockInPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lockInPanel.Location = new System.Drawing.Point(3, 3);
            this.lockInPanel.Name = "lockInPanel";
            this.lockInPanel.Size = new System.Drawing.Size(514, 196);
            this.lockInPanel.TabIndex = 0;
            // 
            // rollOffComboBox
            // 
            this.rollOffComboBox.FormattingEnabled = true;
            this.rollOffComboBox.Items.AddRange(new object[] {
            "6",
            "12",
            "18",
            "24"});
            this.rollOffComboBox.Location = new System.Drawing.Point(137, 124);
            this.rollOffComboBox.Name = "rollOffComboBox";
            this.rollOffComboBox.Size = new System.Drawing.Size(89, 21);
            this.rollOffComboBox.TabIndex = 14;
            this.rollOffComboBox.Text = "18";
            this.rollOffComboBox.SelectedValueChanged += new System.EventHandler(this.rollOffComboBox_SelectedValueChanged);
            // 
            // rollOffLabel
            // 
            this.rollOffLabel.AutoSize = true;
            this.rollOffLabel.ForeColor = System.Drawing.Color.Black;
            this.rollOffLabel.Location = new System.Drawing.Point(9, 127);
            this.rollOffLabel.Name = "rollOffLabel";
            this.rollOffLabel.Size = new System.Drawing.Size(64, 13);
            this.rollOffLabel.TabIndex = 13;
            this.rollOffLabel.Text = "Roll Off [dB]";
            // 
            // enableLockInCheckBox
            // 
            this.enableLockInCheckBox.AutoSize = true;
            this.enableLockInCheckBox.ForeColor = System.Drawing.Color.Black;
            this.enableLockInCheckBox.Location = new System.Drawing.Point(12, 16);
            this.enableLockInCheckBox.Name = "enableLockInCheckBox";
            this.enableLockInCheckBox.Size = new System.Drawing.Size(95, 17);
            this.enableLockInCheckBox.TabIndex = 12;
            this.enableLockInCheckBox.Text = "Enable LockIn";
            this.enableLockInCheckBox.UseVisualStyleBackColor = true;
            this.enableLockInCheckBox.CheckedChanged += new System.EventHandler(this.enableLockInCheckBox_CheckedChanged);
            // 
            // timeConstantComboBox
            // 
            this.timeConstantComboBox.FormattingEnabled = true;
            this.timeConstantComboBox.Items.AddRange(new object[] {
            "10E-6",
            "30E-6",
            "100E-6",
            "300E-6",
            "1E-3",
            "3E-3",
            "10E-3",
            "30E-3",
            "100E-3",
            "300E-3",
            "1",
            "3",
            "10",
            "30",
            "100",
            "300",
            "1E+3",
            "3E+3",
            "10E+3",
            "30E+3"});
            this.timeConstantComboBox.Location = new System.Drawing.Point(137, 86);
            this.timeConstantComboBox.Name = "timeConstantComboBox";
            this.timeConstantComboBox.Size = new System.Drawing.Size(89, 21);
            this.timeConstantComboBox.TabIndex = 11;
            this.timeConstantComboBox.Text = "30E-3";
            this.timeConstantComboBox.SelectedValueChanged += new System.EventHandler(this.timeConstantComboBox_SelectedValueChanged);
            // 
            // timeConstantLabel
            // 
            this.timeConstantLabel.AutoSize = true;
            this.timeConstantLabel.ForeColor = System.Drawing.Color.Black;
            this.timeConstantLabel.Location = new System.Drawing.Point(9, 89);
            this.timeConstantLabel.Name = "timeConstantLabel";
            this.timeConstantLabel.Size = new System.Drawing.Size(101, 13);
            this.timeConstantLabel.TabIndex = 10;
            this.timeConstantLabel.Text = "Time Constant [sec]";
            // 
            // sensitivityComboBox
            // 
            this.sensitivityComboBox.FormattingEnabled = true;
            this.sensitivityComboBox.Items.AddRange(new object[] {
            "2E-9",
            "5E-9",
            "10E-9",
            "20E-9",
            "50E-9",
            "100E-9",
            "200E-9",
            "500E-9",
            "1E-6",
            "2E-6",
            "5E-6",
            "10E-6",
            "20E-6",
            "50E-6",
            "100E-6",
            "200E-6",
            "500E-6",
            "1E-3",
            "2E-3",
            "5E-3",
            "10E-3",
            "20E-3",
            "50E-3",
            "100E-3",
            "200E-3",
            "500E-3",
            "1"});
            this.sensitivityComboBox.Location = new System.Drawing.Point(137, 50);
            this.sensitivityComboBox.Name = "sensitivityComboBox";
            this.sensitivityComboBox.Size = new System.Drawing.Size(89, 21);
            this.sensitivityComboBox.TabIndex = 9;
            this.sensitivityComboBox.Text = "500E-3";
            this.sensitivityComboBox.SelectedValueChanged += new System.EventHandler(this.sensitivityComboBox_SelectedValueChanged);
            // 
            // internalSourceLockInGroupBox
            // 
            this.internalSourceLockInGroupBox.Controls.Add(this.mixerReductionFactorNumericEdit);
            this.internalSourceLockInGroupBox.Controls.Add(this.acVoltageReductionFactorLabel);
            this.internalSourceLockInGroupBox.Controls.Add(this.internalSourceLockInCheckBoxcheckBox);
            this.internalSourceLockInGroupBox.Controls.Add(this.lockInAcVoltageNumericEdit);
            this.internalSourceLockInGroupBox.Controls.Add(this.lockInAcVoltageLabel);
            this.internalSourceLockInGroupBox.ForeColor = System.Drawing.Color.Red;
            this.internalSourceLockInGroupBox.Location = new System.Drawing.Point(260, 3);
            this.internalSourceLockInGroupBox.Name = "internalSourceLockInGroupBox";
            this.internalSourceLockInGroupBox.Size = new System.Drawing.Size(251, 184);
            this.internalSourceLockInGroupBox.TabIndex = 8;
            this.internalSourceLockInGroupBox.TabStop = false;
            // 
            // mixerReductionFactorNumericEdit
            // 
            this.mixerReductionFactorNumericEdit.CoercionInterval = 0.01;
            this.mixerReductionFactorNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateGenericMode("F0");
            this.mixerReductionFactorNumericEdit.Location = new System.Drawing.Point(152, 83);
            this.mixerReductionFactorNumericEdit.Name = "mixerReductionFactorNumericEdit";
            this.mixerReductionFactorNumericEdit.Range = new NationalInstruments.UI.Range(0, 100);
            this.mixerReductionFactorNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.mixerReductionFactorNumericEdit.TabIndex = 10;
            this.mixerReductionFactorNumericEdit.Value = 100;
            // 
            // acVoltageReductionFactorLabel
            // 
            this.acVoltageReductionFactorLabel.AutoSize = true;
            this.acVoltageReductionFactorLabel.ForeColor = System.Drawing.Color.Black;
            this.acVoltageReductionFactorLabel.Location = new System.Drawing.Point(10, 83);
            this.acVoltageReductionFactorLabel.Name = "acVoltageReductionFactorLabel";
            this.acVoltageReductionFactorLabel.Size = new System.Drawing.Size(117, 13);
            this.acVoltageReductionFactorLabel.TabIndex = 9;
            this.acVoltageReductionFactorLabel.Text = "Mixer Reduction Factor";
            // 
            // internalSourceLockInCheckBoxcheckBox
            // 
            this.internalSourceLockInCheckBoxcheckBox.AutoSize = true;
            this.internalSourceLockInCheckBoxcheckBox.ForeColor = System.Drawing.Color.Black;
            this.internalSourceLockInCheckBoxcheckBox.Location = new System.Drawing.Point(11, 13);
            this.internalSourceLockInCheckBoxcheckBox.Name = "internalSourceLockInCheckBoxcheckBox";
            this.internalSourceLockInCheckBoxcheckBox.Size = new System.Drawing.Size(61, 17);
            this.internalSourceLockInCheckBoxcheckBox.TabIndex = 8;
            this.internalSourceLockInCheckBoxcheckBox.Text = "Internal";
            this.internalSourceLockInCheckBoxcheckBox.UseVisualStyleBackColor = true;
            // 
            // lockInAcVoltageNumericEdit
            // 
            this.lockInAcVoltageNumericEdit.CoercionInterval = 0.01;
            this.lockInAcVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.lockInAcVoltageNumericEdit.Location = new System.Drawing.Point(152, 50);
            this.lockInAcVoltageNumericEdit.Name = "lockInAcVoltageNumericEdit";
            this.lockInAcVoltageNumericEdit.Range = new NationalInstruments.UI.Range(1E-05, 1);
            this.lockInAcVoltageNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.lockInAcVoltageNumericEdit.TabIndex = 7;
            this.lockInAcVoltageNumericEdit.Value = 0.01;
            // 
            // lockInAcVoltageLabel
            // 
            this.lockInAcVoltageLabel.AutoSize = true;
            this.lockInAcVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.lockInAcVoltageLabel.Location = new System.Drawing.Point(10, 50);
            this.lockInAcVoltageLabel.Name = "lockInAcVoltageLabel";
            this.lockInAcVoltageLabel.Size = new System.Drawing.Size(76, 13);
            this.lockInAcVoltageLabel.TabIndex = 6;
            this.lockInAcVoltageLabel.Text = "AC Voltage [V]";
            // 
            // lockInSensitivityLabel
            // 
            this.lockInSensitivityLabel.AutoSize = true;
            this.lockInSensitivityLabel.ForeColor = System.Drawing.Color.Black;
            this.lockInSensitivityLabel.Location = new System.Drawing.Point(9, 53);
            this.lockInSensitivityLabel.Name = "lockInSensitivityLabel";
            this.lockInSensitivityLabel.Size = new System.Drawing.Size(70, 13);
            this.lockInSensitivityLabel.TabIndex = 2;
            this.lockInSensitivityLabel.Text = "Sensitivity [V]";
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
            this.emHoldOnMinConductanceNumericEdit.Range = new NationalInstruments.UI.Range(0, 2000);
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
            this.emHoldOnMaxConductanceNumericEdit.Range = new NationalInstruments.UI.Range(0, 2000);
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
            30,
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
            100,
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
            2,
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
            // externalEMTabPage
            // 
            this.externalEMTabPage.Controls.Add(this.externalEMpanel);
            this.externalEMTabPage.Location = new System.Drawing.Point(4, 22);
            this.externalEMTabPage.Name = "externalEMTabPage";
            this.externalEMTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.externalEMTabPage.Size = new System.Drawing.Size(520, 202);
            this.externalEMTabPage.TabIndex = 5;
            this.externalEMTabPage.Text = "External EM";
            this.externalEMTabPage.UseVisualStyleBackColor = true;
            // 
            // externalEMpanel
            // 
            this.externalEMpanel.Controls.Add(this.lambdaZupOutputVoltageNumericEdit);
            this.externalEMpanel.Controls.Add(this.lambdaZupOutputVoltageLabel);
            this.externalEMpanel.Controls.Add(this.useLambdaZupCheckBox);
            this.externalEMpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.externalEMpanel.Location = new System.Drawing.Point(3, 3);
            this.externalEMpanel.Name = "externalEMpanel";
            this.externalEMpanel.Size = new System.Drawing.Size(514, 196);
            this.externalEMpanel.TabIndex = 0;
            // 
            // lambdaZupOutputVoltageNumericEdit
            // 
            this.lambdaZupOutputVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(2);
            this.lambdaZupOutputVoltageNumericEdit.Location = new System.Drawing.Point(116, 39);
            this.lambdaZupOutputVoltageNumericEdit.Name = "lambdaZupOutputVoltageNumericEdit";
            this.lambdaZupOutputVoltageNumericEdit.Range = new NationalInstruments.UI.Range(0, 12);
            this.lambdaZupOutputVoltageNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.lambdaZupOutputVoltageNumericEdit.TabIndex = 24;
            // 
            // lambdaZupOutputVoltageLabel
            // 
            this.lambdaZupOutputVoltageLabel.AutoSize = true;
            this.lambdaZupOutputVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.lambdaZupOutputVoltageLabel.Location = new System.Drawing.Point(5, 41);
            this.lambdaZupOutputVoltageLabel.Name = "lambdaZupOutputVoltageLabel";
            this.lambdaZupOutputVoltageLabel.Size = new System.Drawing.Size(94, 13);
            this.lambdaZupOutputVoltageLabel.TabIndex = 23;
            this.lambdaZupOutputVoltageLabel.Text = "Output Voltage [V]";
            // 
            // useLambdaZupCheckBox
            // 
            this.useLambdaZupCheckBox.AutoSize = true;
            this.useLambdaZupCheckBox.ForeColor = System.Drawing.Color.Black;
            this.useLambdaZupCheckBox.Location = new System.Drawing.Point(8, 8);
            this.useLambdaZupCheckBox.Name = "useLambdaZupCheckBox";
            this.useLambdaZupCheckBox.Size = new System.Drawing.Size(108, 17);
            this.useLambdaZupCheckBox.TabIndex = 22;
            this.useLambdaZupCheckBox.Text = "Use Lambda Zup";
            this.useLambdaZupCheckBox.UseVisualStyleBackColor = true;
            this.useLambdaZupCheckBox.CheckedChanged += new System.EventHandler(this.useLambdaZupCheckBox_CheckedChanged);
            // 
            // channelsConfigurationTabPage
            // 
            this.channelsConfigurationTabPage.Controls.Add(this.channelsSettingsPanel);
            this.channelsConfigurationTabPage.Location = new System.Drawing.Point(4, 22);
            this.channelsConfigurationTabPage.Name = "channelsConfigurationTabPage";
            this.channelsConfigurationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.channelsConfigurationTabPage.Size = new System.Drawing.Size(520, 202);
            this.channelsConfigurationTabPage.TabIndex = 4;
            this.channelsConfigurationTabPage.Text = "Channels";
            this.channelsConfigurationTabPage.UseVisualStyleBackColor = true;
            // 
            // channelsSettingsPanel
            // 
            this.channelsSettingsPanel.Controls.Add(this.channel1ComboBox);
            this.channelsSettingsPanel.Controls.Add(this.channel3ComboBox);
            this.channelsSettingsPanel.Controls.Add(this.channel0CheckBox);
            this.channelsSettingsPanel.Controls.Add(this.channel3CheckBox);
            this.channelsSettingsPanel.Controls.Add(this.channel0ComboBox);
            this.channelsSettingsPanel.Controls.Add(this.channel2ComboBox);
            this.channelsSettingsPanel.Controls.Add(this.channel1CheckBox);
            this.channelsSettingsPanel.Controls.Add(this.channel2CheckBox);
            this.channelsSettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelsSettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.channelsSettingsPanel.Name = "channelsSettingsPanel";
            this.channelsSettingsPanel.Size = new System.Drawing.Size(514, 196);
            this.channelsSettingsPanel.TabIndex = 8;
            // 
            // channel1ComboBox
            // 
            this.channel1ComboBox.FormattingEnabled = true;
            this.channel1ComboBox.Location = new System.Drawing.Point(107, 33);
            this.channel1ComboBox.Name = "channel1ComboBox";
            this.channel1ComboBox.Size = new System.Drawing.Size(202, 21);
            this.channel1ComboBox.TabIndex = 3;
            this.channel1ComboBox.SelectedValueChanged += new System.EventHandler(this.channel1ComboBox_SelectedValueChanged);
            // 
            // channel3ComboBox
            // 
            this.channel3ComboBox.FormattingEnabled = true;
            this.channel3ComboBox.Location = new System.Drawing.Point(107, 87);
            this.channel3ComboBox.Name = "channel3ComboBox";
            this.channel3ComboBox.Size = new System.Drawing.Size(202, 21);
            this.channel3ComboBox.TabIndex = 7;
            this.channel3ComboBox.SelectedValueChanged += new System.EventHandler(this.channel3ComboBox_SelectedValueChanged);
            // 
            // channel0CheckBox
            // 
            this.channel0CheckBox.AutoSize = true;
            this.channel0CheckBox.Checked = true;
            this.channel0CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.channel0CheckBox.ForeColor = System.Drawing.Color.Black;
            this.channel0CheckBox.Location = new System.Drawing.Point(7, 8);
            this.channel0CheckBox.Name = "channel0CheckBox";
            this.channel0CheckBox.Size = new System.Drawing.Size(71, 17);
            this.channel0CheckBox.TabIndex = 0;
            this.channel0CheckBox.Text = "Dev1/ai0";
            this.channel0CheckBox.UseVisualStyleBackColor = true;
            this.channel0CheckBox.CheckedChanged += new System.EventHandler(this.channel0CheckBox_CheckedChanged);
            // 
            // channel3CheckBox
            // 
            this.channel3CheckBox.AutoSize = true;
            this.channel3CheckBox.ForeColor = System.Drawing.Color.Black;
            this.channel3CheckBox.Location = new System.Drawing.Point(7, 89);
            this.channel3CheckBox.Name = "channel3CheckBox";
            this.channel3CheckBox.Size = new System.Drawing.Size(71, 17);
            this.channel3CheckBox.TabIndex = 6;
            this.channel3CheckBox.Text = "Dev1/ai3";
            this.channel3CheckBox.UseVisualStyleBackColor = true;
            this.channel3CheckBox.CheckedChanged += new System.EventHandler(this.channel3CheckBox_CheckedChanged);
            // 
            // channel0ComboBox
            // 
            this.channel0ComboBox.FormattingEnabled = true;
            this.channel0ComboBox.Location = new System.Drawing.Point(107, 6);
            this.channel0ComboBox.Name = "channel0ComboBox";
            this.channel0ComboBox.Size = new System.Drawing.Size(202, 21);
            this.channel0ComboBox.TabIndex = 1;
            this.channel0ComboBox.SelectedValueChanged += new System.EventHandler(this.channel0ComboBox_SelectedValueChanged);
            // 
            // channel2ComboBox
            // 
            this.channel2ComboBox.FormattingEnabled = true;
            this.channel2ComboBox.Location = new System.Drawing.Point(107, 60);
            this.channel2ComboBox.Name = "channel2ComboBox";
            this.channel2ComboBox.Size = new System.Drawing.Size(202, 21);
            this.channel2ComboBox.TabIndex = 5;
            this.channel2ComboBox.SelectedValueChanged += new System.EventHandler(this.channel2ComboBox_SelectedValueChanged);
            // 
            // channel1CheckBox
            // 
            this.channel1CheckBox.AutoSize = true;
            this.channel1CheckBox.ForeColor = System.Drawing.Color.Black;
            this.channel1CheckBox.Location = new System.Drawing.Point(7, 35);
            this.channel1CheckBox.Name = "channel1CheckBox";
            this.channel1CheckBox.Size = new System.Drawing.Size(71, 17);
            this.channel1CheckBox.TabIndex = 2;
            this.channel1CheckBox.Text = "Dev1/ai1";
            this.channel1CheckBox.UseVisualStyleBackColor = true;
            this.channel1CheckBox.CheckedChanged += new System.EventHandler(this.channel1CheckBox_CheckedChanged);
            // 
            // channel2CheckBox
            // 
            this.channel2CheckBox.AutoSize = true;
            this.channel2CheckBox.ForeColor = System.Drawing.Color.Black;
            this.channel2CheckBox.Location = new System.Drawing.Point(7, 62);
            this.channel2CheckBox.Name = "channel2CheckBox";
            this.channel2CheckBox.Size = new System.Drawing.Size(71, 17);
            this.channel2CheckBox.TabIndex = 4;
            this.channel2CheckBox.Text = "Dev1/ai2";
            this.channel2CheckBox.UseVisualStyleBackColor = true;
            this.channel2CheckBox.CheckedChanged += new System.EventHandler(this.channel2CheckBox_CheckedChanged);
            // 
            // ivAcquisition
            // 
            this.ivAcquisition.Controls.Add(this.ivOperateGroupBox);
            this.ivAcquisition.Controls.Add(this.ivPlotGroupBox);
            this.ivAcquisition.Controls.Add(this.ivWaveformGraph);
            this.ivAcquisition.Controls.Add(this.ivSettingsGroupBox);
            this.ivAcquisition.Location = new System.Drawing.Point(4, 22);
            this.ivAcquisition.Name = "ivAcquisition";
            this.ivAcquisition.Padding = new System.Windows.Forms.Padding(3);
            this.ivAcquisition.Size = new System.Drawing.Size(779, 799);
            this.ivAcquisition.TabIndex = 2;
            this.ivAcquisition.Text = "IV Acquisition";
            this.ivAcquisition.UseVisualStyleBackColor = true;
            // 
            // ivOperateGroupBox
            // 
            this.ivOperateGroupBox.AutoSize = true;
            this.ivOperateGroupBox.Controls.Add(this.ivOpenFolderButton);
            this.ivOperateGroupBox.Controls.Add(this.ivStepperUpCheckBox);
            this.ivOperateGroupBox.Controls.Add(this.ivStartStopCheckBox);
            this.ivOperateGroupBox.Controls.Add(this.ivShortCircuitCheckBox);
            this.ivOperateGroupBox.Controls.Add(this.ivNumberOfCyclesNumericUpDown);
            this.ivOperateGroupBox.Controls.Add(this.ivNumberOfCyclesLlabel);
            this.ivOperateGroupBox.Controls.Add(this.ivShortCircuitVoltageNumericUpDown);
            this.ivOperateGroupBox.Controls.Add(this.ivShortCircuitVoltageLabel);
            this.ivOperateGroupBox.Controls.Add(this.ivPathTextBox);
            this.ivOperateGroupBox.Controls.Add(this.ivBrowseButton);
            this.ivOperateGroupBox.Controls.Add(this.ivFileSavingCheckBox);
            this.ivOperateGroupBox.Controls.Add(this.ivFileNumberLabel);
            this.ivOperateGroupBox.Controls.Add(this.ivPathLabel);
            this.ivOperateGroupBox.Controls.Add(this.ivFileNumberNumericUpDown);
            this.ivOperateGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ivOperateGroupBox.ForeColor = System.Drawing.Color.Red;
            this.ivOperateGroupBox.Location = new System.Drawing.Point(3, 612);
            this.ivOperateGroupBox.MinimumSize = new System.Drawing.Size(478, 176);
            this.ivOperateGroupBox.Name = "ivOperateGroupBox";
            this.ivOperateGroupBox.Size = new System.Drawing.Size(773, 184);
            this.ivOperateGroupBox.TabIndex = 27;
            this.ivOperateGroupBox.TabStop = false;
            this.ivOperateGroupBox.Text = "Operate";
            // 
            // ivOpenFolderButton
            // 
            this.ivOpenFolderButton.ForeColor = System.Drawing.Color.Black;
            this.ivOpenFolderButton.Location = new System.Drawing.Point(530, 44);
            this.ivOpenFolderButton.Name = "ivOpenFolderButton";
            this.ivOpenFolderButton.Size = new System.Drawing.Size(74, 23);
            this.ivOpenFolderButton.TabIndex = 29;
            this.ivOpenFolderButton.Text = "Open Folder";
            this.ivOpenFolderButton.UseVisualStyleBackColor = true;
            this.ivOpenFolderButton.Click += new System.EventHandler(this.ivOpenFolderButton_Click);
            // 
            // ivStepperUpCheckBox
            // 
            this.ivStepperUpCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ivStepperUpCheckBox.AutoSize = true;
            this.ivStepperUpCheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivStepperUpCheckBox.Location = new System.Drawing.Point(365, 142);
            this.ivStepperUpCheckBox.MinimumSize = new System.Drawing.Size(74, 23);
            this.ivStepperUpCheckBox.Name = "ivStepperUpCheckBox";
            this.ivStepperUpCheckBox.Size = new System.Drawing.Size(74, 23);
            this.ivStepperUpCheckBox.TabIndex = 17;
            this.ivStepperUpCheckBox.Text = "Stepper Up";
            this.ivStepperUpCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ivStepperUpCheckBox.UseVisualStyleBackColor = true;
            this.ivStepperUpCheckBox.CheckedChanged += new System.EventHandler(this.ivStepperUpCheckBox_CheckedChanged);
            // 
            // ivStartStopCheckBox
            // 
            this.ivStartStopCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ivStartStopCheckBox.AutoSize = true;
            this.ivStartStopCheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivStartStopCheckBox.Location = new System.Drawing.Point(273, 108);
            this.ivStartStopCheckBox.MinimumSize = new System.Drawing.Size(74, 23);
            this.ivStartStopCheckBox.Name = "ivStartStopCheckBox";
            this.ivStartStopCheckBox.Size = new System.Drawing.Size(74, 23);
            this.ivStartStopCheckBox.TabIndex = 16;
            this.ivStartStopCheckBox.Text = "Start";
            this.ivStartStopCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ivStartStopCheckBox.UseVisualStyleBackColor = true;
            this.ivStartStopCheckBox.CheckedChanged += new System.EventHandler(this.ivStartStopCheckBox_CheckedChanged);
            // 
            // ivShortCircuitCheckBox
            // 
            this.ivShortCircuitCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.ivShortCircuitCheckBox.AutoSize = true;
            this.ivShortCircuitCheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivShortCircuitCheckBox.Location = new System.Drawing.Point(273, 142);
            this.ivShortCircuitCheckBox.MinimumSize = new System.Drawing.Size(74, 23);
            this.ivShortCircuitCheckBox.Name = "ivShortCircuitCheckBox";
            this.ivShortCircuitCheckBox.Size = new System.Drawing.Size(74, 23);
            this.ivShortCircuitCheckBox.TabIndex = 15;
            this.ivShortCircuitCheckBox.Text = "Short Circuit";
            this.ivShortCircuitCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ivShortCircuitCheckBox.UseVisualStyleBackColor = true;
            this.ivShortCircuitCheckBox.CheckedChanged += new System.EventHandler(this.ivShortCircuitCheckBox_CheckedChanged);
            // 
            // ivNumberOfCyclesNumericUpDown
            // 
            this.ivNumberOfCyclesNumericUpDown.Location = new System.Drawing.Point(150, 111);
            this.ivNumberOfCyclesNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ivNumberOfCyclesNumericUpDown.Name = "ivNumberOfCyclesNumericUpDown";
            this.ivNumberOfCyclesNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.ivNumberOfCyclesNumericUpDown.TabIndex = 14;
            this.ivNumberOfCyclesNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // ivNumberOfCyclesLlabel
            // 
            this.ivNumberOfCyclesLlabel.AutoSize = true;
            this.ivNumberOfCyclesLlabel.ForeColor = System.Drawing.Color.Black;
            this.ivNumberOfCyclesLlabel.Location = new System.Drawing.Point(7, 113);
            this.ivNumberOfCyclesLlabel.Name = "ivNumberOfCyclesLlabel";
            this.ivNumberOfCyclesLlabel.Size = new System.Drawing.Size(90, 13);
            this.ivNumberOfCyclesLlabel.TabIndex = 13;
            this.ivNumberOfCyclesLlabel.Text = "Number of Cycles";
            // 
            // ivShortCircuitVoltageNumericUpDown
            // 
            this.ivShortCircuitVoltageNumericUpDown.DecimalPlaces = 1;
            this.ivShortCircuitVoltageNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ivShortCircuitVoltageNumericUpDown.Location = new System.Drawing.Point(150, 145);
            this.ivShortCircuitVoltageNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ivShortCircuitVoltageNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.ivShortCircuitVoltageNumericUpDown.Name = "ivShortCircuitVoltageNumericUpDown";
            this.ivShortCircuitVoltageNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.ivShortCircuitVoltageNumericUpDown.TabIndex = 12;
            this.ivShortCircuitVoltageNumericUpDown.Value = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            // 
            // ivShortCircuitVoltageLabel
            // 
            this.ivShortCircuitVoltageLabel.AutoSize = true;
            this.ivShortCircuitVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.ivShortCircuitVoltageLabel.Location = new System.Drawing.Point(7, 147);
            this.ivShortCircuitVoltageLabel.Name = "ivShortCircuitVoltageLabel";
            this.ivShortCircuitVoltageLabel.Size = new System.Drawing.Size(119, 13);
            this.ivShortCircuitVoltageLabel.TabIndex = 11;
            this.ivShortCircuitVoltageLabel.Text = "Short Circuit Voltage [V]";
            // 
            // ivPathTextBox
            // 
            this.ivPathTextBox.Location = new System.Drawing.Point(39, 46);
            this.ivPathTextBox.Name = "ivPathTextBox";
            this.ivPathTextBox.Size = new System.Drawing.Size(400, 20);
            this.ivPathTextBox.TabIndex = 9;
            this.ivPathTextBox.Text = "C:\\sbj\\IV_Measurements";
            this.ivPathTextBox.TextChanged += new System.EventHandler(this.ivPathTextBox_TextChanged);
            // 
            // ivBrowseButton
            // 
            this.ivBrowseButton.ForeColor = System.Drawing.Color.Black;
            this.ivBrowseButton.Location = new System.Drawing.Point(450, 44);
            this.ivBrowseButton.Name = "ivBrowseButton";
            this.ivBrowseButton.Size = new System.Drawing.Size(74, 23);
            this.ivBrowseButton.TabIndex = 10;
            this.ivBrowseButton.Text = "Browse";
            this.ivBrowseButton.UseVisualStyleBackColor = true;
            this.ivBrowseButton.Click += new System.EventHandler(this.ivBrowseButton_Click);
            // 
            // ivFileSavingCheckBox
            // 
            this.ivFileSavingCheckBox.AutoSize = true;
            this.ivFileSavingCheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivFileSavingCheckBox.Location = new System.Drawing.Point(10, 23);
            this.ivFileSavingCheckBox.Name = "ivFileSavingCheckBox";
            this.ivFileSavingCheckBox.Size = new System.Drawing.Size(78, 17);
            this.ivFileSavingCheckBox.TabIndex = 5;
            this.ivFileSavingCheckBox.Text = "File Saving";
            this.ivFileSavingCheckBox.UseVisualStyleBackColor = true;
            // 
            // ivFileNumberLabel
            // 
            this.ivFileNumberLabel.AutoSize = true;
            this.ivFileNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.ivFileNumberLabel.Location = new System.Drawing.Point(7, 79);
            this.ivFileNumberLabel.Name = "ivFileNumberLabel";
            this.ivFileNumberLabel.Size = new System.Drawing.Size(63, 13);
            this.ivFileNumberLabel.TabIndex = 6;
            this.ivFileNumberLabel.Text = "File Number";
            // 
            // ivPathLabel
            // 
            this.ivPathLabel.AutoSize = true;
            this.ivPathLabel.ForeColor = System.Drawing.Color.Black;
            this.ivPathLabel.Location = new System.Drawing.Point(7, 49);
            this.ivPathLabel.Name = "ivPathLabel";
            this.ivPathLabel.Size = new System.Drawing.Size(29, 13);
            this.ivPathLabel.TabIndex = 8;
            this.ivPathLabel.Text = "Path";
            // 
            // ivFileNumberNumericUpDown
            // 
            this.ivFileNumberNumericUpDown.Location = new System.Drawing.Point(150, 77);
            this.ivFileNumberNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ivFileNumberNumericUpDown.Name = "ivFileNumberNumericUpDown";
            this.ivFileNumberNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.ivFileNumberNumericUpDown.TabIndex = 7;
            // 
            // ivPlotGroupBox
            // 
            this.ivPlotGroupBox.Controls.Add(this.ivChannelsListView);
            this.ivPlotGroupBox.ForeColor = System.Drawing.Color.Red;
            this.ivPlotGroupBox.Location = new System.Drawing.Point(493, 13);
            this.ivPlotGroupBox.Name = "ivPlotGroupBox";
            this.ivPlotGroupBox.Size = new System.Drawing.Size(235, 339);
            this.ivPlotGroupBox.TabIndex = 26;
            this.ivPlotGroupBox.TabStop = false;
            this.ivPlotGroupBox.Text = "Plots";
            // 
            // ivChannelsListView
            // 
            this.ivChannelsListView.CheckBoxes = true;
            this.ivChannelsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivChannelsListView.Location = new System.Drawing.Point(3, 16);
            this.ivChannelsListView.Name = "ivChannelsListView";
            this.ivChannelsListView.Size = new System.Drawing.Size(229, 320);
            this.ivChannelsListView.TabIndex = 25;
            this.ivChannelsListView.UseCompatibleStateImageBehavior = false;
            this.ivChannelsListView.View = System.Windows.Forms.View.List;
            // 
            // ivWaveformGraph
            // 
            this.ivWaveformGraph.CaptionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ivWaveformGraph.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ivWaveformGraph.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.ivWaveformGraph.InteractionMode = ((NationalInstruments.UI.GraphInteractionModes)((((((((NationalInstruments.UI.GraphInteractionModes.ZoomX | NationalInstruments.UI.GraphInteractionModes.ZoomY)
                        | NationalInstruments.UI.GraphInteractionModes.ZoomAroundPoint)
                        | NationalInstruments.UI.GraphInteractionModes.PanX)
                        | NationalInstruments.UI.GraphInteractionModes.PanY)
                        | NationalInstruments.UI.GraphInteractionModes.DragCursor)
                        | NationalInstruments.UI.GraphInteractionModes.DragAnnotationCaption)
                        | NationalInstruments.UI.GraphInteractionModes.EditRange)));
            this.ivWaveformGraph.Location = new System.Drawing.Point(13, 16);
            this.ivWaveformGraph.Name = "ivWaveformGraph";
            this.ivWaveformGraph.PlotAreaColor = System.Drawing.Color.LightGray;
            this.ivWaveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.ivWaveformPlot});
            this.ivWaveformGraph.Size = new System.Drawing.Size(467, 339);
            this.ivWaveformGraph.TabIndex = 23;
            this.ivWaveformGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.ivXAxis});
            this.ivWaveformGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.ivYAxis});
            // 
            // ivWaveformPlot
            // 
            this.ivWaveformPlot.LineColor = System.Drawing.Color.Red;          
            this.ivWaveformPlot.ToolTipsEnabled = true;
            this.ivWaveformPlot.XAxis = this.ivXAxis;
            this.ivWaveformPlot.YAxis = this.ivYAxis;
            // 
            // ivXAxis
            // 
            this.ivXAxis.Caption = "IV Cycles";
            this.ivXAxis.Range = new NationalInstruments.UI.Range(0, 10000);
            // 
            // ivYAxis
            // 
            this.ivYAxis.Caption = "Conductance [G0]";
            // 
            // ivSettingsGroupBox
            // 
            this.ivSettingsGroupBox.AutoSize = true;
            this.ivSettingsGroupBox.Controls.Add(this.ivGeneralTabControl);
            this.ivSettingsGroupBox.ForeColor = System.Drawing.Color.Red;
            this.ivSettingsGroupBox.Location = new System.Drawing.Point(3, 369);
            this.ivSettingsGroupBox.MinimumSize = new System.Drawing.Size(521, 200);
            this.ivSettingsGroupBox.Name = "ivSettingsGroupBox";
            this.ivSettingsGroupBox.Size = new System.Drawing.Size(735, 236);
            this.ivSettingsGroupBox.TabIndex = 24;
            this.ivSettingsGroupBox.TabStop = false;
            this.ivSettingsGroupBox.Text = "Settings";
            // 
            // ivGeneralTabControl
            // 
            this.ivGeneralTabControl.Controls.Add(this.ivGeneralSettingsTabPage);
            this.ivGeneralTabControl.Controls.Add(this.ivSteppingMethodTabPage);
            this.ivGeneralTabControl.Controls.Add(this.ivChannelsTabPage);
            this.ivGeneralTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivGeneralTabControl.Location = new System.Drawing.Point(3, 16);
            this.ivGeneralTabControl.Name = "ivGeneralTabControl";
            this.ivGeneralTabControl.SelectedIndex = 0;
            this.ivGeneralTabControl.Size = new System.Drawing.Size(729, 217);
            this.ivGeneralTabControl.TabIndex = 17;
            // 
            // ivGeneralSettingsTabPage
            // 
            this.ivGeneralSettingsTabPage.Controls.Add(this.ivGeneralSettingsPanel);
            this.ivGeneralSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ivGeneralSettingsTabPage.Name = "ivGeneralSettingsTabPage";
            this.ivGeneralSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ivGeneralSettingsTabPage.Size = new System.Drawing.Size(721, 191);
            this.ivGeneralSettingsTabPage.TabIndex = 0;
            this.ivGeneralSettingsTabPage.Text = "General";
            this.ivGeneralSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // ivGeneralSettingsPanel
            // 
            this.ivGeneralSettingsPanel.Controls.Add(this.ivVoltageForTraceNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivVoltageForTraceLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivTimeOfOneIVCycleNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivTimeOfIVCycleLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivOutputUpdateDelayLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivOutputUpdateRateNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivSamplesPerCycleLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivVoltageAmplitudeLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivOutputUpdateRateLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivVoltageAmplitudeNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivOutputUpdateDelayNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivSamplesPerCycleNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivGainPoweComboBox);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivInputSampleRateNumericUpDown);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivTriggerVoltageNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivTriggerVoltageLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivTriggerConductanceNumericEdit);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivTriggerConductanceLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivInputSampleRateLabel);
            this.ivGeneralSettingsPanel.Controls.Add(this.ivGainPowerLabel);
            this.ivGeneralSettingsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ivGeneralSettingsPanel.Location = new System.Drawing.Point(3, -8);
            this.ivGeneralSettingsPanel.MinimumSize = new System.Drawing.Size(0, 151);
            this.ivGeneralSettingsPanel.Name = "ivGeneralSettingsPanel";
            this.ivGeneralSettingsPanel.Size = new System.Drawing.Size(715, 196);
            this.ivGeneralSettingsPanel.TabIndex = 16;
            // 
            // ivVoltageForTraceNumericEdit
            // 
            this.ivVoltageForTraceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(3);
            this.ivVoltageForTraceNumericEdit.Location = new System.Drawing.Point(194, 145);
            this.ivVoltageForTraceNumericEdit.Name = "ivVoltageForTraceNumericEdit";
            this.ivVoltageForTraceNumericEdit.Size = new System.Drawing.Size(78, 20);
            this.ivVoltageForTraceNumericEdit.TabIndex = 69;
            this.ivVoltageForTraceNumericEdit.Value = 0.1;
            // 
            // ivVoltageForTraceLabel
            // 
            this.ivVoltageForTraceLabel.AutoSize = true;
            this.ivVoltageForTraceLabel.ForeColor = System.Drawing.Color.Black;
            this.ivVoltageForTraceLabel.Location = new System.Drawing.Point(15, 148);
            this.ivVoltageForTraceLabel.Name = "ivVoltageForTraceLabel";
            this.ivVoltageForTraceLabel.Size = new System.Drawing.Size(173, 13);
            this.ivVoltageForTraceLabel.TabIndex = 68;
            this.ivVoltageForTraceLabel.Text = "Voltage for the  displayed Trace [V]";
            // 
            // ivTimeOfOneIVCycleNumericEdit
            // 
            this.ivTimeOfOneIVCycleNumericEdit.BackColor = System.Drawing.SystemColors.Control;
            this.ivTimeOfOneIVCycleNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0);
            this.ivTimeOfOneIVCycleNumericEdit.InteractionMode = NationalInstruments.UI.NumericEditInteractionModes.Indicator;
            this.ivTimeOfOneIVCycleNumericEdit.Location = new System.Drawing.Point(455, 145);
            this.ivTimeOfOneIVCycleNumericEdit.Name = "ivTimeOfOneIVCycleNumericEdit";
            this.ivTimeOfOneIVCycleNumericEdit.Size = new System.Drawing.Size(78, 20);
            this.ivTimeOfOneIVCycleNumericEdit.TabIndex = 67;
            this.ivTimeOfOneIVCycleNumericEdit.Value = 50;
            // 
            // ivTimeOfIVCycleLabel
            // 
            this.ivTimeOfIVCycleLabel.AutoSize = true;
            this.ivTimeOfIVCycleLabel.ForeColor = System.Drawing.Color.Black;
            this.ivTimeOfIVCycleLabel.Location = new System.Drawing.Point(312, 147);
            this.ivTimeOfIVCycleLabel.Name = "ivTimeOfIVCycleLabel";
            this.ivTimeOfIVCycleLabel.Size = new System.Drawing.Size(129, 13);
            this.ivTimeOfIVCycleLabel.TabIndex = 66;
            this.ivTimeOfIVCycleLabel.Text = "Time of One IV Cycle [ms]";
            // 
            // ivOutputUpdateDelayLabel
            // 
            this.ivOutputUpdateDelayLabel.AutoSize = true;
            this.ivOutputUpdateDelayLabel.ForeColor = System.Drawing.Color.Black;
            this.ivOutputUpdateDelayLabel.Location = new System.Drawing.Point(312, 89);
            this.ivOutputUpdateDelayLabel.Name = "ivOutputUpdateDelayLabel";
            this.ivOutputUpdateDelayLabel.Size = new System.Drawing.Size(129, 13);
            this.ivOutputUpdateDelayLabel.TabIndex = 65;
            this.ivOutputUpdateDelayLabel.Text = "Output Update Delay [ms]";
            // 
            // ivOutputUpdateRateNumericEdit
            // 
            this.ivOutputUpdateRateNumericEdit.BackColor = System.Drawing.SystemColors.Control;
            this.ivOutputUpdateRateNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0);
            this.ivOutputUpdateRateNumericEdit.InteractionMode = NationalInstruments.UI.NumericEditInteractionModes.Indicator;
            this.ivOutputUpdateRateNumericEdit.Location = new System.Drawing.Point(455, 114);
            this.ivOutputUpdateRateNumericEdit.Name = "ivOutputUpdateRateNumericEdit";
            this.ivOutputUpdateRateNumericEdit.Size = new System.Drawing.Size(78, 20);
            this.ivOutputUpdateRateNumericEdit.TabIndex = 64;
            this.ivOutputUpdateRateNumericEdit.Value = 10000;
            // 
            // ivSamplesPerCycleLabel
            // 
            this.ivSamplesPerCycleLabel.AutoSize = true;
            this.ivSamplesPerCycleLabel.ForeColor = System.Drawing.Color.Black;
            this.ivSamplesPerCycleLabel.Location = new System.Drawing.Point(312, 61);
            this.ivSamplesPerCycleLabel.Name = "ivSamplesPerCycleLabel";
            this.ivSamplesPerCycleLabel.Size = new System.Drawing.Size(95, 13);
            this.ivSamplesPerCycleLabel.TabIndex = 63;
            this.ivSamplesPerCycleLabel.Text = "Samples Per Cycle";
            // 
            // ivVoltageAmplitudeLabel
            // 
            this.ivVoltageAmplitudeLabel.AutoSize = true;
            this.ivVoltageAmplitudeLabel.ForeColor = System.Drawing.Color.Black;
            this.ivVoltageAmplitudeLabel.Location = new System.Drawing.Point(312, 33);
            this.ivVoltageAmplitudeLabel.Name = "ivVoltageAmplitudeLabel";
            this.ivVoltageAmplitudeLabel.Size = new System.Drawing.Size(108, 13);
            this.ivVoltageAmplitudeLabel.TabIndex = 62;
            this.ivVoltageAmplitudeLabel.Text = "Voltage Amplitude [V]";
            // 
            // ivOutputUpdateRateLabel
            // 
            this.ivOutputUpdateRateLabel.AutoSize = true;
            this.ivOutputUpdateRateLabel.ForeColor = System.Drawing.Color.Black;
            this.ivOutputUpdateRateLabel.Location = new System.Drawing.Point(312, 117);
            this.ivOutputUpdateRateLabel.Name = "ivOutputUpdateRateLabel";
            this.ivOutputUpdateRateLabel.Size = new System.Drawing.Size(125, 13);
            this.ivOutputUpdateRateLabel.TabIndex = 61;
            this.ivOutputUpdateRateLabel.Text = "Output Update Rate [Hz]";
            // 
            // ivVoltageAmplitudeNumericEdit
            // 
            this.ivVoltageAmplitudeNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(3);
            this.ivVoltageAmplitudeNumericEdit.Location = new System.Drawing.Point(455, 29);
            this.ivVoltageAmplitudeNumericEdit.Name = "ivVoltageAmplitudeNumericEdit";
            this.ivVoltageAmplitudeNumericEdit.Size = new System.Drawing.Size(78, 20);
            this.ivVoltageAmplitudeNumericEdit.TabIndex = 58;
            this.ivVoltageAmplitudeNumericEdit.Value = 0.2;
            this.ivVoltageAmplitudeNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.ivVoltageAmplitudeNumericEdit_AfterChangeValue);
            // 
            // ivOutputUpdateDelayNumericEdit
            // 
            this.ivOutputUpdateDelayNumericEdit.Location = new System.Drawing.Point(455, 85);
            this.ivOutputUpdateDelayNumericEdit.Name = "ivOutputUpdateDelayNumericEdit";
            this.ivOutputUpdateDelayNumericEdit.Size = new System.Drawing.Size(78, 20);
            this.ivOutputUpdateDelayNumericEdit.TabIndex = 60;
            this.ivOutputUpdateDelayNumericEdit.Value = 0.1;
            this.ivOutputUpdateDelayNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.ivOutputUpdateDelayNumericEdit_AfterChangeValue);
            // 
            // ivSamplesPerCycleNumericEdit
            // 
            this.ivSamplesPerCycleNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(0);
            this.ivSamplesPerCycleNumericEdit.Location = new System.Drawing.Point(455, 56);
            this.ivSamplesPerCycleNumericEdit.Name = "ivSamplesPerCycleNumericEdit";
            this.ivSamplesPerCycleNumericEdit.Size = new System.Drawing.Size(78, 20);
            this.ivSamplesPerCycleNumericEdit.TabIndex = 21;
            this.ivSamplesPerCycleNumericEdit.Value = 200;
            this.ivSamplesPerCycleNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.ivSamplesPerCycleNumericEdit_AfterChangeValue);
            // 
            // ivGainPoweComboBox
            // 
            this.ivGainPoweComboBox.DisplayMember = "3";
            this.ivGainPoweComboBox.FormattingEnabled = true;
            this.ivGainPoweComboBox.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.ivGainPoweComboBox.Location = new System.Drawing.Point(195, 55);
            this.ivGainPoweComboBox.Name = "ivGainPoweComboBox";
            this.ivGainPoweComboBox.Size = new System.Drawing.Size(75, 21);
            this.ivGainPoweComboBox.TabIndex = 18;
            this.ivGainPoweComboBox.Text = "5";
            this.ivGainPoweComboBox.SelectedIndexChanged += new System.EventHandler(this.ivGainPoweComboBox_SelectedIndexChanged);
            // 
            // ivInputSampleRateNumericUpDown
            // 
            this.ivInputSampleRateNumericUpDown.Location = new System.Drawing.Point(195, 29);
            this.ivInputSampleRateNumericUpDown.Maximum = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            this.ivInputSampleRateNumericUpDown.Name = "ivInputSampleRateNumericUpDown";
            this.ivInputSampleRateNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.ivInputSampleRateNumericUpDown.TabIndex = 7;
            this.ivInputSampleRateNumericUpDown.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // ivTriggerVoltageNumericEdit
            // 
            this.ivTriggerVoltageNumericEdit.Enabled = false;
            this.ivTriggerVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.ivTriggerVoltageNumericEdit.Location = new System.Drawing.Point(195, 85);
            this.ivTriggerVoltageNumericEdit.Name = "ivTriggerVoltageNumericEdit";
            this.ivTriggerVoltageNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.ivTriggerVoltageNumericEdit.TabIndex = 11;
            this.ivTriggerVoltageNumericEdit.Value = -0.01;
            // 
            // ivTriggerVoltageLabel
            // 
            this.ivTriggerVoltageLabel.AutoSize = true;
            this.ivTriggerVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.ivTriggerVoltageLabel.Location = new System.Drawing.Point(13, 88);
            this.ivTriggerVoltageLabel.Name = "ivTriggerVoltageLabel";
            this.ivTriggerVoltageLabel.Size = new System.Drawing.Size(95, 13);
            this.ivTriggerVoltageLabel.TabIndex = 10;
            this.ivTriggerVoltageLabel.Text = "Trigger Voltage [V]";
            // 
            // ivTriggerConductanceNumericEdit
            // 
            this.ivTriggerConductanceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.ivTriggerConductanceNumericEdit.Location = new System.Drawing.Point(195, 114);
            this.ivTriggerConductanceNumericEdit.Name = "ivTriggerConductanceNumericEdit";
            this.ivTriggerConductanceNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.ivTriggerConductanceNumericEdit.TabIndex = 9;
            this.ivTriggerConductanceNumericEdit.Value = 0.0129;
            this.ivTriggerConductanceNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.ivTriggerConductanceNumericEdit_AfterChangeValue);
            // 
            // ivTriggerConductanceLabel
            // 
            this.ivTriggerConductanceLabel.AutoSize = true;
            this.ivTriggerConductanceLabel.ForeColor = System.Drawing.Color.Black;
            this.ivTriggerConductanceLabel.Location = new System.Drawing.Point(13, 118);
            this.ivTriggerConductanceLabel.Name = "ivTriggerConductanceLabel";
            this.ivTriggerConductanceLabel.Size = new System.Drawing.Size(130, 13);
            this.ivTriggerConductanceLabel.TabIndex = 8;
            this.ivTriggerConductanceLabel.Text = "Trigger Conductance [G0]";
            // 
            // ivInputSampleRateLabel
            // 
            this.ivInputSampleRateLabel.AutoSize = true;
            this.ivInputSampleRateLabel.ForeColor = System.Drawing.Color.Black;
            this.ivInputSampleRateLabel.Location = new System.Drawing.Point(13, 30);
            this.ivInputSampleRateLabel.Name = "ivInputSampleRateLabel";
            this.ivInputSampleRateLabel.Size = new System.Drawing.Size(117, 13);
            this.ivInputSampleRateLabel.TabIndex = 6;
            this.ivInputSampleRateLabel.Text = "Input Sample Rate [Hz]";
            // 
            // ivGainPowerLabel
            // 
            this.ivGainPowerLabel.AutoSize = true;
            this.ivGainPowerLabel.ForeColor = System.Drawing.Color.Black;
            this.ivGainPowerLabel.Location = new System.Drawing.Point(13, 58);
            this.ivGainPowerLabel.Name = "ivGainPowerLabel";
            this.ivGainPowerLabel.Size = new System.Drawing.Size(62, 13);
            this.ivGainPowerLabel.TabIndex = 2;
            this.ivGainPowerLabel.Text = "Gain Power";
            // 
            // ivSteppingMethodTabPage
            // 
            this.ivSteppingMethodTabPage.Controls.Add(this.ivSteppingMethodPanel);
            this.ivSteppingMethodTabPage.Location = new System.Drawing.Point(4, 22);
            this.ivSteppingMethodTabPage.Name = "ivSteppingMethodTabPage";
            this.ivSteppingMethodTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ivSteppingMethodTabPage.Size = new System.Drawing.Size(721, 191);
            this.ivSteppingMethodTabPage.TabIndex = 3;
            this.ivSteppingMethodTabPage.Text = "Stepping Method";
            this.ivSteppingMethodTabPage.UseVisualStyleBackColor = true;
            // 
            // ivSteppingMethodPanel
            // 
            this.ivSteppingMethodPanel.Controls.Add(this.ivElectroMagnetRadioButton);
            this.ivSteppingMethodPanel.Controls.Add(this.ivElectroMagnetGroupBox);
            this.ivSteppingMethodPanel.Controls.Add(this.ivStepperMotorRadioButton);
            this.ivSteppingMethodPanel.Controls.Add(this.ivStepperMotorGroupBox);
            this.ivSteppingMethodPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivSteppingMethodPanel.Location = new System.Drawing.Point(3, 3);
            this.ivSteppingMethodPanel.Name = "ivSteppingMethodPanel";
            this.ivSteppingMethodPanel.Size = new System.Drawing.Size(715, 185);
            this.ivSteppingMethodPanel.TabIndex = 3;
            // 
            // ivElectroMagnetRadioButton
            // 
            this.ivElectroMagnetRadioButton.AutoSize = true;
            this.ivElectroMagnetRadioButton.ForeColor = System.Drawing.Color.Black;
            this.ivElectroMagnetRadioButton.Location = new System.Drawing.Point(263, 16);
            this.ivElectroMagnetRadioButton.Name = "ivElectroMagnetRadioButton";
            this.ivElectroMagnetRadioButton.Size = new System.Drawing.Size(94, 17);
            this.ivElectroMagnetRadioButton.TabIndex = 24;
            this.ivElectroMagnetRadioButton.Text = "ElectroMagnet";
            this.ivElectroMagnetRadioButton.UseVisualStyleBackColor = true;
            this.ivElectroMagnetRadioButton.CheckedChanged += new System.EventHandler(this.ivElectroMagnetRadioButton_CheckedChanged);
            // 
            // ivElectroMagnetGroupBox
            // 
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMSkipStepperMotorCheckBox);
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMShortCircuitDelayTimeLabel);
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMShortCircuitDelayTimeNumericUpDown);
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMSlowDelayTimeNumericUpDown);
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMSlowDelayTimeLabel);
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMFastDelayTimeNumericUpDown);
            this.ivElectroMagnetGroupBox.Controls.Add(this.ivEMFastDelayTimeLabel);
            this.ivElectroMagnetGroupBox.Enabled = false;
            this.ivElectroMagnetGroupBox.Location = new System.Drawing.Point(263, 29);
            this.ivElectroMagnetGroupBox.Name = "ivElectroMagnetGroupBox";
            this.ivElectroMagnetGroupBox.Size = new System.Drawing.Size(269, 141);
            this.ivElectroMagnetGroupBox.TabIndex = 25;
            this.ivElectroMagnetGroupBox.TabStop = false;
            // 
            // ivEMSkipStepperMotorCheckBox
            // 
            this.ivEMSkipStepperMotorCheckBox.AutoSize = true;
            this.ivEMSkipStepperMotorCheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivEMSkipStepperMotorCheckBox.Location = new System.Drawing.Point(20, 113);
            this.ivEMSkipStepperMotorCheckBox.Name = "ivEMSkipStepperMotorCheckBox";
            this.ivEMSkipStepperMotorCheckBox.Size = new System.Drawing.Size(182, 17);
            this.ivEMSkipStepperMotorCheckBox.TabIndex = 13;
            this.ivEMSkipStepperMotorCheckBox.Text = "Skip First Cycle by Stepper Motor";
            this.ivEMSkipStepperMotorCheckBox.UseVisualStyleBackColor = true;
            // 
            // ivEMShortCircuitDelayTimeLabel
            // 
            this.ivEMShortCircuitDelayTimeLabel.AutoSize = true;
            this.ivEMShortCircuitDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.ivEMShortCircuitDelayTimeLabel.Location = new System.Drawing.Point(16, 18);
            this.ivEMShortCircuitDelayTimeLabel.Name = "ivEMShortCircuitDelayTimeLabel";
            this.ivEMShortCircuitDelayTimeLabel.Size = new System.Drawing.Size(142, 13);
            this.ivEMShortCircuitDelayTimeLabel.TabIndex = 7;
            this.ivEMShortCircuitDelayTimeLabel.Text = "Short Circuit Delay Time [ms]";
            // 
            // ivEMShortCircuitDelayTimeNumericUpDown
            // 
            this.ivEMShortCircuitDelayTimeNumericUpDown.Location = new System.Drawing.Point(167, 15);
            this.ivEMShortCircuitDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ivEMShortCircuitDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ivEMShortCircuitDelayTimeNumericUpDown.Name = "ivEMShortCircuitDelayTimeNumericUpDown";
            this.ivEMShortCircuitDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.ivEMShortCircuitDelayTimeNumericUpDown.TabIndex = 6;
            this.ivEMShortCircuitDelayTimeNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // ivEMSlowDelayTimeNumericUpDown
            // 
            this.ivEMSlowDelayTimeNumericUpDown.Location = new System.Drawing.Point(167, 78);
            this.ivEMSlowDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.ivEMSlowDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ivEMSlowDelayTimeNumericUpDown.Name = "ivEMSlowDelayTimeNumericUpDown";
            this.ivEMSlowDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.ivEMSlowDelayTimeNumericUpDown.TabIndex = 5;
            this.ivEMSlowDelayTimeNumericUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // ivEMSlowDelayTimeLabel
            // 
            this.ivEMSlowDelayTimeLabel.AutoSize = true;
            this.ivEMSlowDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.ivEMSlowDelayTimeLabel.Location = new System.Drawing.Point(16, 80);
            this.ivEMSlowDelayTimeLabel.Name = "ivEMSlowDelayTimeLabel";
            this.ivEMSlowDelayTimeLabel.Size = new System.Drawing.Size(108, 13);
            this.ivEMSlowDelayTimeLabel.TabIndex = 4;
            this.ivEMSlowDelayTimeLabel.Text = "Slow Delay Time [ms]";
            // 
            // ivEMFastDelayTimeNumericUpDown
            // 
            this.ivEMFastDelayTimeNumericUpDown.Location = new System.Drawing.Point(167, 47);
            this.ivEMFastDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ivEMFastDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ivEMFastDelayTimeNumericUpDown.Name = "ivEMFastDelayTimeNumericUpDown";
            this.ivEMFastDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.ivEMFastDelayTimeNumericUpDown.TabIndex = 3;
            this.ivEMFastDelayTimeNumericUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // ivEMFastDelayTimeLabel
            // 
            this.ivEMFastDelayTimeLabel.AutoSize = true;
            this.ivEMFastDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.ivEMFastDelayTimeLabel.Location = new System.Drawing.Point(16, 49);
            this.ivEMFastDelayTimeLabel.Name = "ivEMFastDelayTimeLabel";
            this.ivEMFastDelayTimeLabel.Size = new System.Drawing.Size(105, 13);
            this.ivEMFastDelayTimeLabel.TabIndex = 2;
            this.ivEMFastDelayTimeLabel.Text = "Fast Delay Time [ms]";
            // 
            // ivStepperMotorRadioButton
            // 
            this.ivStepperMotorRadioButton.AutoSize = true;
            this.ivStepperMotorRadioButton.Checked = true;
            this.ivStepperMotorRadioButton.ForeColor = System.Drawing.Color.Black;
            this.ivStepperMotorRadioButton.Location = new System.Drawing.Point(6, 16);
            this.ivStepperMotorRadioButton.Name = "ivStepperMotorRadioButton";
            this.ivStepperMotorRadioButton.Size = new System.Drawing.Size(92, 17);
            this.ivStepperMotorRadioButton.TabIndex = 22;
            this.ivStepperMotorRadioButton.TabStop = true;
            this.ivStepperMotorRadioButton.Text = "Stepper Motor";
            this.ivStepperMotorRadioButton.UseVisualStyleBackColor = true;
            this.ivStepperMotorRadioButton.CheckedChanged += new System.EventHandler(this.ivStepperMotorRadioButton_CheckedChanged);
            // 
            // ivStepperMotorGroupBox
            // 
            this.ivStepperMotorGroupBox.Controls.Add(this.ivStepperDelayTime2NumericUpDown);
            this.ivStepperMotorGroupBox.Controls.Add(this.ivStepperDelayTime1NumericUpDown);
            this.ivStepperMotorGroupBox.Controls.Add(this.ivStepperDelayTime2Label);
            this.ivStepperMotorGroupBox.Controls.Add(this.ivStepperDelayTime1Label);
            this.ivStepperMotorGroupBox.Location = new System.Drawing.Point(6, 29);
            this.ivStepperMotorGroupBox.Name = "ivStepperMotorGroupBox";
            this.ivStepperMotorGroupBox.Size = new System.Drawing.Size(239, 141);
            this.ivStepperMotorGroupBox.TabIndex = 23;
            this.ivStepperMotorGroupBox.TabStop = false;
            // 
            // ivStepperDelayTime2NumericUpDown
            // 
            this.ivStepperDelayTime2NumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ivStepperDelayTime2NumericUpDown.Location = new System.Drawing.Point(137, 52);
            this.ivStepperDelayTime2NumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.ivStepperDelayTime2NumericUpDown.Name = "ivStepperDelayTime2NumericUpDown";
            this.ivStepperDelayTime2NumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.ivStepperDelayTime2NumericUpDown.TabIndex = 21;
            this.ivStepperDelayTime2NumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // ivStepperDelayTime1NumericUpDown
            // 
            this.ivStepperDelayTime1NumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ivStepperDelayTime1NumericUpDown.Location = new System.Drawing.Point(137, 22);
            this.ivStepperDelayTime1NumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.ivStepperDelayTime1NumericUpDown.Name = "ivStepperDelayTime1NumericUpDown";
            this.ivStepperDelayTime1NumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.ivStepperDelayTime1NumericUpDown.TabIndex = 19;
            this.ivStepperDelayTime1NumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // ivStepperDelayTime2Label
            // 
            this.ivStepperDelayTime2Label.AutoSize = true;
            this.ivStepperDelayTime2Label.ForeColor = System.Drawing.Color.Black;
            this.ivStepperDelayTime2Label.Location = new System.Drawing.Point(14, 55);
            this.ivStepperDelayTime2Label.Name = "ivStepperDelayTime2Label";
            this.ivStepperDelayTime2Label.Size = new System.Drawing.Size(103, 13);
            this.ivStepperDelayTime2Label.TabIndex = 20;
            this.ivStepperDelayTime2Label.Text = "Delay Time 2 [msec]";
            // 
            // ivStepperDelayTime1Label
            // 
            this.ivStepperDelayTime1Label.AutoSize = true;
            this.ivStepperDelayTime1Label.ForeColor = System.Drawing.Color.Black;
            this.ivStepperDelayTime1Label.Location = new System.Drawing.Point(14, 25);
            this.ivStepperDelayTime1Label.Name = "ivStepperDelayTime1Label";
            this.ivStepperDelayTime1Label.Size = new System.Drawing.Size(103, 13);
            this.ivStepperDelayTime1Label.TabIndex = 18;
            this.ivStepperDelayTime1Label.Text = "Delay Time 1 [msec]";
            // 
            // ivChannelsTabPage
            // 
            this.ivChannelsTabPage.Controls.Add(this.ivChannelsPanel);
            this.ivChannelsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ivChannelsTabPage.Name = "ivChannelsTabPage";
            this.ivChannelsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ivChannelsTabPage.Size = new System.Drawing.Size(721, 191);
            this.ivChannelsTabPage.TabIndex = 4;
            this.ivChannelsTabPage.Text = "Channels";
            this.ivChannelsTabPage.UseVisualStyleBackColor = true;
            // 
            // ivChannelsPanel
            // 
            this.ivChannelsPanel.Controls.Add(this.ivChannel1ComboBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel3ComboBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel0CheckBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel3CheckBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel0ComboBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel2ComboBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel1CheckBox);
            this.ivChannelsPanel.Controls.Add(this.ivChannel2CheckBox);
            this.ivChannelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivChannelsPanel.Location = new System.Drawing.Point(3, 3);
            this.ivChannelsPanel.Name = "ivChannelsPanel";
            this.ivChannelsPanel.Size = new System.Drawing.Size(715, 185);
            this.ivChannelsPanel.TabIndex = 8;
            // 
            // ivChannel1ComboBox
            // 
            this.ivChannel1ComboBox.FormattingEnabled = true;
            this.ivChannel1ComboBox.Location = new System.Drawing.Point(107, 33);
            this.ivChannel1ComboBox.Name = "ivChannel1ComboBox";
            this.ivChannel1ComboBox.Size = new System.Drawing.Size(202, 21);
            this.ivChannel1ComboBox.TabIndex = 3;
            // 
            // ivChannel3ComboBox
            // 
            this.ivChannel3ComboBox.FormattingEnabled = true;
            this.ivChannel3ComboBox.Location = new System.Drawing.Point(107, 87);
            this.ivChannel3ComboBox.Name = "ivChannel3ComboBox";
            this.ivChannel3ComboBox.Size = new System.Drawing.Size(202, 21);
            this.ivChannel3ComboBox.TabIndex = 7;
            // 
            // ivChannel0CheckBox
            // 
            this.ivChannel0CheckBox.AutoSize = true;
            this.ivChannel0CheckBox.Checked = true;
            this.ivChannel0CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ivChannel0CheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivChannel0CheckBox.Location = new System.Drawing.Point(7, 8);
            this.ivChannel0CheckBox.Name = "ivChannel0CheckBox";
            this.ivChannel0CheckBox.Size = new System.Drawing.Size(71, 17);
            this.ivChannel0CheckBox.TabIndex = 0;
            this.ivChannel0CheckBox.Text = "Dev1/ai0";
            this.ivChannel0CheckBox.UseVisualStyleBackColor = true;
            // 
            // ivChannel3CheckBox
            // 
            this.ivChannel3CheckBox.AutoSize = true;
            this.ivChannel3CheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivChannel3CheckBox.Location = new System.Drawing.Point(7, 89);
            this.ivChannel3CheckBox.Name = "ivChannel3CheckBox";
            this.ivChannel3CheckBox.Size = new System.Drawing.Size(71, 17);
            this.ivChannel3CheckBox.TabIndex = 6;
            this.ivChannel3CheckBox.Text = "Dev1/ai3";
            this.ivChannel3CheckBox.UseVisualStyleBackColor = true;
            // 
            // ivChannel0ComboBox
            // 
            this.ivChannel0ComboBox.FormattingEnabled = true;
            this.ivChannel0ComboBox.Location = new System.Drawing.Point(107, 6);
            this.ivChannel0ComboBox.Name = "ivChannel0ComboBox";
            this.ivChannel0ComboBox.Size = new System.Drawing.Size(202, 21);
            this.ivChannel0ComboBox.TabIndex = 1;
            // 
            // ivChannel2ComboBox
            // 
            this.ivChannel2ComboBox.FormattingEnabled = true;
            this.ivChannel2ComboBox.Location = new System.Drawing.Point(107, 60);
            this.ivChannel2ComboBox.Name = "ivChannel2ComboBox";
            this.ivChannel2ComboBox.Size = new System.Drawing.Size(202, 21);
            this.ivChannel2ComboBox.TabIndex = 5;
            // 
            // ivChannel1CheckBox
            // 
            this.ivChannel1CheckBox.AutoSize = true;
            this.ivChannel1CheckBox.Checked = true;
            this.ivChannel1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ivChannel1CheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivChannel1CheckBox.Location = new System.Drawing.Point(7, 35);
            this.ivChannel1CheckBox.Name = "ivChannel1CheckBox";
            this.ivChannel1CheckBox.Size = new System.Drawing.Size(71, 17);
            this.ivChannel1CheckBox.TabIndex = 2;
            this.ivChannel1CheckBox.Text = "Dev1/ai1";
            this.ivChannel1CheckBox.UseVisualStyleBackColor = true;
            // 
            // ivChannel2CheckBox
            // 
            this.ivChannel2CheckBox.AutoSize = true;
            this.ivChannel2CheckBox.ForeColor = System.Drawing.Color.Black;
            this.ivChannel2CheckBox.Location = new System.Drawing.Point(7, 62);
            this.ivChannel2CheckBox.Name = "ivChannel2CheckBox";
            this.ivChannel2CheckBox.Size = new System.Drawing.Size(71, 17);
            this.ivChannel2CheckBox.TabIndex = 4;
            this.ivChannel2CheckBox.Text = "Dev1/ai2";
            this.ivChannel2CheckBox.UseVisualStyleBackColor = true;
            // 
            // calibrationTabPage
            // 
            this.calibrationTabPage.Controls.Add(this.calibrationPlotGroupBox);
            this.calibrationTabPage.Controls.Add(this.calibrationWaveformGraph);
            this.calibrationTabPage.Controls.Add(this.calibrationSettingsGroupBox);
            this.calibrationTabPage.Controls.Add(this.calibrationOperateGroupBox);
            this.calibrationTabPage.Location = new System.Drawing.Point(4, 22);
            this.calibrationTabPage.Name = "calibrationTabPage";
            this.calibrationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.calibrationTabPage.Size = new System.Drawing.Size(779, 799);
            this.calibrationTabPage.TabIndex = 3;
            this.calibrationTabPage.Text = "Calibration";
            this.calibrationTabPage.UseVisualStyleBackColor = true;
            // 
            // calibrationPlotGroupBox
            // 
            this.calibrationPlotGroupBox.Controls.Add(this.calibrationChannelsListView);
            this.calibrationPlotGroupBox.ForeColor = System.Drawing.Color.Red;
            this.calibrationPlotGroupBox.Location = new System.Drawing.Point(483, 0);
            this.calibrationPlotGroupBox.Name = "calibrationPlotGroupBox";
            this.calibrationPlotGroupBox.Size = new System.Drawing.Size(258, 348);
            this.calibrationPlotGroupBox.TabIndex = 32;
            this.calibrationPlotGroupBox.TabStop = false;
            this.calibrationPlotGroupBox.Text = "Plots";
            // 
            // calibrationChannelsListView
            // 
            this.calibrationChannelsListView.CheckBoxes = true;
            this.calibrationChannelsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calibrationChannelsListView.Location = new System.Drawing.Point(3, 16);
            this.calibrationChannelsListView.Name = "calibrationChannelsListView";
            this.calibrationChannelsListView.Size = new System.Drawing.Size(252, 329);
            this.calibrationChannelsListView.TabIndex = 25;
            this.calibrationChannelsListView.UseCompatibleStateImageBehavior = false;
            this.calibrationChannelsListView.View = System.Windows.Forms.View.List;
            // 
            // calibrationWaveformGraph
            // 
            this.calibrationWaveformGraph.CaptionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.calibrationWaveformGraph.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calibrationWaveformGraph.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.calibrationWaveformGraph.InteractionMode = ((NationalInstruments.UI.GraphInteractionModes)((((((((NationalInstruments.UI.GraphInteractionModes.ZoomX | NationalInstruments.UI.GraphInteractionModes.ZoomY)
                        | NationalInstruments.UI.GraphInteractionModes.ZoomAroundPoint)
                        | NationalInstruments.UI.GraphInteractionModes.PanX)
                        | NationalInstruments.UI.GraphInteractionModes.PanY)
                        | NationalInstruments.UI.GraphInteractionModes.DragCursor)
                        | NationalInstruments.UI.GraphInteractionModes.DragAnnotationCaption)
                        | NationalInstruments.UI.GraphInteractionModes.EditRange)));
            this.calibrationWaveformGraph.Location = new System.Drawing.Point(10, 9);
            this.calibrationWaveformGraph.Name = "calibrationWaveformGraph";
            this.calibrationWaveformGraph.PlotAreaColor = System.Drawing.Color.LightGray;
            this.calibrationWaveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.calibrationWaveformPlot});
            this.calibrationWaveformGraph.Size = new System.Drawing.Size(467, 339);
            this.calibrationWaveformGraph.TabIndex = 31;
            this.calibrationWaveformGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.calibrationXAxis});
            this.calibrationWaveformGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.calibrationYAxis});
            // 
            // calibrationWaveformPlot
            // 
            this.calibrationWaveformPlot.LineColor = System.Drawing.Color.Red;            
            this.calibrationWaveformPlot.ToolTipsEnabled = true;
            this.calibrationWaveformPlot.XAxis = this.calibrationXAxis;
            this.calibrationWaveformPlot.YAxis = this.calibrationYAxis;
            // 
            // calibrationXAxis
            // 
            this.calibrationXAxis.Caption = "Steps";
            this.calibrationXAxis.Range = new NationalInstruments.UI.Range(0, 100);
            // 
            // calibrationYAxis
            // 
            this.calibrationYAxis.Caption = "Current [uA]";
            // 
            // calibrationSettingsGroupBox
            // 
            this.calibrationSettingsGroupBox.Controls.Add(this.calibrationSettingsTabControl);
            this.calibrationSettingsGroupBox.ForeColor = System.Drawing.Color.Red;
            this.calibrationSettingsGroupBox.Location = new System.Drawing.Point(6, 353);
            this.calibrationSettingsGroupBox.Name = "calibrationSettingsGroupBox";
            this.calibrationSettingsGroupBox.Size = new System.Drawing.Size(732, 252);
            this.calibrationSettingsGroupBox.TabIndex = 30;
            this.calibrationSettingsGroupBox.TabStop = false;
            this.calibrationSettingsGroupBox.Text = "Settings";
            // 
            // calibrationSettingsTabControl
            // 
            this.calibrationSettingsTabControl.Controls.Add(this.CalibrationGeneralTabPage);
            this.calibrationSettingsTabControl.Controls.Add(this.tabPage3);
            this.calibrationSettingsTabControl.Controls.Add(this.tabPage2);
            this.calibrationSettingsTabControl.Location = new System.Drawing.Point(8, 18);
            this.calibrationSettingsTabControl.Name = "calibrationSettingsTabControl";
            this.calibrationSettingsTabControl.SelectedIndex = 0;
            this.calibrationSettingsTabControl.Size = new System.Drawing.Size(723, 233);
            this.calibrationSettingsTabControl.TabIndex = 0;
            // 
            // CalibrationGeneralTabPage
            // 
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationMeasurementTypeComboBox);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationMeasurementsTypeLabel);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationDelayTimeNumericUpDown);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationDelayTimeLabel);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationKeithleyCheckBox);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationSampleRateNumericEdit);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationSampleRateLabel);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationTriggerVoltagrLabel);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationGainPowerComboBox);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationBiasNumericEdit);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationGainPowerLabel);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationBiasLabel);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationTriggerVoltageNumericEdit);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationTriggerConductanceNumericEdit);
            this.CalibrationGeneralTabPage.Controls.Add(this.calibrationTriggerConductanceLabel);
            this.CalibrationGeneralTabPage.Location = new System.Drawing.Point(4, 22);
            this.CalibrationGeneralTabPage.Name = "CalibrationGeneralTabPage";
            this.CalibrationGeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.CalibrationGeneralTabPage.Size = new System.Drawing.Size(715, 207);
            this.CalibrationGeneralTabPage.TabIndex = 0;
            this.CalibrationGeneralTabPage.Text = "General";
            this.CalibrationGeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // calibrationMeasurementTypeComboBox
            // 
            this.calibrationMeasurementTypeComboBox.FormattingEnabled = true;
            this.calibrationMeasurementTypeComboBox.Items.AddRange(new object[] {
            "OpenJunction",
            "CloseJunction",
            "BothOpenAndClose"});
            this.calibrationMeasurementTypeComboBox.Location = new System.Drawing.Point(434, 99);
            this.calibrationMeasurementTypeComboBox.Name = "calibrationMeasurementTypeComboBox";
            this.calibrationMeasurementTypeComboBox.Size = new System.Drawing.Size(129, 21);
            this.calibrationMeasurementTypeComboBox.TabIndex = 39;
            this.calibrationMeasurementTypeComboBox.Text = "BothOpenAndClose";
            // 
            // calibrationMeasurementsTypeLabel
            // 
            this.calibrationMeasurementsTypeLabel.AutoSize = true;
            this.calibrationMeasurementsTypeLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationMeasurementsTypeLabel.Location = new System.Drawing.Point(300, 102);
            this.calibrationMeasurementsTypeLabel.Name = "calibrationMeasurementsTypeLabel";
            this.calibrationMeasurementsTypeLabel.Size = new System.Drawing.Size(103, 13);
            this.calibrationMeasurementsTypeLabel.TabIndex = 35;
            this.calibrationMeasurementsTypeLabel.Text = "MeasurementsType:";
            // 
            // calibrationDelayTimeNumericUpDown
            // 
            this.calibrationDelayTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.calibrationDelayTimeNumericUpDown.Location = new System.Drawing.Point(434, 63);
            this.calibrationDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.calibrationDelayTimeNumericUpDown.Name = "calibrationDelayTimeNumericUpDown";
            this.calibrationDelayTimeNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.calibrationDelayTimeNumericUpDown.TabIndex = 34;
            this.calibrationDelayTimeNumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // calibrationDelayTimeLabel
            // 
            this.calibrationDelayTimeLabel.AutoSize = true;
            this.calibrationDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationDelayTimeLabel.Location = new System.Drawing.Point(300, 65);
            this.calibrationDelayTimeLabel.Name = "calibrationDelayTimeLabel";
            this.calibrationDelayTimeLabel.Size = new System.Drawing.Size(94, 13);
            this.calibrationDelayTimeLabel.TabIndex = 33;
            this.calibrationDelayTimeLabel.Text = "Delay Time [msec]";
            // 
            // calibrationKeithleyCheckBox
            // 
            this.calibrationKeithleyCheckBox.AutoSize = true;
            this.calibrationKeithleyCheckBox.Checked = true;
            this.calibrationKeithleyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calibrationKeithleyCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationKeithleyCheckBox.Location = new System.Drawing.Point(6, 3);
            this.calibrationKeithleyCheckBox.Name = "calibrationKeithleyCheckBox";
            this.calibrationKeithleyCheckBox.Size = new System.Drawing.Size(159, 17);
            this.calibrationKeithleyCheckBox.TabIndex = 32;
            this.calibrationKeithleyCheckBox.Text = "Use Keithley as Bias Source";
            this.calibrationKeithleyCheckBox.UseVisualStyleBackColor = true;
            this.calibrationKeithleyCheckBox.CheckedChanged += new System.EventHandler(this.calibrationUseKeithleyCheckBox_CheckedChanged);
            // 
            // calibrationSampleRateNumericEdit
            // 
            this.calibrationSampleRateNumericEdit.Location = new System.Drawing.Point(434, 31);
            this.calibrationSampleRateNumericEdit.Maximum = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            this.calibrationSampleRateNumericEdit.Name = "calibrationSampleRateNumericEdit";
            this.calibrationSampleRateNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.calibrationSampleRateNumericEdit.TabIndex = 27;
            this.calibrationSampleRateNumericEdit.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // calibrationSampleRateLabel
            // 
            this.calibrationSampleRateLabel.AutoSize = true;
            this.calibrationSampleRateLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationSampleRateLabel.Location = new System.Drawing.Point(300, 33);
            this.calibrationSampleRateLabel.Name = "calibrationSampleRateLabel";
            this.calibrationSampleRateLabel.Size = new System.Drawing.Size(90, 13);
            this.calibrationSampleRateLabel.TabIndex = 26;
            this.calibrationSampleRateLabel.Text = "Sample Rate [Hz]";
            // 
            // calibrationTriggerVoltagrLabel
            // 
            this.calibrationTriggerVoltagrLabel.AutoSize = true;
            this.calibrationTriggerVoltagrLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationTriggerVoltagrLabel.Location = new System.Drawing.Point(11, 98);
            this.calibrationTriggerVoltagrLabel.Name = "calibrationTriggerVoltagrLabel";
            this.calibrationTriggerVoltagrLabel.Size = new System.Drawing.Size(95, 13);
            this.calibrationTriggerVoltagrLabel.TabIndex = 12;
            this.calibrationTriggerVoltagrLabel.Text = "Trigger Voltage [V]";
            // 
            // calibrationGainPowerComboBox
            // 
            this.calibrationGainPowerComboBox.DisplayMember = "3";
            this.calibrationGainPowerComboBox.FormattingEnabled = true;
            this.calibrationGainPowerComboBox.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.calibrationGainPowerComboBox.Location = new System.Drawing.Point(159, 62);
            this.calibrationGainPowerComboBox.Name = "calibrationGainPowerComboBox";
            this.calibrationGainPowerComboBox.Size = new System.Drawing.Size(75, 21);
            this.calibrationGainPowerComboBox.TabIndex = 25;
            this.calibrationGainPowerComboBox.Text = "5";
            this.calibrationGainPowerComboBox.SelectedIndexChanged += new System.EventHandler(this.calibrationGainComboBox_ValueChanged);
            // 
            // calibrationBiasNumericEdit
            // 
            this.calibrationBiasNumericEdit.CoercionInterval = 0.01;
            this.calibrationBiasNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(3);
            this.calibrationBiasNumericEdit.Location = new System.Drawing.Point(159, 31);
            this.calibrationBiasNumericEdit.Name = "calibrationBiasNumericEdit";
            this.calibrationBiasNumericEdit.Range = new NationalInstruments.UI.Range(0, 1);
            this.calibrationBiasNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.calibrationBiasNumericEdit.TabIndex = 2;
            this.calibrationBiasNumericEdit.Value = 0.1;
            this.calibrationBiasNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.calibrationbiasNumericEdit_AfterChangeValue);
            // 
            // calibrationGainPowerLabel
            // 
            this.calibrationGainPowerLabel.AutoSize = true;
            this.calibrationGainPowerLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationGainPowerLabel.Location = new System.Drawing.Point(11, 65);
            this.calibrationGainPowerLabel.Name = "calibrationGainPowerLabel";
            this.calibrationGainPowerLabel.Size = new System.Drawing.Size(62, 13);
            this.calibrationGainPowerLabel.TabIndex = 4;
            this.calibrationGainPowerLabel.Text = "Gain Power";
            // 
            // calibrationBiasLabel
            // 
            this.calibrationBiasLabel.AutoSize = true;
            this.calibrationBiasLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationBiasLabel.Location = new System.Drawing.Point(11, 33);
            this.calibrationBiasLabel.Name = "calibrationBiasLabel";
            this.calibrationBiasLabel.Size = new System.Drawing.Size(43, 13);
            this.calibrationBiasLabel.TabIndex = 2;
            this.calibrationBiasLabel.Text = "Bias [V]";
            // 
            // calibrationTriggerVoltageNumericEdit
            // 
            this.calibrationTriggerVoltageNumericEdit.Enabled = false;
            this.calibrationTriggerVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.calibrationTriggerVoltageNumericEdit.Location = new System.Drawing.Point(159, 95);
            this.calibrationTriggerVoltageNumericEdit.Name = "calibrationTriggerVoltageNumericEdit";
            this.calibrationTriggerVoltageNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.calibrationTriggerVoltageNumericEdit.TabIndex = 12;
            this.calibrationTriggerVoltageNumericEdit.Value = -0.01;
            // 
            // calibrationTriggerConductanceNumericEdit
            // 
            this.calibrationTriggerConductanceNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2, true);
            this.calibrationTriggerConductanceNumericEdit.Location = new System.Drawing.Point(159, 128);
            this.calibrationTriggerConductanceNumericEdit.Name = "calibrationTriggerConductanceNumericEdit";
            this.calibrationTriggerConductanceNumericEdit.Size = new System.Drawing.Size(75, 20);
            this.calibrationTriggerConductanceNumericEdit.TabIndex = 14;
            this.calibrationTriggerConductanceNumericEdit.Value = 0.0129;
            this.calibrationTriggerConductanceNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.calibrationTriggerConductanceNumericEdit_AfterChangeValue);
            // 
            // calibrationTriggerConductanceLabel
            // 
            this.calibrationTriggerConductanceLabel.AutoSize = true;
            this.calibrationTriggerConductanceLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationTriggerConductanceLabel.Location = new System.Drawing.Point(11, 131);
            this.calibrationTriggerConductanceLabel.Name = "calibrationTriggerConductanceLabel";
            this.calibrationTriggerConductanceLabel.Size = new System.Drawing.Size(130, 13);
            this.calibrationTriggerConductanceLabel.TabIndex = 13;
            this.calibrationTriggerConductanceLabel.Text = "Trigger Conductance [G0]";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.calibrationElectroMagnetPanel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(715, 207);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "ElectroMagnet";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // calibrationElectroMagnetPanel
            // 
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMSkipShortCircuitByStepperMotorCheckBox);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMShortCircuitDelayTimeLabel);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMShortCircuitDelayTimeNumericUpDown);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMSlowDelayTimeNumericUpDown);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMSlowDelayTimeLabel);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMFastDelayTimeNumericUpDown);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEMFastDelayTimeLabel);
            this.calibrationElectroMagnetPanel.Controls.Add(this.calibrationEnableElectroMagnetCheckBox);
            this.calibrationElectroMagnetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calibrationElectroMagnetPanel.Location = new System.Drawing.Point(3, 3);
            this.calibrationElectroMagnetPanel.Name = "calibrationElectroMagnetPanel";
            this.calibrationElectroMagnetPanel.Size = new System.Drawing.Size(709, 201);
            this.calibrationElectroMagnetPanel.TabIndex = 4;
            // 
            // calibrationEMSkipShortCircuitByStepperMotorCheckBox
            // 
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.AutoSize = true;
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Enabled = false;
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Location = new System.Drawing.Point(8, 164);
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Name = "calibrationEMSkipShortCircuitByStepperMotorCheckBox";
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Size = new System.Drawing.Size(191, 17);
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.TabIndex = 13;
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.Text = "Skip Short Circuit by Stepper Motor";
            this.calibrationEMSkipShortCircuitByStepperMotorCheckBox.UseVisualStyleBackColor = true;
            // 
            // calibrationEMShortCircuitDelayTimeLabel
            // 
            this.calibrationEMShortCircuitDelayTimeLabel.AutoSize = true;
            this.calibrationEMShortCircuitDelayTimeLabel.Enabled = false;
            this.calibrationEMShortCircuitDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationEMShortCircuitDelayTimeLabel.Location = new System.Drawing.Point(11, 41);
            this.calibrationEMShortCircuitDelayTimeLabel.Name = "calibrationEMShortCircuitDelayTimeLabel";
            this.calibrationEMShortCircuitDelayTimeLabel.Size = new System.Drawing.Size(142, 13);
            this.calibrationEMShortCircuitDelayTimeLabel.TabIndex = 7;
            this.calibrationEMShortCircuitDelayTimeLabel.Text = "Short Circuit Delay Time [ms]";
            // 
            // calibrationEMShortCircuitDelayTimeNumericUpDown
            // 
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Enabled = false;
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Location = new System.Drawing.Point(162, 38);
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Name = "calibrationEMShortCircuitDelayTimeNumericUpDown";
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.TabIndex = 6;
            this.calibrationEMShortCircuitDelayTimeNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // calibrationEMSlowDelayTimeNumericUpDown
            // 
            this.calibrationEMSlowDelayTimeNumericUpDown.Enabled = false;
            this.calibrationEMSlowDelayTimeNumericUpDown.Location = new System.Drawing.Point(162, 101);
            this.calibrationEMSlowDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.calibrationEMSlowDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.calibrationEMSlowDelayTimeNumericUpDown.Name = "calibrationEMSlowDelayTimeNumericUpDown";
            this.calibrationEMSlowDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.calibrationEMSlowDelayTimeNumericUpDown.TabIndex = 5;
            this.calibrationEMSlowDelayTimeNumericUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // calibrationEMSlowDelayTimeLabel
            // 
            this.calibrationEMSlowDelayTimeLabel.AutoSize = true;
            this.calibrationEMSlowDelayTimeLabel.Enabled = false;
            this.calibrationEMSlowDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationEMSlowDelayTimeLabel.Location = new System.Drawing.Point(11, 103);
            this.calibrationEMSlowDelayTimeLabel.Name = "calibrationEMSlowDelayTimeLabel";
            this.calibrationEMSlowDelayTimeLabel.Size = new System.Drawing.Size(108, 13);
            this.calibrationEMSlowDelayTimeLabel.TabIndex = 4;
            this.calibrationEMSlowDelayTimeLabel.Text = "Slow Delay Time [ms]";
            // 
            // calibrationEMFastDelayTimeNumericUpDown
            // 
            this.calibrationEMFastDelayTimeNumericUpDown.Enabled = false;
            this.calibrationEMFastDelayTimeNumericUpDown.Location = new System.Drawing.Point(162, 70);
            this.calibrationEMFastDelayTimeNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.calibrationEMFastDelayTimeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.calibrationEMFastDelayTimeNumericUpDown.Name = "calibrationEMFastDelayTimeNumericUpDown";
            this.calibrationEMFastDelayTimeNumericUpDown.Size = new System.Drawing.Size(87, 20);
            this.calibrationEMFastDelayTimeNumericUpDown.TabIndex = 3;
            this.calibrationEMFastDelayTimeNumericUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // calibrationEMFastDelayTimeLabel
            // 
            this.calibrationEMFastDelayTimeLabel.AutoSize = true;
            this.calibrationEMFastDelayTimeLabel.Enabled = false;
            this.calibrationEMFastDelayTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationEMFastDelayTimeLabel.Location = new System.Drawing.Point(11, 72);
            this.calibrationEMFastDelayTimeLabel.Name = "calibrationEMFastDelayTimeLabel";
            this.calibrationEMFastDelayTimeLabel.Size = new System.Drawing.Size(105, 13);
            this.calibrationEMFastDelayTimeLabel.TabIndex = 2;
            this.calibrationEMFastDelayTimeLabel.Text = "Fast Delay Time [ms]";
            // 
            // calibrationEnableElectroMagnetCheckBox
            // 
            this.calibrationEnableElectroMagnetCheckBox.AutoSize = true;
            this.calibrationEnableElectroMagnetCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationEnableElectroMagnetCheckBox.Location = new System.Drawing.Point(14, 8);
            this.calibrationEnableElectroMagnetCheckBox.Name = "calibrationEnableElectroMagnetCheckBox";
            this.calibrationEnableElectroMagnetCheckBox.Size = new System.Drawing.Size(131, 17);
            this.calibrationEnableElectroMagnetCheckBox.TabIndex = 0;
            this.calibrationEnableElectroMagnetCheckBox.Text = "Enable ElectroMagnet";
            this.calibrationEnableElectroMagnetCheckBox.UseVisualStyleBackColor = true;
            this.calibrationEnableElectroMagnetCheckBox.CheckedChanged += new System.EventHandler(this.calibrationEnableElectroMagnetCheckBox_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.calibrationChannelsPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(715, 207);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Channels";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // calibrationChannelsPanel
            // 
            this.calibrationChannelsPanel.Controls.Add(this.calibrationChannel1CheckBox);
            this.calibrationChannelsPanel.Controls.Add(this.calibrationChannel1ComboBox);
            this.calibrationChannelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calibrationChannelsPanel.Location = new System.Drawing.Point(3, 3);
            this.calibrationChannelsPanel.Name = "calibrationChannelsPanel";
            this.calibrationChannelsPanel.Size = new System.Drawing.Size(709, 201);
            this.calibrationChannelsPanel.TabIndex = 9;
            // 
            // calibrationChannel1CheckBox
            // 
            this.calibrationChannel1CheckBox.AutoSize = true;
            this.calibrationChannel1CheckBox.Checked = true;
            this.calibrationChannel1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calibrationChannel1CheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationChannel1CheckBox.Location = new System.Drawing.Point(8, 15);
            this.calibrationChannel1CheckBox.Name = "calibrationChannel1CheckBox";
            this.calibrationChannel1CheckBox.Size = new System.Drawing.Size(71, 17);
            this.calibrationChannel1CheckBox.TabIndex = 0;
            this.calibrationChannel1CheckBox.Text = "Dev1/ai0";
            this.calibrationChannel1CheckBox.UseVisualStyleBackColor = true;
            this.calibrationChannel1CheckBox.CheckedChanged += new System.EventHandler(this.calibrationChannel1CheckBox_CheckedChanged);
            // 
            // calibrationChannel1ComboBox
            // 
            this.calibrationChannel1ComboBox.FormattingEnabled = true;
            this.calibrationChannel1ComboBox.Location = new System.Drawing.Point(110, 15);
            this.calibrationChannel1ComboBox.Name = "calibrationChannel1ComboBox";
            this.calibrationChannel1ComboBox.Size = new System.Drawing.Size(202, 21);
            this.calibrationChannel1ComboBox.TabIndex = 1;
            this.calibrationChannel1ComboBox.SelectedIndexChanged += new System.EventHandler(this.calibrationChannel1ComboBox_SelectedValueChanged);
            // 
            // calibrationOperateGroupBox
            // 
            this.calibrationOperateGroupBox.AutoSize = true;
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationOpenFolderButton);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationStepperUpCheckBox);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationStartStopCheckBox);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationShortCircuitCheckBox);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationNumberOfCyclesNumericUpDown);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationNumberOfCyclesLabel);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationShortCircuitVoltageumericUpDown);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationShortCircuitVoltageLabel);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationPathTextBox);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationBrowseButton);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationSavingFilesCheckBox);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationFileNumberLabel);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationPathLabel);
            this.calibrationOperateGroupBox.Controls.Add(this.calibrationCycleNumberNumericUpDown);
            this.calibrationOperateGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.calibrationOperateGroupBox.ForeColor = System.Drawing.Color.Red;
            this.calibrationOperateGroupBox.Location = new System.Drawing.Point(3, 612);
            this.calibrationOperateGroupBox.MinimumSize = new System.Drawing.Size(478, 176);
            this.calibrationOperateGroupBox.Name = "calibrationOperateGroupBox";
            this.calibrationOperateGroupBox.Size = new System.Drawing.Size(773, 184);
            this.calibrationOperateGroupBox.TabIndex = 29;
            this.calibrationOperateGroupBox.TabStop = false;
            this.calibrationOperateGroupBox.Text = "Operate";
            // 
            // calibrationOpenFolderButton
            // 
            this.calibrationOpenFolderButton.ForeColor = System.Drawing.Color.Black;
            this.calibrationOpenFolderButton.Location = new System.Drawing.Point(530, 44);
            this.calibrationOpenFolderButton.Name = "calibrationOpenFolderButton";
            this.calibrationOpenFolderButton.Size = new System.Drawing.Size(74, 23);
            this.calibrationOpenFolderButton.TabIndex = 29;
            this.calibrationOpenFolderButton.Text = "Open Folder";
            this.calibrationOpenFolderButton.UseVisualStyleBackColor = true;
            this.calibrationOpenFolderButton.Click += new System.EventHandler(this.calibrationOpenFolderButton_Click);
            // 
            // calibrationStepperUpCheckBox
            // 
            this.calibrationStepperUpCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.calibrationStepperUpCheckBox.AutoSize = true;
            this.calibrationStepperUpCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationStepperUpCheckBox.Location = new System.Drawing.Point(365, 142);
            this.calibrationStepperUpCheckBox.MinimumSize = new System.Drawing.Size(74, 23);
            this.calibrationStepperUpCheckBox.Name = "calibrationStepperUpCheckBox";
            this.calibrationStepperUpCheckBox.Size = new System.Drawing.Size(74, 23);
            this.calibrationStepperUpCheckBox.TabIndex = 17;
            this.calibrationStepperUpCheckBox.Text = "Stepper Up";
            this.calibrationStepperUpCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.calibrationStepperUpCheckBox.UseVisualStyleBackColor = true;
            this.calibrationStepperUpCheckBox.CheckedChanged += new System.EventHandler(this.calibrationStepperUpCheckBox_CheckedChanged);
            // 
            // calibrationStartStopCheckBox
            // 
            this.calibrationStartStopCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.calibrationStartStopCheckBox.AutoSize = true;
            this.calibrationStartStopCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationStartStopCheckBox.Location = new System.Drawing.Point(272, 142);
            this.calibrationStartStopCheckBox.MinimumSize = new System.Drawing.Size(74, 23);
            this.calibrationStartStopCheckBox.Name = "calibrationStartStopCheckBox";
            this.calibrationStartStopCheckBox.Size = new System.Drawing.Size(74, 23);
            this.calibrationStartStopCheckBox.TabIndex = 16;
            this.calibrationStartStopCheckBox.Text = "Start";
            this.calibrationStartStopCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.calibrationStartStopCheckBox.UseVisualStyleBackColor = true;
            this.calibrationStartStopCheckBox.CheckedChanged += new System.EventHandler(this.StartStopCalibrationcheckBox_CheckedChanged);
            // 
            // calibrationShortCircuitCheckBox
            // 
            this.calibrationShortCircuitCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.calibrationShortCircuitCheckBox.AutoSize = true;
            this.calibrationShortCircuitCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationShortCircuitCheckBox.Location = new System.Drawing.Point(459, 142);
            this.calibrationShortCircuitCheckBox.MinimumSize = new System.Drawing.Size(74, 23);
            this.calibrationShortCircuitCheckBox.Name = "calibrationShortCircuitCheckBox";
            this.calibrationShortCircuitCheckBox.Size = new System.Drawing.Size(74, 23);
            this.calibrationShortCircuitCheckBox.TabIndex = 15;
            this.calibrationShortCircuitCheckBox.Text = "Short Circuit";
            this.calibrationShortCircuitCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.calibrationShortCircuitCheckBox.UseVisualStyleBackColor = true;
            this.calibrationShortCircuitCheckBox.Click += new System.EventHandler(this.calibrationShortCircuitButton_CheckedChanged);
            // 
            // calibrationNumberOfCyclesNumericUpDown
            // 
            this.calibrationNumberOfCyclesNumericUpDown.Location = new System.Drawing.Point(150, 111);
            this.calibrationNumberOfCyclesNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.calibrationNumberOfCyclesNumericUpDown.Name = "calibrationNumberOfCyclesNumericUpDown";
            this.calibrationNumberOfCyclesNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.calibrationNumberOfCyclesNumericUpDown.TabIndex = 14;
            this.calibrationNumberOfCyclesNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // calibrationNumberOfCyclesLabel
            // 
            this.calibrationNumberOfCyclesLabel.AutoSize = true;
            this.calibrationNumberOfCyclesLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationNumberOfCyclesLabel.Location = new System.Drawing.Point(7, 113);
            this.calibrationNumberOfCyclesLabel.Name = "calibrationNumberOfCyclesLabel";
            this.calibrationNumberOfCyclesLabel.Size = new System.Drawing.Size(90, 13);
            this.calibrationNumberOfCyclesLabel.TabIndex = 13;
            this.calibrationNumberOfCyclesLabel.Text = "Number of Cycles";
            // 
            // calibrationShortCircuitVoltageumericUpDown
            // 
            this.calibrationShortCircuitVoltageumericUpDown.DecimalPlaces = 1;
            this.calibrationShortCircuitVoltageumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.calibrationShortCircuitVoltageumericUpDown.Location = new System.Drawing.Point(150, 145);
            this.calibrationShortCircuitVoltageumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.calibrationShortCircuitVoltageumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.calibrationShortCircuitVoltageumericUpDown.Name = "calibrationShortCircuitVoltageumericUpDown";
            this.calibrationShortCircuitVoltageumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.calibrationShortCircuitVoltageumericUpDown.TabIndex = 12;
            this.calibrationShortCircuitVoltageumericUpDown.Value = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            // 
            // calibrationShortCircuitVoltageLabel
            // 
            this.calibrationShortCircuitVoltageLabel.AutoSize = true;
            this.calibrationShortCircuitVoltageLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationShortCircuitVoltageLabel.Location = new System.Drawing.Point(7, 147);
            this.calibrationShortCircuitVoltageLabel.Name = "calibrationShortCircuitVoltageLabel";
            this.calibrationShortCircuitVoltageLabel.Size = new System.Drawing.Size(119, 13);
            this.calibrationShortCircuitVoltageLabel.TabIndex = 11;
            this.calibrationShortCircuitVoltageLabel.Text = "Short Circuit Voltage [V]";
            // 
            // calibrationPathTextBox
            // 
            this.calibrationPathTextBox.Location = new System.Drawing.Point(39, 46);
            this.calibrationPathTextBox.Name = "calibrationPathTextBox";
            this.calibrationPathTextBox.Size = new System.Drawing.Size(400, 20);
            this.calibrationPathTextBox.TabIndex = 9;
            this.calibrationPathTextBox.Text = "C:\\sbj\\Measurements";
            this.calibrationPathTextBox.TextChanged += new System.EventHandler(this.calibrationPathTextBox_TextChanged);
            // 
            // calibrationBrowseButton
            // 
            this.calibrationBrowseButton.ForeColor = System.Drawing.Color.Black;
            this.calibrationBrowseButton.Location = new System.Drawing.Point(450, 44);
            this.calibrationBrowseButton.Name = "calibrationBrowseButton";
            this.calibrationBrowseButton.Size = new System.Drawing.Size(74, 23);
            this.calibrationBrowseButton.TabIndex = 10;
            this.calibrationBrowseButton.Text = "Browse";
            this.calibrationBrowseButton.UseVisualStyleBackColor = true;
            this.calibrationBrowseButton.Click += new System.EventHandler(this.calibrationBrowseButton_Click);
            // 
            // calibrationSavingFilesCheckBox
            // 
            this.calibrationSavingFilesCheckBox.AutoSize = true;
            this.calibrationSavingFilesCheckBox.ForeColor = System.Drawing.Color.Black;
            this.calibrationSavingFilesCheckBox.Location = new System.Drawing.Point(10, 23);
            this.calibrationSavingFilesCheckBox.Name = "calibrationSavingFilesCheckBox";
            this.calibrationSavingFilesCheckBox.Size = new System.Drawing.Size(78, 17);
            this.calibrationSavingFilesCheckBox.TabIndex = 5;
            this.calibrationSavingFilesCheckBox.Text = "File Saving";
            this.calibrationSavingFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // calibrationFileNumberLabel
            // 
            this.calibrationFileNumberLabel.AutoSize = true;
            this.calibrationFileNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationFileNumberLabel.Location = new System.Drawing.Point(7, 79);
            this.calibrationFileNumberLabel.Name = "calibrationFileNumberLabel";
            this.calibrationFileNumberLabel.Size = new System.Drawing.Size(63, 13);
            this.calibrationFileNumberLabel.TabIndex = 6;
            this.calibrationFileNumberLabel.Text = "File Number";
            // 
            // calibrationPathLabel
            // 
            this.calibrationPathLabel.AutoSize = true;
            this.calibrationPathLabel.ForeColor = System.Drawing.Color.Black;
            this.calibrationPathLabel.Location = new System.Drawing.Point(7, 49);
            this.calibrationPathLabel.Name = "calibrationPathLabel";
            this.calibrationPathLabel.Size = new System.Drawing.Size(29, 13);
            this.calibrationPathLabel.TabIndex = 8;
            this.calibrationPathLabel.Text = "Path";
            // 
            // calibrationCycleNumberNumericUpDown
            // 
            this.calibrationCycleNumberNumericUpDown.Location = new System.Drawing.Point(150, 77);
            this.calibrationCycleNumberNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.calibrationCycleNumberNumericUpDown.Name = "calibrationCycleNumberNumericUpDown";
            this.calibrationCycleNumberNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.calibrationCycleNumberNumericUpDown.TabIndex = 7;
            // 
            // controlPanelsTabPage
            // 
            this.controlPanelsTabPage.AutoScroll = true;
            this.controlPanelsTabPage.Controls.Add(this.electroMagnetGroupBox);
            this.controlPanelsTabPage.Controls.Add(this.stepperMotorGroupBox);
            this.controlPanelsTabPage.Location = new System.Drawing.Point(4, 22);
            this.controlPanelsTabPage.Name = "controlPanelsTabPage";
            this.controlPanelsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.controlPanelsTabPage.Size = new System.Drawing.Size(779, 799);
            this.controlPanelsTabPage.TabIndex = 1;
            this.controlPanelsTabPage.Text = "Control Panels";
            this.controlPanelsTabPage.UseVisualStyleBackColor = true;
            // 
            // electroMagnetGroupBox
            // 
            this.electroMagnetGroupBox.Controls.Add(this.electroMagnetUserControl1);
            this.electroMagnetGroupBox.Location = new System.Drawing.Point(13, 183);
            this.electroMagnetGroupBox.Name = "electroMagnetGroupBox";
            this.electroMagnetGroupBox.Size = new System.Drawing.Size(281, 278);
            this.electroMagnetGroupBox.TabIndex = 2;
            this.electroMagnetGroupBox.TabStop = false;
            this.electroMagnetGroupBox.Text = "ElectroMagnet";
            // 
            // electroMagnetUserControl1
            // 
            this.electroMagnetUserControl1.Location = new System.Drawing.Point(7, 12);
            this.electroMagnetUserControl1.Name = "electroMagnetUserControl1";
            this.electroMagnetUserControl1.Size = new System.Drawing.Size(266, 265);
            this.electroMagnetUserControl1.TabIndex = 0;
            // 
            // stepperMotorGroupBox
            // 
            this.stepperMotorGroupBox.Controls.Add(this.stepperMotorUserControl2);
            this.stepperMotorGroupBox.Location = new System.Drawing.Point(12, 6);
            this.stepperMotorGroupBox.Name = "stepperMotorGroupBox";
            this.stepperMotorGroupBox.Size = new System.Drawing.Size(282, 170);
            this.stepperMotorGroupBox.TabIndex = 1;
            this.stepperMotorGroupBox.TabStop = false;
            this.stepperMotorGroupBox.Text = "Stepper Motor";
            // 
            // stepperMotorUserControl2
            // 
            this.stepperMotorUserControl2.Location = new System.Drawing.Point(6, 19);
            this.stepperMotorUserControl2.Name = "stepperMotorUserControl2";
            this.stepperMotorUserControl2.Size = new System.Drawing.Size(266, 134);
            this.stepperMotorUserControl2.TabIndex = 0;
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
            // ivCyclesBackgroundWorker
            // 
            this.ivCyclesBackgroundWorker.WorkerReportsProgress = true;
            this.ivCyclesBackgroundWorker.WorkerSupportsCancellation = true;
            this.ivCyclesBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ivCyclesBackgroundWorker_DoWork);
            this.ivCyclesBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ivCyclesBackgroundWorker_RunWorkerCompleted);
            // 
            // calibrationBackGroundWorker
            // 
            this.calibrationBackGroundWorker.WorkerReportsProgress = true;
            this.calibrationBackGroundWorker.WorkerSupportsCancellation = true;
            this.calibrationBackGroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calibrationBackGroundWorker_DoWork);
            this.calibrationBackGroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calibrationBackGroundWorker_RunWorkerCompleted);
            // 
            // manualStartBackgroundWorker
            // 
            this.manualStartBackgroundWorker.WorkerSupportsCancellation = true;
            this.manualStartBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.manualStartBackgroundWorker_DoWork);
            this.manualStartBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.manualStartBackgroundWorker_RunWorkerCompleted);
            // 
            // continuousSamplingBackgroundWorker
            // 
            this.continuousSamplingBackgroundWorker.WorkerSupportsCancellation = true;
            this.continuousSamplingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.continuousSamplingBackgroundWorker_DoWork);
            this.continuousSamplingBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.continuousSamplingBackgroundWorker_RunWorkerCompleted);
            // 
            // useDefaultGainCheckBox
            // 
            this.useDefaultGainCheckBox.AutoSize = true;
            this.useDefaultGainCheckBox.ForeColor = System.Drawing.Color.Black;
            this.useDefaultGainCheckBox.Location = new System.Drawing.Point(253, 117);
            this.useDefaultGainCheckBox.Name = "useDefaultGainCheckBox";
            this.useDefaultGainCheckBox.Size = new System.Drawing.Size(161, 17);
            this.useDefaultGainCheckBox.TabIndex = 32;
            this.useDefaultGainCheckBox.Text = "Use E5 Gain for Short Circuit";
            this.useDefaultGainCheckBox.UseVisualStyleBackColor = true;
            // 
            // SBJControllerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(787, 825);
            this.Controls.Add(this.controllerTabControl);
            this.Name = "SBJControllerMainForm";
            this.Text = "SBJControllerMainForm";
            this.Shown += new System.EventHandler(this.SBJControllerMainForm_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SBJControllerMainForm_FormClosed);
            this.controllerTabControl.ResumeLayout(false);
            this.dataAquisitionTabPage.ResumeLayout(false);
            this.dataAquisitionTabPage.PerformLayout();
            this.plotGroupBox.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.secondEOMFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstEOMFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.externalFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laserAmplitudeOnSampleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laserAmplitudeWNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laserAmplitudeNumericUpDown)).EndInit();
            this.LockInSettingsTabPage.ResumeLayout(false);
            this.lockInPanel.ResumeLayout(false);
            this.lockInPanel.PerformLayout();
            this.internalSourceLockInGroupBox.ResumeLayout(false);
            this.internalSourceLockInGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mixerReductionFactorNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lockInAcVoltageNumericEdit)).EndInit();
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
            this.externalEMTabPage.ResumeLayout(false);
            this.externalEMpanel.ResumeLayout(false);
            this.externalEMpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lambdaZupOutputVoltageNumericEdit)).EndInit();
            this.channelsConfigurationTabPage.ResumeLayout(false);
            this.channelsSettingsPanel.ResumeLayout(false);
            this.channelsSettingsPanel.PerformLayout();
            this.ivAcquisition.ResumeLayout(false);
            this.ivAcquisition.PerformLayout();
            this.ivOperateGroupBox.ResumeLayout(false);
            this.ivOperateGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivNumberOfCyclesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivShortCircuitVoltageNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivFileNumberNumericUpDown)).EndInit();
            this.ivPlotGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ivWaveformGraph)).EndInit();
            this.ivSettingsGroupBox.ResumeLayout(false);
            this.ivGeneralTabControl.ResumeLayout(false);
            this.ivGeneralSettingsTabPage.ResumeLayout(false);
            this.ivGeneralSettingsPanel.ResumeLayout(false);
            this.ivGeneralSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivVoltageForTraceNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivTimeOfOneIVCycleNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivOutputUpdateRateNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivVoltageAmplitudeNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivOutputUpdateDelayNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivSamplesPerCycleNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivInputSampleRateNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivTriggerVoltageNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivTriggerConductanceNumericEdit)).EndInit();
            this.ivSteppingMethodTabPage.ResumeLayout(false);
            this.ivSteppingMethodPanel.ResumeLayout(false);
            this.ivSteppingMethodPanel.PerformLayout();
            this.ivElectroMagnetGroupBox.ResumeLayout(false);
            this.ivElectroMagnetGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivEMShortCircuitDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivEMSlowDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivEMFastDelayTimeNumericUpDown)).EndInit();
            this.ivStepperMotorGroupBox.ResumeLayout(false);
            this.ivStepperMotorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ivStepperDelayTime2NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivStepperDelayTime1NumericUpDown)).EndInit();
            this.ivChannelsTabPage.ResumeLayout(false);
            this.ivChannelsPanel.ResumeLayout(false);
            this.ivChannelsPanel.PerformLayout();
            this.calibrationTabPage.ResumeLayout(false);
            this.calibrationTabPage.PerformLayout();
            this.calibrationPlotGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.calibrationWaveformGraph)).EndInit();
            this.calibrationSettingsGroupBox.ResumeLayout(false);
            this.calibrationSettingsTabControl.ResumeLayout(false);
            this.CalibrationGeneralTabPage.ResumeLayout(false);
            this.CalibrationGeneralTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationSampleRateNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationBiasNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationTriggerVoltageNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationTriggerConductanceNumericEdit)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.calibrationElectroMagnetPanel.ResumeLayout(false);
            this.calibrationElectroMagnetPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationEMShortCircuitDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationEMSlowDelayTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationEMFastDelayTimeNumericUpDown)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.calibrationChannelsPanel.ResumeLayout(false);
            this.calibrationChannelsPanel.PerformLayout();
            this.calibrationOperateGroupBox.ResumeLayout(false);
            this.calibrationOperateGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationNumberOfCyclesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationShortCircuitVoltageumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationCycleNumberNumericUpDown)).EndInit();
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
        private System.Windows.Forms.NumericUpDown laserAmplitudeNumericUpDown;
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
        private System.Windows.Forms.Label lockInSensitivityLabel;
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
        private System.Windows.Forms.Label biasErrorLabel;
        private System.Windows.Forms.CheckBox fixBiasCheckBoxButton;
        private System.ComponentModel.BackgroundWorker fixBiasBackgroundWorker;
        private NationalInstruments.UI.WindowsForms.NumericEdit biasErrorNumericEdit;
        private System.Windows.Forms.TabPage channelsConfigurationTabPage;
        private System.Windows.Forms.CheckBox channel0CheckBox;
        private System.Windows.Forms.ComboBox channel3ComboBox;
        private System.Windows.Forms.CheckBox channel3CheckBox;
        private System.Windows.Forms.ComboBox channel2ComboBox;
        private System.Windows.Forms.CheckBox channel2CheckBox;
        private System.Windows.Forms.ComboBox channel1ComboBox;
        private System.Windows.Forms.CheckBox channel1CheckBox;
        private System.Windows.Forms.ComboBox channel0ComboBox;
        private NationalInstruments.UI.WindowsForms.NumericEdit lockInAcVoltageNumericEdit;
        private System.Windows.Forms.Label lockInAcVoltageLabel;
        private System.Windows.Forms.Panel channelsSettingsPanel;
        private System.Windows.Forms.ComboBox sensitivityComboBox;
        private System.Windows.Forms.GroupBox internalSourceLockInGroupBox;
        private System.Windows.Forms.CheckBox internalSourceLockInCheckBoxcheckBox;
        private System.Windows.Forms.Label timeConstantLabel;
        private System.Windows.Forms.ComboBox timeConstantComboBox;
        private System.Windows.Forms.CheckBox enableLockInCheckBox;
        private System.Windows.Forms.ComboBox rollOffComboBox;
        private System.Windows.Forms.Label rollOffLabel;
        private System.Windows.Forms.GroupBox plotGroupBox;
        private System.Windows.Forms.ListView channelsListView;
        private NationalInstruments.UI.WindowsForms.NumericEdit mixerReductionFactorNumericEdit;
        private System.Windows.Forms.Label acVoltageReductionFactorLabel;
        private System.ComponentModel.BackgroundWorker ivCyclesBackgroundWorker;
        private System.Windows.Forms.CheckBox useKeithleyCheckBox;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.TabPage ivAcquisition;
        private System.Windows.Forms.GroupBox ivOperateGroupBox;
        private System.Windows.Forms.Button ivOpenFolderButton;
        private System.Windows.Forms.CheckBox ivStepperUpCheckBox;
        private System.Windows.Forms.CheckBox ivStartStopCheckBox;
        private System.Windows.Forms.CheckBox ivShortCircuitCheckBox;
        private System.Windows.Forms.NumericUpDown ivNumberOfCyclesNumericUpDown;
        private System.Windows.Forms.Label ivNumberOfCyclesLlabel;
        private System.Windows.Forms.NumericUpDown ivShortCircuitVoltageNumericUpDown;
        private System.Windows.Forms.Label ivShortCircuitVoltageLabel;
        private System.Windows.Forms.TextBox ivPathTextBox;
        private System.Windows.Forms.Button ivBrowseButton;
        private System.Windows.Forms.CheckBox ivFileSavingCheckBox;
        private System.Windows.Forms.Label ivFileNumberLabel;
        private System.Windows.Forms.Label ivPathLabel;
        private System.Windows.Forms.NumericUpDown ivFileNumberNumericUpDown;
        private System.Windows.Forms.GroupBox ivPlotGroupBox;
        private System.Windows.Forms.ListView ivChannelsListView;
        private NationalInstruments.UI.WindowsForms.WaveformGraph ivWaveformGraph;
        private NationalInstruments.UI.WaveformPlot ivWaveformPlot;
        private NationalInstruments.UI.XAxis ivXAxis;
        private NationalInstruments.UI.YAxis ivYAxis;
        private System.Windows.Forms.GroupBox ivSettingsGroupBox;
        private System.Windows.Forms.TabControl ivGeneralTabControl;
        private System.Windows.Forms.TabPage ivGeneralSettingsTabPage;
        private System.Windows.Forms.Panel ivGeneralSettingsPanel;
        private System.Windows.Forms.ComboBox ivGainPoweComboBox;
        private System.Windows.Forms.NumericUpDown ivInputSampleRateNumericUpDown;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivTriggerVoltageNumericEdit;
        private System.Windows.Forms.Label ivTriggerVoltageLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivTriggerConductanceNumericEdit;
        private System.Windows.Forms.Label ivTriggerConductanceLabel;
        private System.Windows.Forms.Label ivInputSampleRateLabel;
        private System.Windows.Forms.Label ivGainPowerLabel;
        private System.Windows.Forms.TabPage ivSteppingMethodTabPage;
        private System.Windows.Forms.Panel ivSteppingMethodPanel;
        private System.Windows.Forms.CheckBox ivEMSkipStepperMotorCheckBox;
        private System.Windows.Forms.Label ivEMShortCircuitDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown ivEMShortCircuitDelayTimeNumericUpDown;
        private System.Windows.Forms.NumericUpDown ivEMSlowDelayTimeNumericUpDown;
        private System.Windows.Forms.Label ivEMSlowDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown ivEMFastDelayTimeNumericUpDown;
        private System.Windows.Forms.Label ivEMFastDelayTimeLabel;
        private System.Windows.Forms.TabPage ivChannelsTabPage;
        private System.Windows.Forms.Panel ivChannelsPanel;
        private System.Windows.Forms.ComboBox ivChannel1ComboBox;
        private System.Windows.Forms.ComboBox ivChannel3ComboBox;
        private System.Windows.Forms.CheckBox ivChannel0CheckBox;
        private System.Windows.Forms.CheckBox ivChannel3CheckBox;
        private System.Windows.Forms.ComboBox ivChannel0ComboBox;
        private System.Windows.Forms.ComboBox ivChannel2ComboBox;
        private System.Windows.Forms.CheckBox ivChannel1CheckBox;
        private System.Windows.Forms.CheckBox ivChannel2CheckBox;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivVoltageForTraceNumericEdit;
        private System.Windows.Forms.Label ivVoltageForTraceLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivTimeOfOneIVCycleNumericEdit;
        private System.Windows.Forms.Label ivTimeOfIVCycleLabel;
        private System.Windows.Forms.Label ivOutputUpdateDelayLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivOutputUpdateRateNumericEdit;
        private System.Windows.Forms.Label ivSamplesPerCycleLabel;
        private System.Windows.Forms.Label ivVoltageAmplitudeLabel;
        private System.Windows.Forms.Label ivOutputUpdateRateLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivVoltageAmplitudeNumericEdit;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivOutputUpdateDelayNumericEdit;
        private NationalInstruments.UI.WindowsForms.NumericEdit ivSamplesPerCycleNumericEdit;
        private System.Windows.Forms.RadioButton ivElectroMagnetRadioButton;
        private System.Windows.Forms.GroupBox ivElectroMagnetGroupBox;
        private System.Windows.Forms.RadioButton ivStepperMotorRadioButton;
        private System.Windows.Forms.GroupBox ivStepperMotorGroupBox;
        private System.Windows.Forms.NumericUpDown ivStepperDelayTime2NumericUpDown;
        private System.Windows.Forms.NumericUpDown ivStepperDelayTime1NumericUpDown;
        private System.Windows.Forms.Label ivStepperDelayTime2Label;
        private System.Windows.Forms.Label ivStepperDelayTime1Label;
        private System.Windows.Forms.TabPage calibrationTabPage;
        private System.Windows.Forms.GroupBox calibrationSettingsGroupBox;
        private System.Windows.Forms.TabControl calibrationSettingsTabControl;
        private System.Windows.Forms.TabPage CalibrationGeneralTabPage;
        private System.Windows.Forms.NumericUpDown calibrationSampleRateNumericEdit;
        private System.Windows.Forms.Label calibrationSampleRateLabel;
        private System.Windows.Forms.Label calibrationTriggerVoltagrLabel;
        private System.Windows.Forms.ComboBox calibrationGainPowerComboBox;
        private NationalInstruments.UI.WindowsForms.NumericEdit calibrationBiasNumericEdit;
        private System.Windows.Forms.Label calibrationGainPowerLabel;
        private System.Windows.Forms.Label calibrationBiasLabel;
        private NationalInstruments.UI.WindowsForms.NumericEdit calibrationTriggerVoltageNumericEdit;
        private NationalInstruments.UI.WindowsForms.NumericEdit calibrationTriggerConductanceNumericEdit;
        private System.Windows.Forms.Label calibrationTriggerConductanceLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel calibrationElectroMagnetPanel;
        private System.Windows.Forms.CheckBox calibrationEMSkipShortCircuitByStepperMotorCheckBox;
        private System.Windows.Forms.Label calibrationEMShortCircuitDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown calibrationEMShortCircuitDelayTimeNumericUpDown;
        private System.Windows.Forms.NumericUpDown calibrationEMFastDelayTimeNumericUpDown;
        private System.Windows.Forms.Label calibrationEMFastDelayTimeLabel;
        private System.Windows.Forms.CheckBox calibrationEnableElectroMagnetCheckBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel calibrationChannelsPanel;
        private System.Windows.Forms.CheckBox calibrationChannel1CheckBox;
        private System.Windows.Forms.ComboBox calibrationChannel1ComboBox;
        private System.Windows.Forms.GroupBox calibrationOperateGroupBox;
        private System.Windows.Forms.Button calibrationOpenFolderButton;
        private System.Windows.Forms.CheckBox calibrationStepperUpCheckBox;
        private System.Windows.Forms.CheckBox calibrationStartStopCheckBox;
        private System.Windows.Forms.CheckBox calibrationShortCircuitCheckBox;
        private System.Windows.Forms.NumericUpDown calibrationNumberOfCyclesNumericUpDown;
        private System.Windows.Forms.Label calibrationNumberOfCyclesLabel;
        private System.Windows.Forms.NumericUpDown calibrationShortCircuitVoltageumericUpDown;
        private System.Windows.Forms.Label calibrationShortCircuitVoltageLabel;
        private System.Windows.Forms.TextBox calibrationPathTextBox;
        private System.Windows.Forms.Button calibrationBrowseButton;
        private System.Windows.Forms.CheckBox calibrationSavingFilesCheckBox;
        private System.Windows.Forms.Label calibrationFileNumberLabel;
        private System.Windows.Forms.Label calibrationPathLabel;
        private System.Windows.Forms.NumericUpDown calibrationCycleNumberNumericUpDown;
        private System.ComponentModel.BackgroundWorker calibrationBackGroundWorker;
        private NationalInstruments.UI.WindowsForms.WaveformGraph calibrationWaveformGraph;
        private NationalInstruments.UI.WaveformPlot calibrationWaveformPlot;
        private NationalInstruments.UI.XAxis calibrationXAxis;
        private NationalInstruments.UI.YAxis calibrationYAxis;
        private System.Windows.Forms.CheckBox calibrationKeithleyCheckBox;
        private System.Windows.Forms.CheckBox manualStartCheckBoxButton;
        private System.ComponentModel.BackgroundWorker manualStartBackgroundWorker;
        private System.Windows.Forms.NumericUpDown laserAmplitudeWNumericUpDown;
        private System.Windows.Forms.Label laserAmplitudeWLabel;
        private System.Windows.Forms.NumericUpDown laserAmplitudeOnSampleNumericUpDown;
        private System.Windows.Forms.Label laserAmplitudeOnSampleLabel;
        private System.Windows.Forms.NumericUpDown externalFrequencyNumericUpDown;
        private System.Windows.Forms.Label externalFrequencyLabel;
        private System.Windows.Forms.CheckBox enableFirstEOMcheckBox;
        private System.Windows.Forms.CheckBox enableChopperCheckBox;
        private System.Windows.Forms.GroupBox calibrationPlotGroupBox;
        private System.Windows.Forms.ListView calibrationChannelsListView;
        private System.Windows.Forms.NumericUpDown calibrationDelayTimeNumericUpDown;
        private System.Windows.Forms.Label calibrationDelayTimeLabel;
        private System.Windows.Forms.NumericUpDown calibrationEMSlowDelayTimeNumericUpDown;
        private System.Windows.Forms.Label calibrationEMSlowDelayTimeLabel;
        private System.Windows.Forms.Label calibrationMeasurementsTypeLabel;
        private System.Windows.Forms.TabPage externalEMTabPage;
        private System.Windows.Forms.Panel externalEMpanel;
        private System.Windows.Forms.CheckBox useLambdaZupCheckBox;
        private NationalInstruments.UI.WindowsForms.NumericEdit lambdaZupOutputVoltageNumericEdit;
        private System.Windows.Forms.Label lambdaZupOutputVoltageLabel;
        private System.Windows.Forms.ComboBox calibrationMeasurementTypeComboBox;
        private ElectroMagnetUserControl electroMagnetUserControl1;
        private StepperMotorUserControl stepperMotorUserControl2;
        private System.Windows.Forms.NumericUpDown secondEOMFrequencyNumericUpDown;
        private System.Windows.Forms.Label secondEOMFrequencyLabel;
        private System.Windows.Forms.CheckBox enableSecondEOMCheckBox;
        private System.Windows.Forms.NumericUpDown firstEOMFrequencyNumericUpDown;
        private System.Windows.Forms.Label firstEOMFrequencyLabel;
        private System.Windows.Forms.ComboBox eomCOnfigurationComboBox;
        private System.Windows.Forms.CheckBox continuousSamplingCheckBox;
        private System.ComponentModel.BackgroundWorker continuousSamplingBackgroundWorker;
        private System.Windows.Forms.CheckBox useDefaultGainCheckBox;
                         
    }
}