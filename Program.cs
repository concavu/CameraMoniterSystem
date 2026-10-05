using System;
using System.Windows.Forms;

namespace CameraMonitorSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Khởi tạo đối tượng Server trước
            server manHinhServer = new server();

            // 2. Ép Server phải hiển thị và nhảy lên lớp trên cùng của màn hình (không bị đè)
            manHinhServer.Load += (sender, e) => {
                manHinhServer.TopMost = true;  // Đưa lên trên cùng
                manHinhServer.TopMost = false; // Nhả ra để người dùng di chuyển bình thường
            };
            manHinhServer.Show();

            // 3. Chạy luồng giao diện chính cho Client
            Application.Run(new client());
        }
    }
}