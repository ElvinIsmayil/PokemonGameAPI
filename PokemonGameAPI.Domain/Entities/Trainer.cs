using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int Level { get; set; } = 1;
        public int ExperiencePoints { get; set; } = 0;

        public string AppUserId { get; set; } = default!;
        public AppUser AppUser { get; set; } = default!;

        public ICollection<Badge> Badges { get; set; } = new List<Badge>();
        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public ICollection<Battle> Battles { get; set; } = new List<Battle>();


    }
}
