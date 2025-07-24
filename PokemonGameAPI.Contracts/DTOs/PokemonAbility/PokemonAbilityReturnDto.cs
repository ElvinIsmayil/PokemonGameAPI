using PokemonGameAPI.Contracts.DTOs.Pokemon;

namespace PokemonGameAPI.Contracts.DTOs.PokemonAbility
{
    public record PokemonAbilityReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public ICollection<PokemonListItemDto> Pokemons { get; init; } = default!;
    }
}
