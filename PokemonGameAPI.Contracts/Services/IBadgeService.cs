using Microsoft.AspNetCore.Http;
using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IBadgeService : IGenericService<Badge, BadgeRequestDto, BadgeResponseDto>
    {
        Task<BadgeResponseDto> UploadImgAsync(int id, IFormFile file);
    }
}
