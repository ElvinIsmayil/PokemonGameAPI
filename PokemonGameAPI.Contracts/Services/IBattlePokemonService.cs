using PokemonGameAPI.Contracts.DTOs.BattlePokemon;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IBattlePokemonService : IGenericService<BattlePokemon, BattlePokemonRequestDto, BattlePokemonResponseDto>
    {
    }
}
