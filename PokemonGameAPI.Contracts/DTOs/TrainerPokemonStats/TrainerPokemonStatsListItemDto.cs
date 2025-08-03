namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats
{
    public record TrainerPokemonStatsListItemDto
    {
        public int Id { get; init; }
        public int Level { get; init; }
        public int ExperiencePoints { get; init; }
        public int HealthPoints { get; init; }

        public int AvailableSkillPoints { get; init; }

    }
}
