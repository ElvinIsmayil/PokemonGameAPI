namespace PokemonGameAPI.Domain.Entities
{
    public class BattlePokemon
    {
        public int BattleId { get; set; }
        public Battle Battle { get; set; } = default!;

        public int TrainerPokemonId { get; set; }
        public TrainerPokemon TrainerPokemon { get; set; } = default!;

        public int CurrentHP { get; set; }

        public int CurrentLevel { get; set; }

        public bool IsFainted => CurrentHP <= 0;
    }
}
