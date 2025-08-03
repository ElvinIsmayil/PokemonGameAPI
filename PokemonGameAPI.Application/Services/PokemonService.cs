using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IRepository<Pokemon> _repository;
        private readonly IRepository<PokemonStats> _pokemonStatsRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<PokemonCategory> _pokemonCategoryRepository;
        private readonly IRepository<PokemonAbility> _pokemonAbilityRepository;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string folderName = "pokemons";
        private readonly PokemonSettings _pokemonSettings;


        public PokemonService(IRepository<Pokemon> repository, IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, IRepository<PokemonStats> pokemonStatsRepository, IRepository<PokemonCategory> pokemonCategoryRepository, IRepository<Location> locationRepository, IRepository<PokemonAbility> pokemonAbilityRepository, IOptions<PokemonSettings> pokemonSettings)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _pokemonStatsRepository = pokemonStatsRepository;
            _pokemonCategoryRepository = pokemonCategoryRepository;
            _locationRepository = locationRepository;
            _pokemonAbilityRepository = pokemonAbilityRepository;
            _pokemonSettings = pokemonSettings.Value;
        }

        public async Task<PokemonReturnDto> CreateAsync(PokemonCreateDto model)
        {
            var pokemonExists = await _repository.IsExistsAsync(x => x.Name == model.Name);
            if (pokemonExists)
                throw new InvalidOperationException($"Pokemon with the name '{model.Name}' already exists.");

            var locationExists = await _locationRepository.IsExistsAsync(x => x.Id == model.LocationId);
            if (!locationExists)
                throw new NotFoundException($"Location with ID {model.LocationId} not found.");

            var pokemonCategoryExists = await _pokemonCategoryRepository.IsExistsAsync(x => x.Id == model.PokemonCategoryId);
            if (!pokemonCategoryExists)
                throw new NotFoundException($"Pokemon Category with ID {model.PokemonCategoryId} not found.");

            if (model.BaseStats == null)
                throw new ValidationException("Base stats are required.");

            var allAbilities = await _pokemonAbilityRepository.GetAllAsync();
            var selectedAbilities = allAbilities
                .Where(a => model.AbilitiesIds.Contains(a.Id))
                .ToList();

            var pokemon = _mapper.Map<Pokemon>(model);
            var baseStats = _mapper.Map<PokemonStats>(model.BaseStats);

            pokemon.Abilities = selectedAbilities;
            pokemon.BaseStats = baseStats;

            await _repository.CreateAsync(pokemon);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PokemonReturnDto>(pokemon);
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

        public async Task<PagedResponse<PokemonListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery().Include(x => x.BaseStats)
                .Include(x => x.Category)
                .Include(x => x.Location);

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<PokemonListItemDto>>(entities);

            return new PagedResponse<PokemonListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<PokemonReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
                predicate: x => x.Id == id,
                asNoTracking: true,
                includes: new Func<IQueryable<Pokemon>, IQueryable<Pokemon>>[]
                {
                    query => query
                    .Include(t => t.Category)
                    .Include(t => t.Location)
                    .Include(t => t.BaseStats)
                    .Include(t => t.Abilities)
                    .Include(t => t.TrainerPokemons)

                }
                );
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<PokemonReturnDto>(entity);
        }

        public async Task<PokemonReturnDto> UpdateAsync(int id, PokemonUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PokemonReturnDto>(updatedEntity);
        }


        public async Task<PokemonReturnDto> UploadImgAsync(int id, IFormFile imageFile)
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
            var fileValidationErrors = _imageService.ValidateFileType(imageFile);
            if (fileValidationErrors.Count > 0)
            {
                throw new ValidationException(string.Join(",", fileValidationErrors));
            }
            (string? imageUrl, List<string> validationErrors) = await _imageService.SaveImageAsync(imageFile, folderName, entity.ImageUrl);
            if (validationErrors.Count > 0)
            {
                throw new InvalidOperationException(string.Join(", ", validationErrors));
            }
            entity.ImageUrl = imageUrl;
            var updatedEntity = await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PokemonReturnDto>(updatedEntity);
        }

        public async Task<IEnumerable<PokemonReturnDto>> GetStarterPokemonsAsync()
        {
            var starterPokemonIds = _pokemonSettings.StarterPokemons;

            var starterPokemons = await _repository.GetAllAsync(p => starterPokemonIds.Contains(p.Id));

            if (starterPokemons == null || !starterPokemons.Any())
                throw new NotFoundException("No starter pokemons found.");

            return _mapper.Map<IEnumerable<PokemonReturnDto>>(starterPokemons);
        }
    }
}
