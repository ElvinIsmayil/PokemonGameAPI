using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
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
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string folderName = "users";

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, IImageService imageService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("User not found with this Id");

            IdentityResult result = await _userManager.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();

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

        public async Task<UserReturnDto> UploadImgAsync(string id, IFormFile imageFile)
        {

            var entity = await _userManager.FindByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            var fileValidationErrors = _imageService.ValidateFileType(imageFile);
            if (fileValidationErrors.Count > 0)
            {
                throw new ValidationException(string.Join(",", fileValidationErrors));
            }
            (string? imageUrl, List<string> validationErrors) = await _imageService.SaveImageAsync(imageFile, folderName, entity.ProfilePictureUrl);
            if (validationErrors.Count > 0)
            {
                throw new InvalidOperationException(string.Join(", ", validationErrors));
            }
            entity.ProfilePictureUrl = imageUrl;
            var updatedEntity = await _userManager.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserReturnDto>(updatedEntity);
        }
    }
}