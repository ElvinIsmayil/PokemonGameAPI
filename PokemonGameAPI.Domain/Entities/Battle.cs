using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Battle : BaseEntity
    {
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }


        public int Trainer1Id { get; set; }
        public Trainer Trainer1 { get; set; } = default!;

        public int Trainer2Id { get; set; }
        public Trainer Trainer2 { get; set; } = default!;

        public ICollection<Pokemon> Trainer1Pokemons { get; set; } = new List<Pokemon>();
        public ICollection<Pokemon> Trainer2Pokemons { get; set; } = new List<Pokemon>();

        public int? WinnerId { get; set; }
        public Trainer? Winner { get; set; }


    }
}
