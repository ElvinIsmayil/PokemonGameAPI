using Microsoft.AspNetCore.Diagnostics;
using PokemonGameAPI.Application.Exceptions;
using System.Text.Json;

namespace PokemonGameAPI.Presentation.ExceptionHandlers
{
    public class UnauthorizedExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<UnauthorizedExceptionHandler> _logger;

        public UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not UnAuthorizedException unauthorizedException)
            {
                return false; // Let other handlers deal with it
            }

            // Log the exception as warning
            _logger.LogWarning(exception, "UnauthorizedException caught: {Message}", unauthorizedException.Message);

            // Set HTTP 401 Unauthorized response
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            httpContext.Response.ContentType = "application/json";

            var errorDetails = new
            {
                status = 401,
                title = "Unauthorized",
                detail = unauthorizedException.Message,
                timestamp = DateTime.UtcNow,
                path = httpContext.Request.Path
            };

            var json = JsonSerializer.Serialize(errorDetails);

            await httpContext.Response.WriteAsync(json, cancellationToken);
            return true;
        }
    }
}
