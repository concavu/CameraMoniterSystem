# CameraMonitorSystem

![Build](https://github.com/concavu/CameraMoniterSystem/actions/workflows/build.yml/badge.svg?branch=main)
![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-lightgrey)
![Language](https://img.shields.io/badge/language-C%23-512BD4)
![License](https://img.shields.io/badge/license-MIT-blue)

<img src="assets/banner.svg" alt="CameraMonitorSystem banner" width="100%" />

CameraMonitorSystem là prototype WinForms cho bài tập lập trình mạng, kết hợp truyền khung hình camera trong LAN, xử lý ảnh với OpenCV và phát hiện người tùy chọn bằng ONNX Runtime. Ứng dụng minh họa luồng client/server và cảnh báo thử nghiệm; chưa được thiết kế hay kiểm thử như một hệ thống an ninh production.

## Tính năng chính
- Theo dõi luồng camera trực tiếp
- Xử lý khung hình và video theo thời gian thực
- Lưu ảnh cảnh báo/snapshot khi phát hiện sự kiện
- Tùy chọn chạy mô hình YOLO ONNX cục bộ để phát hiện người
- Giao tiếp giữa client/server trong mạng nội bộ
- Giao diện desktop dễ sử dụng trên Windows

## Công nghệ sử dụng
- C# / .NET WinForms
- OpenCVSharp
- AForge.Video.DirectShow
- ONNX Runtime
- SMTP qua Gmail khi được cấu hình

## Cấu trúc dự án
- `Program.cs` - điểm khởi động ứng dụng
- `client.cs` - xử lý camera phía client
- `server.cs` - xử lý phía server và truyền nhận dữ liệu
- `CameraMonitorSystem.csproj` - cấu hình dự án
- `docs/` - hướng dẫn cài đặt, sử dụng và kiến trúc

## Bắt đầu nhanh
### Yêu cầu
- Windows 10/11
- .NET 10 SDK
- Visual Studio 2022 hoặc VS Code

### Cài đặt
```powershell
dotnet restore
dotnet build CameraMonitorSystem.slnx --configuration Release
```

### Chạy ứng dụng
```powershell
dotnet run --project CameraMonitorSystem.csproj
```

Build và chạy cả client/server trên Windows Desktop. Cấp quyền camera/mạng khi Windows Firewall hỏi; để client gửi luồng tới đúng địa chỉ IP LAN và cổng mà server đang lắng nghe.

## Cấu hình tùy chọn

### Phát hiện AI

AI cần file `yolov8n.onnx`, nhưng model không được lưu trong repo. Đặt model tương thích tại thư mục project trước khi build để MSBuild copy vào output, hoặc đặt cạnh file `.exe` sau khi build. Server sẽ thông báo nếu model chưa có; phần xem/truyền video vẫn có thể dùng mà không bật AI.

Chỉ tải/export model từ nguồn đáng tin cậy, kiểm tra [hướng dẫn ONNX của Ultralytics](https://docs.ultralytics.com/modes/export/#onnx), giấy phép model và khả năng tương thích với pipeline trong ứng dụng. Không đưa model có bản quyền hoặc file lớn vào Git nếu chưa có quyền phân phối.

### Email cảnh báo

Lần chạy server đầu tiên tạo `appsettings.json` với các trường trống. Nếu muốn gửi email, điền cấu hình cục bộ theo mẫu dưới đây; dùng Google App Password, không dùng mật khẩu đăng nhập tài khoản:

```json
{
  "EmailSettings": {
    "FromEmail": "sender@example.com",
    "AppPassword": "replace-with-a-provider-issued-app-password",
    "ToEmail": "recipient@example.com"
  }
}
```

`appsettings.json` đã được loại khỏi Git. Không commit file cấu hình, app password hoặc ảnh snapshot có dữ liệu cá nhân.

## Giới hạn an toàn

- Khung hình camera được truyền bằng UDP trong LAN, không có mã hóa hay xác thực ở tầng ứng dụng. Chỉ chạy trên mạng riêng đáng tin cậy; không chuyển tiếp cổng server ra Internet.
- Prototype chưa có cơ chế xác thực client/server, phân quyền người dùng hoặc kiểm thử bảo mật độc lập. Không dùng làm hệ thống giám sát production.
- Tắt email nếu chưa cấu hình thông tin SMTP hợp lệ; chỉ bật trên tài khoản thử nghiệm phù hợp.

## Tài liệu
- [Hướng dẫn cài đặt](docs/INSTALL.md)
- [Hướng dẫn sử dụng](docs/USAGE.md)
- [Kiến trúc dự án](docs/ARCHITECTURE.md)

## Đóng góp
Mọi đóng góp đều được hoan nghênh. Vui lòng xem [CONTRIBUTING.md](CONTRIBUTING.md) và [SECURITY.md](SECURITY.md).

## Giấy phép
Dự án này được cấp phép theo [MIT License](LICENSE).

## Phạm vi

Dự án phục vụ học tập và thử nghiệm xử lý video/mạng trên Windows. Kết quả phát hiện AI có thể sai và không nên được dùng làm căn cứ duy nhất cho quyết định an ninh hoặc an toàn.
