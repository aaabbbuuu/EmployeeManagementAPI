using System.Net;
using System.Text.Json;

namespace EmployeeManagementAPI.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {ErrorMessage}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error occurred. Please try again later.",
            };

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var devResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message,
                    StackTrace = exception.StackTrace?.ToString()
                };
                return context.Response.WriteAsync(JsonSerializer.Serialize(devResponse));
            }


            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}