using PokemonGameAPI.Contracts.DTOs.Badge;

namespace PokemonGameAPI.Contracts.DTOs.Gym
{
    public record GymRequestDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;

        public int GymLeaderTrainerId { get; init; }
        public BadgeRequestDto Badge { get; init; } = default!;
    }
}
