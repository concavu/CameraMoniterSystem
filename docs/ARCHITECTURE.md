# Kiến trúc dự án

## Tổng quan
Dự án gồm hai thành phần chính:
- Client: chịu trách nhiệm kết nối camera và hiển thị stream
- Server: quản lý dữ liệu, cảnh báo và truyền nhận thông tin

## Luồng xử lý
1. Khởi tạo giao diện WinForms
2. Kết nối nguồn video
3. Xử lý khung hình
4. Lưu hoặc cảnh báo khi phát hiện sự kiện
5. Gửi/nhận dữ liệu qua mạng nếu cần

## Công nghệ sử dụng
- WinForms cho giao diện người dùng
- OpenCVSharp cho xử lý ảnh
- AForge cho DirectShow camera
- ONNX Runtime cho mô hình AI
- RestSharp cho API/network integration
