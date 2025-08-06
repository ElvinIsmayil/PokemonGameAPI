using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.CustomExceptions;
using PokemonGameAPI.Contracts.DTOs.Role;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoleListItemDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleListItemDto>>(roles);
        }
        public async Task<RoleReturnDto> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                throw new CustomException("Role not found with this id");
            return _mapper.Map<RoleReturnDto>(role);
        }
        public async Task CreateRoleAsync(RoleCreateDto roleCreateDto)
        {
            var role = _mapper.Map<IdentityRole>(roleCreateDto);
            var existRole = await _roleManager.FindByNameAsync(role.Name);
            if (existRole is not null)
                throw new CustomException("Role already exist with this name");
            await _roleManager.CreateAsync(role);
        }

        public async Task AssignRoleAsync(RoleAssignDto roleAssignDto)
        {
            var role = await _roleManager.FindByIdAsync(roleAssignDto.RoleId);
            if (role is null)
                throw new CustomException("Role not found with this id");

            var user = await _userManager.FindByIdAsync(roleAssignDto.UserId);
            if (user is null)
                throw new CustomException("User not found with this id");

            var result = await _userManager.AddToRoleAsync(user, roleAssignDto.RoleId);
            await _unitOfWork.SaveChangesAsync();


            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new CustomException($"Failed to assign role: {errors}");
            }

        }
    }
}
