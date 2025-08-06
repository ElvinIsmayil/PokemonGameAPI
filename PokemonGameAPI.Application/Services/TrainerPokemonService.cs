using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class TrainerPokemonService : GenericService<TrainerPokemon, TrainerPokemonRequestDto, TrainerPokemonResponseDto>, ITrainerPokemonService
    {
        public TrainerPokemonService(IGenericRepository<TrainerPokemon> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }

        public Task EvolveAsync(int trainerPokemonId)
        {
            throw new NotImplementedException();

        }

        public Task<TrainerPokemonResponseDto> GainExperienceAsync(LevelUpDto model)
        {
            throw new NotImplementedException();

        }

        public Task LevelUpAsync(LevelUpDto model)
        {
            throw new NotImplementedException();

        }
    }
}
