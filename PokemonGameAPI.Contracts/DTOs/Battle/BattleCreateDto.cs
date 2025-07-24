namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleCreateDto
    {
        public int Trainer1Id { get; init; }
        public int Trainer2Id { get; init; }

        public DateTime? StartTime { get; init; }
        public DateTime? EndTime { get; init; }

        public ICollection<int> Trainer1Pokemons { get; init; } = new List<int>();
        public ICollection<int> Trainer2Pokemons { get; init; } = new List<int>();

    }
}
