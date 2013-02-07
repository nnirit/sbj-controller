namespace SBJController
{
    partial class ElectroMagnetUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EMVoltageMeter = new NationalInstruments.UI.WindowsForms.Meter();
            this.runStepsBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.runStepsCheckBox = new System.Windows.Forms.CheckBox();
            this.numberOfStepsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.delayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfStepsLabel = new System.Windows.Forms.Label();
            this.applyVoltageLabel = new System.Windows.Forms.Label();
            this.manualVoltageCheckBox = new System.Windows.Forms.CheckBox();
            this.downLabel = new System.Windows.Forms.Label();
            this.upLabel = new System.Windows.Forms.Label();
            this.EMDirectionSwitch = new NationalInstruments.UI.WindowsForms.Switch();
            this.runContinuouslyCheckBox = new System.Windows.Forms.CheckBox();
            this.runContinuouslyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.manualVoltageNumericEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            ((System.ComponentModel.ISupportInitialize)(this.EMVoltageMeter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfStepsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMDirectionSwitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manualVoltageNumericEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // EMVoltageMeter
            //             
            this.EMVoltageMeter.CoercionInterval = 0.005D;
            this.EMVoltageMeter.CoercionMode = NationalInstruments.UI.NumericCoercionMode.ToInterval;
            this.EMVoltageMeter.DialColor = System.Drawing.SystemColors.ControlLightLight;
            this.EMVoltageMeter.Location = new System.Drawing.Point(42, 169);
            this.EMVoltageMeter.Name = "EMVoltageMeter";
            this.EMVoltageMeter.Range = new NationalInstruments.UI.Range(-10D, 10D);
            this.EMVoltageMeter.Size = new System.Drawing.Size(181, 81);
            this.EMVoltageMeter.SpindleColor = System.Drawing.SystemColors.ControlDark;
            this.EMVoltageMeter.TabIndex = 0;
            // 
            // runStepsBackgroundWorker
            // 
            this.runStepsBackgroundWorker.WorkerSupportsCancellation = true;
            this.runStepsBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.runStepsBackgroundWorker_DoWork);
            this.runStepsBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.runStepsBackgroundWorker_RunWorkerCompleted);
            // 
            // runStepsCheckBox
            // 
            this.runStepsCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.runStepsCheckBox.AutoSize = true;
            this.runStepsCheckBox.Location = new System.Drawing.Point(42, 91);
            this.runStepsCheckBox.Name = "runStepsCheckBox";
            this.runStepsCheckBox.Size = new System.Drawing.Size(67, 23);
            this.runStepsCheckBox.TabIndex = 1;
            this.runStepsCheckBox.Text = "Run Steps";
            this.runStepsCheckBox.UseVisualStyleBackColor = true;
            this.runStepsCheckBox.CheckedChanged += new System.EventHandler(this.runStepsCheckBox_CheckedChanged);
            // 
            // numberOfStepsNumericUpDown
            // 
            this.numberOfStepsNumericUpDown.Location = new System.Drawing.Point(106, 48);
            this.numberOfStepsNumericUpDown.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.numberOfStepsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfStepsNumericUpDown.Name = "numberOfStepsNumericUpDown";
            this.numberOfStepsNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.numberOfStepsNumericUpDown.TabIndex = 2;
            this.numberOfStepsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // delayNumericUpDown
            // 
            this.delayNumericUpDown.Location = new System.Drawing.Point(106, 17);
            this.delayNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.delayNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.delayNumericUpDown.Name = "delayNumericUpDown";
            this.delayNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.delayNumericUpDown.TabIndex = 3;
            this.delayNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Time Delay [ms]";
            // 
            // numberOfStepsLabel
            // 
            this.numberOfStepsLabel.AutoSize = true;
            this.numberOfStepsLabel.Location = new System.Drawing.Point(12, 50);
            this.numberOfStepsLabel.Name = "numberOfStepsLabel";
            this.numberOfStepsLabel.Size = new System.Drawing.Size(86, 13);
            this.numberOfStepsLabel.TabIndex = 5;
            this.numberOfStepsLabel.Text = "Number of Steps";
            // 
            // applyVoltageLabel
            // 
            this.applyVoltageLabel.AutoSize = true;
            this.applyVoltageLabel.Location = new System.Drawing.Point(34, 143);
            this.applyVoltageLabel.Name = "applyVoltageLabel";
            this.applyVoltageLabel.Size = new System.Drawing.Size(88, 13);
            this.applyVoltageLabel.TabIndex = 7;
            this.applyVoltageLabel.Text = "Apply Voltage [V]";
            // 
            // manualVoltageCheckBox
            // 
            this.manualVoltageCheckBox.AutoSize = true;
            this.manualVoltageCheckBox.Location = new System.Drawing.Point(20, 141);
            this.manualVoltageCheckBox.Name = "manualVoltageCheckBox";
            this.manualVoltageCheckBox.Size = new System.Drawing.Size(124, 17);
            this.manualVoltageCheckBox.TabIndex = 11;
            this.manualVoltageCheckBox.Text = "Manual Voltage [mV]";
            this.manualVoltageCheckBox.UseVisualStyleBackColor = true;
            this.manualVoltageCheckBox.CheckedChanged += new System.EventHandler(this.manualVoltageCheckBox_CheckedChanged);
            // 
            // downLabel
            // 
            this.downLabel.AutoSize = true;
            this.downLabel.Location = new System.Drawing.Point(219, 50);
            this.downLabel.Name = "downLabel";
            this.downLabel.Size = new System.Drawing.Size(35, 13);
            this.downLabel.TabIndex = 14;
            this.downLabel.Text = "Down";
            // 
            // upLabel
            // 
            this.upLabel.AutoSize = true;
            this.upLabel.Location = new System.Drawing.Point(219, 24);
            this.upLabel.Name = "upLabel";
            this.upLabel.Size = new System.Drawing.Size(21, 13);
            this.upLabel.TabIndex = 13;
            this.upLabel.Text = "Up";
            // 
            // EMDirectionSwitch
            // 
            this.EMDirectionSwitch.Location = new System.Drawing.Point(176, 6);
            this.EMDirectionSwitch.Name = "EMDirectionSwitch";
            this.EMDirectionSwitch.Size = new System.Drawing.Size(47, 79);
            this.EMDirectionSwitch.SwitchStyle = NationalInstruments.UI.SwitchStyle.VerticalToggle3D;
            this.EMDirectionSwitch.TabIndex = 12;
            this.EMDirectionSwitch.Value = true;
            this.EMDirectionSwitch.StateChanged += new NationalInstruments.UI.ActionEventHandler(this.EMDirectionSwitch_StateChanged);
            // 
            // runContinuouslyCheckBox
            // 
            this.runContinuouslyCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.runContinuouslyCheckBox.AutoSize = true;
            this.runContinuouslyCheckBox.Location = new System.Drawing.Point(126, 91);
            this.runContinuouslyCheckBox.Name = "runContinuouslyCheckBox";
            this.runContinuouslyCheckBox.Size = new System.Drawing.Size(100, 23);
            this.runContinuouslyCheckBox.TabIndex = 15;
            this.runContinuouslyCheckBox.Text = "Run Continuously";
            this.runContinuouslyCheckBox.UseVisualStyleBackColor = true;
            this.runContinuouslyCheckBox.CheckedChanged += new System.EventHandler(this.runContinuouslyCheckBox_CheckedChanged);
            // 
            // runContinuouslyBackgroundWorker
            // 
            this.runContinuouslyBackgroundWorker.WorkerSupportsCancellation = true;
            this.runContinuouslyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.runContinuouslyBackgroundWorker_DoWork);
            this.runContinuouslyBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.runContinuouslyBackgroundWorker_RunWorkerCompleted);
            // 
            // manualVoltageNumericEdit
            // 
            this.manualVoltageNumericEdit.CoercionInterval = 0.005D;
            this.manualVoltageNumericEdit.CoercionMode = NationalInstruments.UI.NumericCoercionMode.ToInterval;
            this.manualVoltageNumericEdit.Enabled = false;
            this.manualVoltageNumericEdit.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(3);
            this.manualVoltageNumericEdit.Location = new System.Drawing.Point(169, 138);
            this.manualVoltageNumericEdit.Name = "manualVoltageNumericEdit";
            this.manualVoltageNumericEdit.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange;
            this.manualVoltageNumericEdit.Range = new NationalInstruments.UI.Range(-10D, 10D);
            this.manualVoltageNumericEdit.Size = new System.Drawing.Size(71, 20);
            this.manualVoltageNumericEdit.TabIndex = 16;
            this.manualVoltageNumericEdit.AfterChangeValue += new NationalInstruments.UI.AfterChangeNumericValueEventHandler(this.manualVoltageNumericEdit_AfterChangeValue);
            // 
            // ElectroMagnetUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.manualVoltageNumericEdit);
            this.Controls.Add(this.runContinuouslyCheckBox);
            this.Controls.Add(this.downLabel);
            this.Controls.Add(this.upLabel);
            this.Controls.Add(this.EMDirectionSwitch);
            this.Controls.Add(this.manualVoltageCheckBox);
            this.Controls.Add(this.applyVoltageLabel);
            this.Controls.Add(this.numberOfStepsLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayNumericUpDown);
            this.Controls.Add(this.numberOfStepsNumericUpDown);
            this.Controls.Add(this.runStepsCheckBox);
            this.Controls.Add(this.EMVoltageMeter);
            this.Name = "ElectroMagnetUserControl";
            this.Size = new System.Drawing.Size(266, 265);
            ((System.ComponentModel.ISupportInitialize)(this.EMVoltageMeter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfStepsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMDirectionSwitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manualVoltageNumericEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NationalInstruments.UI.WindowsForms.Meter EMVoltageMeter;
        private System.ComponentModel.BackgroundWorker runStepsBackgroundWorker;
        private System.Windows.Forms.CheckBox runStepsCheckBox;
        private System.Windows.Forms.NumericUpDown numberOfStepsNumericUpDown;
        private System.Windows.Forms.NumericUpDown delayNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label numberOfStepsLabel;
        private System.Windows.Forms.Label applyVoltageLabel;
        private System.Windows.Forms.CheckBox manualVoltageCheckBox;
        private System.Windows.Forms.Label downLabel;
        private System.Windows.Forms.Label upLabel;
        private NationalInstruments.UI.WindowsForms.Switch EMDirectionSwitch;
        private System.Windows.Forms.CheckBox runContinuouslyCheckBox;
        private System.ComponentModel.BackgroundWorker runContinuouslyBackgroundWorker;
        private NationalInstruments.UI.WindowsForms.NumericEdit manualVoltageNumericEdit;

    }
}
