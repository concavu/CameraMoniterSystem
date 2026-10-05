# CameraMonitorSystem

![Build](https://img.shields.io/badge/build-passing-brightgreen)
![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-lightgrey)
![Language](https://img.shields.io/badge/language-C%23-512BD4)
![License](https://img.shields.io/badge/license-MIT-blue)
![Status](https://img.shields.io/badge/status-active-00C853)

<img src="assets/banner.svg" alt="CameraMonitorSystem banner" width="100%" />

CameraMonitorSystem là một ứng dụng giám sát camera theo thời gian thực, được xây dựng bằng C# WinForms và tích hợp xử lý hình ảnh, AI/ONNX, cũng như cơ chế cảnh báo dựa trên mạng nội bộ. Dự án hướng tới mục tiêu quan sát luồng video, phát hiện sự kiện và lưu ảnh snapshot một cách nhanh chóng và trực quan.

## Tính năng chính
- Theo dõi luồng camera trực tiếp
- Xử lý khung hình và video theo thời gian thực
- Lưu ảnh cảnh báo/snapshot khi phát hiện sự kiện
- Tích hợp mô hình AI/ONNX để hỗ trợ nhận diện và cảnh báo
- Giao tiếp giữa client/server trong mạng nội bộ
- Giao diện desktop dễ sử dụng trên Windows

## Công nghệ sử dụng
- C# / .NET WinForms
- OpenCVSharp
- AForge.Video.DirectShow
- ONNX Runtime
- RestSharp

## Cấu trúc dự án
- `Program.cs` - điểm khởi động ứng dụng
- `client.cs` - xử lý camera phía client
- `server.cs` - xử lý phía server và truyền nhận dữ liệu
- `CameraMonitorSystem.csproj` - cấu hình dự án
- `docs/` - hướng dẫn cài đặt, sử dụng và kiến trúc

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

## Đóng góp
Mọi đóng góp đều được hoan nghênh. Vui lòng xem [CONTRIBUTING.md](CONTRIBUTING.md) và [SECURITY.md](SECURITY.md).

## Giấy phép
Dự án này được cấp phép theo [MIT License](LICENSE).

## Ghi chú
Dự án này phù hợp cho mục đích giám sát, cảnh báo sự kiện và nghiên cứu ứng dụng xử lý hình ảnh thời gian thực trên nền Windows.
