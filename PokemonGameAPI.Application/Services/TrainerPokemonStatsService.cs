using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemonStats;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class TrainerPokemonStatsService : ITrainerPokemonStatsService
    {

        private readonly IRepository<TrainerPokemonStats> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerPokemonStatsService(IRepository<TrainerPokemonStats> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TrainerPokemonStatsReturnDto> CreateAsync(TrainerPokemonStatsCreateDto model)
        {
            var entity = _mapper.Map<TrainerPokemonStats>(model);
            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TrainerPokemonStatsReturnDto>(entity);
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

        public async Task<PagedResponse<TrainerPokemonStatsListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery().Include(x => x.TrainerPokemon);

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<TrainerPokemonStatsListItemDto>>(entities);

            return new PagedResponse<TrainerPokemonStatsListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<TrainerPokemonStatsReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
                 predicate: x => x.Id == id,
                 asNoTracking: true,
                 includes: new Func<IQueryable<TrainerPokemonStats>, IQueryable<TrainerPokemonStats>>[]
                 {
                    query => query.Include(t => t.TrainerPokemon)
                 }
                 );
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<TrainerPokemonStatsReturnDto>(entity);
        }

        public async Task<TrainerPokemonStatsReturnDto> UpdateAsync(int id, TrainerPokemonStatsUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TrainerPokemonStatsReturnDto>(updatedEntity);
        }
    }
}
