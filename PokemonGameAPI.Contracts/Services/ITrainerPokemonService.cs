using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

using PokemonGameAPI.Domain.Entities;
namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerPokemonService : IGenericService<TrainerPokemon, TrainerPokemonRequestDto, TrainerPokemonResponseDto>
    {

        Task LevelUpAsync(LevelUpDto model);
        Task EvolveAsync(int trainerPokemonId);
        Task<TrainerPokemonResponseDto> GainExperienceAsync(LevelUpDto model);
    }
}
