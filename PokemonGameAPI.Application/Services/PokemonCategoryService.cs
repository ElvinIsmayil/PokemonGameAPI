using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.PokemonCategory;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class PokemonCategoryService : IPokemonCategoryService
    {
        private readonly IRepository<PokemonCategory> _repository;
        private readonly IRepository<Pokemon> _pokemonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonCategoryService(IRepository<PokemonCategory> repository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<Pokemon> pokemonRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<PokemonCategoryReturnDto> CreateAsync(PokemonCategoryCreateDto model)
        {
            var entity = _mapper.Map<PokemonCategory>(model);
             await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PokemonCategoryReturnDto>(entity);

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
            await _unitOfWork.SaveChangesAsync();
            return await _repository.DeleteAsync(entity);

        }

        public async Task<PagedResponse<PokemonCategoryListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery();

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<PokemonCategoryListItemDto>>(entities);

            return new PagedResponse<PokemonCategoryListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<PokemonCategoryReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(x => x.Id == id, asNoTracking: true);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<PokemonCategoryReturnDto>(entity);


        }

        public async Task<PokemonCategoryReturnDto> UpdateAsync(int id, PokemonCategoryUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PokemonCategoryReturnDto>(updatedEntity);
        }

        public async Task<PokemonCategoryReturnDto> AssignPokemonCategory(PokemonCategoryAssignDto model)
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
            return _mapper.Map<PokemonCategoryReturnDto>(pokemonCategory);

        }
    }
}
