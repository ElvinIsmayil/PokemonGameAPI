using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Trainer;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerService : IGenericService<Trainer, TrainerRequestDto, TrainerResponseDto>
    {
        Task ChooseStarterPokemonAsync(AssignPokemonDto model);
        Task AssignPokemonToTrainerAsync(AssignPokemonDto model);
        Task<PagedResponse<TrainerPokemonResponseDto>> GetTrainerPokemonsAsync(int trainerId, int pageNumber, int pageSize);
    }
}
