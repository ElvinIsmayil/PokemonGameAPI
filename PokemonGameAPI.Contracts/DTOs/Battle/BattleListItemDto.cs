namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleListItemDto
    {
        public int Id { get; init; }
        public string Trainer1Name { get; init; } = default!;
        public string Trainer2Name { get; init; } = default!;
        public DateTime StartTime { get; init; } = DateTime.UtcNow;
        public DateTime EndTime { get; init; }

    }
}
