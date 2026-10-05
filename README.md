# CameraMonitorSystem

![Build](https://img.shields.io/badge/build-passing-brightgreen)
![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-lightgrey)
![Language](https://img.shields.io/badge/language-C%23-512BD4)
![License](https://img.shields.io/badge/license-MIT-blue)

Hệ thống giám sát camera theo thời gian thực được xây dựng bằng C# WinForms, tập trung vào việc quan sát luồng video, phát hiện sự kiện và lưu ảnh cảnh báo.

## Tính năng chính
- Theo dõi luồng camera trực tiếp
- Xử lý hình ảnh và video theo thời gian thực
- Lưu ảnh snapshot khi phát hiện sự kiện
- Hỗ trợ mô hình AI/ONNX để nhận diện hoặc cảnh báo
- Tương tác giữa client/server trong mạng nội bộ

## Công nghệ sử dụng
- C# / .NET WinForms
- OpenCVSharp
- AForge.Video.DirectShow
- ONNX Runtime
- RestSharp

## Cấu trúc dự án
- `Program.cs` - điểm khởi động ứng dụng
- `client.cs` - xử lý client camera
- `server.cs` - xử lý server và truyền nhận dữ liệu
- `CameraMonitorSystem.csproj` - cấu hình project
- `docs/` - tài liệu cài đặt, sử dụng và kiến trúc

## Bắt đầu nhanh
### Yêu cầu
- Windows 10/11
- .NET SDK tương thích
- Visual Studio 2022 hoặc VS Code

### Cài đặt
```powershell
dotnet restore
dotnet build
```

### Chạy ứng dụng
```powershell
dotnet run --project CameraMonitorSystem.csproj
```

## Tài liệu
- [Hướng dẫn cài đặt](docs/INSTALL.md)
- [Hướng dẫn sử dụng](docs/USAGE.md)
- [Kiến trúc dự án](docs/ARCHITECTURE.md)

## Ghi chú
Dự án phù hợp cho mục đích giám sát, kiểm soát cảnh báo và nghiên cứu ứng dụng hình ảnh thời gian thực trên môi trường Windows.
