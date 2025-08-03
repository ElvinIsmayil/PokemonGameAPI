namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleTurnDto
    {
        public int BattleId { get; init; }
        public int AttackingBattlePokemonId { get; init; }
        public int TargetBattlePokemonId { get; init; }
        public int AbilityId { get; init; }
    }
}
