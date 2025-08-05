using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class PokemonAbilityService : GenericService<PokemonAbility, PokemonAbilityRequestDto, PokemonAbilityResponseDto>, IPokemonAbilityService
    {
        private readonly IGenericRepository<Pokemon> _pokemonRepository;

        public PokemonAbilityService(IGenericRepository<PokemonAbility> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Pokemon> pokemonRepository) : base(repository, unitOfWork, mapper)
        {
            _pokemonRepository = pokemonRepository;
        }


        public async Task<PokemonAbilityResponseDto> AssignPokemonAbility(PokemonAbilityAssignDto model)
        {
            var pokemonAbility = await _repository.GetEntityAsync(
                x => x.Id == model.AbilityId,
                includes: x => x.Include(a => a.Pokemons)
            );

            if (pokemonAbility == null)
                throw new NotFoundException($"Ability with ID {model.AbilityId} not found");

            var pokemons = await _pokemonRepository.GetQuery()
                .Where(p => model.PokemonIds.Contains(p.Id))
                .ToListAsync();

            if (!pokemons.Any())
                throw new NotFoundException("No valid Pokémon found with the provided IDs");

            foreach (var pokemon in pokemons)
            {
                if (!pokemonAbility.Pokemons.Any(p => p.Id == pokemon.Id))
                {
                    pokemonAbility.Pokemons.Add(pokemon);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PokemonAbilityResponseDto>(pokemonAbility);
        }

        Task<PagedResponse<PokemonAbilityResponseDto>> IPokemonAbilityService.GetAllAbilitiesByPokemonIdAsync(int pokemonId)
        {
            throw new NotImplementedException();
        }
    }
}
