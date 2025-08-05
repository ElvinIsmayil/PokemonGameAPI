using AutoMapper;
using PokemonGameAPI.Contracts.DTOs.Tournament;
using PokemonGameAPI.Contracts.Services;
using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Repository;

namespace PokemonGameAPI.Application.Services
{
    public class TournamentService : GenericService<Tournament, TournamentRequestDto, TournamentResponseDto>, ITournamentService
    {
        public TournamentService(IGenericRepository<Tournament> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }
    }
}
