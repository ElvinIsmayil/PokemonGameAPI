using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class TrainerTournament : BaseEntity
    {
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = default!;

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = default!;

        public int Score { get; set; }
        public int Rank { get; set; }
        public bool IsEliminated { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }

}
