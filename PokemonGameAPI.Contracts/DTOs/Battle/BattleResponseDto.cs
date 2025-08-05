using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleResponseDto
    {
        public int Id { get; init; }
        public string Trainer1Name { get; init; } = default!;
        public string Trainer2Name { get; init; } = default!;
        public DateTime StartTime { get; init; } = DateTime.UtcNow;
        public DateTime EndTime { get; init; }
        public ICollection<TrainerPokemonResponseDto> Trainer1BattlePokemons { get; init; } = new List<TrainerPokemonResponseDto>();
        public ICollection<TrainerPokemonResponseDto> Trainer2BattlePokemons { get; init; } = new List<TrainerPokemonResponseDto>();
        public string WinnerName { get; init; } = default!;
    }
}
