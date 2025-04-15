using BookStore.Application.DTOs.Carts;
using BookStore.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItemDto>> GetItemsByCartIdAsync(int cartId);
        Task<Result<bool>> UpdateQuantityAsync(int cartItemId, int newQuantity);
    }
}
