namespace PokemonGameAPI.Contracts.DTOs.Battle
{
    public record BattleResultDto
    {
        public int BattleId { get; init; }
        public string ActionSummary { get; init; } = string.Empty;
        public int DamageDealt { get; init; }
        public int TargetRemainingHP { get; init; }
        public bool IsTargetFainted { get; init; }
        public bool IsBattleOver { get; init; }
        public string? WinnerTrainerName { get; init; }
    }
}
