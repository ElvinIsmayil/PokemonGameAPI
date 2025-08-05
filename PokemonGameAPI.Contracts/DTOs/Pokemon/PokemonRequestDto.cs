using PokemonGameAPI.Contracts.DTOs.PokemonStats;

namespace PokemonGameAPI.Contracts.DTOs.Pokemon
{
    public record PokemonRequestDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsLegendary { get; init; } = false;
        public bool IsWild { get; init; } = false;

        public int PokemonCategoryId { get; init; }

        public ICollection<int> AbilitiesIds { get; init; } = new List<int>();

        public PokemonStatsRequestDto BaseStats { get; init; } = default!;

    }
}
