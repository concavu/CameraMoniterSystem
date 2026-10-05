# Hướng dẫn cài đặt

## Yêu cầu
- Windows 10/11
- .NET SDK 10.0 hoặc phiên bản tương thích
- Visual Studio 2022 hoặc VS Code
- Camera hoặc thiết bị phát video hỗ trợ

## Bước 1: Clone repo
```bash
git clone https://github.com/concavu/CameraMoniterSystem.git
```

## Bước 2: Khởi động project
Mở file `CameraMonitorSystem.slnx` hoặc project `CameraMonitorSystem.csproj` trong Visual Studio.

## Bước 3: Restore package
```powershell
dotnet restore
```

## Bước 4: Build
```powershell
dotnet build
```

## Bước 5: Chạy
```powershell
dotnet run --project CameraMonitorSystem.csproj
```

## Ghi chú
Nếu máy của bạn chưa có .NET SDK, hãy cài đặt trước khi chạy ứng dụng.
