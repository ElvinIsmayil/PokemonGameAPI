namespace PokemonGameAPI.Contracts.DTOs.Badge
{
    public record BadgeRequestDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int GymId { get; init; }
    }
}
