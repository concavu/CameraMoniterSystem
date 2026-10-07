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

## Cấu hình tùy chọn

- Lần đầu mở server, ứng dụng tạo `appsettings.json` với cấu hình email trống. Nếu cần gửi cảnh báo, điền `FromEmail`, `AppPassword` và `ToEmail` bằng thông tin riêng của bạn; chỉ dùng app password do nhà cung cấp cấp.
- AI cần `yolov8n.onnx`, không có trong repo. Đặt model tương thích tại thư mục project trước khi build hoặc cạnh file thực thi sau khi build. Kiểm tra nguồn và giấy phép model trước khi sử dụng.
- `appsettings.json` và model ONNX được loại khỏi Git; không commit bí mật, model không có quyền phân phối hoặc ảnh snapshot.

## Ghi chú
Truyền video bằng UDP không mã hóa/xác thực; chỉ thử trên LAN đáng tin cậy và không mở cổng ra Internet. Đây là prototype học tập, không phải hệ thống giám sát production.
