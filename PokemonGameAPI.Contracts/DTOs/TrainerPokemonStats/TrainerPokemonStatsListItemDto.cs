using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsListItemDto : PokemonStatsListItemDto
    {
        public int AvailableSkillPoints { get; init; }
        public TrainerPokemonListItemDto TrainerPokemonListItemDto { get; init; } = default!;

    }
}
