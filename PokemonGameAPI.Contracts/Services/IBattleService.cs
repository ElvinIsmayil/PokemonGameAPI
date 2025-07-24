using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.DTOs.Pagination;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IBattleService
    {
        Task<PagedResponse<BattleListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<BattleReturnDto> GetByIdAsync(int id);
        Task<BattleReturnDto> CreateAsync(BattleCreateDto model);
        Task<BattleReturnDto> UpdateAsync(int id, BattleUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
