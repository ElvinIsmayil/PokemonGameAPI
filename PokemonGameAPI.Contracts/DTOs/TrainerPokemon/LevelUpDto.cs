namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemon
{
    public record LevelUpDto
    {
        public int TrainerPokemonId { get; init; }
        public int NewExperiencePoints { get; init; }
    }
}
