using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Trainer;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Contracts.Settings;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class TrainerService : GenericService<Trainer, TrainerRequestDto, TrainerResponseDto>, ITrainerService
    {
        private readonly IGenericRepository<TrainerPokemon> _trainerPokemonRepository;
        private readonly IGenericRepository<TrainerPokemonStats> _trainerPokemonStatsRepository;
        private readonly IGenericRepository<Pokemon> _pokemonRepository;
        private readonly PokemonSettings _pokemonSettings;

        public TrainerService(IGenericRepository<Trainer> repository, IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<TrainerPokemon> trainerPokemonRepository,
            IGenericRepository<TrainerPokemonStats> trainerPokemonStatsRepository,
            IGenericRepository<Pokemon> pokemonRepository, IOptions<PokemonSettings> pokemonSettings) : base(repository, unitOfWork, mapper)
        {
            _trainerPokemonRepository = trainerPokemonRepository;
            _trainerPokemonStatsRepository = trainerPokemonStatsRepository;
            _pokemonRepository = pokemonRepository;
            _pokemonSettings = pokemonSettings.Value;
        }

        public async Task AssignPokemonToTrainerAsync(AssignPokemonDto model)
        {
            bool pokemonExists = await _pokemonRepository.IsExistsAsync(x => x.Id == model.PokemonId);
            if (!pokemonExists)
                throw new NotFoundException($"Pokemon with ID {model.PokemonId} not found");
            var trainer = await _repository.GetEntityAsync(
     x => x.Id == model.TrainerId,
     includes: new Func<IQueryable<Trainer>, IQueryable<Trainer>>[]
     {
        q => q.Include(t => t.TrainerPokemons)
     }
 );

            if (trainer is null)
                throw new NotFoundException($"Trainer with ID {model.TrainerId} not found");

            var trainerPokemonExists = await _trainerPokemonRepository.IsExistsAsync(x => x.TrainerId == model.TrainerId && x.PokemonId == model.PokemonId);

            if (trainerPokemonExists)
                throw new ConflictException($"Trainer with ID {model.TrainerId} already has Pokemon with ID {model.PokemonId}");

            var trainerPokemon = new TrainerPokemon
            {
                TrainerId = model.TrainerId,
                PokemonId = model.PokemonId
            };
            await _trainerPokemonRepository.CreateAsync(trainerPokemon);
            await _unitOfWork.SaveChangesAsync();

        }
        public async Task ChooseStarterPokemonAsync(AssignPokemonDto model)
        {
            var trainer = await _repository.GetEntityAsync(x => x.Id == model.TrainerId);
            if (trainer == null)
                throw new NotFoundException($"Trainer with ID {model.TrainerId} not found");

            if (!_pokemonSettings.StarterPokemons.Contains(model.PokemonId))
            {
                throw new ValidationException("Selected Pokémon is not a valid starter Pokémon.");

            }
            var starterPokemon = await _pokemonRepository.GetEntityAsync(x => x.Id == model.PokemonId);
            if (starterPokemon == null)
                throw new NotFoundException($"Pokemon with ID {model.PokemonId} not found");

            if (trainer.TrainerPokemons.Any(p => p.PokemonId == model.PokemonId))
                throw new ConflictException($"Trainer with ID {model.TrainerId} already has Pokemon with ID {model.PokemonId}");

            var trainerPokemon = new TrainerPokemon
            {
                TrainerId = model.TrainerId,
                PokemonId = model.PokemonId
            };

            await _trainerPokemonRepository.CreateAsync(trainerPokemon);
            await _unitOfWork.SaveChangesAsync();

            var trainerPokemonStats = new TrainerPokemonStats
            {
                Level = starterPokemon.BaseStats.Level,
                ExperiencePoints = starterPokemon.BaseStats.ExperiencePoints,
                HealthPoints = starterPokemon.BaseStats.HealthPoints,
                MaxHealthPoints = starterPokemon.BaseStats.MaxHealthPoints,
                AttackPoints = starterPokemon.BaseStats.AttackPoints,
                DefensePoints = starterPokemon.BaseStats.DefensePoints,
                AvailableSkillPoints = 0,
                TrainerPokemonId = trainerPokemon.Id
            };

            await _trainerPokemonStatsRepository.CreateAsync(trainerPokemonStats);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<PagedResponse<TrainerPokemonResponseDto>> GetTrainerPokemonsAsync(int trainerId, int pageSize, int pageNumber)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _trainerPokemonRepository.GetQuery().Include(x => x.Pokemon)
                .Include(x => x.Trainer)
                .Where(x => x.TrainerId == trainerId);


            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<TrainerPokemonResponseDto>>(entities);

            return new PagedResponse<TrainerPokemonResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }


    }
}
