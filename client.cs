using AForge.Video;
using AForge.Video.DirectShow;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Tracking;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace CameraMonitorSystem
{
    public partial class client : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private UdpClient udpClient;
        private bool isStreaming = false;
        private bool isClosing = false;

        private Tracker _opencvTracker = null;
        private Rect _trackedRect = new Rect();
        private bool _isTrackingActive = false;
        private readonly object _trackerLock = new object();

        private bool _hasNewServerBox = false;
        private Rectangle _serverIncomingBox = Rectangle.Empty;

        // Cơ chế quản lý luồng bằng biến volatile chống nghẽn
        private volatile bool _isSending = false;
        private volatile bool _isUiBusy = false;

        // BẢNG MÀU MODERN DARK MODE (ĐỒNG BỘ VỚI SERVER)
        private readonly Color COLOR_BG_MAIN = Color.FromArgb(15, 23, 42);
        private readonly Color COLOR_BG_PANEL = Color.FromArgb(30, 41, 59);
        private readonly Color COLOR_CYAN_NORMAL = Color.FromArgb(14, 165, 233);
        private readonly Color COLOR_CYAN_HOVER = Color.FromArgb(2, 132, 199);
        private readonly Color COLOR_TXT_PRIMARY = Color.FromArgb(241, 245, 249);
        private readonly Color COLOR_BORDER_NORMAL = Color.FromArgb(71, 85, 105);

        public client()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.DoubleBuffered = true;
        }

        private void client_Load(object sender, EventArgs e)
        {
            SetupClientModernTheme();
            try
            {
                udpClient = new UdpClient();
                udpClient.Client.SendBufferSize = 5 * 1024 * 1024; // Nới rộng buffer 5MB tránh nghẽn mạng

                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy webcam phần cứng!", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo mạng: {ex.Message}", "Lỗi Kết Nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region GIAO DIỆN HIỆN ĐẠI (UI/UX MODERN DARK MODE)
        private void SetupClientModernTheme()
        {
            this.BackColor = COLOR_BG_MAIN;
            this.ForeColor = COLOR_TXT_PRIMARY;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            if (pnlTop != null) pnlTop.BackColor = COLOR_BG_PANEL;

            ApplyStyleToChildControls(this.Controls);
        }

        private void ApplyStyleToChildControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.Font = new Font("Segoe UI", ctrl.Font.Size < 10F ? 10F : ctrl.Font.Size, ctrl.Font.Style);

                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.ForeColor = Color.White;

                    if (btn != btnStartCamera && btn != btnStream)
                    {
                        btn.BackColor = COLOR_CYAN_NORMAL;
                    }
                    else
                    {
                        btn.BackColor = COLOR_BG_PANEL;
                        btn.FlatAppearance.BorderSize = 1;
                        btn.FlatAppearance.BorderColor = COLOR_BORDER_NORMAL;
                    }

                    btn.MouseEnter += Button_MouseEnter;
                    btn.MouseLeave += Button_MouseLeave;
                    btn.Paint += Button_Paint_DrawRound;
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(51, 65, 85);
                    txt.ForeColor = COLOR_TXT_PRIMARY;
                    txt.BorderStyle = BorderStyle.None;

                    txt.Enter += TextBox_FocusChanged;
                    txt.Leave += TextBox_FocusChanged;

                    if (txt.Parent != null)
                    {
                        txt.Parent.Paint += (s, e) => DrawTextBoxBorder(txt, e.Graphics);
                    }
                }
                else if (ctrl is Label lbl && lbl.ForeColor == Color.White)
                {
                    lbl.ForeColor = COLOR_TXT_PRIMARY;
                }

                if (ctrl.HasChildren)
                {
                    ApplyStyleToChildControls(ctrl.Controls);
                }
            }
        }

        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.BackColor == COLOR_CYAN_NORMAL) btn.BackColor = COLOR_CYAN_HOVER;
                else if (btn.BackColor == COLOR_BG_PANEL) btn.FlatAppearance.BorderColor = COLOR_CYAN_NORMAL;
            }
        }

        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.BackColor == COLOR_CYAN_HOVER) btn.BackColor = COLOR_CYAN_NORMAL;
                else if (btn.BackColor == COLOR_BG_PANEL) btn.FlatAppearance.BorderColor = COLOR_BORDER_NORMAL;
            }
        }

        private void Button_Paint_DrawRound(object? sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 10;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    btn.Region = new Region(path);
                }
            }
        }

        private void TextBox_FocusChanged(object? sender, EventArgs e)
        {
            if (sender is TextBox txt && txt.Parent != null) txt.Parent.Invalidate();
        }

        private void DrawTextBoxBorder(TextBox txt, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color currentBorderColor = txt.Focused ? COLOR_CYAN_NORMAL : COLOR_BORDER_NORMAL;
            Rectangle rect = new Rectangle(txt.Location.X - 2, txt.Location.Y - 2, txt.Width + 4, txt.Height + 4);

            using (SolidBrush bgBrush = new SolidBrush(txt.BackColor)) g.FillRectangle(bgBrush, rect);
            using (Pen pen = new Pen(currentBorderColor, 1.5f)) g.DrawRectangle(pen, rect);
        }
        #endregion

        public void OnReceiveServerCoordinates(Rectangle newBox)
        {
            lock (_trackerLock)
            {
                _serverIncomingBox = newBox;
                _hasNewServerBox = true;
            }
        }

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            if (videoSource == null || !videoSource.IsRunning)
            {
                if (videoDevices.Count > 0)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    videoSource.VideoResolution = videoSource.VideoCapabilities[0];
                    videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                    videoSource.Start();

                    btnStartCamera.Text = "TẮT CAMERA";
                    btnStartCamera.BackColor = Color.FromArgb(239, 68, 68);
                    btnStartCamera.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
                }
            }
            else
            {
                StopCamera();
            }
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (isClosing || videoSource == null) return;

            Bitmap bmpOriginal = null;
            Bitmap bmpResized = null;

            try
            {
                bmpOriginal = (Bitmap)eventArgs.Frame.Clone();
                bmpResized = new Bitmap(bmpOriginal, new System.Drawing.Size(640, 480));

                using (Mat frameMat = BitmapConverter.ToMat(bmpResized))
                {
                    Cv2.Flip(frameMat, frameMat, FlipMode.Y);

                    // KHỐI XỬ LÝ TRACKING AI ĐỘC LẬP
                    lock (_trackerLock)
                    {
                        if (_hasNewServerBox)
                        {
                            _opencvTracker?.Dispose();
                            _opencvTracker = TrackerCSRT.Create(); // Hoặc dùng TrackerKCF.Create() nếu CPU yếu
                            _trackedRect = new Rect(_serverIncomingBox.X, _serverIncomingBox.Y, _serverIncomingBox.Width, _serverIncomingBox.Height);
                            _opencvTracker.Init(frameMat, _trackedRect);
                            _hasNewServerBox = false;
                            _isTrackingActive = true;
                        }
                        else if (_isTrackingActive && _opencvTracker != null)
                        {
                            Rect newRect = _trackedRect;
                            bool isFound = _opencvTracker.Update(frameMat, ref newRect);

                            if (isFound)
                            {
                                if (Math.Abs(newRect.X - _trackedRect.X) > 1 || Math.Abs(newRect.Y - _trackedRect.Y) > 1)
                                {
                                    _trackedRect = newRect;
                                }
                            }
                            else
                            {
                                _isTrackingActive = false;
                            }
                        }

                        // Vẽ mục tiêu lên màn hình Camera
                        if (_isTrackingActive)
                        {
                            var topLeft = new OpenCvSharp.Point(_trackedRect.X, _trackedRect.Y);
                            var bottomRight = new OpenCvSharp.Point(_trackedRect.X + _trackedRect.Width, _trackedRect.Y + _trackedRect.Height);
                            Cv2.Rectangle(frameMat, topLeft, bottomRight, Scalar.Red, 2, LineTypes.AntiAlias);
                            Cv2.PutText(frameMat, "TARGET LOCKED", new OpenCvSharp.Point(_trackedRect.X, _trackedRect.Y - 8),
                                        HersheyFonts.HersheyPlain, 1.0, Scalar.Red, 1, LineTypes.AntiAlias);
                        }
                    }

                    // TỐI ƯU LUỒNG MẠNG: Drop Frame nếu luồng trước đang gửi, chống dồn gói gây lag kịch khung
                    if (isStreaming && !_isSending)
                    {
                        _isSending = true;
                        Mat networkMat = frameMat.Clone();
                        int quality = trackJpegQuality != null ? trackJpegQuality.Value : 75;
                        string ip = txtServerIP != null ? txtServerIP.Text.Trim() : "127.0.0.1";
                        if (txtPort == null || !int.TryParse(txtPort.Text.Trim(), out int port)) port = 9999;

                        ThreadPool.QueueUserWorkItem(state => SendFrameOverUdp(networkMat, quality, ip, port));
                    }

                    // TỐI ƯU GIAO DIỆN UI: Không render đè nếu UI đang bận xử lý đồ họa
                    if (!_isUiBusy && !this.IsDisposed && !isClosing)
                    {
                        _isUiBusy = true;
                        Bitmap bmpForUI = BitmapConverter.ToBitmap(frameMat);

                        this.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                if (!isClosing && picClient != null && !picClient.IsDisposed)
                                {
                                    Image oldImg = picClient.Image;
                                    picClient.Image = bmpForUI;
                                    oldImg?.Dispose(); // Thu hồi bộ nhớ ảnh cũ ngay lập tức tránh tràn RAM
                                }
                                else
                                {
                                    bmpForUI.Dispose();
                                }
                            }
                            catch
                            {
                                if (bmpForUI != null) bmpForUI.Dispose();
                            }
                            finally
                            {
                                _isUiBusy = false;
                            }
                        }));
                    }
                }
            }
            catch { }
            finally
            {
                bmpOriginal?.Dispose();
                bmpResized?.Dispose();
            }
        }

        private void SendFrameOverUdp(Mat matToStream, int quality, string ip, int port)
        {
            try
            {
                if (isClosing || udpClient == null) return;

                ImageEncodingParam[] encodeParams = new ImageEncodingParam[]
                {
                    new ImageEncodingParam(ImwriteFlags.JpegQuality, quality)
                };

                byte[] byteData;
                Cv2.ImEncode(".jpg", matToStream, out byteData, encodeParams);

                if (byteData != null && byteData.Length > 0 && byteData.Length < 65500)
                {
                    udpClient.Send(byteData, byteData.Length, ip, port);
                }
            }
            catch { }
            finally
            {
                matToStream?.Dispose(); // Giải phóng tài nguyên Unmanaged Memory của OpenCV
                _isSending = false;
            }
        }

        private void btnStream_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                isStreaming = !isStreaming;
                if (isStreaming)
                {
                    btnStream.Text = "DỪNG PHÁT";
                    btnStream.BackColor = Color.FromArgb(16, 185, 129);
                    btnStream.FlatAppearance.BorderColor = Color.FromArgb(16, 185, 129);
                }
                else
                {
                    btnStream.Text = "PHÁT VIDEO";
                    btnStream.BackColor = COLOR_BG_PANEL;
                    btnStream.FlatAppearance.BorderColor = COLOR_BORDER_NORMAL;
                }
            }
        }

        private void trackJpegQuality_Scroll(object sender, EventArgs e)
        {
            if (lblQuality != null)
            {
                lblQuality.Text = trackJpegQuality.Value + "%";
            }
        }

        private void StopCamera()
        {
            if (videoSource != null)
            {
                videoSource.NewFrame -= videoSource_NewFrame;
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                }
                videoSource = null;
            }
            isStreaming = false;

            if (picClient != null && picClient.Image != null)
            {
                picClient.Image.Dispose();
                picClient.Image = null;
            }

            lock (_trackerLock)
            {
                _opencvTracker?.Dispose();
                _opencvTracker = null;
                _isTrackingActive = false;
            }

            if (btnStartCamera != null)
            {
                btnStartCamera.Text = "BẬT CAMERA";
                btnStartCamera.BackColor = COLOR_BG_PANEL;
                btnStartCamera.FlatAppearance.BorderColor = COLOR_BORDER_NORMAL;
            }
            if (btnStream != null)
            {
                btnStream.Text = "PHÁT VIDEO";
                btnStream.BackColor = COLOR_BG_PANEL;
                btnStream.FlatAppearance.BorderColor = COLOR_BORDER_NORMAL;
            }
        }

        private void client_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            StopCamera();
            try
            {
                udpClient?.Close();
            }
            catch { }
        }
    }
}