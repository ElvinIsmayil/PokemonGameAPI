using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class PokemonCategoryService : GenericService<PokemonCategory, PokemonCategoryRequestDto, PokemonCategoryResponseDto>, IPokemonCategoryService
    {
        private readonly IGenericRepository<Pokemon> _pokemonRepository;

        public PokemonCategoryService(IGenericRepository<PokemonCategory> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Pokemon> pokemonRepository) : base(repository, unitOfWork, mapper)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<PokemonCategoryResponseDto> AssignPokemonCategory(PokemonCategoryAssignDto model)
        {
            var pokemonCategory = await _repository.GetEntityAsync(x => x.Id == model.CategoryId);
            if (pokemonCategory == null)
            {
                throw new NotFoundException($"Pokemon Category with ID {model.CategoryId} not found");
            }

            var pokemons = await _pokemonRepository.GetQuery()
                .Where(p => model.PokemonIds.Contains(p.Id))
                .ToListAsync();

            if (pokemons.Count != model.PokemonIds.Count)
            {
                throw new NotFoundException("One or more Pokemon IDs not found");
            }


            foreach (var pokemon in pokemons)
            {
                if (!(pokemon.PokemonCategoryId == model.CategoryId))
                {
                    pokemon.PokemonCategoryId = model.CategoryId;
                }

            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PokemonCategoryResponseDto>(pokemonCategory);

        }
    }
}
