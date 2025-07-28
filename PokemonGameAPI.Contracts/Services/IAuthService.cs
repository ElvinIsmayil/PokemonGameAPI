using PokemonGameAPI.Contracts.DTOs.Auth;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

    }
}
