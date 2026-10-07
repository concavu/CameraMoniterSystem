using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.Json; // Thêm thư viện để đọc JSON xử lý cấu hình động
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraMonitorSystem
{
    public partial class server : Form
    {
        private UdpClient? udpListener;
        private Thread? listenThread;
        private bool isListening = false;
        private bool isPaused = false;

        private bool isEmailAlertEnabled = true;
        private volatile bool isAiDetectionEnabled = true;

        private long packetCount = 0;
        private long totalBytes = 0;

        private volatile bool isAiBusy = false;
        private volatile bool isUiBusy = false;

        private DateTime lastBeepTime = DateTime.MinValue;
        private DateTime lastEmailTime = DateTime.MinValue;
        private readonly object imageLock = new object();

        private List<Rectangle> laCameraMonitorSystemDetectedBoxes = new List<Rectangle>();
        private readonly object detectedBoxesLock = new object();

        // Thuật toán chống rung khung hình (EMA Smoothing)
        private Rectangle smoothedBox = Rectangle.Empty;
        private const float SMOOTHING_FACTOR = 0.20f;

        private const float CONFIDENCE_THRESHOLD = 0.50f;
        private const float NMS_THRESHOLD = 0.45f;
        private const int COOLDOWN_BEEP_SECONDS = 2;
        private const int COOLDOWN_EMAIL_MINUTES = 5;

        // CẤU HÌNH HỆ THỐNG - Đã chuyển sang đọc từ file appsettings.json động
        private const string SMTP_SERVER = "smtp.gmail.com";
        private const int SMTP_PORT = 587;

        private InferenceSession? inferenceSession;
        private readonly string modelPath = Path.Combine(Application.StartupPath, "yolov8n.onnx");
        private readonly string configPath = Path.Combine(Application.StartupPath, "appsettings.json");

        private int memoryCleanupCounter = 0;

        // BẢNG MÀU MODERN DARK MODE
        private readonly Color COLOR_BG_MAIN = Color.FromArgb(15, 23, 42);
        private readonly Color COLOR_BG_PANEL = Color.FromArgb(30, 41, 59);
        private readonly Color COLOR_CYAN_NORMAL = Color.FromArgb(14, 165, 233);
        private readonly Color COLOR_CYAN_HOVER = Color.FromArgb(2, 132, 199);
        private readonly Color COLOR_TXT_PRIMARY = Color.FromArgb(241, 245, 249);
        private readonly Color COLOR_BORDER_NORMAL = Color.FromArgb(71, 85, 105);

        public server()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.DoubleBuffered = true;
        }

        private void server_Load(object sender, EventArgs e)
        {
            SetupModernDarkTheme();

            // Tự động kiểm tra và khởi tạo tệp cấu hình mẫu nếu chưa tồn tại
            EnsureConfigFileExists();

            if (statTimer != null)
            {
                statTimer.Interval = 1000;
                statTimer.Start();
            }
            InitYoloModel();
            UpdateAiButtonUi();
        }

        #region GIAO DIỆN HIỆN ĐẠI (UI/UX MODERN DARK MODE)
        private void SetupModernDarkTheme()
        {
            this.BackColor = COLOR_BG_MAIN;
            this.ForeColor = COLOR_TXT_PRIMARY;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            if (pnlStatusBorder != null) pnlStatusBorder.BackColor = COLOR_BG_PANEL;
            if (pnlControlGroup != null) pnlControlGroup.BackColor = COLOR_BG_PANEL;

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

                    if (btn != btnListen && btn != btnPause && btn != btnCapture && btn != btnToggleEmail && btn != btnToggleAi)
                    {
                        btn.BackColor = COLOR_CYAN_NORMAL;
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
            if (sender is Button btn && btn.BackColor == COLOR_CYAN_NORMAL)
            {
                btn.BackColor = COLOR_CYAN_HOVER;
            }
        }

        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.BackColor == COLOR_CYAN_HOVER)
            {
                btn.BackColor = COLOR_CYAN_NORMAL;
            }
        }

        private void Button_Paint_DrawRound(object? sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 12;

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

        #region XỬ LÝ QUẢN LÝ FILE CẤU HÌNH ĐỘNG JSON
        private void EnsureConfigFileExists()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    var defaultSettings = new EmailConfig
                    {
                        EmailSettings = new EmailSettings
                        {
                            FromEmail = string.Empty,
                            AppPassword = string.Empty,
                            ToEmail = string.Empty
                        }
                    };

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(defaultSettings, options);
                    File.WriteAllText(configPath, jsonString);
                }
            }
            catch { }
        }

        private EmailSettings? LoadEmailSettings()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string jsonString = File.ReadAllText(configPath);
                    var config = JsonSerializer.Deserialize<EmailConfig>(jsonString);
                    return config?.EmailSettings;
                }
            }
            catch { }
            return null;
        }
        #endregion

        private void btnToggleAi_Click(object sender, EventArgs e)
        {
            isAiDetectionEnabled = !isAiDetectionEnabled;
            UpdateAiButtonUi();

            if (!isAiDetectionEnabled)
            {
                lock (detectedBoxesLock)
                {
                    laCameraMonitorSystemDetectedBoxes.Clear();
                }
                smoothedBox = Rectangle.Empty;
            }
        }

        private void UpdateAiButtonUi()
        {
            if (btnToggleAi == null) return;

            if (isAiDetectionEnabled)
            {
                btnToggleAi.Text = "🤖 AI DETECT: BẬT";
                btnToggleAi.BackColor = Color.FromArgb(14, 165, 233);
            }
            else
            {
                btnToggleAi.Text = "🔒 AI DETECT: TẮT";
                btnToggleAi.BackColor = Color.FromArgb(100, 116, 139);
            }
        }

        private void btnToggleEmail_Click(object sender, EventArgs e)
        {
            isEmailAlertEnabled = !isEmailAlertEnabled;

            if (isEmailAlertEnabled)
            {
                btnToggleEmail.Text = "🔔 EMAIL: BẬT";
                btnToggleEmail.BackColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                btnToggleEmail.Text = "🔕 EMAIL: TẮT";
                btnToggleEmail.BackColor = Color.FromArgb(239, 68, 68);
            }
        }

        private void InitYoloModel()
        {
            try
            {
                if (File.Exists(modelPath))
                {
                    var options = new SessionOptions();
                    options.ExecutionMode = ExecutionMode.ORT_SEQUENTIAL;
                    options.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;

                    inferenceSession = new InferenceSession(modelPath, options);
                }
                else
                {
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "⚠️ THIẾU MODEL AI: ĐẶT FILE yolov8n.onnx VÀO THƯ MỤC DEBUG!";
                        lblStatus.ForeColor = Color.Orange;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp mô hình YOLO: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statTimer_Tick(object sender, EventArgs e)
        {
            long currentPackets = Interlocked.Exchange(ref packetCount, 0);
            long currentBytes = Interlocked.Exchange(ref totalBytes, 0);
            double bandwidthKB = currentBytes / 1024.0;

            if (lblStats != null)
            {
                lblStats.Text = $"FPS: {currentPackets} kh/s | Tải dữ liệu: {bandwidthKB:F1} KB/s";
            }

            if (isListening && currentPackets == 0 && lblStatus != null)
            {
                lblStatus.Text = "HỆ THỐNG - MẤT KẾT NỐI CAMERA HOẶC QUÁ TẢI CHẤT LƯỢNG ẢNH (>90%)";
                lblStatus.ForeColor = Color.OrangeRed;
            }

            if (isListening)
            {
                memoryCleanupCounter++;
                if (memoryCleanupCounter >= 5)
                {
                    GC.Collect(0, GCCollectionMode.Optimized);
                    memoryCleanupCounter = 0;
                }
            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!isListening)
            {
                int port = 9999;
                if (txtListenPort != null && !string.IsNullOrEmpty(txtListenPort.Text))
                {
                    if (!int.TryParse(txtListenPort.Text.Trim(), out port)) port = 9999;
                }

                try
                {
                    udpListener = new UdpClient(port);
                    udpListener.Client.ReceiveBufferSize = 10 * 1024 * 1024; // 10MB Buffer Chống drop gói mạng khi dồn dữ liệu

                    isListening = true;
                    Interlocked.Exchange(ref packetCount, 0);
                    Interlocked.Exchange(ref totalBytes, 0);

                    lock (detectedBoxesLock) { laCameraMonitorSystemDetectedBoxes.Clear(); }
                    smoothedBox = Rectangle.Empty;

                    isUiBusy = false;
                    isAiBusy = false;

                    listenThread = new Thread(ReceiveData) { IsBackground = true };
                    listenThread.Start();

                    if (btnListen != null)
                    {
                        btnListen.Text = "DỪNG LẮNG NGHE";
                        btnListen.BackColor = Color.FromArgb(239, 68, 68);
                    }
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "HỆ THỐNG - ĐANG LẮNG NGHE ĐƯỜNG TRUYỀN UDP...";
                        lblStatus.ForeColor = COLOR_TXT_PRIMARY;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cấu hình cổng UDP: " + ex.Message, "Lỗi Kết Nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                StopServer();
            }
        }

        private void ReceiveData()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (isListening)
            {
                try
                {
                    if (udpListener == null) break;
                    byte[] data = udpListener.Receive(ref remoteEP);
                    if (data == null || data.Length == 0 || isPaused) continue;

                    Interlocked.Increment(ref packetCount);
                    Interlocked.Add(ref totalBytes, data.Length);

                    // TỐI ƯU GIẢM LAG THỜI GIAN THỰC
                    if (isAiBusy && isUiBusy) continue;

                    Bitmap bmp;
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            bmp = new Bitmap(ms);
                        }
                    }
                    catch { continue; }

                    // ĐIỀU PHỐI LUỒNG AI (YOLOv8) BẤT ĐỒNG BỘ 
                    if (isAiDetectionEnabled && !isAiBusy && inferenceSession != null)
                    {
                        isAiBusy = true;
                        Bitmap bmpForAi = new Bitmap(bmp);
                        Task.Run(() => RunYoloInference(bmpForAi));
                    }

                    // ĐIỀU PHỐI HIỂN THỊ UI KHÔNG ĐỒNG BỘ
                    if (!isUiBusy && picServer != null)
                    {
                        isUiBusy = true;
                        Bitmap bmpForUi = new Bitmap(bmp);

                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            try
                            {
                                List<Rectangle> currentBoxes;
                                lock (detectedBoxesLock)
                                {
                                    currentBoxes = new List<Rectangle>(laCameraMonitorSystemDetectedBoxes);
                                }

                                using (Graphics g = Graphics.FromImage(bmpForUi))
                                {
                                    g.SmoothingMode = SmoothingMode.AntiAlias;

                                    if (isAiDetectionEnabled && currentBoxes.Count > 0)
                                    {
                                        Rectangle targetBox = currentBoxes[0];

                                        if (smoothedBox.IsEmpty)
                                        {
                                            smoothedBox = targetBox;
                                        }
                                        else
                                        {
                                            int smoothX = (int)(smoothedBox.X + SMOOTHING_FACTOR * (targetBox.X - smoothedBox.X));
                                            int smoothY = (int)(smoothedBox.Y + SMOOTHING_FACTOR * (targetBox.Y - smoothedBox.Y));
                                            int smoothW = (int)(smoothedBox.Width + SMOOTHING_FACTOR * (targetBox.Width - smoothedBox.Width));
                                            int smoothH = (int)(smoothedBox.Height + SMOOTHING_FACTOR * (targetBox.Height - smoothedBox.Height));

                                            smoothedBox = new Rectangle(smoothX, smoothY, smoothW, smoothH);
                                        }

                                        using (Pen pen = new Pen(Color.Red, 3))
                                        {
                                            g.DrawRectangle(pen, smoothedBox);
                                            g.DrawString("LOCK - TARGET", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Red, smoothedBox.X, smoothedBox.Y - 18);
                                        }
                                    }
                                    else
                                    {
                                        smoothedBox = Rectangle.Empty;
                                    }
                                }

                                lock (imageLock)
                                {
                                    Image? oldImage = picServer.Image;
                                    picServer.Image = bmpForUi;
                                    if (oldImage != null) oldImage.Dispose(); // Giải phóng RAM triệt để
                                    picServer.Invalidate();
                                }
                            }
                            catch
                            {
                                if (bmpForUi != null) bmpForUi.Dispose();
                            }
                            finally
                            {
                                isUiBusy = false;
                            }
                        });
                    }

                    bmp.Dispose();
                }
                catch { Thread.Sleep(1); }
            }
        }

        private void RunYoloInference(Bitmap bmp)
        {
            List<Prediction> candidates = new List<Prediction>();
            int originalWidth = bmp.Width;
            int originalHeight = bmp.Height;

            try
            {
                int modelWidth = 640, modelHeight = 640;

                using (Bitmap resizedBmp = new Bitmap(bmp, new Size(modelWidth, modelHeight)))
                {
                    var inputTensor = new DenseTensor<float>(new[] { 1, 3, modelHeight, modelWidth });
                    BitmapData bmpData = resizedBmp.LockBits(new Rectangle(0, 0, modelWidth, modelHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                    try
                    {
                        int stride = bmpData.Stride;
                        IntPtr scan0 = bmpData.Scan0;

                        // TỐI ƯU TOÀN DIỆN: Sử dụng xử lý song song đa lõi để xử lý Tensor siêu tốc
                        Parallel.For(0, modelHeight, y =>
                        {
                            unsafe
                            {
                                byte* pScan0 = (byte*)scan0.ToPointer();
                                byte* pRow = pScan0 + (y * stride);
                                for (int x = 0; x < modelWidth; x++)
                                {
                                    int bIdx = x * 3;
                                    // Chuyển đổi định dạng & chuẩn hóa dữ liệu ảnh về khoảng [0, 1]
                                    inputTensor[0, 0, y, x] = pRow[bIdx + 2] * 0.0039215684f; // Red
                                    inputTensor[0, 1, y, x] = pRow[bIdx + 1] * 0.0039215684f; // Green
                                    inputTensor[0, 2, y, x] = pRow[bIdx] * 0.0039215684f;     // Blue
                                }
                            }
                        });
                    }
                    finally
                    {
                        resizedBmp.UnlockBits(bmpData);
                    }

                    if (inferenceSession != null)
                    {
                        var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor("images", inputTensor) };
                        using (var results = inferenceSession.Run(inputs))
                        {
                            var outputTensor = results.First().AsTensor<float>();

                            for (int i = 0; i < 8400; i++)
                            {
                                float confidence = outputTensor[0, 4, i];
                                if (confidence > CONFIDENCE_THRESHOLD)
                                {
                                    float xCenter = outputTensor[0, 0, i] * originalWidth / modelWidth;
                                    float yCenter = outputTensor[0, 1, i] * originalHeight / modelHeight;
                                    float w = outputTensor[0, 2, i] * originalWidth / modelWidth;
                                    float h = outputTensor[0, 3, i] * originalHeight / modelHeight;

                                    int x = (int)(xCenter - w / 2);
                                    int y = (int)(yCenter - h / 2);

                                    lock (candidates)
                                    {
                                        candidates.Add(new Prediction { Box = new Rectangle(x, y, (int)w, (int)h), Score = confidence });
                                    }
                                }
                            }
                        }
                    }
                }

                List<Rectangle> finalBoxes = ApplyNMS(candidates, NMS_THRESHOLD);

                lock (detectedBoxesLock)
                {
                    laCameraMonitorSystemDetectedBoxes = finalBoxes;
                }

                if (finalBoxes.Count > 0)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        if (lblStatus != null)
                        {
                            lblStatus.Text = "⚠️ CẢNH BÁO: PHÁT HIỆN CÓ NGƯỜI XÂM NHẬP VÙNG QUÉT!";
                            lblStatus.ForeColor = Color.FromArgb(239, 68, 68);
                        }
                    });

                    if ((DateTime.Now - lastBeepTime).TotalSeconds > COOLDOWN_BEEP_SECONDS)
                    {
                        lastBeepTime = DateTime.Now;
                        ThreadPool.QueueUserWorkItem(s => Console.Beep(1500, 200));

                        Bitmap bmpToSave = new Bitmap(bmp);
                        ThreadPool.QueueUserWorkItem(s => ProcessAlertPayload(bmpToSave));
                    }
                }
                else
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        if (lblStatus != null)
                        {
                            lblStatus.Text = "HỆ THỐNG - TRẠNG THÁI ỔN ĐỊNH (STREAMING)";
                            lblStatus.ForeColor = Color.FromArgb(16, 185, 129);
                        }
                    });
                }
            }
            catch { }
            finally
            {
                bmp.Dispose();
                isAiBusy = false;
            }
        }

        private List<Rectangle> ApplyNMS(List<Prediction> predictions, float nmsThreshold)
        {
            var sortedPredictions = predictions.OrderByDescending(p => p.Score).ToList();
            var selectedResult = new List<Rectangle>();
            var activeIndices = new bool[sortedPredictions.Count];
            for (int i = 0; i < activeIndices.Length; i++) activeIndices[i] = true;

            for (int i = 0; i < sortedPredictions.Count; i++)
            {
                if (!activeIndices[i]) continue;

                var mainBox = sortedPredictions[i].Box;
                selectedResult.Add(mainBox);

                for (int j = i + 1; j < sortedPredictions.Count; j++)
                {
                    if (!activeIndices[j]) continue;

                    var compareBox = sortedPredictions[j].Box;
                    float intersectionArea = Rectangle.Intersect(mainBox, compareBox).Width * Rectangle.Intersect(mainBox, compareBox).Height;
                    float unionArea = (mainBox.Width * mainBox.Height) + (compareBox.Width * compareBox.Height) - intersectionArea;
                    float iou = unionArea > 0 ? intersectionArea / unionArea : 0;

                    if (iou > nmsThreshold) activeIndices[j] = false;
                }
            }
            return selectedResult;
        }

        private void ProcessAlertPayload(Bitmap bmpToProcess)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "CanhBao");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string fileName = $"Snapshot_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                string fullPath = Path.Combine(folderPath, fileName);

                bmpToProcess.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                if (isEmailAlertEnabled && (DateTime.Now - lastEmailTime).TotalMinutes > COOLDOWN_EMAIL_MINUTES)
                {
                    lastEmailTime = DateTime.Now;
                    SendAlertEmailBackground(fullPath);
                }
            }
            catch { }
            finally
            {
                bmpToProcess.Dispose();
            }
        }

        private void SendAlertEmailBackground(string imagePath)
        {
            if (!File.Exists(imagePath)) return;

            // ĐỌC THÔNG TIN TỪ TỆP CẤU HÌNH ĐỘNG
            EmailSettings? settings = LoadEmailSettings();
            if (settings == null || string.IsNullOrEmpty(settings.FromEmail) || string.IsNullOrEmpty(settings.AppPassword))
            {
                return; // Nếu chưa được cấu hình đúng trong file json thì thoát ra, chống crash app
            }

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(settings.FromEmail, "CAMERA AN NINH AI");
                    mail.To.Add(settings.ToEmail);
                    mail.Subject = $"[⚠️ CẢNH BÁO] - PHÁT HIỆN XÂM NHẬP LÚC {DateTime.Now:HH:mm:ss}";
                    mail.IsBodyHtml = true;
                    mail.Body = $"<h3>HỆ THỐNG GIÁM SÁT AN NINH AI</h3><p>Thời gian ghi nhận: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p><p>Phát hiện đối tượng xâm nhập vùng quét kiểm soát bảo mật.</p>";

                    using (Attachment attachment = new Attachment(imagePath))
                    {
                        mail.Attachments.Add(attachment);
                        using (SmtpClient smtp = new SmtpClient(SMTP_SERVER, SMTP_PORT))
                        {
                            smtp.Credentials = new NetworkCredential(settings.FromEmail, settings.AppPassword);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                }
            }
            catch { }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            Bitmap? currentFrame = null;
            lock (imageLock)
            {
                if (picServer != null && picServer.Image != null) currentFrame = new Bitmap(picServer.Image);
            }

            if (currentFrame != null)
            {
                try
                {
                    string folderPath = Path.Combine(Application.StartupPath, "Snapshots");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                    string fileName = $"Snapshot_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                    currentFrame.Save(Path.Combine(folderPath, fileName), System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("Đã lưu ảnh chụp thủ công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch { }
                finally { currentFrame.Dispose(); }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;
            if (btnPause != null) btnPause.Text = isPaused ? "▶ TIẾP TỤC" : "  ⏸ TẠM DỪNG";
        }

        private void btnOpenWorkspace_Click(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(Application.StartupPath, "CanhBao");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            System.Diagnostics.Process.Start("explorer.exe", folderPath);
        }

        private void StopServer()
        {
            isListening = false;
            if (udpListener != null) { udpListener.Close(); udpListener = null; }
            if (btnListen != null)
            {
                btnListen.Text = "BẮT ĐẦU LẮNG NGHE";
                btnListen.BackColor = COLOR_CYAN_NORMAL;
            }
            if (lblStatus != null)
            {
                lblStatus.Text = "HỆ THỐNG - NGẮT KẾT NỐI TRUYỀN TẢI";
                lblStatus.ForeColor = Color.DarkGray;
            }
            lock (detectedBoxesLock) { laCameraMonitorSystemDetectedBoxes.Clear(); }
            smoothedBox = Rectangle.Empty;
            isUiBusy = false;
            isAiBusy = false;
        }

        private void server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }
    }

    // Các lớp phục vụ cấu trúc đọc ghi JSON
    public class EmailConfig
    {
        public EmailSettings EmailSettings { get; set; }
    }

    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string AppPassword { get; set; }
        public string ToEmail { get; set; }
    }

    public class Prediction
    {
        public Rectangle Box { get; set; }
        public float Score { get; set; }
    }
}