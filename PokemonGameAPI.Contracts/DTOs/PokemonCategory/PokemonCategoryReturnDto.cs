namespace PokemonGameAPI.Contracts.DTOs.PokemonCategory
{
    public record PokemonCategoryReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
