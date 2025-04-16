using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        // GET: api/cartitem/cart/5
        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> GetItemsByCartId(int cartId)
        {
            var items = await _cartItemService.GetItemsByCartIdAsync(cartId);
            return Ok(items);
        }

        // PUT: api/cartitem/5/quantity/3
        [HttpPut("{cartItemId}/quantity/{newQuantity}")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int newQuantity)
        {
            var result = await _cartItemService.UpdateQuantityAsync(cartItemId, newQuantity);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
