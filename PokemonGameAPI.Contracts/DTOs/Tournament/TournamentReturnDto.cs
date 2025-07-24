using PokemonGameAPI.Contracts.DTOs.Battle;
using PokemonGameAPI.Contracts.DTOs.Trainer;

namespace PokemonGameAPI.Contracts.DTOs.Tournament
{
    public record TournamentReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int LocationId { get; init; }
        public ICollection<TrainerListItemDto> ParticipantIds { get; init; } = default!;
        public ICollection<BattleListItemDto> BattleIds { get; init; } = default!;
        public string WinnerName { get; init; } = default!;
    }
}
