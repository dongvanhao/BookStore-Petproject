# 📚 BookStore – PetProject

> Hệ thống quản lý bán sách online xây dựng bằng ASP.NET Core theo kiến trúc **Clean Architecture**.

---

## 🎯 Mục tiêu dự án

- ✅ CRUD sách, lọc/sắp xếp theo thể loại, giá, đánh giá
- ✅ Quản lý người dùng (Admin & Customer)
- ✅ Giỏ hàng & Đặt hàng
- ✅ API rõ ràng, có logging, xử lý lỗi tốt
- ✅ Áp dụng quy trình Dev thực tế (GitHub, CI/CD, AutoMapper, Middleware,...)

---

## 🏗️ Kiến trúc – Clean Architecture


> ✅ Tuân theo nguyên tắc Dependency Rule: `Domain` → `Application` → `Infrastructure` → `API`

---

## 🧰 Công nghệ sử dụng

| Công nghệ | Mục đích |
|-----------|----------|
| ASP.NET Core 8 | Web API framework |
| Entity Framework Core | ORM – quản lý dữ liệu |
| SQL Server | Cơ sở dữ liệu chính |
| AutoMapper | Map giữa Entity ↔ DTO |
| FluentValidation | Validate dữ liệu đầu vào |
| Serilog (tuỳ chọn) | Ghi log nghiệp vụ |
| GitHub Actions | CI/CD (Build + Test) |
| Swagger | Test API trực quan |

---

## 🚀 Cài đặt và chạy dự án
## 🧠 Những kỹ thuật nổi bật

| Kỹ thuật                              | Giải thích |
|---------------------------------------|------------|
| **Result<T> pattern**                 | Chuẩn hóa kết quả trả về (Success/Fail + message + data) |
| **Middleware**                        | Bắt lỗi toàn cục → trả lỗi rõ ràng |
| **Repository Pattern**                | Truy xuất dữ liệu tách biệt DB |
| **AutoMapper**                        | Chuyển DTO ↔ Entity tự động |
| **ValidationFilter + FluentValidation** | Validate đầu vào đẹp, rõ, dễ test |
| **GitHub Actions**                    | CI/CD: Tự động build, test khi push code |

---

## 📦 Module chính

### 📘 Book
- Thêm, sửa, xóa sách
- Lọc theo tên, thể loại, giá

### 👤 User
- Đăng ký, đăng nhập
- Phân quyền: Admin / Customer

### 🛒 Cart & Order
- Thêm vào giỏ
- Xem giỏ hàng
- Đặt hàng → tạo Order + OrderItem
- Xóa giỏ hàng sau khi đặt

---

## 🛠 Các tiện ích hỗ trợ

| Tên thư mục        | Mục đích |
|--------------------|----------|
| `Helpers/`         | Các hàm tái sử dụng như format thời gian, sinh mã,... |
| `Constants/`       | Tránh hardcode `"admin"`, `"not found"` ở nhiều nơi |
| `Extensions/`      | Gói gọn cấu hình DI, Swagger, Auth, Logging,... |

---

## 📌 TODO – Những tính năng có thể phát triển thêm

- [ ] ✅ Thêm **JWT Authentication**
- [ ] ✅ Tạo **trang quản lý Admin**
- [ ] ✅ Thêm **tính năng đánh giá sách (Review)**
- [ ] ✅ Viết **Unit Test** (xUnit + Moq)
- [ ] ✅ Tích hợp **Docker**, hoặc **Railway** để deploy

---

## 👨‍💻 Tác giả

- 🔗 GitHub: [@dongvanhao](https://github.com/dongvanhao)
- 💬 Dự án được tạo với mục tiêu **rèn luyện chuyên sâu** & mô phỏng **quy trình làm việc thực tế trong doanh nghiệp**.

---

> ✨ Cảm ơn bạn đã xem qua dự án này! Nếu bạn thấy hữu ích, hãy ⭐️ Star để ủng hộ nhé!

### 1. Clone dự án

```bash
git clone https://github.com/dongvanhao/BookStore-Petproject.git
cd BookStore-Petproject
```

