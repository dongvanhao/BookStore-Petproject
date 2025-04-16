using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // GET: api/orderitem/order/5
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetItemsByOrderId(int orderId)
        {
            var items = await _orderItemService.GetItemsByOrderIdAsync(orderId);
            return Ok(items);
        }
    }
}
