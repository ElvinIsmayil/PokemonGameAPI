using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public int Age { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public ICollection<Battle> Battles { get; set; } = new List<Battle>();
    }
}
