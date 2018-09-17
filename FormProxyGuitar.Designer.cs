namespace ProxyGuitar {
    partial class FormProxyGuitar {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cmbUSBDevices = new System.Windows.Forms.ComboBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnUsbipForceClose = new System.Windows.Forms.Button();
            this.btnUsbipRun = new System.Windows.Forms.Button();
            this.chkUsbipShow = new System.Windows.Forms.CheckBox();
            this.groupUsbip = new System.Windows.Forms.GroupBox();
            this.btnProfile = new System.Windows.Forms.Button();
            this.groupUsbip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbUSBDevices
            // 
            this.cmbUSBDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUSBDevices.FormattingEnabled = true;
            this.cmbUSBDevices.Location = new System.Drawing.Point(12, 12);
            this.cmbUSBDevices.Name = "cmbUSBDevices";
            this.cmbUSBDevices.Size = new System.Drawing.Size(376, 21);
            this.cmbUSBDevices.TabIndex = 0;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(12, 39);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(92, 23);
            this.btnServer.TabIndex = 1;
            this.btnServer.Text = "Server Status";
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 68);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(594, 220);
            this.txtLog.TabIndex = 2;
            // 
            // btnUsbipForceClose
            // 
            this.btnUsbipForceClose.Location = new System.Drawing.Point(131, 15);
            this.btnUsbipForceClose.Name = "btnUsbipForceClose";
            this.btnUsbipForceClose.Size = new System.Drawing.Size(70, 23);
            this.btnUsbipForceClose.TabIndex = 3;
            this.btnUsbipForceClose.Text = "Disconnect";
            this.btnUsbipForceClose.UseVisualStyleBackColor = true;
            this.btnUsbipForceClose.Click += new System.EventHandler(this.btnUsbipForceClose_Click);
            // 
            // btnUsbipRun
            // 
            this.btnUsbipRun.Location = new System.Drawing.Point(65, 15);
            this.btnUsbipRun.Name = "btnUsbipRun";
            this.btnUsbipRun.Size = new System.Drawing.Size(60, 23);
            this.btnUsbipRun.TabIndex = 4;
            this.btnUsbipRun.Text = "Connect";
            this.btnUsbipRun.UseVisualStyleBackColor = true;
            this.btnUsbipRun.Click += new System.EventHandler(this.btnUsbipRun_Click);
            // 
            // chkUsbipShow
            // 
            this.chkUsbipShow.AutoSize = true;
            this.chkUsbipShow.Location = new System.Drawing.Point(6, 19);
            this.chkUsbipShow.Name = "chkUsbipShow";
            this.chkUsbipShow.Size = new System.Drawing.Size(53, 17);
            this.chkUsbipShow.TabIndex = 5;
            this.chkUsbipShow.Text = "Show";
            this.chkUsbipShow.UseVisualStyleBackColor = true;
            // 
            // groupUsbip
            // 
            this.groupUsbip.Controls.Add(this.chkUsbipShow);
            this.groupUsbip.Controls.Add(this.btnUsbipForceClose);
            this.groupUsbip.Controls.Add(this.btnUsbipRun);
            this.groupUsbip.Location = new System.Drawing.Point(399, 12);
            this.groupUsbip.Name = "groupUsbip";
            this.groupUsbip.Size = new System.Drawing.Size(207, 46);
            this.groupUsbip.TabIndex = 6;
            this.groupUsbip.TabStop = false;
            this.groupUsbip.Text = "USB/IP";
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(110, 39);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(92, 23);
            this.btnProfile.TabIndex = 7;
            this.btnProfile.Text = "Profile";
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // FormProxyGuitar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 300);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.groupUsbip);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.cmbUSBDevices);
            this.Name = "FormProxyGuitar";
            this.Text = "ProxyGuitar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProxyGuitar_FormClosing);
            this.Load += new System.EventHandler(this.FormProxyGuitar_Load);
            this.groupUsbip.ResumeLayout(false);
            this.groupUsbip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbUSBDevices;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnUsbipForceClose;
        private System.Windows.Forms.Button btnUsbipRun;
        private System.Windows.Forms.CheckBox chkUsbipShow;
        private System.Windows.Forms.GroupBox groupUsbip;
        private System.Windows.Forms.Button btnProfile;
    }
}

