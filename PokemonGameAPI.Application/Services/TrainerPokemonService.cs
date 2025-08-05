using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class TrainerPokemonService : ITrainerPokemonService
    {
        private readonly IRepository<TrainerPokemon> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerPokemonService(IRepository<TrainerPokemon> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TrainerPokemonReturnDto> CreateAsync(TrainerPokemonCreateDto model)
        {
            var entity = _mapper.Map<TrainerPokemon>(model);
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TrainerPokemonReturnDto>(entity);
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

        public async Task<PagedResponse<TrainerPokemonListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery()
                .Include(x => x.Trainer)
                .Include(x => x.Pokemon);

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

        public async Task<TrainerPokemonReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
                 predicate: x => x.Id == id,
                 asNoTracking: true,
                 includes: new Func<IQueryable<TrainerPokemon>, IQueryable<TrainerPokemon>>[]
                 {
                    query => query.Include(t => t.Trainer)
                                  .Include(t => t.Pokemon)
                                  .Include(t => t.TrainerPokemonStats)
                 }
             );
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<TrainerPokemonReturnDto>(entity);
        }

        public async Task<TrainerPokemonReturnDto> UpdateAsync(int id, TrainerPokemonUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TrainerPokemonReturnDto>(updatedEntity);
        }

        public async Task LevelUpAsync(LevelUpDto model)
        {
            var entity = await _repository.GetEntityAsync(
                x => x.Id == model.TrainerPokemonId,
                includes: new Func<IQueryable<TrainerPokemon>, IQueryable<TrainerPokemon>>[]
                {
                    q => q.Include(tp => tp.TrainerPokemonStats)
                });

            if (entity == null)
                throw new NotFoundException($"TrainerPokemon with ID {model.TrainerPokemonId} not found.");

            var stats = entity.TrainerPokemonStats;
            stats.ExperiencePoints += model.NewExperiencePoints;

            while (stats.ExperiencePoints >= 100)
            {
                stats.Level++;
                stats.ExperiencePoints -= 100;

                stats.MaxHealthPoints += 10;
                stats.HealthPoints = stats.MaxHealthPoints; // Heal on level up
                stats.AttackPoints += 2;
                stats.DefensePoints += 2;
                stats.AvailableSkillPoints += 1;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EvolveAsync(int trainerPokemonId)
        {
            var entity = await _repository.GetEntityAsync(
                x => x.Id == trainerPokemonId,
                includes: new Func<IQueryable<TrainerPokemon>, IQueryable<TrainerPokemon>>[]
                {
                    q => q.Include(tp => tp.Pokemon)
                          .ThenInclude(p => p.PokemonCategoryId) // if needed for evolution data
                });

            if (entity == null)
                throw new NotFoundException($"TrainerPokemon with ID {trainerPokemonId} not found.");

            var currentPokemon = entity.Pokemon;

            if (entity.TrainerPokemonStats.Level >= 16)
            {
                var evolvedPokemon = await FindEvolvedPokemonAsync(currentPokemon.Id);

                if (evolvedPokemon != null)
                {
                    entity.PokemonId = evolvedPokemon.Id;
                    entity.Pokemon = evolvedPokemon;

                    entity.TrainerPokemonStats.Level = 1;
                    entity.TrainerPokemonStats.ExperiencePoints = 0;
                    entity.TrainerPokemonStats.HealthPoints = evolvedPokemon.BaseStats.MaxHealthPoints;
                    entity.TrainerPokemonStats.MaxHealthPoints = evolvedPokemon.BaseStats.MaxHealthPoints;
                    entity.TrainerPokemonStats.AttackPoints = evolvedPokemon.BaseStats.AttackPoints;
                    entity.TrainerPokemonStats.DefensePoints = evolvedPokemon.BaseStats.DefensePoints;
                    entity.TrainerPokemonStats.AvailableSkillPoints = 0;

                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        private async Task<Pokemon?> FindEvolvedPokemonAsync(int currentPokemonId)
        {
            return null;
        }

        public async Task<TrainerPokemonReturnDto> GainExperienceAsync(LevelUpDto model)
        {
            await LevelUpAsync(model);
            await EvolveAsync(model.TrainerPokemonId);
            return await GetByIdAsync(model.TrainerPokemonId);
        }
    }
}
