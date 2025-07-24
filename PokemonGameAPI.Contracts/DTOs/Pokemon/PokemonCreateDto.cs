namespace PokemonGameAPI.Contracts.DTOs.Pokemon
{
    public record PokemonCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsLegendary { get; init; } = false;
        public bool IsWild { get; init; } = false;
        public string? ImageUrl { get; init; }

        public int PokemonCategoryId { get; init; }
        public int LocationId { get; init; }
        public int BaseStatsId { get; init; }
        public ICollection<int> AbilitiesIds { get; init; } = new List<int>();
        public ICollection<int> CategoriesIds { get; init; } = new List<int>();

    }
}
