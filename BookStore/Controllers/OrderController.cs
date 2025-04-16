using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var result = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(result);
        }

        // GET: api/order/5
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var result = await _orderService.GetOrderDetailAsync(orderId);
            return result != null ? Ok(result) : NotFound();
        }

        // POST: api/order/user/5
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> CreateOrderFromCart(int userId)
        {
            var result = await _orderService.CreateOrderFromCartAsync(userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
