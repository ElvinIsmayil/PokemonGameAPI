namespace PokemonGameAPI.Contracts.DTOs.User
{
    public record UserListItemDto
    {
        public string Id { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
