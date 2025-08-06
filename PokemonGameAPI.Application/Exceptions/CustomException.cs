namespace PokemonGameAPI.Application.CustomExceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public object? ErrorDetails { get; }

        public CustomException(string message, int statusCode = 400, object? errorDetails = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorDetails = errorDetails;
        }
    }
}
