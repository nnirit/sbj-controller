namespace SBJController
{
    partial class StepperMotorUserControl
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
            this.stepperDirectionSwitch = new NationalInstruments.UI.WindowsForms.Switch();
            this.runContinuouslyCheckBox = new System.Windows.Forms.CheckBox();
            this.moveUpLabel = new System.Windows.Forms.Label();
            this.moveDownLabel = new System.Windows.Forms.Label();
            this.halfStepRadioButton = new System.Windows.Forms.RadioButton();
            this.fullStepRadioButton = new System.Windows.Forms.RadioButton();
            this.runCheckBox = new System.Windows.Forms.CheckBox();
            this.numberOfStepsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.runContinuouslyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.runBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.stepperDirectionSwitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfStepsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // stepperDirectionSwitch
            // 
            this.stepperDirectionSwitch.Location = new System.Drawing.Point(89, 0);
            this.stepperDirectionSwitch.Name = "stepperDirectionSwitch";
            this.stepperDirectionSwitch.Size = new System.Drawing.Size(47, 79);
            this.stepperDirectionSwitch.SwitchStyle = NationalInstruments.UI.SwitchStyle.VerticalToggle3D;
            this.stepperDirectionSwitch.TabIndex = 0;
            this.stepperDirectionSwitch.Value = true;
            this.stepperDirectionSwitch.StateChanged += new NationalInstruments.UI.ActionEventHandler(this.stepperDirectionSwitch_StateChanged);
            // 
            // runContinuouslyCheckBox
            // 
            this.runContinuouslyCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.runContinuouslyCheckBox.AutoSize = true;
            this.runContinuouslyCheckBox.Location = new System.Drawing.Point(151, 13);
            this.runContinuouslyCheckBox.Name = "runContinuouslyCheckBox";
            this.runContinuouslyCheckBox.Size = new System.Drawing.Size(100, 23);
            this.runContinuouslyCheckBox.TabIndex = 1;
            this.runContinuouslyCheckBox.Text = "Run Continuously";
            this.runContinuouslyCheckBox.UseVisualStyleBackColor = true;
            this.runContinuouslyCheckBox.CheckedChanged += new System.EventHandler(this.runContinuouslyCheckBox_CheckedChanged);
            // 
            // moveUpLabel
            // 
            this.moveUpLabel.AutoSize = true;
            this.moveUpLabel.Location = new System.Drawing.Point(18, 18);
            this.moveUpLabel.Name = "moveUpLabel";
            this.moveUpLabel.Size = new System.Drawing.Size(51, 13);
            this.moveUpLabel.TabIndex = 2;
            this.moveUpLabel.Text = "Move Up";
            // 
            // moveDownLabel
            // 
            this.moveDownLabel.AutoSize = true;
            this.moveDownLabel.Location = new System.Drawing.Point(18, 44);
            this.moveDownLabel.Name = "moveDownLabel";
            this.moveDownLabel.Size = new System.Drawing.Size(65, 13);
            this.moveDownLabel.TabIndex = 3;
            this.moveDownLabel.Text = "Move Down";
            // 
            // halfStepRadioButton
            // 
            this.halfStepRadioButton.AutoSize = true;
            this.halfStepRadioButton.Location = new System.Drawing.Point(21, 70);
            this.halfStepRadioButton.Name = "halfStepRadioButton";
            this.halfStepRadioButton.Size = new System.Drawing.Size(69, 17);
            this.halfStepRadioButton.TabIndex = 4;
            this.halfStepRadioButton.TabStop = true;
            this.halfStepRadioButton.Text = "Half Step";
            this.halfStepRadioButton.UseVisualStyleBackColor = true;
            this.halfStepRadioButton.CheckedChanged += new System.EventHandler(this.halfStepRadioButton_CheckedChanged);
            // 
            // fullStepRadioButton
            // 
            this.fullStepRadioButton.AutoSize = true;
            this.fullStepRadioButton.Checked = true;
            this.fullStepRadioButton.Location = new System.Drawing.Point(21, 100);
            this.fullStepRadioButton.Name = "fullStepRadioButton";
            this.fullStepRadioButton.Size = new System.Drawing.Size(66, 17);
            this.fullStepRadioButton.TabIndex = 5;
            this.fullStepRadioButton.TabStop = true;
            this.fullStepRadioButton.Text = "Full Step";
            this.fullStepRadioButton.UseVisualStyleBackColor = true;
            this.fullStepRadioButton.CheckedChanged += new System.EventHandler(this.fullStepRadioButton_CheckedChanged);
            // 
            // runCheckBox
            // 
            this.runCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.runCheckBox.AutoSize = true;
            this.runCheckBox.Location = new System.Drawing.Point(151, 67);
            this.runCheckBox.Name = "runCheckBox";
            this.runCheckBox.Size = new System.Drawing.Size(100, 23);
            this.runCheckBox.TabIndex = 6;
            this.runCheckBox.Text = "     Run Steps      ";
            this.runCheckBox.UseVisualStyleBackColor = true;
            this.runCheckBox.CheckedChanged += new System.EventHandler(this.runCheckBox_CheckedChanged);
            // 
            // numberOfStepsNumericUpDown
            // 
            this.numberOfStepsNumericUpDown.Location = new System.Drawing.Point(151, 100);
            this.numberOfStepsNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numberOfStepsNumericUpDown.Name = "numberOfStepsNumericUpDown";
            this.numberOfStepsNumericUpDown.Size = new System.Drawing.Size(100, 20);
            this.numberOfStepsNumericUpDown.TabIndex = 7;
            // 
            // runContinuouslyBackgroundWorker
            // 
            this.runContinuouslyBackgroundWorker.WorkerReportsProgress = true;
            this.runContinuouslyBackgroundWorker.WorkerSupportsCancellation = true;
            this.runContinuouslyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.runContinuouslyBackgroundWorker_DoWork);
            this.runContinuouslyBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.runContinuouslyBackgroundWorker_RunWorkerCompleted);
            // 
            // runBackgroundWorker
            // 
            this.runBackgroundWorker.WorkerSupportsCancellation = true;
            this.runBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.runBackgroundWorker_DoWork);
            this.runBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.runBackgroundWorker_RunWorkerCompleted);
            // 
            // StepperMotorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numberOfStepsNumericUpDown);
            this.Controls.Add(this.runCheckBox);
            this.Controls.Add(this.fullStepRadioButton);
            this.Controls.Add(this.halfStepRadioButton);
            this.Controls.Add(this.moveDownLabel);
            this.Controls.Add(this.moveUpLabel);
            this.Controls.Add(this.runContinuouslyCheckBox);
            this.Controls.Add(this.stepperDirectionSwitch);
            this.Name = "StepperMotorUserControl";
            this.Size = new System.Drawing.Size(266, 134);
            ((System.ComponentModel.ISupportInitialize)(this.stepperDirectionSwitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfStepsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NationalInstruments.UI.WindowsForms.Switch stepperDirectionSwitch;
        private System.Windows.Forms.CheckBox runContinuouslyCheckBox;
        private System.Windows.Forms.Label moveUpLabel;
        private System.Windows.Forms.Label moveDownLabel;
        private System.Windows.Forms.RadioButton halfStepRadioButton;
        private System.Windows.Forms.RadioButton fullStepRadioButton;
        private System.Windows.Forms.CheckBox runCheckBox;
        private System.Windows.Forms.NumericUpDown numberOfStepsNumericUpDown;
        private System.ComponentModel.BackgroundWorker runContinuouslyBackgroundWorker;
        private System.ComponentModel.BackgroundWorker runBackgroundWorker;
    }
}
