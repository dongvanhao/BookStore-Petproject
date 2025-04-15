
using BookStore.Application.Exceptions;

namespace BookStore.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { ex.Message });
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { Message = "Internal server error." });
            }
        }
    }

}
