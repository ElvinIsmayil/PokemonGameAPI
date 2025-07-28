namespace PokemonGameAPI.Contracts.DTOs.Auth
{
    public record LoginDto
    {
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
    };
}
