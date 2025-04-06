using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class CartItem
    {
        [Key] // Khóa chính
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cart")] // FK đến giỏ hàng
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        [Required]
        [ForeignKey("Book")] // FK đến sách
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required] // Số lượng sách trong giỏ
        public int Quantity { get; set; }
    }
}
