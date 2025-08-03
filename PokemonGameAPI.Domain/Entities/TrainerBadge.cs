using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGameAPI.Domain.Entities
{
    public class TrainerBadge
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = default!;

        public int BadgeId { get; set; }
        public Badge Badge { get; set; } = default!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
