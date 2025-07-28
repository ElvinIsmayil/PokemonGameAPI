using PokemonGameAPI.Contracts.DTOs.PokemonStats;

namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsReturnDto : PokemonStatsReturnDto
    {
        public int AvailableSkillPoints { get; init; }
    }
}
