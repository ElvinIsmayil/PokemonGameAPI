using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class BattleService : GenericService<Battle, BattleRequestDto, BattleResponseDto>, IBattleService
    {
        public BattleService(IGenericRepository<Battle> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }

        public Task<BattleResponseDto> StartBattleAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public Task<BattleResponseDto> FinishBattleAsync(int battleId)
        {
            throw new NotImplementedException();
        }

        public Task<BattleResponseDto> GetBattleResultAsync(int battleId)
        {
            throw new NotImplementedException();

        }

        public Task<BattleResultDto> ExecuteTurnAsync(BattleTurnDto turn)
        {
            throw new NotImplementedException();

        }
    }
}
