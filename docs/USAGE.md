# Hướng dẫn sử dụng

## Mở ứng dụng
Sau khi build thành công, chạy ứng dụng từ Visual Studio hoặc từ lệnh:

```powershell
dotnet run --project CameraMonitorSystem.csproj
```

## Kết nối camera
- Chọn thiết bị camera từ danh sách có sẵn.
- Kiểm tra trạng thái kết nối.
- Xem luồng trực tiếp trên giao diện.

## Cảnh báo và lưu ảnh
Khi phát hiện sự kiện, hệ thống có thể lưu ảnh cảnh báo vào thư mục tương ứng.

## Mẹo sử dụng
- Kiểm tra quyền truy cập camera trước khi chạy.
- Đảm bảo máy có đủ tài nguyên cho xử lý hình ảnh.
- Chạy trên Windows Desktop để đảm bảo tương thích tốt nhất.
