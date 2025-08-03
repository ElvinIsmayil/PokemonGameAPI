using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Badge : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }

        public int GymId { get; set; }
        public Gym Gym { get; set; } = default!;

        public ICollection<TrainerBadge> TrainerBadges { get; set; } = new List<TrainerBadge>();
    }
}
