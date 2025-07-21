using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Tournament : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; } = default!;
        public DateTime Date { get; set; }

        public ICollection<Trainer> Participants { get; set; } = new List<Trainer>();
        public ICollection<Battle> Battles { get; set; } = new List<Battle>();

        public int? WinnerId { get; set; }
        public Trainer? Winner { get; set; }

    }
}
