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
        private readonly IRepository<Battle> _battleRepository;
        private readonly IRepository<TrainerPokemon> _trainerPokemonRepository;
        private readonly IRepository<PokemonAbility> _abilityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BattleService(
            IRepository<Battle> battleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<TrainerPokemon> trainerPokemonRepository,
            IRepository<PokemonAbility> abilityRepository)
        {
            _battleRepository = battleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trainerPokemonRepository = trainerPokemonRepository;
            _abilityRepository = abilityRepository;
        }

        public async Task<BattleReturnDto> CreateAsync(BattleCreateDto model)
        {
            var battle = _mapper.Map<Battle>(model);
            battle.StartTime = default;
            battle.EndTime = default;

            await _battleRepository.CreateAsync(battle);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BattleReturnDto>(battle);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");

            var entity = await _battleRepository.GetEntityAsync(x => x.Id == id);
            if (entity == null)
                throw new NotFoundException($"Battle with ID {id} not found");

            var result = await _battleRepository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<BattleReturnDto> GetByIdAsync(int id)
        {
            var entity = await _battleRepository.GetEntityAsync(
                predicate: x => x.Id == id,
                asNoTracking: true,
                includes: new Func<IQueryable<Battle>, IQueryable<Battle>>[]
                {
                    q => q.Include(b => b.Trainer1),
                    q => q.Include(b => b.Trainer2),
                    q => q.Include(b => b.BattlePokemons)
                            .ThenInclude(bp => bp.TrainerPokemon)
                            .ThenInclude(tp => tp.Pokemon),
                    q => q.Include(b => b.BattlePokemons)
                            .ThenInclude(bp => bp.TrainerPokemon)
                            .ThenInclude(tp => tp.TrainerPokemonStats)
                });

            if (entity == null)
                throw new NotFoundException($"Battle with ID {id} not found");

            return _mapper.Map<BattleReturnDto>(entity);
        }

        public async Task<PagedResponse<BattleListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var query = _battleRepository.GetQuery()
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

        public async Task<BattleReturnDto> UpdateAsync(int id, BattleUpdateDto model)
        {
            var existingEntity = await _battleRepository.GetEntityAsync(x => x.Id == id);
            if (existingEntity == null)
                throw new NotFoundException($"Entity with ID {id} not found");

            _mapper.Map(model, existingEntity);

            var updatedEntity = await _battleRepository.UpdateAsync(existingEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BattleReturnDto>(updatedEntity);
        }

        public Task<BattleReturnDto> StartBattleAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public Task<BattleReturnDto> FinishBattleAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public Task<BattleReturnDto> GetBattleResultAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public Task<BattleResultDto> ExecuteTurnAsync(BattleTurnDto turn)
        {
            throw new NotImplementedException();
        }
    }
}
