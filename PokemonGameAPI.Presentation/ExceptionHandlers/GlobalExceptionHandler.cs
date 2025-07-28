using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace PokemonGameAPI.Presentation.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is null)
                return false;

            // Log full error with stack trace
            _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

            // Prepare error response
            var errorDetails = new
            {
                status = 500,
                title = "Internal Server Error",
                detail = exception.Message,
                timestamp = DateTime.UtcNow,
                path = httpContext.Request.Path
            };

            var json = JsonSerializer.Serialize(errorDetails);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }
    }
}
