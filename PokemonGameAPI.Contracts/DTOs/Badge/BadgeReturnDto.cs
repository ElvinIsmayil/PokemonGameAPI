namespace PokemonGameAPI.Contracts.DTOs.Badge
{
    public record BadgeReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string? ImageUrl { get; init; }
    }
}
