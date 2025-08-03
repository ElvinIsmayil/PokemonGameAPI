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
    public class PokemonAbilityService : IPokemonAbilityService
    {
        private readonly IRepository<PokemonAbility> _repository;
        private readonly IRepository<Pokemon> _pokemonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonAbilityService(IRepository<PokemonAbility> repository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<Pokemon> pokemonRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
        }
        public async Task<PokemonAbilityReturnDto> CreateAsync(PokemonAbilityCreateDto model)
        {
            var entity = _mapper.Map<PokemonAbility>(model);
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PokemonAbilityReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
            }
            var entity = await _repository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            var result = await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<PagedResponse<PokemonAbilityListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery();

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<PokemonAbilityListItemDto>>(entities);

            return new PagedResponse<PokemonAbilityListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<PokemonAbilityReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id, asNoTracking: true);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<PokemonAbilityReturnDto>(entity);
        }

        public async Task<PokemonAbilityReturnDto> UpdateAsync(int id, PokemonAbilityUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PokemonAbilityReturnDto>(updatedEntity);
        }

        public async Task<PokemonAbilityReturnDto> AssignPokemonAbility(PokemonAbilityAssignDto model)
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

            return _mapper.Map<PokemonAbilityReturnDto>(pokemonAbility);
        }

    }
}
