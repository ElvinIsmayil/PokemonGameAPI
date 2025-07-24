using PokemonGameAPI.Contracts.DTOs.PokemonStats;

namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsCreateDto : PokemonStatsCreateDto
    {
        public int AvailableSkillPoints { get; init; }
        public int TrainerPokemonId { get; init; } = default!;

    }
}
