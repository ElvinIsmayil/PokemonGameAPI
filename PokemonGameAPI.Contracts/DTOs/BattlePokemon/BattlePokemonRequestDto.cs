namespace PokemonGameAPI.Contracts.DTOs.BattlePokemon
{
    public record BattlePokemonRequestDto
    {
        public int BattleId { get; set; }
        public int TrainerPokemonId { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentLevel { get; set; }
        public bool IsFainted => CurrentHP <= 0;
    }
}
