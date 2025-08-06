using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PokemonGameAPI.Application.CustomExceptions;
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
        private readonly PokemonSettings _pokemonSettings;
        private readonly ICloudinaryService _cloudinaryService;

        public PokemonService(IGenericRepository<Pokemon> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<PokemonStats> pokemonStatsRepository, IGenericRepository<PokemonCategory> pokemonCategoryRepository, IGenericRepository<PokemonAbility> pokemonAbilityRepository, IOptions<PokemonSettings> pokemonSettings, ICloudinaryService cloudinaryService) : base(repository, unitOfWork, mapper)
        {
            _pokemonStatsRepository = pokemonStatsRepository;
            _pokemonCategoryRepository = pokemonCategoryRepository;
            _pokemonAbilityRepository = pokemonAbilityRepository;
            _pokemonSettings = pokemonSettings.Value;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<PokemonResponseDto> UploadImgAsync(int id, IFormFile file)
        {
            var pokemon = await _repository.GetEntityAsync(b => b.Id == id);
            if (pokemon is null)
                throw new CustomException($"pokemon with ID {id} not found.");
            var uploadResult = await _cloudinaryService.UploadImageAsync(file);
            if (uploadResult.Error != null)
                throw new CustomException($"Image upload failed: {uploadResult.Error.Message}");
            pokemon.ImageUrl = uploadResult.SecureUrl.ToString();
            await _repository.UpdateAsync(pokemon);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PokemonResponseDto>(pokemon);
        }
        public async Task<IEnumerable<PokemonResponseDto>> GetStarterPokemonsAsync()
        {
            var starterPokemonIds = _pokemonSettings.StarterPokemons;

            var starterPokemons = await _repository.GetAllAsync(p => starterPokemonIds.Contains(p.Id));

            if (starterPokemons == null || !starterPokemons.Any())
                throw new CustomException("No starter pokemons found.");

            return _mapper.Map<IEnumerable<PokemonResponseDto>>(starterPokemons);
        }
    }
}
