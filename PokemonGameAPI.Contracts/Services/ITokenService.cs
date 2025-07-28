using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITokenService
    {
        string GetToken(AppUser existUser, IList<string> roles);
    }
}
