namespace PokemonGameAPI.Contracts.DTOs.PokemonStats
{
    public record PokemonStatsCreateDto
    {
        public int Level { get; init; }
        public int ExperiencePoints { get; init; }
        public int HealthPoints { get; init; }
        public int MaxHealthPoints { get; init; }
        public int AttackPoints { get; init; }
        public int DefensePoints { get; init; }

    }
}
