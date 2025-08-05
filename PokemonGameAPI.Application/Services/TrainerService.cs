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
    public class TrainerService : ITrainerService
    {
        private readonly IRepository<Trainer> _trainerRepository;
        private readonly IRepository<TrainerPokemon> _trainerPokemonRepository;
        private readonly IRepository<TrainerPokemonStats> _trainerPokemonStatsRepository;
        private readonly IRepository<Pokemon> _pokemonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PokemonSettings _pokemonSettings;

        public TrainerService(IRepository<Trainer> repository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<TrainerPokemon> trainerPokemonRepository, IRepository<TrainerPokemonStats> trainerPokemonStatsRepository, IRepository<Pokemon> pokemonRepository, IOptions<PokemonSettings> pokemonSettings)
        {
            _trainerRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            var trainer = await _trainerRepository.GetEntityAsync(
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
            var trainer = await _trainerRepository.GetEntityAsync(x => x.Id == model.TrainerId);
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

        public async Task<TrainerReturnDto> CreateAsync(TrainerCreateDto model)
        {
            var entity = _mapper.Map<Trainer>(model);
            await _trainerRepository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TrainerReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
            }
            var entity = await _trainerRepository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            var result = await _trainerRepository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<PagedResponse<TrainerListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _trainerRepository.GetQuery()
                .Include(x => x.AppUser)
                .Include(x => x.TrainerPokemons)
                .Include(x => x.Tournaments);


            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<TrainerListItemDto>>(entities);

            return new PagedResponse<TrainerListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<TrainerReturnDto> GetByIdAsync(int id)
        {
            var entity = await _trainerRepository.GetEntityAsync(
                predicate: x => x.Id == id,
                asNoTracking: true,
                includes: new Func<IQueryable<Trainer>, IQueryable<Trainer>>[]
                {
                    query => query.Include(t => t.AppUser)
                    .Include(t=> t.TrainerPokemons)
                    .Include(t=> t.Tournaments)
                    .Include(t=>t.TrainerBadges)
                }
                );
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<TrainerReturnDto>(entity);
        }

        public async Task<PagedResponse<TrainerPokemonListItemDto>> GetTrainerPokemonsAsync(int trainerId, int pageSize, int pageNumber)
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

            var data = _mapper.Map<List<TrainerPokemonListItemDto>>(entities);

            return new PagedResponse<TrainerPokemonListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<TrainerReturnDto> UpdateAsync(int id, TrainerUpdateDto model)
        {
            var existingEntity = await _trainerRepository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _trainerRepository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TrainerReturnDto>(updatedEntity);
        }


    }
}
