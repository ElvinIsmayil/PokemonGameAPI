namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemon
{
    public record TrainerPokemonListItemDto
    {
        public int Id { get; init; }
        public string PokemonName { get; init; } = default!;
        public string TrainerName { get; init; } = default!;

    }
}
