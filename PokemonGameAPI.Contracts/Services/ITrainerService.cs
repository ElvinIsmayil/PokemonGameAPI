using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Trainer;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITrainerService
    {
        Task<PagedResponse<TrainerListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<TrainerReturnDto> GetByIdAsync(int id);
        Task<TrainerReturnDto> CreateAsync(TrainerCreateDto model);
        Task<TrainerReturnDto> UpdateAsync(int id, TrainerUpdateDto model);
        Task<bool> DeleteAsync(int id);

        Task ChooseStarterPokemonAsync(AssignPokemonDto model);
        Task AssignPokemonToTrainerAsync(AssignPokemonDto model);
        Task<PagedResponse<TrainerPokemonListItemDto>> GetTrainerPokemonsAsync(int trainerId, int pageNumber, int pageSize);
    }
}
