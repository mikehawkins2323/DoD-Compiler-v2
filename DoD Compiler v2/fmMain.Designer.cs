namespace DoD_Compiler_v2
{
    partial class fmMain
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.chkCombineAreas = new System.Windows.Forms.CheckBox();
            this.gpAreas = new System.Windows.Forms.GroupBox();
            this.chkUSalaska = new System.Windows.Forms.CheckBox();
            this.chkEEA = new System.Windows.Forms.CheckBox();
            this.chkUS = new System.Windows.Forms.CheckBox();
            this.chkPAA = new System.Windows.Forms.CheckBox();
            this.chkENAME = new System.Windows.Forms.CheckBox();
            this.chkCSA = new System.Windows.Forms.CheckBox();
            this.chkCNA = new System.Windows.Forms.CheckBox();
            this.chkAFR = new System.Windows.Forms.CheckBox();
            this.lblAuthorStatic = new System.Windows.Forms.Label();
            this.txtAuthorName = new System.Windows.Forms.TextBox();
            this.dtpEffectiveTo = new System.Windows.Forms.DateTimePicker();
            this.lblEffectiveStatic = new System.Windows.Forms.Label();
            this.lblVersionStatic = new System.Windows.Forms.Label();
            this.dtpEffectiveFrom = new System.Windows.Forms.DateTimePicker();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtSaveLoc = new System.Windows.Forms.TextBox();
            this.btnChooseSave = new System.Windows.Forms.Button();
            this.txtDiscLocation = new System.Windows.Forms.TextBox();
            this.btnFindDisc = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.backwkrCompileDoc = new System.ComponentModel.BackgroundWorker();
            this.gpAreas.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(192, 336);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 25);
            this.btnCreate.TabIndex = 27;
            this.btnCreate.Text = "CREATE";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // chkCombineAreas
            // 
            this.chkCombineAreas.AutoSize = true;
            this.chkCombineAreas.Enabled = false;
            this.chkCombineAreas.Location = new System.Drawing.Point(21, 309);
            this.chkCombineAreas.Name = "chkCombineAreas";
            this.chkCombineAreas.Size = new System.Drawing.Size(238, 21);
            this.chkCombineAreas.TabIndex = 26;
            this.chkCombineAreas.Text = "Combine areas in one document.";
            this.chkCombineAreas.UseVisualStyleBackColor = true;
            // 
            // gpAreas
            // 
            this.gpAreas.Controls.Add(this.chkUSalaska);
            this.gpAreas.Controls.Add(this.chkEEA);
            this.gpAreas.Controls.Add(this.chkUS);
            this.gpAreas.Controls.Add(this.chkPAA);
            this.gpAreas.Controls.Add(this.chkENAME);
            this.gpAreas.Controls.Add(this.chkCSA);
            this.gpAreas.Controls.Add(this.chkCNA);
            this.gpAreas.Controls.Add(this.chkAFR);
            this.gpAreas.Location = new System.Drawing.Point(15, 180);
            this.gpAreas.Name = "gpAreas";
            this.gpAreas.Size = new System.Drawing.Size(252, 128);
            this.gpAreas.TabIndex = 25;
            this.gpAreas.TabStop = false;
            this.gpAreas.Text = "Areas";
            // 
            // chkUSalaska
            // 
            this.chkUSalaska.AutoSize = true;
            this.chkUSalaska.Location = new System.Drawing.Point(140, 102);
            this.chkUSalaska.Name = "chkUSalaska";
            this.chkUSalaska.Size = new System.Drawing.Size(105, 21);
            this.chkUSalaska.TabIndex = 17;
            this.chkUSalaska.Tag = "7";
            this.chkUSalaska.Text = "US (Alaska)";
            this.chkUSalaska.UseVisualStyleBackColor = true;
            this.chkUSalaska.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkEEA
            // 
            this.chkEEA.AutoSize = true;
            this.chkEEA.Location = new System.Drawing.Point(6, 102);
            this.chkEEA.Name = "chkEEA";
            this.chkEEA.Size = new System.Drawing.Size(57, 21);
            this.chkEEA.TabIndex = 16;
            this.chkEEA.Tag = "3";
            this.chkEEA.Text = "EEA";
            this.chkEEA.UseVisualStyleBackColor = true;
            this.chkEEA.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkUS
            // 
            this.chkUS.AutoSize = true;
            this.chkUS.Location = new System.Drawing.Point(140, 75);
            this.chkUS.Name = "chkUS";
            this.chkUS.Size = new System.Drawing.Size(49, 21);
            this.chkUS.TabIndex = 15;
            this.chkUS.Tag = "6";
            this.chkUS.Text = "US";
            this.chkUS.UseVisualStyleBackColor = true;
            this.chkUS.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkPAA
            // 
            this.chkPAA.AutoSize = true;
            this.chkPAA.Location = new System.Drawing.Point(140, 48);
            this.chkPAA.Name = "chkPAA";
            this.chkPAA.Size = new System.Drawing.Size(57, 21);
            this.chkPAA.TabIndex = 14;
            this.chkPAA.Tag = "5";
            this.chkPAA.Text = "PAA";
            this.chkPAA.UseVisualStyleBackColor = true;
            this.chkPAA.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkENAME
            // 
            this.chkENAME.AutoSize = true;
            this.chkENAME.Location = new System.Drawing.Point(140, 23);
            this.chkENAME.Name = "chkENAME";
            this.chkENAME.Size = new System.Drawing.Size(78, 21);
            this.chkENAME.TabIndex = 13;
            this.chkENAME.Tag = "4";
            this.chkENAME.Text = "ENAME";
            this.chkENAME.UseVisualStyleBackColor = true;
            this.chkENAME.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkCSA
            // 
            this.chkCSA.AutoSize = true;
            this.chkCSA.Location = new System.Drawing.Point(6, 75);
            this.chkCSA.Name = "chkCSA";
            this.chkCSA.Size = new System.Drawing.Size(57, 21);
            this.chkCSA.TabIndex = 13;
            this.chkCSA.Tag = "2";
            this.chkCSA.Text = "CSA";
            this.chkCSA.UseVisualStyleBackColor = true;
            this.chkCSA.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkCNA
            // 
            this.chkCNA.AutoSize = true;
            this.chkCNA.Location = new System.Drawing.Point(6, 48);
            this.chkCNA.Name = "chkCNA";
            this.chkCNA.Size = new System.Drawing.Size(86, 21);
            this.chkCNA.TabIndex = 13;
            this.chkCNA.Tag = "1";
            this.chkCNA.Text = "CANADA";
            this.chkCNA.UseVisualStyleBackColor = true;
            this.chkCNA.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // chkAFR
            // 
            this.chkAFR.AutoSize = true;
            this.chkAFR.Location = new System.Drawing.Point(6, 21);
            this.chkAFR.Name = "chkAFR";
            this.chkAFR.Size = new System.Drawing.Size(57, 21);
            this.chkAFR.TabIndex = 12;
            this.chkAFR.Tag = "0";
            this.chkAFR.Text = "AFR";
            this.chkAFR.UseVisualStyleBackColor = true;
            this.chkAFR.CheckedChanged += new System.EventHandler(this.chkAreas_CheckedChanged);
            // 
            // lblAuthorStatic
            // 
            this.lblAuthorStatic.AutoSize = true;
            this.lblAuthorStatic.Location = new System.Drawing.Point(12, 155);
            this.lblAuthorStatic.Name = "lblAuthorStatic";
            this.lblAuthorStatic.Size = new System.Drawing.Size(50, 17);
            this.lblAuthorStatic.TabIndex = 24;
            this.lblAuthorStatic.Text = "Author";
            // 
            // txtAuthorName
            // 
            this.txtAuthorName.Location = new System.Drawing.Point(80, 152);
            this.txtAuthorName.Name = "txtAuthorName";
            this.txtAuthorName.Size = new System.Drawing.Size(187, 22);
            this.txtAuthorName.TabIndex = 23;
            // 
            // dtpEffectiveTo
            // 
            this.dtpEffectiveTo.CustomFormat = "dd MMM yy";
            this.dtpEffectiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectiveTo.Location = new System.Drawing.Point(80, 124);
            this.dtpEffectiveTo.Name = "dtpEffectiveTo";
            this.dtpEffectiveTo.Size = new System.Drawing.Size(187, 22);
            this.dtpEffectiveTo.TabIndex = 22;
            // 
            // lblEffectiveStatic
            // 
            this.lblEffectiveStatic.AutoSize = true;
            this.lblEffectiveStatic.Location = new System.Drawing.Point(12, 99);
            this.lblEffectiveStatic.Name = "lblEffectiveStatic";
            this.lblEffectiveStatic.Size = new System.Drawing.Size(62, 17);
            this.lblEffectiveStatic.TabIndex = 21;
            this.lblEffectiveStatic.Text = "Effective";
            // 
            // lblVersionStatic
            // 
            this.lblVersionStatic.AutoSize = true;
            this.lblVersionStatic.Location = new System.Drawing.Point(12, 71);
            this.lblVersionStatic.Name = "lblVersionStatic";
            this.lblVersionStatic.Size = new System.Drawing.Size(56, 17);
            this.lblVersionStatic.TabIndex = 20;
            this.lblVersionStatic.Text = "Version";
            // 
            // dtpEffectiveFrom
            // 
            this.dtpEffectiveFrom.CustomFormat = "dd MMM yy";
            this.dtpEffectiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectiveFrom.Location = new System.Drawing.Point(80, 96);
            this.dtpEffectiveFrom.Name = "dtpEffectiveFrom";
            this.dtpEffectiveFrom.Size = new System.Drawing.Size(187, 22);
            this.dtpEffectiveFrom.TabIndex = 19;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(80, 68);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(187, 22);
            this.txtVersion.TabIndex = 18;
            // 
            // txtSaveLoc
            // 
            this.txtSaveLoc.ForeColor = System.Drawing.Color.DarkGray;
            this.txtSaveLoc.Location = new System.Drawing.Point(12, 40);
            this.txtSaveLoc.Name = "txtSaveLoc";
            this.txtSaveLoc.Size = new System.Drawing.Size(215, 22);
            this.txtSaveLoc.TabIndex = 17;
            this.txtSaveLoc.Tag = "Save Location";
            this.txtSaveLoc.Text = "Save Location";
            this.txtSaveLoc.Enter += new System.EventHandler(this.txtDirectoriesSel_Enter);
            this.txtSaveLoc.Leave += new System.EventHandler(this.txtDirectoriesSel_Leave);
            // 
            // btnChooseSave
            // 
            this.btnChooseSave.Location = new System.Drawing.Point(233, 40);
            this.btnChooseSave.Name = "btnChooseSave";
            this.btnChooseSave.Size = new System.Drawing.Size(34, 23);
            this.btnChooseSave.TabIndex = 16;
            this.btnChooseSave.Text = "...";
            this.btnChooseSave.UseVisualStyleBackColor = true;
            this.btnChooseSave.Click += new System.EventHandler(this.btnChooseSave_Click);
            // 
            // txtDiscLocation
            // 
            this.txtDiscLocation.ForeColor = System.Drawing.Color.DarkGray;
            this.txtDiscLocation.Location = new System.Drawing.Point(12, 12);
            this.txtDiscLocation.Name = "txtDiscLocation";
            this.txtDiscLocation.Size = new System.Drawing.Size(215, 22);
            this.txtDiscLocation.TabIndex = 15;
            this.txtDiscLocation.Tag = "Disc Location";
            this.txtDiscLocation.Text = "Disc Location";
            this.txtDiscLocation.Enter += new System.EventHandler(this.txtDirectoriesSel_Enter);
            this.txtDiscLocation.Leave += new System.EventHandler(this.txtDirectoriesSel_Leave);
            // 
            // btnFindDisc
            // 
            this.btnFindDisc.Location = new System.Drawing.Point(233, 12);
            this.btnFindDisc.Name = "btnFindDisc";
            this.btnFindDisc.Size = new System.Drawing.Size(34, 23);
            this.btnFindDisc.TabIndex = 14;
            this.btnFindDisc.Text = "...";
            this.btnFindDisc.UseVisualStyleBackColor = true;
            this.btnFindDisc.Click += new System.EventHandler(this.btnFindDisc_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "To";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 340);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 17);
            this.lblProgress.TabIndex = 29;
            // 
            // btnInfo
            // 
            this.btnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.Location = new System.Drawing.Point(161, 336);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(25, 25);
            this.btnInfo.TabIndex = 30;
            this.btnInfo.Text = "?";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // backwkrCompileDoc
            // 
            this.backwkrCompileDoc.WorkerReportsProgress = true;
            this.backwkrCompileDoc.WorkerSupportsCancellation = true;
            this.backwkrCompileDoc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backwkrCompileDoc_DoWork);
            this.backwkrCompileDoc.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backwkrCompileDoc_ProgressChanged);
            this.backwkrCompileDoc.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backwkrCompileDoc_RunWorkerCompleted);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 373);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.chkCombineAreas);
            this.Controls.Add(this.gpAreas);
            this.Controls.Add(this.lblAuthorStatic);
            this.Controls.Add(this.txtAuthorName);
            this.Controls.Add(this.dtpEffectiveTo);
            this.Controls.Add(this.lblEffectiveStatic);
            this.Controls.Add(this.lblVersionStatic);
            this.Controls.Add(this.dtpEffectiveFrom);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtSaveLoc);
            this.Controls.Add(this.btnChooseSave);
            this.Controls.Add(this.txtDiscLocation);
            this.Controls.Add(this.btnFindDisc);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmMain";
            this.Text = "DoD Compiler";
            this.gpAreas.ResumeLayout(false);
            this.gpAreas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.CheckBox chkCombineAreas;
        private System.Windows.Forms.GroupBox gpAreas;
        private System.Windows.Forms.CheckBox chkUS;
        private System.Windows.Forms.CheckBox chkPAA;
        private System.Windows.Forms.CheckBox chkENAME;
        private System.Windows.Forms.CheckBox chkCSA;
        private System.Windows.Forms.CheckBox chkCNA;
        private System.Windows.Forms.CheckBox chkAFR;
        private System.Windows.Forms.Label lblAuthorStatic;
        private System.Windows.Forms.TextBox txtAuthorName;
        private System.Windows.Forms.DateTimePicker dtpEffectiveTo;
        private System.Windows.Forms.Button btnFindDisc;
        private System.Windows.Forms.TextBox txtDiscLocation;
        private System.Windows.Forms.Button btnChooseSave;
        private System.Windows.Forms.TextBox txtSaveLoc;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.DateTimePicker dtpEffectiveFrom;
        private System.Windows.Forms.Label lblVersionStatic;
        private System.Windows.Forms.Label lblEffectiveStatic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.CheckBox chkEEA;
        private System.Windows.Forms.CheckBox chkUSalaska;
        private System.Windows.Forms.Button btnInfo;
        private System.ComponentModel.BackgroundWorker backwkrCompileDoc;
    }
}

