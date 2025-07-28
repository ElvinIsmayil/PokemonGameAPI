namespace PokemonGameAPI.Contracts.DTOs.Role
{
    public record RoleReturnDto
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
    }
}
