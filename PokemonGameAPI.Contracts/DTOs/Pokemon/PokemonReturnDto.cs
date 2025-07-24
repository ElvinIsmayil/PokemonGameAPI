using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.DTOs.Pokemon
{
    public record PokemonReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public bool IsLegendary { get; init; } = false;
        public bool IsWild { get; init; } = false;
        public string? ImageUrl { get; init; }
        public string PokemonCategoryName { get; init; } = default!;
        public string? LocationName { get; init; }
        public PokemonStatsListItemDto BaseStats { get; init; } = default!;
        public ICollection<PokemonAbilityListItemDto> Abilities { get; init; } = new List<PokemonAbilityListItemDto>();
        public ICollection<TrainerPokemonListItemDto> TrainerPokemons { get; init; } = new List<TrainerPokemonListItemDto>();

    }
}
