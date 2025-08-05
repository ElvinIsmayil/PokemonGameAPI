using PokemonGameAPI.Contracts.DTOs.Log;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ILogDataService
    {
        public Task LogAsync(LogDataDto model);
    }
}
