using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Trainer;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;
using System.Text.RegularExpressions;

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

        public TrainerService(IRepository<Trainer> repository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<TrainerPokemon> trainerPokemonRepository, IRepository<Pokemon> pokemonRepository, IRepository<TrainerPokemonStats> trainerPokemonStatsRepository)
        {
            _trainerRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trainerPokemonRepository = trainerPokemonRepository;
            _pokemonRepository = pokemonRepository;
            _trainerPokemonStatsRepository = trainerPokemonStatsRepository;
        }

        public async Task AssignStarterPokemonAsync(ChooseStarterDto model)
        {
            var trainer = await _trainerRepository.GetEntityAsync(x => x.Id == model.TrainerId);

            if (trainer == null)
                throw new NotFoundException($"Trainer with ID {model.TrainerId} not found");

            var starterPokemon = await _pokemonRepository.GetEntityAsync(x => x.Id == model.StarterPokemonId, asNoTracking: true);
            
            if (starterPokemon == null)
                throw new NotFoundException($"Pokemon with ID {model.StarterPokemonId} not found");
            if (trainer.TrainerPokemons.Any(p => p.Id == model.StarterPokemonId))
                throw new InvalidOperationException($"Trainer with ID {model.TrainerId} already has Pokemon with ID {model.StarterPokemonId}");

            var trainerPokemonStats = _mapper.Map<TrainerPokemonStats>(starterPokemon.BaseStats);
            await _trainerPokemonStatsRepository.CreateAsync(trainerPokemonStats);

            var TrainerPokemon = new TrainerPokemon
            {
                TrainerId = model.TrainerId,
                PokemonId = model.StarterPokemonId,
                TrainerPokemonStatsId = trainerPokemonStats.Id,
                TrainerPokemonStats = trainerPokemonStats,
            };

            await _trainerPokemonRepository.CreateAsync(TrainerPokemon);
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
            await _unitOfWork.SaveChangesAsync();
            return await _trainerRepository.DeleteAsync(entity);
        }

        public async Task<PagedResponse<TrainerListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _trainerRepository.GetQuery().Include(x=>x.AppUser);

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
                x => x.Id == id,
                asNoTracking: true, 
                includes: new Func<IQueryable<Trainer>, IQueryable<Trainer>>[]
                {
                    query => query.Include(t => t.AppUser),
                }
                );
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<TrainerReturnDto>(entity);
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
