using BookStore.Application.DTOs.Carts;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var result = await _cartService.GetCartByUserIdAsync(userId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        // POST: api/cart/user/5
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] AddToCartDto dto)
        {
            var result = await _cartService.AddToCartAsync(userId, dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // DELETE: api/cart/user/5/book/10
        [HttpDelete("user/{userId}/book/{bookId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int bookId)
        {
            var result = await _cartService.RemoveFromCartAsync(userId, bookId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        // DELETE: api/cart/user/5
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var result = await _cartService.ClearCartAsync(userId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}