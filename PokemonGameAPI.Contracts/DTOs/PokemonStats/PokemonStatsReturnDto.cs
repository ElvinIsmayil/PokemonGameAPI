namespace PokemonGameAPI.Contracts.DTOs.PokemonStats
{
    public record PokemonStatsReturnDto
    {
        public int Id { get; init; }
        public int Level { get; init; }
        public int HealthPoints { get; init; }

        public string PokemonName { get; init; } = default!;
    }
}
