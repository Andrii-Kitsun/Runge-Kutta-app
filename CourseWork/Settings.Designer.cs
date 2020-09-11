namespace CourseWork
{
    partial class Settings
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
            this.DisableToolStrip = new System.Windows.Forms.CheckBox();
            this.DisableStatusStrip = new System.Windows.Forms.CheckBox();
            this.DisableMenuStrip = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearLogFile = new System.Windows.Forms.Button();
            this.Autosave = new System.Windows.Forms.CheckBox();
            this.Exit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PrecisionCounter = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrecisionCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // DisableToolStrip
            // 
            this.DisableToolStrip.AutoSize = true;
            this.DisableToolStrip.Checked = true;
            this.DisableToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisableToolStrip.Location = new System.Drawing.Point(7, 54);
            this.DisableToolStrip.Name = "DisableToolStrip";
            this.DisableToolStrip.Size = new System.Drawing.Size(178, 23);
            this.DisableToolStrip.TabIndex = 1;
            this.DisableToolStrip.Text = "Панель інструментів";
            this.DisableToolStrip.UseVisualStyleBackColor = true;
            this.DisableToolStrip.CheckedChanged += new System.EventHandler(this.DisableToolStrip_CheckedChanged);
            // 
            // DisableStatusStrip
            // 
            this.DisableStatusStrip.AutoSize = true;
            this.DisableStatusStrip.Checked = true;
            this.DisableStatusStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisableStatusStrip.Location = new System.Drawing.Point(7, 83);
            this.DisableStatusStrip.Name = "DisableStatusStrip";
            this.DisableStatusStrip.Size = new System.Drawing.Size(125, 23);
            this.DisableStatusStrip.TabIndex = 2;
            this.DisableStatusStrip.Text = "Панель стану";
            this.DisableStatusStrip.UseVisualStyleBackColor = true;
            this.DisableStatusStrip.CheckedChanged += new System.EventHandler(this.DisableStatusStrip_CheckedChanged);
            // 
            // DisableMenuStrip
            // 
            this.DisableMenuStrip.AutoSize = true;
            this.DisableMenuStrip.Checked = true;
            this.DisableMenuStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisableMenuStrip.Location = new System.Drawing.Point(7, 25);
            this.DisableMenuStrip.Name = "DisableMenuStrip";
            this.DisableMenuStrip.Size = new System.Drawing.Size(132, 23);
            this.DisableMenuStrip.TabIndex = 3;
            this.DisableMenuStrip.Text = "Головне меню";
            this.DisableMenuStrip.UseVisualStyleBackColor = true;
            this.DisableMenuStrip.CheckedChanged += new System.EventHandler(this.DisableMenuStrip_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ClearLogFile);
            this.groupBox1.Controls.Add(this.Autosave);
            this.groupBox1.Controls.Add(this.DisableMenuStrip);
            this.groupBox1.Controls.Add(this.DisableToolStrip);
            this.groupBox1.Controls.Add(this.DisableStatusStrip);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 142);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Панелі";
            // 
            // ClearLogFile
            // 
            this.ClearLogFile.Location = new System.Drawing.Point(154, 109);
            this.ClearLogFile.Name = "ClearLogFile";
            this.ClearLogFile.Size = new System.Drawing.Size(97, 26);
            this.ClearLogFile.TabIndex = 5;
            this.ClearLogFile.Text = "Очистити";
            this.ClearLogFile.UseVisualStyleBackColor = true;
            this.ClearLogFile.Click += new System.EventHandler(this.ClearLogFile_Click);
            // 
            // Autosave
            // 
            this.Autosave.AutoSize = true;
            this.Autosave.Location = new System.Drawing.Point(6, 112);
            this.Autosave.Name = "Autosave";
            this.Autosave.Size = new System.Drawing.Size(150, 23);
            this.Autosave.TabIndex = 4;
            this.Autosave.Text = "Автозбереження";
            this.Autosave.UseVisualStyleBackColor = true;
            this.Autosave.CheckedChanged += new System.EventHandler(this.Autosave_CheckedChanged);
            // 
            // Exit
            // 
            this.Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Exit.Location = new System.Drawing.Point(101, 225);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(79, 28);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "Закрити";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.PrecisionCounter);
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 59);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Кількість знаків після коми";
            // 
            // PrecisionCounter
            // 
            this.PrecisionCounter.Location = new System.Drawing.Point(7, 25);
            this.PrecisionCounter.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PrecisionCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PrecisionCounter.Name = "PrecisionCounter";
            this.PrecisionCounter.ReadOnly = true;
            this.PrecisionCounter.Size = new System.Drawing.Size(82, 26);
            this.PrecisionCounter.TabIndex = 0;
            this.PrecisionCounter.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.PrecisionCounter.ValueChanged += new System.EventHandler(this.PrecisionCounter_ValueChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit;
            this.ClientSize = new System.Drawing.Size(281, 261);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштування";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PrecisionCounter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox DisableToolStrip;
        private System.Windows.Forms.CheckBox DisableStatusStrip;
        private System.Windows.Forms.CheckBox DisableMenuStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown PrecisionCounter;
        private System.Windows.Forms.CheckBox Autosave;
        private System.Windows.Forms.Button ClearLogFile;
    }
}