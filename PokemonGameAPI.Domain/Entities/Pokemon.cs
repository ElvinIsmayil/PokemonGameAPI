using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Pokemon : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Height { get; set; } 
        public int Weight { get; set; } 

        public int BaseExperience { get; set; } 

        public int CategoryId { get; set; }
        public PokemonCategory Category { get; set; } 
        
        public ICollection<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();


        public string ImageUrl { get; set; } = string.Empty;

    }
}
