namespace PokemonGameAPI.Contracts.DTOs.Trainer
{
    public record ChooseStarterDto
    {
        public int TrainerId { get; init; }
        public int StarterPokemonId { get; init; }
       
    }
}
