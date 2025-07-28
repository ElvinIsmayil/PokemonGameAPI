namespace PokemonGameAPI.Contracts.DTOs.User
{
    public record UserUpdateDto
    {
        public string Name { get; init; } = default!;
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
