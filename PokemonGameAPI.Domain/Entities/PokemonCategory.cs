using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class PokemonCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();

    }
}
