using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.User;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("User not found with this Id");

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exception("User could not be deleted");
            return result;
        }

        public async Task<PagedResponse<UserListItemDto>> GetAllUsers(int pageNumber, int pageSize)
        {
            List<AppUser> users = await _userManager
                .Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalUsers = await _userManager.Users.CountAsync();


            return new PagedResponse<UserListItemDto>
            {
                Data = _mapper.Map<List<UserListItemDto>>(users),
                TotalCount = totalUsers,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<UserReturnDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("User not found with this Id");
            return _mapper.Map<UserReturnDto>(user);
        }

        public async Task<UserReturnDto> UpdateUserAsync(string userId, UserUpdateDto user)
        {
            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser is null)
                throw new Exception("User not found");
            _mapper.Map(user, existUser);
            var result = await _userManager.UpdateAsync(existUser);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            return _mapper.Map<UserReturnDto>(existUser);
        }
    }
}