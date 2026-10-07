# Kiến trúc dự án

## Tổng quan
Dự án gồm hai thành phần chính:
- Client WinForms: lấy hình ảnh từ camera, mã hóa khung hình JPEG và gửi datagram UDP tới server.
- Server WinForms: nhận datagram, hiển thị luồng, chạy phát hiện người tùy chọn bằng ONNX Runtime, lưu snapshot và gửi email khi được cấu hình.

## Luồng xử lý
1. Khởi tạo giao diện WinForms
2. Kết nối nguồn video
3. Xử lý khung hình
4. Lưu hoặc cảnh báo khi phát hiện sự kiện
5. Server nhận khung hình UDP và cập nhật giao diện.
6. Nếu có model ONNX tương thích, server xử lý khung hình và hiển thị cảnh báo.
7. Lưu snapshot; gửi email tùy chọn nếu `appsettings.json` có thông tin SMTP hợp lệ.

## Giới hạn an toàn

Luồng UDP không có mã hóa, xác thực hay bảo đảm giao nhận. Chỉ dùng trong LAN đáng tin cậy, không mở cổng ra Internet. Đây là prototype học tập; cần thiết kế lại giao thức và đánh giá bảo mật trước khi dùng thực tế.

## Công nghệ sử dụng
- WinForms cho giao diện người dùng
- OpenCVSharp cho xử lý ảnh
- AForge cho DirectShow camera
- ONNX Runtime cho mô hình AI
- RestSharp cho API/network integration
