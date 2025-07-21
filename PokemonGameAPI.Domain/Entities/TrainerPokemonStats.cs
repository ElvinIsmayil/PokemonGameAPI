namespace PokemonGameAPI.Domain.Entities
{
    public class TrainerPokemonStats : PokemonStats
    {
        public int AvailableSkillPoints { get; set; }

        public int TrainerPokemonId { get; set; }
        public TrainerPokemon TrainerPokemon { get; set; } = default!;
    }
}
