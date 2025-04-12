using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
