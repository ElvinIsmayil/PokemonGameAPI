
namespace PokemonGameAPI.Contracts.DTOs.Role
{
    public record RoleAssignDto
    {
        public string UserId { get; init; } = string.Empty;
        public string RoleId { get; init; } = string.Empty;
    }
}
