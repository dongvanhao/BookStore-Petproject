using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class OrderItem
    {
        [Key] // Khóa chính
        public int Id { get; set; }

        [Required]
        [ForeignKey("Order")] // FK đến đơn hàng
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        [ForeignKey("Book")] // FK đến sách
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required] // Số lượng mua
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Giá lúc đặt
        public decimal Price { get; set; }
    }
}
