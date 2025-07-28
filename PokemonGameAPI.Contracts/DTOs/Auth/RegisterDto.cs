namespace PokemonGameAPI.Contracts.DTOs.Auth
{
    public record RegisterDto
    {
        public string Name { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string ConfirmPassword { get; init; } = default!;
    }
}
