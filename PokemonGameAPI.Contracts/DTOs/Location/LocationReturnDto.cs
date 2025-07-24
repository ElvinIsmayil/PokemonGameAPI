using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Contracts.DTOs.Tournament;

namespace PokemonGameAPI.Contracts.DTOs.Location
{
    public record LocationReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public double Longitude { get; init; }
        public double Latitude { get; init; }

        public ICollection<PokemonListItemDto> WildPokemonIds { get; init; } = default!;
        public ICollection<GymListItemDto> GymIds { get; init; } = default!;
        public ICollection<TournamentListItemDto> TournamentIds { get; init; } = default!;
    }
}
