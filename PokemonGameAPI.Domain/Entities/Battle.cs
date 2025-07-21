using PokemonGameAPI.Domain.Entities.Common;
using PokemonGameAPI.Domain.Enum;

namespace PokemonGameAPI.Domain.Entities
{
    public class Battle : BaseEntity
    {
        public int Trainer1Id { get; set; }
        public Trainer Trainer1 { get; set; } = default!;

        public int Trainer2Id { get; set; }
        public Trainer Trainer2 { get; set; } = default!;


        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }

        public BattleResult Result { get; set; }

        public ICollection<TrainerPokemon> Trainer1Pokemons { get; set; } = new List<TrainerPokemon>();
        public ICollection<TrainerPokemon> Trainer2Pokemons { get; set; } = new List<TrainerPokemon>();

    }
}
