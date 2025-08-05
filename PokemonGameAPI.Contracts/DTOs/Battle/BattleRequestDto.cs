namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleRequestDto
    {
        public int Trainer1Id { get; init; }
        public int Trainer2Id { get; init; }

        public ICollection<int> Trainer1BattlePokemons { get; init; } = new List<int>();
        public ICollection<int> Trainer2BattlePokemons { get; init; } = new List<int>();

        public DateTime? StartTime { get; init; }
        public DateTime? EndTime { get; init; }


    }
}
