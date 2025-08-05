using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonAbilityService : IGenericService<PokemonAbility, PokemonAbilityRequestDto, PokemonAbilityResponseDto>
    {
        Task<PagedResponse<PokemonAbilityResponseDto>> GetAllAbilitiesByPokemonIdAsync(int pokemonId);
        Task<PokemonAbilityResponseDto> AssignPokemonAbility(PokemonAbilityAssignDto model);
    }
}
