namespace PokemonGameAPI.Contracts.DTOs.BattlePokemon
{
    public record BattlePokemonListItemDto
    {
        public int Id { get; set; }
        public string BattleName { get; set; } = default!;
        public string TrainerPokemonName { get; set; } = default!;
        public int CurrentHP { get; set; }
        public int CurrentLevel { get; set; }
        public bool IsFainted => CurrentHP <= 0;
    }
}
