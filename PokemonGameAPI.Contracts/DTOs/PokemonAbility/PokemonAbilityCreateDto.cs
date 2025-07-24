namespace PokemonGameAPI.Contracts.DTOs.PokemonAbility
{
    public record PokemonAbilityCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
    }
}
