using BookStore.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDto>> GetItemsByOrderIdAsync(int orderId);
    }
}
