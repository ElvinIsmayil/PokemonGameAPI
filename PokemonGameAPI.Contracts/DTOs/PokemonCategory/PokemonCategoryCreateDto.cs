namespace PokemonGameAPI.Contracts.DTOs.PokemonCategory
{
    public record PokemonCategoryCreateDto
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;

    }
}
