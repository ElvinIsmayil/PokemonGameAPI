using PokemonGameAPI.Domain.Entities.Common;
using PokemonGameAPI.Domain.Enum;

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

        public ICollection<BattlePokemon> BattlePokemons { get; set; } = new List<BattlePokemon>();

        public BattleResult BattleResult { get; set; } = BattleResult.Pending;   

    }
}
