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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepo, IMapper mapper, ILogger<CartService> logger)
        {
            _cartRepository = cartRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<CartDto>> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetCartWithItemsAsync(userId);
            if (cart == null)
                return Result<CartDto>.Failure("Không tìm thấy giỏ hàng");

            return Result<CartDto>.Success(_mapper.Map<CartDto>(cart));
        }
        public async Task<Result<bool>> AddToCartAsync(int userId, AddToCartDto dto)
        {
            var success = await _cartRepository.AddItemAsync(userId, dto.BookId, dto.Quantity);
            return success
                ? Result<bool>.Success(true, "Đã thêm vào giỏ hàng")
                : Result<bool>.Failure("Không thể thêm vào giỏ hàng");
        }

        public async Task<Result<bool>> RemoveFromCartAsync(int userId, int bookId)
        {
            var cart = await _cartRepository.GetCartWithItemsAsync(userId);
            if (cart == null) return Result<bool>.Failure("Giỏ hàng không tồn tại");

            var item = cart.CartItems.FirstOrDefault(x => x.BookId == bookId);
            if (item == null) return Result<bool>.Failure("Không tìm thấy sách trong giỏ hàng");

            await _cartRepository.RemoveItemAsync(item.Id);
            return Result<bool>.Success(true, "Đã xóa sách khỏi giỏ hàng");
        }

        public async Task<Result<bool>> ClearCartAsync(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return Result<bool>.Success(true, "Đã xóa toàn bộ giỏ hàng");
        }
    }
}

