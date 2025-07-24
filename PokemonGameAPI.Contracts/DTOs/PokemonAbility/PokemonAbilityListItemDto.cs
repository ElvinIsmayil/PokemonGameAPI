namespace PokemonGameAPI.Contracts.DTOs.PokemonAbility
{
    public record PokemonAbilityListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int PokemonCount { get; init; } = default!;
    }
}
