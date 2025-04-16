using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Carts
{
    public class AddToCartDto
    {
        [Required]
        public int BookId { get; set; }
        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng tối thiểu là 1")]
        public int Quantity { get; set; }
    }
}
