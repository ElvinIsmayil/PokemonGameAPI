namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsResponseDto
    {
        public int Id { get; init; }
        public int Level { get; init; }
        public int ExperiencePoints { get; init; }
        public int HealthPoints { get; init; }
        public int MaxHealthPoints { get; init; }
        public int AttackPoints { get; init; }
        public int DefensePoints { get; init; }

        public int AvailableSkillPoints { get; init; }
    }
}
