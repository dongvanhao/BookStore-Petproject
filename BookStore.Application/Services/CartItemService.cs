using AutoMapper;
using BookStore.Application.DTOs.Carts;
using BookStore.Application.Interfaces;
using BookStore.Common.Results;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartItemService> _logger;

        public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper, ILogger<CartItemService> logger)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CartItemDto>> GetItemsByCartIdAsync(int cartId)
        {
            var items = await _cartItemRepository.GetByCartIdAsync(cartId);
            return _mapper.Map<List<CartItemDto>>(items);
        }

        public async Task<Result<bool>> UpdateQuantityAsync(int cartItemId, int newQuantity)
        {
            var item = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (item == null)
            {
                _logger.LogWarning("CartItem not found: {Id}", cartItemId);
                return Result<bool>.Failure("Không tìm thấy mục trong giỏ hàng");
            }

            item.Quantity = newQuantity;
            await _cartItemRepository.UpdateAsync(item);

            return Result<bool>.Success(true, "Cập nhật số lượng thành công");
        }
    }
}
