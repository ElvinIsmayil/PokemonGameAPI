using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Tournament : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
        public ICollection<Battle> Battles { get; set; } = new List<Battle>();

    }
}
