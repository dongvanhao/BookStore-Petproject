using BookStore.Application.DTOs.Carts;
using BookStore.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface ICartService
    {
        Task<Result<CartDto>> GetCartByUserIdAsync(int userId);
        Task<Result<bool>> AddToCartAsync(int userId, AddToCartDto dto);
        Task<Result<bool>> RemoveFromCartAsync(int userId, int bookId);
        Task<Result<bool>> ClearCartAsync(int userId);
    }
}
