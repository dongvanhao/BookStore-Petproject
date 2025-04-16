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
        public async Task<List<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Book)
                .ToListAsync();
        }

    }

}
