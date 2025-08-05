using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.Gym;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IGymService : IGenericService<Gym, GymRequestDto, GymResponseDto>
    {
        Task<BadgeResponseDto> AwardBadgeAsync(AwardBadgeDto model);

    }
}
