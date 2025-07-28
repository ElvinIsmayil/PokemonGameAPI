namespace PokemonGameAPI.Contracts.DTOs.Tournament
{
    public record TournamentListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public string LocationName { get; init; } = default!;
        public int ParticipantCount { get; init; }
        public int BattleCount { get; init; }
        public string WinnerName { get; init; } = default!;
    }
}
