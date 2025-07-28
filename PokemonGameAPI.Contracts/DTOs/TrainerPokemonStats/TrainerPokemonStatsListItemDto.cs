using PokemonGameAPI.Contracts.DTOs.PokemonStats;

namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsListItemDto : PokemonStatsListItemDto
    {
        public int AvailableSkillPoints { get; init; }

    }
}
