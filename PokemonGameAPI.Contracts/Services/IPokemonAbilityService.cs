using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.PokemonAbility;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonAbilityService
    {
        Task<PagedResponse<PokemonAbilityListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<PokemonAbilityReturnDto> GetByIdAsync(int id);
        Task<PokemonAbilityReturnDto> CreateAsync(PokemonAbilityCreateDto model);
        Task<PokemonAbilityReturnDto> UpdateAsync(int id, PokemonAbilityUpdateDto model);
        Task<bool> DeleteAsync(int id);

        Task<List<PokemonAbilityListItemDto>> GetAllAbilitiesByPokemonIdAsync(int pokemonId);
        Task<PokemonAbilityReturnDto> AssignPokemonAbility(PokemonAbilityAssignDto model);
    }
}
