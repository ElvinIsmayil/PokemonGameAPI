using Microsoft.AspNetCore.Diagnostics;
using PokemonGameAPI.Application.CustomExceptions;
using System.Text.Json;

namespace PokemonGameAPI.Presentation.ExceptionHandlers
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not CustomException customException)
                return false;

            _logger.LogWarning(customException, "CustomException caught: {Message}", customException.Message);

            httpContext.Response.StatusCode = customException.StatusCode;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                status = customException.StatusCode,
                title = GetTitleByStatusCode(customException.StatusCode),
                detail = customException.Message,
                errors = customException.ErrorDetails,
                timestamp = DateTime.UtcNow,
                path = httpContext.Request.Path
            };

            var json = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }

        private static string GetTitleByStatusCode(int statusCode) =>
            statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                422 => "Unprocessable Entity",
                500 => "Internal Server Error",
                _ => "Error"
            };
    }
}
