namespace PokemonGameAPI.Contracts.DTOs.Badge
{
    public record BadgeCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
    }
}
