using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerPokemonService
    {
        Task<PagedResponse<TrainerPokemonListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<TrainerPokemonReturnDto> GetByIdAsync(int id);
        Task<TrainerPokemonReturnDto> CreateAsync(TrainerPokemonCreateDto model);
        Task<TrainerPokemonReturnDto> UpdateAsync(int id, TrainerPokemonUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
