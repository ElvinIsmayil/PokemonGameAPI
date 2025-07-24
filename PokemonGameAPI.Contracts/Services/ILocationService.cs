using PokemonGameAPI.Contracts.DTOs.Location;
using PokemonGameAPI.Contracts.DTOs.Pagination;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ILocationService
    {
        Task<PagedResponse<LocationListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<LocationReturnDto> GetByIdAsync(int id);
        Task<LocationReturnDto> CreateAsync(LocationCreateDto model);
        Task<LocationReturnDto> UpdateAsync(int id, LocationUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
