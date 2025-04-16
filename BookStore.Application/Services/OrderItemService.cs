using AutoMapper;
using BookStore.Application.DTOs.Orders;
using BookStore.Application.Interfaces;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderItemService> _logger;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper, ILogger<OrderItemService> logger)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrderItemDto>> GetItemsByOrderIdAsync(int orderId)
        {
            var items = await _orderItemRepository.GetByOrderIdAsync(orderId);
            return _mapper.Map<List<OrderItemDto>>(items);
        }
    }
}
