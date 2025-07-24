namespace PokemonGameAPI.Contracts.DTOs.PokemonAbility
{
    public record PokemonAbilityAssignDto
    {
        public int AbilityId { get; init; }
        public ICollection<int> PokemonIds { get; init; } = default!;
    }
}
