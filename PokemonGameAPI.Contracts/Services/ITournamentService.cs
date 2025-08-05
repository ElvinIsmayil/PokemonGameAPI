using PokemonGameAPI.Contracts.DTOs.Tournament;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.Services
{
    public interface ITournamentService : IGenericService<Tournament, TournamentRequestDto, TournamentResponseDto>
    {

    }
}
