using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerPokemonStatsService : IGenericService<TrainerPokemonStats, TrainerPokemonStatsRequestDto, TrainerPokemonStatsResponseDto>
    {

    }
}
