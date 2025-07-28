using PokemonGameAPI.Contracts.DTOs.Role;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleListItemDto>> GetAllRolesAsync();
        Task<RoleReturnDto> GetRoleByIdAsync(string id);
        Task CreateRoleAsync(RoleCreateDto roleCreateDto);
        Task AssignRoleAsync(RoleAssignDto roleAssignDto);
    }
}
