using PokemonGameAPI.Domain.Entities.Common;
using PokemonGameAPI.Domain.Enum;

namespace PokemonGameAPI.Domain.Entities
{
    public class BattlePokemon : BaseEntity
    {
        public int BattleId { get; set; }
        public Battle Battle { get; set; } = default!;

        public TrainerSide Side { get; set; }

        public int TrainerPokemonId { get; set; }
        public TrainerPokemon TrainerPokemon { get; set; } = default!;

        public int CurrentHP { get; set; }

        public int CurrentLevel { get; set; }

        public bool IsFainted => CurrentHP <= 0;
    }
}
