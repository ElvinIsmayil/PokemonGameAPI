namespace PokemonGameAPI.Contracts.DTOs.Gym
{
    public record GymCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int LocationId { get; init; }
        public int GymLeaderTrainerId { get; init; }
        public int BadgeId { get; init; }
    }
}
