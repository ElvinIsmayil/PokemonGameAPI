using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Trainer;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerService
    {
        Task<PagedResponse<TrainerListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<TrainerReturnDto> GetByIdAsync(int id);
        Task<TrainerReturnDto> CreateAsync(TrainerCreateDto model);
        Task<TrainerReturnDto> UpdateAsync(int id, TrainerUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
