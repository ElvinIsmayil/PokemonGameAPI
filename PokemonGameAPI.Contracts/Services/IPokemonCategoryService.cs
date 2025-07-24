using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonCategoryService
    {
        Task<PagedResponse<PokemonCategoryListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<PokemonCategoryReturnDto> GetByIdAsync(int id);
        Task<PokemonCategoryReturnDto> CreateAsync(PokemonCategoryCreateDto model);
        Task<PokemonCategoryReturnDto> UpdateAsync(int id, PokemonCategoryUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
