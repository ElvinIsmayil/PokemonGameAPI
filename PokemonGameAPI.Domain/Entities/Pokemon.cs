using PokemonGameAPI.Domain.Entities.Common;

namespace PokemonGameAPI.Domain.Entities
{
    public class Pokemon : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsLegendary { get; set; } = false;
        public bool IsWild { get; set; } = false;
        public string? ImageUrl { get; set; }

        public int PokemonCategoryId { get; set; }
        public PokemonCategory Category { get; set; } = default!;


        public PokemonStats BaseStats { get; set; } = default!;

        public ICollection<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();
        public ICollection<TrainerPokemon> TrainerPokemons { get; set; } = new List<TrainerPokemon>();

    }
}
