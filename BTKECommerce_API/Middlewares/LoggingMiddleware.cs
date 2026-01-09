using BTKECommerce_Core.Extensions;

namespace BTKECommerce_API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,ILogService logService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await logService.LogAsync("Error", ex.Message, context);
                throw;
            }
        }
    }
}
