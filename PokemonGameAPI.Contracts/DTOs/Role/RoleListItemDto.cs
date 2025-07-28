namespace PokemonGameAPI.Contracts.DTOs.Role
{
    public record RoleListItemDto
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
    }
}
