using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IBattleService : IGenericService<Battle, BattleRequestDto, BattleResponseDto>
    {
        Task<BattleResponseDto> StartBattleAsync(int battleId);
        Task<BattleResponseDto> FinishBattleAsync(int battleId);
        Task<BattleResponseDto> GetBattleResultAsync(int battleId);
        Task<BattleResultDto> ExecuteTurnAsync(BattleTurnDto turn);


    }
}
