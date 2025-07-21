using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Badge : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }

        public ICollection<Gym> Gyms { get; set; } = new List<Gym>();
        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
