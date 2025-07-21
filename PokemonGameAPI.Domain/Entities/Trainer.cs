using PokemonGameAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

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
        public ICollection<TrainerPokemon> TrainerPokemons { get; set; } = new List<TrainerPokemon>();
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();

        public ICollection<Battle> BattlesAsTrainer1 { get; set; } = new List<Battle>();
        public ICollection<Battle> BattlesAsTrainer2 { get; set; } = new List<Battle>();

        public IEnumerable<Battle> Battles => BattlesAsTrainer1.Concat(BattlesAsTrainer2);


    }
}
