# CameraMonitorSystem

Hệ thống giám sát camera thời gian thực được xây dựng bằng C# WinForms.

## Mô tả
Dự án này hỗ trợ:
- Kết nối và xem luồng camera
- Ghi lại ảnh chụp cảnh báo theo thời gian thực
- Xử lý hình ảnh với OpenCVSharp
- Tích hợp mô hình AI/ONNX cho nhận diện hoặc phát hiện sự kiện
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
- .NET SDK phù hợp
- Camera hoặc thiết bị video hỗ trợ USB/RTSP

## Cách chạy
```powershell
dotnet restore
dotnet build
dotnet run --project CameraMonitorSystem.csproj
```

## Lưu ý
Dự án này phù hợp cho mục đích nghiên cứu, giám sát, và phát triển hệ thống an ninh/giám sát hình ảnh trên Windows.
