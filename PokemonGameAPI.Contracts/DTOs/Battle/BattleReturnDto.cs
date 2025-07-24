using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Domain.Enum;

namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleReturnDto
    {
        public int Id { get; init; }
        public string Trainer1Name { get; init; } = default!;
        public string Trainer2Name { get; init; } = default!;
        public DateTime StartTime { get; init; } = DateTime.UtcNow;
        public DateTime EndTime { get; init; }
        public BattleResult Result { get; init; }
        public ICollection<TrainerPokemonReturnDto> Trainer1Pokemons { get; init; } = new List<TrainerPokemonReturnDto>();
        public ICollection<TrainerPokemonReturnDto> Trainer2Pokemons { get; init; } = new List<TrainerPokemonReturnDto>();
        public string WinnerName { get; init; } = default!;
    }
}
