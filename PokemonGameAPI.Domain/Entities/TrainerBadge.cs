using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class TrainerBadge : BaseEntity
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = default!;

        public int BadgeId { get; set; }
        public Badge Badge { get; set; } = default!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
