using PokemonGameAPI.Contracts.DTOs.PokemonCategory;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface IPokemonCategoryService : IGenericService<PokemonCategory, PokemonCategoryRequestDto, PokemonCategoryResponseDto>
    {

    }
}
