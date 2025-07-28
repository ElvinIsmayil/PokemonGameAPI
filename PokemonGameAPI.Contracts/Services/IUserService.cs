using Microsoft.AspNetCore.Identity;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.User;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IUserService
    {
        Task<PagedResponse<UserListItemDto>> GetAllUsers(int pageNumber, int pageSize);
        Task<UserReturnDto> GetUserById(string userId);
        Task<UserReturnDto> UpdateUserAsync(string userId, UserUpdateDto user);
        Task<IdentityResult> DeleteUser(string userId);

    }
}
