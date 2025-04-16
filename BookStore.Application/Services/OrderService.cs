using AutoMapper;
using BookStore.Application.DTOs.Orders;
using BookStore.Application.Interfaces;
using BookStore.Common.Results;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderDetailAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
            return order != null ? _mapper.Map<OrderDto>(order) : null;
        }

        public async Task<Result<OrderDto>> CreateOrderFromCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartWithItemsAsync(userId);
            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                return Result<OrderDto>.Failure("Giỏ hàng trống hoặc không tồn tại.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    BookId = ci.BookId,
                    Quantity = ci.Quantity,
                    Price = ci.Book?.Price ?? 0
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            await _cartRepository.ClearCartAsync(userId);

            return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order), "Tạo đơn hàng thành công.");
        }
    }
}
