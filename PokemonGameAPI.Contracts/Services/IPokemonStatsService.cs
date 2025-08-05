using PokemonGameAPI.Contracts.DTOs.PokemonStats;

using PokemonGameAPI.Domain.Entities;
namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonStatsService : IGenericService<PokemonStats, PokemonStatsRequestDto, PokemonStatsResponseDto>
    {
    }
}
