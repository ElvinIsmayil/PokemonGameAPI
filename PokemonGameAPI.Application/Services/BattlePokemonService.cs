using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.BattlePokemon;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class BattlePokemonService : GenericService<BattlePokemon, BattlePokemonRequestDto, BattlePokemonResponseDto>, IBattlePokemonService
    {
        public BattlePokemonService(IGenericRepository<BattlePokemon> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }
    }
}
