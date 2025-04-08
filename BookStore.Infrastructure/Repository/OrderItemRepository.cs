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
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<OrderItem>> GetItemsByOrderIdAsync(int orderId)
        {
            return await _dbSet
                .Include(oi => oi.Book)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<int> GetTotalQuantityAsync(int orderId)
        {
            return await _dbSet
                .Where(oi => oi.OrderId == orderId)
                .SumAsync(oi => oi.Quantity);
        }

        public async Task<decimal> GetOrderTotalAsync(int orderId)
        {
            return await _dbSet
                .Where(oi => oi.OrderId == orderId)
                .SumAsync(oi => oi.Quantity * oi.Price);
        }
    }
    
}
