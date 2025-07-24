namespace PokemonGameAPI.Contracts.DTOs.Location
{
    public record LocationListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public double Longitude { get; init; }
        public double Latitude { get; init; }

        public int WildPokemonCount { get; init; } = default!;
        public int GymCount { get; init; } = default!;
        public int TournamentCount { get; init; } = default!;


    }
}
