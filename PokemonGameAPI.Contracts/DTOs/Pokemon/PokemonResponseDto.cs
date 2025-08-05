using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.DTOs.Pokemon
{
    public record PokemonResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsLegendary { get; init; } = false;
        public bool IsWild { get; init; } = false;
        public string? ImageUrl { get; init; }
        public string PokemonCategoryName { get; init; } = default!;
        public string? LocationName { get; init; }
        public PokemonStatsResponseDto BaseStats { get; init; } = default!;
        public ICollection<PokemonAbilityResponseDto> Abilities { get; init; } = new List<PokemonAbilityResponseDto>();
        public ICollection<TrainerPokemonResponseDto> TrainerPokemons { get; init; } = new List<TrainerPokemonResponseDto>();

    }
}
