using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Gym : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;

        public Badge Badge { get; set; } = default!;

        public int GymLeaderTrainerId { get; set; }
        public Trainer GymLeader { get; set; } = default!;

        public ICollection<Battle> Battles { get; set; } = new List<Battle>();
        public ICollection<Trainer> NpcTrainers { get; set; } = new List<Trainer>();

    }
}
