using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Contracts.DTOs.Pagination;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IGymService
    {
        Task<PagedResponse<GymListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<GymReturnDto> GetByIdAsync(int id);
        Task<GymReturnDto> CreateAsync(GymCreateDto model);
        Task<GymReturnDto> UpdateAsync(int id, GymUpdateDto model);
        Task<bool> DeleteAsync(int id);

        Task<BadgeReturnDto> AwardBadgeAsync(AwardBadgeDto model);

    }
}
