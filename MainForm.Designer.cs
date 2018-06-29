namespace RedBASE_Config
{
    partial class MainForm
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
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.portNamesGroup = new System.Windows.Forms.GroupBox();
            this.openClosePortBtn = new System.Windows.Forms.Button();
            this.portNameCbx = new System.Windows.Forms.ComboBox();
            this.deviceInfoGroup = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deviceInfoTxb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialTxb = new System.Windows.Forms.TextBox();
            this.buoyAddressGroup = new System.Windows.Forms.GroupBox();
            this.applyBuoyAddrBtn = new System.Windows.Forms.Button();
            this.buoyAddrCbx = new System.Windows.Forms.ComboBox();
            this.webLink = new System.Windows.Forms.LinkLabel();
            this.mainStatusStrip.SuspendLayout();
            this.portNamesGroup.SuspendLayout();
            this.deviceInfoGroup.SuspendLayout();
            this.buoyAddressGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 427);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(689, 25);
            this.mainStatusStrip.TabIndex = 0;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(34, 20);
            this.statusLbl.Text = "Idle";
            // 
            // portNamesGroup
            // 
            this.portNamesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portNamesGroup.Controls.Add(this.openClosePortBtn);
            this.portNamesGroup.Controls.Add(this.portNameCbx);
            this.portNamesGroup.Location = new System.Drawing.Point(12, 12);
            this.portNamesGroup.Name = "portNamesGroup";
            this.portNamesGroup.Size = new System.Drawing.Size(665, 80);
            this.portNamesGroup.TabIndex = 1;
            this.portNamesGroup.TabStop = false;
            this.portNamesGroup.Text = "Port";
            // 
            // openClosePortBtn
            // 
            this.openClosePortBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openClosePortBtn.Enabled = false;
            this.openClosePortBtn.Location = new System.Drawing.Point(549, 31);
            this.openClosePortBtn.Name = "openClosePortBtn";
            this.openClosePortBtn.Size = new System.Drawing.Size(107, 33);
            this.openClosePortBtn.TabIndex = 1;
            this.openClosePortBtn.Text = "Open";
            this.openClosePortBtn.UseVisualStyleBackColor = true;
            this.openClosePortBtn.Click += new System.EventHandler(this.openClosePortBtn_Click);
            // 
            // portNameCbx
            // 
            this.portNameCbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameCbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.portNameCbx.FormattingEnabled = true;
            this.portNameCbx.Location = new System.Drawing.Point(6, 31);
            this.portNameCbx.Name = "portNameCbx";
            this.portNameCbx.Size = new System.Drawing.Size(527, 33);
            this.portNameCbx.TabIndex = 0;
            // 
            // deviceInfoGroup
            // 
            this.deviceInfoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceInfoGroup.Controls.Add(this.label2);
            this.deviceInfoGroup.Controls.Add(this.deviceInfoTxb);
            this.deviceInfoGroup.Controls.Add(this.label1);
            this.deviceInfoGroup.Controls.Add(this.serialTxb);
            this.deviceInfoGroup.Location = new System.Drawing.Point(12, 118);
            this.deviceInfoGroup.Name = "deviceInfoGroup";
            this.deviceInfoGroup.Size = new System.Drawing.Size(665, 158);
            this.deviceInfoGroup.TabIndex = 2;
            this.deviceInfoGroup.TabStop = false;
            this.deviceInfoGroup.Text = "Device data";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Device info";
            // 
            // deviceInfoTxb
            // 
            this.deviceInfoTxb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceInfoTxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceInfoTxb.Location = new System.Drawing.Point(6, 112);
            this.deviceInfoTxb.Name = "deviceInfoTxb";
            this.deviceInfoTxb.ReadOnly = true;
            this.deviceInfoTxb.Size = new System.Drawing.Size(653, 30);
            this.deviceInfoTxb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial number";
            // 
            // serialTxb
            // 
            this.serialTxb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serialTxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serialTxb.Location = new System.Drawing.Point(6, 55);
            this.serialTxb.Name = "serialTxb";
            this.serialTxb.ReadOnly = true;
            this.serialTxb.Size = new System.Drawing.Size(653, 30);
            this.serialTxb.TabIndex = 0;
            // 
            // buoyAddressGroup
            // 
            this.buoyAddressGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buoyAddressGroup.Controls.Add(this.applyBuoyAddrBtn);
            this.buoyAddressGroup.Controls.Add(this.buoyAddrCbx);
            this.buoyAddressGroup.Enabled = false;
            this.buoyAddressGroup.Location = new System.Drawing.Point(12, 298);
            this.buoyAddressGroup.Name = "buoyAddressGroup";
            this.buoyAddressGroup.Size = new System.Drawing.Size(665, 80);
            this.buoyAddressGroup.TabIndex = 2;
            this.buoyAddressGroup.TabStop = false;
            this.buoyAddressGroup.Text = "Buoy address";
            // 
            // applyBuoyAddrBtn
            // 
            this.applyBuoyAddrBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyBuoyAddrBtn.Location = new System.Drawing.Point(549, 31);
            this.applyBuoyAddrBtn.Name = "applyBuoyAddrBtn";
            this.applyBuoyAddrBtn.Size = new System.Drawing.Size(107, 33);
            this.applyBuoyAddrBtn.TabIndex = 1;
            this.applyBuoyAddrBtn.Text = "Apply";
            this.applyBuoyAddrBtn.UseVisualStyleBackColor = true;
            this.applyBuoyAddrBtn.Click += new System.EventHandler(this.applyBuoyAddrBtn_Click);
            // 
            // buoyAddrCbx
            // 
            this.buoyAddrCbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buoyAddrCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.buoyAddrCbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buoyAddrCbx.FormattingEnabled = true;
            this.buoyAddrCbx.Location = new System.Drawing.Point(6, 31);
            this.buoyAddrCbx.Name = "buoyAddrCbx";
            this.buoyAddrCbx.Size = new System.Drawing.Size(527, 33);
            this.buoyAddrCbx.TabIndex = 0;
            // 
            // webLink
            // 
            this.webLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.webLink.AutoSize = true;
            this.webLink.Location = new System.Drawing.Point(558, 401);
            this.webLink.Name = "webLink";
            this.webLink.Size = new System.Drawing.Size(119, 17);
            this.webLink.TabIndex = 3;
            this.webLink.TabStop = true;
            this.webLink.Text = "www.unavlab.com";
            this.webLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webLink_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 452);
            this.Controls.Add(this.webLink);
            this.Controls.Add(this.buoyAddressGroup);
            this.Controls.Add(this.deviceInfoGroup);
            this.Controls.Add(this.portNamesGroup);
            this.Controls.Add(this.mainStatusStrip);
            this.Name = "MainForm";
            this.Text = "RedBASE Config";
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.portNamesGroup.ResumeLayout(false);
            this.deviceInfoGroup.ResumeLayout(false);
            this.deviceInfoGroup.PerformLayout();
            this.buoyAddressGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.GroupBox portNamesGroup;
        private System.Windows.Forms.Button openClosePortBtn;
        private System.Windows.Forms.ComboBox portNameCbx;
        private System.Windows.Forms.GroupBox deviceInfoGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox deviceInfoTxb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serialTxb;
        private System.Windows.Forms.GroupBox buoyAddressGroup;
        private System.Windows.Forms.Button applyBuoyAddrBtn;
        private System.Windows.Forms.ComboBox buoyAddrCbx;
        private System.Windows.Forms.LinkLabel webLink;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
    }
}

