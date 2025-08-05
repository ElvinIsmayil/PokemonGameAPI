using PokemonGameAPI.Domain.Entities;
using PokemonGameAPI.Domain.Entities.Common;

public class Trainer : BaseEntity
{
    public string Name { get; set; } = default!;
    public int Level { get; set; } = 1;
    public int ExperiencePoints { get; set; } = 0;

    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<TrainerBadge> TrainerBadges { get; set; } = new List<TrainerBadge>();
    public ICollection<TrainerPokemon> TrainerPokemons { get; set; } = new List<TrainerPokemon>();
    public ICollection<TrainerTournament> Tournaments { get; set; } = new List<TrainerTournament>();

    public ICollection<Battle> BattlesAsTrainer1 { get; set; } = new List<Battle>();
    public ICollection<Battle> BattlesAsTrainer2 { get; set; } = new List<Battle>();

    public int? GymId { get; set; }
    public Gym? Gym { get; set; }


    public IEnumerable<Battle> Battles => BattlesAsTrainer1.Concat(BattlesAsTrainer2);

}
