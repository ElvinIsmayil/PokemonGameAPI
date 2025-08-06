using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.CustomExceptions;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.User;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new CustomException("User not found with this Id");

            IdentityResult result = await _userManager.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();

            if (!result.Succeeded)
                throw new CustomException("User could not be deleted");
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
                throw new CustomException("User not found with this Id");
            return _mapper.Map<UserReturnDto>(user);
        }

        public async Task<UserReturnDto> UpdateUserAsync(string userId, UserUpdateDto user)
        {
            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser is null)
                throw new CustomException("User not found");
            _mapper.Map(user, existUser);
            var result = await _userManager.UpdateAsync(existUser);
            if (!result.Succeeded)
                throw new CustomException(string.Join(", ", result.Errors.Select(e => e.Description)));
            return _mapper.Map<UserReturnDto>(existUser);
        }


        public async Task<UserReturnDto> UploadImgAsync(string id, IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new CustomException($"user with ID {id} not found.");
            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            if (uploadResult.Error != null)
                throw new CustomException($"Image upload failed: {uploadResult.Error.Message}");
            user.ProfilePictureUrl = uploadResult.SecureUrl.ToString();
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserReturnDto>(user);
        }
    }
}