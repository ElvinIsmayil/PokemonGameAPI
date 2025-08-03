using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerPokemonStatsService
    {
        Task<PagedResponse<TrainerPokemonStatsListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<TrainerPokemonStatsReturnDto> GetByIdAsync(int id);
        Task<TrainerPokemonStatsReturnDto> CreateAsync(TrainerPokemonStatsCreateDto model);
        Task<TrainerPokemonStatsReturnDto> UpdateAsync(int id, TrainerPokemonStatsUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
