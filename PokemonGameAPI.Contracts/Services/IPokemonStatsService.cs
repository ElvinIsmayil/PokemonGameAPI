using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonStatsService
    {
        Task<PagedResponse<PokemonStatsListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<PokemonStatsReturnDto> GetByIdAsync(int id);
        Task<PokemonStatsReturnDto> CreateAsync(PokemonStatsCreateDto model);
        Task<PokemonStatsReturnDto> UpdateAsync(int id, PokemonStatsUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
