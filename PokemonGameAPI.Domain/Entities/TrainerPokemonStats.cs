using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class TrainerPokemonStats : BaseEntity
    {
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public int AttackPoints { get; set; }
        public int DefensePoints { get; set; }
        public int AvailableSkillPoints { get; set; }

        public int TrainerPokemonId { get; set; }
        public TrainerPokemon TrainerPokemon { get; set; } = default!;
    }
}
