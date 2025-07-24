using PokemonGameAPI.Contracts.DTOs.Pagination;
using PokemonGameAPI.Contracts.DTOs.Tournament;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITournamentService
    {
        Task<PagedResponse<TournamentListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<TournamentReturnDto> GetByIdAsync(int id);
        Task<TournamentReturnDto> CreateAsync(TournamentCreateDto model);
        Task<TournamentReturnDto> UpdateAsync(int id, TournamentUpdateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
