using PokemonGameAPI.Contracts.DTOs.Pokemon;

namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsReturnDto : PokemonReturnDto
    {
        public int AvailableSkillPoints { get; init; }
        public PokemonReturnDto PokemonReturnDto { get; init; } = default!;
    }
}
