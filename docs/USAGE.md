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
- Cấu hình địa chỉ IP của server trong LAN và đảm bảo hai máy có thể liên lạc.

## Cảnh báo và lưu ảnh
Khi phát hiện sự kiện, hệ thống có thể lưu ảnh cảnh báo vào thư mục tương ứng.
Email cảnh báo chỉ gửi khi đã điền `appsettings.json` bằng thông tin SMTP hợp lệ và bật nút email.

## Phát hiện AI
Đặt model `yolov8n.onnx` cạnh file thực thi để bật phát hiện. Nếu model chưa có, phần xem/truyền video vẫn có thể chạy nhưng phát hiện AI không khả dụng.

## Mẹo sử dụng
- Kiểm tra quyền truy cập camera trước khi chạy.
- Đảm bảo máy có đủ tài nguyên cho xử lý hình ảnh.
- Chạy trên Windows Desktop để đảm bảo tương thích tốt nhất.
- Chỉ dùng trong LAN đáng tin cậy; luồng UDP hiện không được mã hóa hoặc xác thực.
