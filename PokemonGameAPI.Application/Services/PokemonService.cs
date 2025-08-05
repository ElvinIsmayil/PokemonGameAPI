using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class PokemonService : GenericService<Pokemon, PokemonRequestDto, PokemonResponseDto>, IPokemonService
    {
        private readonly IGenericRepository<PokemonStats> _pokemonStatsRepository;
        private readonly IGenericRepository<PokemonCategory> _pokemonCategoryRepository;
        private readonly IGenericRepository<PokemonAbility> _pokemonAbilityRepository;
        private readonly IImageService _imageService;
        private const string folderName = "pokemons";
        private readonly PokemonSettings _pokemonSettings;

        public PokemonService(IGenericRepository<Pokemon> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<PokemonStats> pokemonStatsRepository, IGenericRepository<PokemonCategory> pokemonCategoryRepository, IGenericRepository<PokemonAbility> pokemonAbilityRepository, IImageService imageService, IOptions<PokemonSettings> pokemonSettings) : base(repository, unitOfWork, mapper)
        {
            _pokemonStatsRepository = pokemonStatsRepository;
            _pokemonCategoryRepository = pokemonCategoryRepository;
            _pokemonAbilityRepository = pokemonAbilityRepository;
            _imageService = imageService;
            _pokemonSettings = pokemonSettings.Value;
        }

        public async Task<PokemonResponseDto> UploadImgAsync(int id, IFormFile imageFile)
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
            return _mapper.Map<PokemonResponseDto>(updatedEntity);
        }

        public async Task<IEnumerable<PokemonResponseDto>> GetStarterPokemonsAsync()
        {
            var starterPokemonIds = _pokemonSettings.StarterPokemons;

            var starterPokemons = await _repository.GetAllAsync(p => starterPokemonIds.Contains(p.Id));

            if (starterPokemons == null || !starterPokemons.Any())
                throw new NotFoundException("No starter pokemons found.");

            return _mapper.Map<IEnumerable<PokemonResponseDto>>(starterPokemons);
        }
    }
}
