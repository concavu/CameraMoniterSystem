# CameraMonitorSystem

Prototype WinForms cho bài tập lập trình mạng: truyền khung hình camera trong LAN, xử lý với OpenCV và phát hiện người tùy chọn bằng ONNX Runtime. Đây là dự án học tập, không phải hệ thống giám sát production.

## Mô tả
Dự án này hỗ trợ:
- Kết nối và xem luồng camera
- Ghi lại ảnh chụp cảnh báo theo thời gian thực
- Xử lý hình ảnh với OpenCVSharp
- Tùy chọn phát hiện người bằng mô hình YOLO ONNX cục bộ
- Hỗ trợ giao tiếp giữa client/server trong mạng nội bộ

## Công nghệ chính
- C# / .NET WinForms
- OpenCVSharp
- AForge.Video.DirectShow
- ONNX Runtime
- RestSharp

## Cấu trúc thư mục
- `Program.cs`: điểm khởi động ứng dụng
- `client.cs`: xử lý luồng camera phía client
- `server.cs`: xử lý phía server và truyền nhận dữ liệu
- `CameraMonitorSystem.csproj`: cấu hình dự án

## Yêu cầu hệ thống
- Windows 10/11
- .NET 10 SDK
- Camera hoặc thiết bị video hỗ trợ USB/RTSP

## Cách chạy
```powershell
dotnet restore
dotnet build CameraMonitorSystem.slnx --configuration Release
dotnet run --project CameraMonitorSystem.csproj
```

## Model AI

File `yolov8n.onnx` không được lưu trong repo. Đặt model tương thích tại thư mục project trước khi build hoặc cạnh file `.exe` sau khi build. Nếu thiếu model, server thông báo và tính năng AI không hoạt động; chỉ tải model từ nguồn đáng tin cậy, kiểm tra giấy phép và không commit model khi chưa có quyền phân phối.

## Email cảnh báo

Server tạo `appsettings.json` với các trường email trống. Điền cấu hình cục bộ nếu cần SMTP; dùng app password của nhà cung cấp và không commit file cấu hình hoặc bí mật.

## An toàn và phạm vi

Luồng camera truyền bằng UDP không mã hóa/xác thực. Chỉ dùng trong LAN đáng tin cậy, không mở cổng ra Internet. Prototype chưa có xác thực client/server và chưa được kiểm thử bảo mật độc lập.

Xem [README tiếng Anh](README.md), [hướng dẫn cài đặt](docs/INSTALL.md) và [hướng dẫn sử dụng](docs/USAGE.md).
