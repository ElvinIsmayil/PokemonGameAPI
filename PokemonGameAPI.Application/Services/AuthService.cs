using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Auth;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IGenericRepository<Trainer> _trainerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, IOptions<JwtSettings> jwtOptions, IGenericRepository<Trainer> trainerRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _jwtSettings = jwtOptions.Value;
            _trainerRepository = trainerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            if (_userManager.Users.Any(u => u.Email == registerDto.Email))
                throw new Exception("Email already in use");
            var existUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (existUser != null)
                throw new Exception("Username already in use");
            AppUser appUser = new AppUser();
            appUser.UserName = registerDto.UserName;
            appUser.Email = registerDto.Email;
            appUser.Name = registerDto.Name;
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);

                throw new Exception($"Registration failed: {string.Join(", ", errorMessages.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
            }

            var Trainer = new Trainer()
            {
                Name = registerDto.UserName,
                AppUser = appUser,
                AppUserId = appUser.Id,
            };
            await _trainerRepository.CreateAsync(Trainer);
            await _unitOfWork.SaveChangesAsync();
            await _userManager.AddToRoleAsync(appUser, "Player");
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var existUser = await _userManager.FindByEmailAsync(loginDto.Email);
            var result = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);
            if (!result)
                throw new UnAuthorizedException("Password is incorrect");
            IList<string> roles = await _userManager.GetRolesAsync(existUser);
            var token = _tokenService.GetToken(existUser, roles);
            return new AuthResponseDto()
            {
                Token = token
            };
        }
    }
}
