using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order> 
    {
        Task<List<Order>> GetOrderByUserIdAsync(int userId);// Lay danh sach don hang cua mot nguoi dung
        Task<Order?> GetOrderDetailAsync(int orderId);// Lay chi tiet don hang bao gom item va sach
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order?> GetOrderWithItemsAsync(int orderId);

    }
}
