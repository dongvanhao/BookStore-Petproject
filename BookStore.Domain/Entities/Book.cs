using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        // Khóa chính định danh duy nhất cho mỗi cuốn sách
        [Key]
        public int Id { get; set; }

        // Tiêu đề của sách (bắt buộc, tối đa 200 ký tự)
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        // Tên tác giả (bắt buộc, tối đa 100 ký tự)
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        // Mô tả nội dung sách (không bắt buộc, tối đa 1000 ký tự)
        [MaxLength(1000)]
        public string? Description { get; set; }

        // Giá bán sách (bắt buộc, dùng kiểu decimal để chính xác số tiền)
        [Required]
        [Column(TypeName = "decimal(18,2)")] // định nghĩa kiểu tiền tệ khi tạo database
        public decimal Price { get; set; }

        // Số lượng sách hiện có trong kho (bắt buộc)
        [Required]
        public int Stock { get; set; }

        // Mã ISBN của sách (tùy chọn, thường 13 ký tự)
        [MaxLength(13)]
        public string? ISBN { get; set; }

        // Link ảnh bìa sách (tùy chọn, dùng để hiển thị trên giao diện)
        public string? ImageUrl { get; set; }

        // Ngày sách được thêm vào hệ thống (tự động gán khi tạo)
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Ngày sách được cập nhật gần nhất (chỉ gán khi có chỉnh sửa)
        public DateTime? UpdatedAt { get; set; }

        // Trạng thái còn bán hay đã ngưng bán (soft delete)
        public bool IsActive { get; set; } = true;
        public List<CartItem> CartItem { get; set; } = new();
        public List<OrderItem> OrderItem { get; set; } = new();
    }
}
