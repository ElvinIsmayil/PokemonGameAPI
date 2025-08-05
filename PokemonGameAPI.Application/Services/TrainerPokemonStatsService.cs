using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class TrainerPokemonStatsService : GenericService<TrainerPokemonStats, TrainerPokemonStatsRequestDto, TrainerPokemonStatsResponseDto>, ITrainerPokemonStatsService
    {
        public TrainerPokemonStatsService(IGenericRepository<TrainerPokemonStats> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }
    }
}
