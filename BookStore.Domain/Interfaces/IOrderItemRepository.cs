using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<List<OrderItem>> GetItemsByOrderIdAsync(int orderId);// Lay Danh Sach OrderItem THeo OrderId
        Task<int> GetTotalQuantityAsync(int orderId); // Tinh Tong So Luong San Pham Trong Don Hang
        Task<decimal> GetOrderTotalAsync(int orderId); //Tinh Tong Tien Cua Don Hang
    }
}
