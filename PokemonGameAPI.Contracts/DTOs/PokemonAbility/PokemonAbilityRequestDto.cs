namespace PokemonGameAPI.Contracts.DTOs.PokemonAbility
{
    public record PokemonAbilityRequestDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
    }
}
