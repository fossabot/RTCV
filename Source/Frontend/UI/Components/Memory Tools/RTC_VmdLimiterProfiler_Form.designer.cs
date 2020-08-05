﻿namespace RTCV.UI
{
    partial class RTC_VmdLimiterProfiler_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTC_VmdLimiterProfiler_Form));
            this.pnLimiterList = new System.Windows.Forms.Panel();
            this.cbVectorLimiterList = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbVmdName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerateVMD = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbEndianTypeValue = new System.Windows.Forms.Label();
            this.lbWordSizeValue = new System.Windows.Forms.Label();
            this.lbDomainSizeValue = new System.Windows.Forms.Label();
            this.lbEndianTypeLabel = new System.Windows.Forms.Label();
            this.lbWordSizeLabel = new System.Windows.Forms.Label();
            this.lbDomainSizeLabel = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnLoadDomains = new System.Windows.Forms.Button();
            this.cbSelectedMemoryDomain = new System.Windows.Forms.ComboBox();
            this.cbLoadBeforeGenerate = new System.Windows.Forms.CheckBox();
            this.pnLimiterList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnLimiterList
            // 
            this.pnLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnLimiterList.Controls.Add(this.cbVectorLimiterList);
            this.pnLimiterList.Controls.Add(this.label13);
            this.pnLimiterList.ForeColor = System.Drawing.Color.White;
            this.pnLimiterList.Location = new System.Drawing.Point(6, 56);
            this.pnLimiterList.Name = "pnLimiterList";
            this.pnLimiterList.Size = new System.Drawing.Size(216, 51);
            this.pnLimiterList.TabIndex = 135;
            this.pnLimiterList.Tag = "color:dark2";
            // 
            // cbVectorLimiterList
            // 
            this.cbVectorLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbVectorLimiterList.DataSource = ((object)(resources.GetObject("cbVectorLimiterList.DataSource")));
            this.cbVectorLimiterList.DisplayMember = "Name";
            this.cbVectorLimiterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVectorLimiterList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVectorLimiterList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbVectorLimiterList.ForeColor = System.Drawing.Color.White;
            this.cbVectorLimiterList.FormattingEnabled = true;
            this.cbVectorLimiterList.IntegralHeight = false;
            this.cbVectorLimiterList.Location = new System.Drawing.Point(8, 21);
            this.cbVectorLimiterList.MaxDropDownItems = 15;
            this.cbVectorLimiterList.Name = "cbVectorLimiterList";
            this.cbVectorLimiterList.Size = new System.Drawing.Size(200, 21);
            this.cbVectorLimiterList.TabIndex = 78;
            this.cbVectorLimiterList.Tag = "color:normal";
            this.cbVectorLimiterList.ValueMember = "Value";
            this.cbVectorLimiterList.SelectedIndexChanged += new System.EventHandler(this.CbVectorLimiterList_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.Location = new System.Drawing.Point(6, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 79;
            this.label13.Text = "Limiter list:";
            // 
            // tbVmdName
            // 
            this.tbVmdName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVmdName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbVmdName.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbVmdName.ForeColor = System.Drawing.Color.White;
            this.tbVmdName.Location = new System.Drawing.Point(74, 191);
            this.tbVmdName.Name = "tbVmdName";
            this.tbVmdName.Size = new System.Drawing.Size(311, 22);
            this.tbVmdName.TabIndex = 132;
            this.tbVmdName.Tag = "color:dark1";
            this.tbVmdName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 131;
            this.label2.Text = "VMD Name:";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // btnGenerateVMD
            // 
            this.btnGenerateVMD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateVMD.BackColor = System.Drawing.Color.Gray;
            this.btnGenerateVMD.Enabled = false;
            this.btnGenerateVMD.FlatAppearance.BorderSize = 0;
            this.btnGenerateVMD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateVMD.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnGenerateVMD.ForeColor = System.Drawing.Color.White;
            this.btnGenerateVMD.Location = new System.Drawing.Point(6, 220);
            this.btnGenerateVMD.Name = "btnGenerateVMD";
            this.btnGenerateVMD.Size = new System.Drawing.Size(379, 26);
            this.btnGenerateVMD.TabIndex = 124;
            this.btnGenerateVMD.TabStop = false;
            this.btnGenerateVMD.Tag = "color:light1";
            this.btnGenerateVMD.Text = "Generate VMD";
            this.btnGenerateVMD.UseVisualStyleBackColor = false;
            this.btnGenerateVMD.Click += new System.EventHandler(this.btnGenerateVMD_Click);
            this.btnGenerateVMD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbEndianTypeValue);
            this.groupBox1.Controls.Add(this.lbWordSizeValue);
            this.groupBox1.Controls.Add(this.lbDomainSizeValue);
            this.groupBox1.Controls.Add(this.lbEndianTypeLabel);
            this.groupBox1.Controls.Add(this.lbWordSizeLabel);
            this.groupBox1.Controls.Add(this.lbDomainSizeLabel);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(229, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 102);
            this.groupBox1.TabIndex = 123;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Domain summary";
            this.groupBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // lbEndianTypeValue
            // 
            this.lbEndianTypeValue.AutoSize = true;
            this.lbEndianTypeValue.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbEndianTypeValue.ForeColor = System.Drawing.Color.White;
            this.lbEndianTypeValue.Location = new System.Drawing.Point(86, 56);
            this.lbEndianTypeValue.Name = "lbEndianTypeValue";
            this.lbEndianTypeValue.Size = new System.Drawing.Size(42, 13);
            this.lbEndianTypeValue.TabIndex = 92;
            this.lbEndianTypeValue.Text = "#####";
            this.lbEndianTypeValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // lbWordSizeValue
            // 
            this.lbWordSizeValue.AutoSize = true;
            this.lbWordSizeValue.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbWordSizeValue.ForeColor = System.Drawing.Color.White;
            this.lbWordSizeValue.Location = new System.Drawing.Point(86, 37);
            this.lbWordSizeValue.Name = "lbWordSizeValue";
            this.lbWordSizeValue.Size = new System.Drawing.Size(42, 13);
            this.lbWordSizeValue.TabIndex = 91;
            this.lbWordSizeValue.Text = "#####";
            this.lbWordSizeValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // lbDomainSizeValue
            // 
            this.lbDomainSizeValue.AutoSize = true;
            this.lbDomainSizeValue.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbDomainSizeValue.ForeColor = System.Drawing.Color.White;
            this.lbDomainSizeValue.Location = new System.Drawing.Point(86, 20);
            this.lbDomainSizeValue.Name = "lbDomainSizeValue";
            this.lbDomainSizeValue.Size = new System.Drawing.Size(42, 13);
            this.lbDomainSizeValue.TabIndex = 90;
            this.lbDomainSizeValue.Text = "#####";
            this.lbDomainSizeValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // lbEndianTypeLabel
            // 
            this.lbEndianTypeLabel.AutoSize = true;
            this.lbEndianTypeLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbEndianTypeLabel.ForeColor = System.Drawing.Color.White;
            this.lbEndianTypeLabel.Location = new System.Drawing.Point(6, 54);
            this.lbEndianTypeLabel.Name = "lbEndianTypeLabel";
            this.lbEndianTypeLabel.Size = new System.Drawing.Size(72, 13);
            this.lbEndianTypeLabel.TabIndex = 88;
            this.lbEndianTypeLabel.Text = "Endian Type:";
            this.lbEndianTypeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // lbWordSizeLabel
            // 
            this.lbWordSizeLabel.AutoSize = true;
            this.lbWordSizeLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbWordSizeLabel.ForeColor = System.Drawing.Color.White;
            this.lbWordSizeLabel.Location = new System.Drawing.Point(6, 36);
            this.lbWordSizeLabel.Name = "lbWordSizeLabel";
            this.lbWordSizeLabel.Size = new System.Drawing.Size(62, 13);
            this.lbWordSizeLabel.TabIndex = 87;
            this.lbWordSizeLabel.Text = "Word Size:";
            this.lbWordSizeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // lbDomainSizeLabel
            // 
            this.lbDomainSizeLabel.AutoSize = true;
            this.lbDomainSizeLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbDomainSizeLabel.ForeColor = System.Drawing.Color.White;
            this.lbDomainSizeLabel.Location = new System.Drawing.Point(6, 18);
            this.lbDomainSizeLabel.Name = "lbDomainSizeLabel";
            this.lbDomainSizeLabel.Size = new System.Drawing.Size(73, 13);
            this.lbDomainSizeLabel.TabIndex = 86;
            this.lbDomainSizeLabel.Text = "Domain Size:";
            this.lbDomainSizeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(88, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 117;
            this.label17.Text = "Memory Domain";
            this.label17.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // btnLoadDomains
            // 
            this.btnLoadDomains.BackColor = System.Drawing.Color.Gray;
            this.btnLoadDomains.FlatAppearance.BorderSize = 0;
            this.btnLoadDomains.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadDomains.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLoadDomains.ForeColor = System.Drawing.Color.White;
            this.btnLoadDomains.Location = new System.Drawing.Point(6, 5);
            this.btnLoadDomains.Name = "btnLoadDomains";
            this.btnLoadDomains.Size = new System.Drawing.Size(80, 47);
            this.btnLoadDomains.TabIndex = 17;
            this.btnLoadDomains.TabStop = false;
            this.btnLoadDomains.Tag = "color:light1";
            this.btnLoadDomains.Text = "Load Domains";
            this.btnLoadDomains.UseVisualStyleBackColor = false;
            this.btnLoadDomains.Click += new System.EventHandler(this.btnLoadDomains_Click);
            this.btnLoadDomains.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // cbSelectedMemoryDomain
            // 
            this.cbSelectedMemoryDomain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbSelectedMemoryDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedMemoryDomain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSelectedMemoryDomain.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cbSelectedMemoryDomain.ForeColor = System.Drawing.Color.White;
            this.cbSelectedMemoryDomain.FormattingEnabled = true;
            this.cbSelectedMemoryDomain.Location = new System.Drawing.Point(92, 26);
            this.cbSelectedMemoryDomain.Name = "cbSelectedMemoryDomain";
            this.cbSelectedMemoryDomain.Size = new System.Drawing.Size(130, 25);
            this.cbSelectedMemoryDomain.TabIndex = 16;
            this.cbSelectedMemoryDomain.Tag = "color:dark1";
            this.cbSelectedMemoryDomain.SelectedIndexChanged += new System.EventHandler(this.cbSelectedMemoryDomain_SelectedIndexChanged);
            this.cbSelectedMemoryDomain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            // 
            // cbLoadBeforeGenerate
            // 
            this.cbLoadBeforeGenerate.AutoSize = true;
            this.cbLoadBeforeGenerate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbLoadBeforeGenerate.ForeColor = System.Drawing.Color.White;
            this.cbLoadBeforeGenerate.Location = new System.Drawing.Point(6, 168);
            this.cbLoadBeforeGenerate.Name = "cbLoadBeforeGenerate";
            this.cbLoadBeforeGenerate.Size = new System.Drawing.Size(272, 17);
            this.cbLoadBeforeGenerate.TabIndex = 136;
            this.cbLoadBeforeGenerate.Text = "Load Glitch Harvester Savestate Before Generate";
            this.cbLoadBeforeGenerate.UseVisualStyleBackColor = true;
            // 
            // RTC_VmdLimiterProfiler_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(390, 250);
            this.Controls.Add(this.cbLoadBeforeGenerate);
            this.Controls.Add(this.pnLimiterList);
            this.Controls.Add(this.tbVmdName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerateVMD);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnLoadDomains);
            this.Controls.Add(this.cbSelectedMemoryDomain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RTC_VmdLimiterProfiler_Form";
            this.Tag = "color:dark3";
            this.Text = "Limiter Profiler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.Load += new System.EventHandler(this.RTC_VmdLimiterProfiler_Form_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            this.pnLimiterList.ResumeLayout(false);
            this.pnLimiterList.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbSelectedMemoryDomain;
        private System.Windows.Forms.Button btnLoadDomains;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lbWordSizeLabel;
        public System.Windows.Forms.Label lbDomainSizeLabel;
        public System.Windows.Forms.Label lbEndianTypeLabel;
        private System.Windows.Forms.Button btnGenerateVMD;
        public System.Windows.Forms.Label lbDomainSizeValue;
        public System.Windows.Forms.Label lbEndianTypeValue;
        public System.Windows.Forms.Label lbWordSizeValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbVmdName;
        private System.Windows.Forms.Panel pnLimiterList;
        public System.Windows.Forms.ComboBox cbVectorLimiterList;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.CheckBox cbLoadBeforeGenerate;
    }
}