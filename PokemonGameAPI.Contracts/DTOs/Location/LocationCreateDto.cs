namespace PokemonGameAPI.Contracts.DTOs.Location
{
    public record LocationCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public double Longitude { get; init; }
        public double Latitude { get; init; }

        public ICollection<int> WildPokemonIds { get; init; } = default!;
        public ICollection<int> GymIds { get; init; } = default!;
        public ICollection<int> TournamentIds { get; init; } = default!;



    }
}
