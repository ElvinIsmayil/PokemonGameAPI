namespace PokemonGameAPI.Contracts.DTOs.Tournament
{
    public record TournamentCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int LocationId { get; init; }
    }
}
