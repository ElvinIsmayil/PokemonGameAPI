namespace PokemonGameAPI.Contracts.DTOs.Trainer
{
    public record TrainerRequestDto
    {
        public string Name { get; init; } = default!;
        public int Level { get; init; } = 1;
        public int ExperiencePoints { get; init; } = 0;

        public int AppUserId { get; init; }

        public ICollection<int> TrainerPokemonIds { get; init; } = new List<int>();
        public ICollection<int> BadgeIds { get; init; } = new List<int>();
        public ICollection<int> TournamentIds { get; init; } = new List<int>();
    }
}
