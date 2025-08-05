using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.PokemonStats;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class PokemonStatsService : GenericService<PokemonStats, PokemonStatsRequestDto, PokemonStatsResponseDto>, IPokemonStatsService
    {
        public PokemonStatsService(IGenericRepository<PokemonStats> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }
    }
}
