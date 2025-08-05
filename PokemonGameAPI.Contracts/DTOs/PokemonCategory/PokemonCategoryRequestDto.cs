namespace PokemonGameAPI.Contracts.DTOs.PokemonCategory
{
    public record PokemonCategoryRequestDto
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;

    }
}
