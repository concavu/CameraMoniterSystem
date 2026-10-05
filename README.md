# CameraMonitorSystem

Hệ thống giám sát camera theo thời gian thực được xây dựng bằng C# WinForms.

## Mục tiêu
- Theo dõi luồng camera từ thiết bị đầu vào
- Xử lý hình ảnh và phát hiện sự kiện
- Lưu ảnh cảnh báo hoặc snapshot
- Giao tiếp giữa client/server trong mạng nội bộ

## Công nghệ sử dụng
- C# / .NET Windows Forms
- OpenCVSharp
- AForge.Video.DirectShow
- ONNX Runtime
- RestSharp

## Cấu trúc dự án
- `Program.cs` - điểm khởi tạo ứng dụng
- `client.cs` - xử lý client camera
- `server.cs` - xử lý server và giao tiếp
- `CameraMonitorSystem.csproj` - cấu hình project

## Cài đặt
1. Cài đặt .NET SDK phù hợp
2. Mở project bằng Visual Studio hoặc VS Code
3. Restore NuGet package
4. Build và chạy project

## Chạy ứng dụng
```powershell
dotnet run --project CameraMonitorSystem.csproj
```

## Ghi chú
Dự án này dùng cho mục đích giám sát và phát hiện sự kiện trong môi trường Windows.
