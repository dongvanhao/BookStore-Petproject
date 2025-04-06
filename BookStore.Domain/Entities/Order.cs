using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class Order
    {
        [Key] // Khóa chính
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")] // FK đến người đặt
        public int UserId { get; set; }

        public User User { get; set; }

        // Ngày đặt hàng
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Tổng tiền đơn hàng
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)] // Trạng thái đơn hàng (Pending, Paid,...)
        public string Status { get; set; } = "Pending";

        // Danh sách item trong đơn
        public List<OrderItem> OrderItems { get; set; } = new();

        // Cờ soft-delete
        public bool IsActive { get; set; } = true;

        // Ngày tạo
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Ngày cập nhật gần nhất
        public DateTime? UpdatedAt { get; set; }
    }
}
