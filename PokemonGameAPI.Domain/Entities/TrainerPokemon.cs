using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class TrainerPokemon : BaseEntity
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = default!;

        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; } = default!;

        public TrainerPokemonStats TrainerPokemonStats { get; set; } = default!;
    }
}
