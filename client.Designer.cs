namespace CameraMonitorSystem
{
    partial class client
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picClient = new System.Windows.Forms.PictureBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnStream = new System.Windows.Forms.Button();
            this.btnStartCamera = new System.Windows.Forms.Button();
            this.lblTrackName = new System.Windows.Forms.Label();
            this.trackJpegQuality = new System.Windows.Forms.TrackBar();
            this.lblQuality = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackJpegQuality)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(664, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(14, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(286, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRANSMITTER - UDP CAMERA STREAM";
            // 
            // picClient
            // 
            this.picClient.Location = new System.Drawing.Point(16, 65);
            this.picClient.Name = "picClient";
            this.picClient.Size = new System.Drawing.Size(632, 350);
            this.picClient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picClient.TabIndex = 1;
            this.picClient.TabStop = false;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(16, 434);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(63, 17);
            this.lblIP.TabIndex = 2;
            this.lblIP.Text = "Server IP:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServerIP.Location = new System.Drawing.Point(85, 430);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(110, 25);
            this.txtServerIP.TabIndex = 3;
            this.txtServerIP.Text = "127.0.0.1";
            this.txtServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(208, 434);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 17);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPort.Location = new System.Drawing.Point(245, 430);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(65, 25);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "9999";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStream
            // 
            this.btnStream.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStream.Location = new System.Drawing.Point(330, 427);
            this.btnStream.Name = "btnStream";
            this.btnStream.Size = new System.Drawing.Size(140, 32);
            this.btnStream.TabIndex = 6;
            this.btnStream.Text = "PHÁT VIDEO";
            this.btnStream.UseVisualStyleBackColor = false;
            this.btnStream.Click += new System.EventHandler(this.btnStream_Click);
            // 
            // btnStartCamera
            // 
            this.btnStartCamera.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartCamera.Location = new System.Drawing.Point(485, 427);
            this.btnStartCamera.Name = "btnStartCamera";
            this.btnStartCamera.Size = new System.Drawing.Size(140, 32);
            this.btnStartCamera.TabIndex = 7;
            this.btnStartCamera.Text = "BẬT CAMERA";
            this.btnStartCamera.UseVisualStyleBackColor = false;
            this.btnStartCamera.Click += new System.EventHandler(this.btnStartCamera_Click);
            // 
            // lblTrackName
            // 
            this.lblTrackName.AutoSize = true;
            this.lblTrackName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackName.Location = new System.Drawing.Point(16, 480);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(107, 17);
            this.lblTrackName.TabIndex = 8;
            this.lblTrackName.Text = "Chất lượng JPEG:";
            // 
            // trackJpegQuality
            // 
            this.trackJpegQuality.Location = new System.Drawing.Point(125, 476);
            this.trackJpegQuality.Maximum = 100;
            this.trackJpegQuality.Minimum = 10;
            this.trackJpegQuality.Name = "trackJpegQuality";
            this.trackJpegQuality.Size = new System.Drawing.Size(460, 45);
            this.trackJpegQuality.TabIndex = 9;
            this.trackJpegQuality.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackJpegQuality.Value = 80;
            this.trackJpegQuality.Scroll += new System.EventHandler(this.trackJpegQuality_Scroll);
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuality.Location = new System.Drawing.Point(595, 479);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(38, 19);
            this.lblQuality.TabIndex = 10;
            this.lblQuality.Text = "80%";
            // 
            // client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 530);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.trackJpegQuality);
            this.Controls.Add(this.lblTrackName);
            this.Controls.Add(this.btnStartCamera);
            this.Controls.Add(this.btnStream);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.picClient);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UDP STREAM - CLIENT CONTROLLER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.client_FormClosing);
            this.Load += new System.EventHandler(this.client_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackJpegQuality)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picClient;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStream;
        private System.Windows.Forms.Button btnStartCamera;
        private System.Windows.Forms.Label lblTrackName;
        private System.Windows.Forms.TrackBar trackJpegQuality;
        private System.Windows.Forms.Label lblQuality;
    }
}