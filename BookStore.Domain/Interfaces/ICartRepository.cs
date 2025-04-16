using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdAsync(int UserId);//Lay gio hangf theo UserID
        Task<Cart?> GetCartWithItemsAsync(int cartId);//Lay gio hang Chi Tiet(Cart + CartItems + Book)
        Task<bool> AddItemAsync(int userId, int bookId, int quantity); // ✅ THÊM
        Task<bool> RemoveItemAsync(int cartItemId); // ✅ XOÁ MỤC
        Task ClearCartAsync(int userId); // ✅ XOÁ TẤT CẢ
    }
}
