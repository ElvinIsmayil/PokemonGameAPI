namespace PokemonGameAPI.Contracts.DTOs.PokemonCategory
{
    public record PokemonCategoryAssignDto
    {
        public int CategoryId { get; init; }
        public ICollection<int> PokemonIds { get; init; } = new List<int>();
    }
}
