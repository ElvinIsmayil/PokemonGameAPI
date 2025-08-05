namespace PokemonGameAPI.Contracts.DTOs.BattlePokemon
{
    public record BattlePokemonReturnDto
    {
        public int Id { get; set; }
        public string BattleName { get; set; } = default!;
        public int TrainerPokemonName { get; set; } = default!;
        public int CurrentHP { get; set; }
        public int CurrentLevel { get; set; }
        public bool IsFainted => CurrentHP <= 0;

    }
}
