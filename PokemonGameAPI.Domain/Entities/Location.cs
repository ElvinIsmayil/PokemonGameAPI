using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<Pokemon> WildPokemons { get; set; } = new List<Pokemon>();
        public ICollection<Gym> Gyms { get; set; } = new List<Gym>();
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();

    }
}
