namespace PokemonGameAPI.Contracts.DTOs.Auth
{
    public record AuthResponseDto
    {
        public string Token { get; init; } = default!;
    }
}
