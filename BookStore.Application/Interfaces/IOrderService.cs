using BookStore.Application.DTOs.Orders;
using BookStore.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDto?> GetOrderDetailAsync(int orderId);
        Task<Result<OrderDto>> CreateOrderFromCartAsync(int userId);
    }
}
