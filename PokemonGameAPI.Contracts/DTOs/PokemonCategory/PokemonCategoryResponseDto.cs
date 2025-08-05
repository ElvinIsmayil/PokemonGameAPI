namespace PokemonGameAPI.Contracts.DTOs.PokemonCategory
{
    public record PokemonCategoryResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
