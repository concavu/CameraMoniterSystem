namespace CameraMonitorSystem
{
    partial class server
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox picServer;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnToggleEmail;
        private System.Windows.Forms.Button btnToggleAi;
        private System.Windows.Forms.Button btnOpenWorkspace;
        private System.Windows.Forms.TextBox txtListenPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Panel pnlStatusBorder;
        private System.Windows.Forms.Panel pnlControlGroup;
        private System.Windows.Forms.Timer statTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picServer = new System.Windows.Forms.PictureBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnToggleEmail = new System.Windows.Forms.Button();
            this.btnToggleAi = new System.Windows.Forms.Button();
            this.btnOpenWorkspace = new System.Windows.Forms.Button();
            this.txtListenPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.pnlStatusBorder = new System.Windows.Forms.Panel();
            this.pnlControlGroup = new System.Windows.Forms.Panel();
            this.statTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picServer)).BeginInit();
            this.pnlStatusBorder.SuspendLayout();
            this.pnlControlGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // picServer
            // 
            this.picServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.picServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.picServer.Location = new System.Drawing.Point(16, 85);
            this.picServer.Name = "picServer";
            this.picServer.Size = new System.Drawing.Size(952, 540);
            this.picServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picServer.TabIndex = 0;
            this.picServer.TabStop = false;
            // 
            // btnListen
            // 
            this.btnListen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnListen.FlatAppearance.BorderSize = 0;
            this.btnListen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnListen.ForeColor = System.Drawing.Color.White;
            this.btnListen.Location = new System.Drawing.Point(180, 10);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(120, 35);
            this.btnListen.TabIndex = 1;
            this.btnListen.Text = "BẮT ĐẦU";
            this.btnListen.UseVisualStyleBackColor = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(310, 10);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(120, 35);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "⏸ TẠM DỪNG";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCapture.FlatAppearance.BorderSize = 0;
            this.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapture.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCapture.ForeColor = System.Drawing.Color.White;
            this.btnCapture.Location = new System.Drawing.Point(440, 10);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(110, 35);
            this.btnCapture.TabIndex = 3;
            this.btnCapture.Text = "📸 CHỤP ẢNH";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnToggleEmail
            // 
            this.btnToggleEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnToggleEmail.FlatAppearance.BorderSize = 0;
            this.btnToggleEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnToggleEmail.ForeColor = System.Drawing.Color.White;
            this.btnToggleEmail.Location = new System.Drawing.Point(560, 10);
            this.btnToggleEmail.Name = "btnToggleEmail";
            this.btnToggleEmail.Size = new System.Drawing.Size(130, 35);
            this.btnToggleEmail.TabIndex = 4;
            this.btnToggleEmail.Text = "🔔 EMAIL: BẬT";
            this.btnToggleEmail.UseVisualStyleBackColor = false;
            this.btnToggleEmail.Click += new System.EventHandler(this.btnToggleEmail_Click);
            // 
            // btnToggleAi
            // 
            this.btnToggleAi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnToggleAi.FlatAppearance.BorderSize = 0;
            this.btnToggleAi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleAi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnToggleAi.ForeColor = System.Drawing.Color.White;
            this.btnToggleAi.Location = new System.Drawing.Point(700, 10);
            this.btnToggleAi.Name = "btnToggleAi";
            this.btnToggleAi.Size = new System.Drawing.Size(150, 35);
            this.btnToggleAi.TabIndex = 5;
            this.btnToggleAi.Text = "🤖 AI DETECT: BẬT";
            this.btnToggleAi.UseVisualStyleBackColor = false;
            this.btnToggleAi.Click += new System.EventHandler(this.btnToggleAi_Click);
            // 
            // btnOpenWorkspace
            // 
            this.btnOpenWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))); // TỐI ƯU: Neo lề phải khi giãn màn hình
            this.btnOpenWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnOpenWorkspace.FlatAppearance.BorderSize = 0;
            this.btnOpenWorkspace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenWorkspace.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOpenWorkspace.ForeColor = System.Drawing.Color.White;
            this.btnOpenWorkspace.Location = new System.Drawing.Point(860, 10);
            this.btnOpenWorkspace.Name = "btnOpenWorkspace";
            this.btnOpenWorkspace.Size = new System.Drawing.Size(100, 35);
            this.btnOpenWorkspace.TabIndex = 6;
            this.btnOpenWorkspace.Text = "📁 THƯ MỤC";
            this.btnOpenWorkspace.UseVisualStyleBackColor = false;
            this.btnOpenWorkspace.Click += new System.EventHandler(this.btnOpenWorkspace_Click);
            // 
            // txtListenPort
            // 
            this.txtListenPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtListenPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtListenPort.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtListenPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.txtListenPort.Location = new System.Drawing.Point(65, 17);
            this.txtListenPort.Name = "txtListenPort";
            this.txtListenPort.Size = new System.Drawing.Size(95, 20);
            this.txtListenPort.TabIndex = 7;
            this.txtListenPort.Text = "9999";
            this.txtListenPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblPort.Location = new System.Drawing.Point(14, 17);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(42, 19);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "Port:";
            // 
            // pnlControlGroup
            // 
            this.pnlControlGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlControlGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlControlGroup.Controls.Add(this.lblPort);
            this.pnlControlGroup.Controls.Add(this.txtListenPort);
            this.pnlControlGroup.Controls.Add(this.btnListen);
            this.pnlControlGroup.Controls.Add(this.btnPause);
            this.pnlControlGroup.Controls.Add(this.btnCapture);
            this.pnlControlGroup.Controls.Add(this.btnToggleEmail);
            this.pnlControlGroup.Controls.Add(this.btnToggleAi);
            this.pnlControlGroup.Controls.Add(this.btnOpenWorkspace);
            this.pnlControlGroup.Location = new System.Drawing.Point(0, 0);
            this.pnlControlGroup.Name = "pnlControlGroup";
            this.pnlControlGroup.Size = new System.Drawing.Size(984, 55);
            this.pnlControlGroup.TabIndex = 9;
            // 
            // pnlStatusBorder
            // 
            this.pnlStatusBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatusBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlStatusBorder.Controls.Add(this.lblStatus);
            this.pnlStatusBorder.Controls.Add(this.lblStats);
            this.pnlStatusBorder.Location = new System.Drawing.Point(0, 641);
            this.pnlStatusBorder.Name = "pnlStatusBorder";
            this.pnlStatusBorder.Size = new System.Drawing.Size(984, 40);
            this.pnlStatusBorder.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblStatus.Location = new System.Drawing.Point(14, 11);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(262, 19);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "HỆ THỐNG - TRẠNG THÁI ỔN ĐỊNH";
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblStats.Location = new System.Drawing.Point(620, 11);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(350, 19);
            this.lblStats.TabIndex = 1;
            this.lblStats.Text = "FPS: 0 kh/s | Tải dữ liệu: 0.0 KB/s";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // statTimer
            // 
            this.statTimer.Interval = 1000; // TỐI ƯU: Đặt chu kỳ 1 giây (1000ms) để tiết kiệm tài nguyên CPU
            this.statTimer.Tick += new System.EventHandler(this.statTimer_Tick);
            // 
            // server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(984, 681);
            this.Controls.Add(this.pnlStatusBorder);
            this.Controls.Add(this.pnlControlGroup);
            this.Controls.Add(this.picServer);
            this.DoubleBuffered = true; // TỐI ƯU: Kích hoạt Double Buffer chống nhấp nháy màn hình khi render video
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1000, 720);
            this.Name = "server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI SECURITY SYSTEM - SERVER CONTROL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.server_FormClosing);
            this.Load += new System.EventHandler(this.server_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picServer)).EndInit();
            this.pnlStatusBorder.ResumeLayout(false);
            this.pnlStatusBorder.PerformLayout();
            this.pnlControlGroup.ResumeLayout(false);
            this.pnlControlGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}