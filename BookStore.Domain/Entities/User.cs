using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class User
    {
        [Key] // Khóa chính định danh người dùng
        public int Id { get; set; }

        [Required] // Bắt buộc nhập tên
        [MaxLength(100)] // Tối đa 100 ký tự
        public string Name { get; set; }

        [Required] // Bắt buộc nhập email
        [MaxLength(100)] // Tối đa 100 ký tự
        [EmailAddress] // Kiểm tra định dạng email
        public string Email { get; set; }

        [Required] // Bắt buộc có mật khẩu (dạng hash)
        public string PasswordHash { get; set; }

        [Required] // Vai trò hệ thống: Admin hoặc User
        [MaxLength(20)]
        public string Role { get; set; } = "User";

        [MaxLength(15)] // Số điện thoại (tùy chọn)
        public string? Phone { get; set; }

        [MaxLength(255)] // Địa chỉ (tùy chọn)
        public string? Address { get; set; }

        // Ngày tạo người dùng (tự động gán)
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Ngày cập nhật gần nhất (tùy chọn)
        public DateTime? UpdatedAt { get; set; }

        // Trạng thái hoạt động (true = đang hoạt động)
        public bool IsActive { get; set; } = true;

        // Mỗi người dùng có 1 giỏ hàng
        public Cart? Cart { get; set; }

        // Danh sách đơn hàng đã đặt
        public List<Order> Orders { get; set; } = new();
    }
}
