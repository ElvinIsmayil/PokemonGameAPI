namespace PokemonGameAPI.Contracts.DTOs.Pokemon
{
    public record PokemonListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsLegendary { get; init; } = false;
        public bool IsWild { get; init; } = false;
        public string? ImageUrl { get; init; }
        public string PokemonCategoryName { get; init; } = default!;
        public string? LocationName { get; init; }
    }
}
