using Microsoft.AspNetCore.Http;
using PokemonGameAPI.Contracts.DTOs.Pokemon;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonService : IGenericService<Pokemon, PokemonRequestDto, PokemonResponseDto>
    {

        Task<PokemonResponseDto> UploadImgAsync(int id, IFormFile file);

        Task<IEnumerable<PokemonResponseDto>> GetStarterPokemonsAsync();
    }
}
