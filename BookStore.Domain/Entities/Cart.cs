using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart
    {
        // Khóa chính định danh duy nhất cho mỗi giỏ hàng
        [Key]
        public int Id { get; set; }

        // Khóa ngoại liên kết tới bảng User (mỗi User có 1 Cart)
        [Required]
        public int UserId { get; set; }

        // Điều hướng quan hệ 1-1: Một User sở hữu một Cart
        [ForeignKey("UserId")]
        public User User { get; set; }

        // Danh sách các mục trong giỏ hàng (mối quan hệ 1-N)
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        // Ngày giỏ hàng được tạo (giúp theo dõi)
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Ngày giỏ hàng được cập nhật gần nhất
        public DateTime? UpdatedAt { get; set; }

    }
}
