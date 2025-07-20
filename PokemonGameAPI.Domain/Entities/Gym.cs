using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Gym : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string LeaderName { get; set; }
        public string BadgeAwarded { get; set; }
        public string Description { get; set; }
        // Navigation properties
        public virtual ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

    }
}
