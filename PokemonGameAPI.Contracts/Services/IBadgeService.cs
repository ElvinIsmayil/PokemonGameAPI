using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Pagination;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IBadgeService
    {
        Task<PagedResponse<BadgeListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<BadgeReturnDto> GetByIdAsync(int id);
        Task<BadgeReturnDto> CreateAsync(BadgeCreateDto model);
        Task<BadgeReturnDto> UpdateAsync(int id, BadgeUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
