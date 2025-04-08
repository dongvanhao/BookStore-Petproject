using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repository
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<CartItem>> GetItemsByCartIdAsync(int cartId)
        {
            return await _dbSet
                .Include(ci => ci.Book)
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
        }

        public async Task<CartItem?> GetItemAsync(int cartId, int bookId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.BookId == bookId);
        }

        public async Task RemoveAllItemAsync(int cartId)
        {
            var items = await _dbSet.Where(ci => ci.CartId == cartId).ToListAsync();
            _dbSet.RemoveRange(items);
        }
    }
}
