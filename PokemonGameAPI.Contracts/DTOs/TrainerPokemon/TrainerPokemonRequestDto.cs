namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemon
{
    public record TrainerPokemonRequestDto
    {
        public int TrainerId { get; init; }
        public int PokemonId { get; init; }
        public int TrainerPokemonStatsId { get; set; }
    }
}
