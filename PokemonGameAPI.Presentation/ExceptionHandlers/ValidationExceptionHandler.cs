using Microsoft.AspNetCore.Diagnostics;
using PokemonGameAPI.Application.Exceptions;
using System.Text.Json;

namespace PokemonGameAPI.Presentation.ExceptionHandlers
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public ValidationExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
            {
                return false; // Let other handlers deal with it
            }

            // Log the exception
            _logger.LogWarning(exception, "ValidationException caught: {Message}", validationException.Message);

            // Set response
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            httpContext.Response.ContentType = "application/json";

            var errorDetails = new
            {
                status = 404,
                title = "Validation",
                detail = validationException.Message,
                timestamp = DateTime.UtcNow,
                path = httpContext.Request.Path
            };

            var json = JsonSerializer.Serialize(errorDetails);

            await httpContext.Response.WriteAsync(json, cancellationToken);
            return true;
        }
    }
}
