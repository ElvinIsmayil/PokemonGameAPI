namespace PokemonGameAPI.Contracts.DTOs.Badge
{
    public record AwardBadgeDto
    {
        public int TrainerId { get; set; } = default!;
        public int GymId { get; set; } = default!;
    }
}
