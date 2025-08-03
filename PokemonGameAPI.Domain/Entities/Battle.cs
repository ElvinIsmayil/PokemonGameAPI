using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Battle : BaseEntity
    {
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }


        public int Trainer1Id { get; set; }
        public Trainer Trainer1 { get; set; } = default!;

        public int Trainer2Id { get; set; }
        public Trainer Trainer2 { get; set; } = default!;

        public ICollection<BattlePokemon> Trainer1BattlePokemons { get; set; } = new List<BattlePokemon>();
        public ICollection<BattlePokemon> Trainer2BattlePokemons { get; set; } = new List<BattlePokemon>();

        public int? WinnerId { get; set; }
        public Trainer? Winner { get; set; }


    }
}
