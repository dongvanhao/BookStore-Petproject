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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart?> GetCartWithItemsAsync(int userId)
        {
            return await _dbSet
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<bool> AddItemAsync(int userId, int bookId, int quantity)
        {
            var cart = await _dbSet
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _dbSet.Add(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem { BookId = bookId, Quantity = quantity });
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null) return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _dbSet.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
        }
    }
}
