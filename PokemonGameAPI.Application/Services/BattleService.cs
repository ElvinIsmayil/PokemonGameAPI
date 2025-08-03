using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonGameAPI.Application.Exceptions;
using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class BattleService : IBattleService
    {
        private readonly IRepository<Battle> _repository;
        private readonly IRepository<TrainerPokemon> _trainerPokemonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BattleService(IRepository<Battle> repository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<TrainerPokemon> trainerPokemonRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trainerPokemonRepository = trainerPokemonRepository;
        }
        public async Task<BattleReturnDto> CreateAsync(BattleCreateDto model)
        {
            var entity = _mapper.Map<Battle>(model);
            entity.Trainer1BattlePokemons = await _trainerPokemonRepository.GetAllAsync(tp => model.Trainer1BattlePokemons.Contains(tp.Id) && model.Trainer1Id == tp.TrainerId);
            entity.Trainer2BattlePokemons = await _trainerPokemonRepository.GetAllAsync(tp => model.Trainer2BattlePokemons.Contains(tp.Id) && model.Trainer2Id == tp.TrainerId);

            await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BattleReturnDto>(entity);
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

        public Task<BattleResultDto> ExecuteTurnAsync(BattleTurnDto turn)
        {
            throw new NotImplementedException();
        }

        public Task<BattleReturnDto> FinishBattleAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<BattleListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _repository.GetQuery()
                .Include(x => x.Trainer1)
                .Include(x => x.Trainer2);

            int totalCount = await query.CountAsync();

            var entities = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<BattleListItemDto>>(entities);

            return new PagedResponse<BattleListItemDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public Task<BattleReturnDto> GetBattleResultAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public async Task<BattleReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
     predicate: x => x.Id == id,
     asNoTracking: true,
     includes: new Func<IQueryable<Battle>, IQueryable<Battle>>[]
     {
        query => query
            .Include(t => t.Trainer1)
            .Include(t => t.Trainer2)
            .Include(t => t.Winner)
            .Include(t => t.Trainer1BattlePokemons)
            .Include(t => t.Trainer2BattlePokemons)
     });

            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }
            return _mapper.Map<BattleReturnDto>(entity);
        }

        public Task<BattleReturnDto> StartBattleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BattleReturnDto> UpdateAsync(int id, BattleUpdateDto model)
        {
            var existingEntity = await _repository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found");
            }

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BattleReturnDto>(updatedEntity);
        }
    }
}
