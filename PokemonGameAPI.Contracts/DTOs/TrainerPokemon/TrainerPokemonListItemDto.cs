namespace PokemonGameAPI.Contracts.DTOs.TrainerPokemon
{
    public record TrainerPokemonListItemDto
    {
        public int Id { get; init; }
        public string TrainerName { get; init; } = default!;
        public string PokemonName { get; init; } = default!;

    }
}
