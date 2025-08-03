using Microsoft.AspNetCore.Http;
using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Pokemon;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonService
    {
        Task<PagedResponse<PokemonListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<PokemonReturnDto> GetByIdAsync(int id);
        Task<PokemonReturnDto> CreateAsync(PokemonCreateDto model);
        Task<PokemonReturnDto> UpdateAsync(int id, PokemonUpdateDto model);
        Task<PokemonReturnDto> UploadImgAsync(int id, IFormFile file);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PokemonReturnDto>> GetStarterPokemonsAsync();
    }
}
