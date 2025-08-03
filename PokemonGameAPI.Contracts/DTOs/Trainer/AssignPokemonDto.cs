namespace PokemonGameAPI.Contracts.DTOs.Trainer
{
    public record AssignPokemonDto
    {
        public int TrainerId { get; init; }
        public int PokemonId { get; init; }
    }
}
