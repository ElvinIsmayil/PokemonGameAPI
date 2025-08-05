using PokemonGameAPI.Contracts.DTOs.BattlePokemon;
using PokemonGameAPI.Contracts.DTOs.Pagination;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IBattlePokemonService
    {
        Task<PagedResponse<BattlePokemonListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<BattlePokemonReturnDto> GetByIdAsync(int id);
        Task<BattlePokemonReturnDto> CreateAsync(BattlePokemonCreateDto model);
        Task<BattlePokemonReturnDto> UpdateAsync(int id, BattlePokemonUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
