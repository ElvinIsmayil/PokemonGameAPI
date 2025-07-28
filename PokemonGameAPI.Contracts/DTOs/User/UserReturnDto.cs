namespace PokemonGameAPI.Contracts.DTOs.User
{
    public record UserReturnDto
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
