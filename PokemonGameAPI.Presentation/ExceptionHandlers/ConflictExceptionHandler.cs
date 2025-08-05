using Microsoft.AspNetCore.Diagnostics;
using PokemonGameAPI.Application.Exceptions;
using System.Text.Json;

namespace PokemonGameAPI.Presentation.ExceptionHandlers
{
    public class ConflictExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ConflictExceptionHandler> _logger;

        public ConflictExceptionHandler(ILogger<ConflictExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ConflictException conflictException)
            {
                return false; // Let other handlers deal with it
            }

            // Log the exception
            _logger.LogWarning(exception, "ConflictException caught: {Message}", conflictException.Message);

            // Set response
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            httpContext.Response.ContentType = "application/json";

            var errorDetails = new
            {
                status = 409,
                title = "Conflict",
                detail = conflictException.Message,
                timestamp = DateTime.UtcNow,
                path = httpContext.Request.Path
            };

            var json = JsonSerializer.Serialize(errorDetails);

            await httpContext.Response.WriteAsync(json, cancellationToken);
            return true;
        }
    }
}
