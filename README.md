# 📚 BookStore – PetProject

> Hệ thống quản lý bán sách online bằng **ASP.NET Core 8** theo kiến trúc **Clean Architecture**.

---

## 🎯 Mục tiêu

- CRUD sách, lọc/sắp xếp
- Quản lý người dùng (Admin/Customer)
- Giỏ hàng & Đặt hàng
- Áp dụng quy trình Dev chuyên nghiệp: GitHub, CI/CD, AutoMapper, Middleware,...

---

## 🏗 Kiến trúc Clean Architecture


> Theo đúng nguyên tắc: `Domain → Application → Infrastructure → API`

---

## 🧰 Công nghệ chính

| Công nghệ | Mục đích |
|----------|----------|
| ASP.NET Core 8 | Web API |
| EF Core + SQL Server | ORM + CSDL |
| AutoMapper | Mapping DTO ↔ Entity |
| FluentValidation | Validate đầu vào |
| GitHub Actions | CI tự động build/test |
| Swagger | Test API |

---

## 🔍 Kỹ thuật nổi bật

- ✅ `Result<T>` Pattern
- ✅ Middleware xử lý lỗi toàn cục
- ✅ Repository Pattern
- ✅ AutoMapper & FluentValidation
- ✅ GitHub Actions CI

---

## 📦 Module chính

| Module | Mô tả |
|--------|------|
| 📘 Book | CRUD, lọc theo thể loại, giá |
| 👤 User | Đăng ký, đăng nhập, phân quyền |
| 🛒 Cart & Order | Thêm giỏ, đặt hàng, tạo OrderItem |

---

## 🛠 Hỗ trợ

| Thư mục | Mục đích |
|--------|----------|
| `Helpers/` | Hàm dùng lại (format, tạo mã,...) |
| `Constants/` | Tránh hardcode |
| `Extensions/` | Setup DI, Swagger, Auth,... |

---

## 📌 TODO

- [ ] JWT Authentication
- [ ] Trang quản trị Admin
- [ ] Đánh giá sách (Review)
- [ ] Unit Test (xUnit + Moq)
- [ ] Docker hoặc Railway deploy

---

## 🚀 Cài đặt nhanh

```bash
git clone https://github.com/dongvanhao/BookStore-Petproject.git
cd BookStore-Petproject
dotnet ef database update -s BookStore.API -p BookStore.Infrastructure
dotnet run --project BookStore.API
```
---

## 👨‍💻 Tác giả

- 🔗 GitHub: [@dongvanhao](https://github.com/dongvanhao)
- 💬 Dự án được tạo với mục tiêu **rèn luyện chuyên sâu** & mô phỏng **quy trình làm việc thực tế trong doanh nghiệp**.

---

> ✨ Cảm ơn bạn đã xem qua dự án này! Nếu bạn thấy hữu ích, hãy ⭐️ Star để ủng hộ nhé!
