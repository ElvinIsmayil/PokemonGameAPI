using PokemonGameAPI.Contracts.DTOs.Badge;

namespace PokemonGameAPI.Contracts.DTOs.Gym
{
    public record GymCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;

        public int GymLeaderTrainerId { get; init; }
        public BadgeCreateDto Badge { get; init; } = default!;
    }
}
