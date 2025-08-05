namespace PokemonGameAPI.Contracts.DTOs.Tournament
{
    public record TournamentResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public string LocationName { get; init; } = default!;
        public string WinnerName { get; init; } = default!;

        public int ParticipantCount { get; init; } = default!;
        public int BattleCount { get; init; } = default!;
    }
}
