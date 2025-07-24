using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;

namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemon
{
    public record TrainerPokemonReturnDto
    {
        public int Id { get; init; }
        public string TrainerName { get; init; } = default!;
        public string PokemonName { get; init; } = default!;
        public TrainerPokemonStatsReturnDto TrainerPokemonStatsDto { get; init; } = default!;
    }
}
