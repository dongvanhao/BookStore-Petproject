using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<List<CartItem>> GetItemsByCartIdAsync(int cartId);// Lay tat ca CartItem cua 1 Cart
        Task<CartItem?> GetItemAsync(int cartId, int bookId);// Tim CartItem theo CartId va BookId(De kiem tra ton tai hay khong)
        Task RemoveAllItemAsync(int cartId);// Xoa tat ca CartItem cua 1 Cart
    }
}
