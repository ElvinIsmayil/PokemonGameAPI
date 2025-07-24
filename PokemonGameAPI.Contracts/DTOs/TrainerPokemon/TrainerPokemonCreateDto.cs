namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemon
{
    public record TrainerPokemonCreateDto
    {
        public int TrainerId { get; init; }
        public int PokemonId { get; init; }
        public int TrainerPokemonStatsId { get; set; }
    }
}
