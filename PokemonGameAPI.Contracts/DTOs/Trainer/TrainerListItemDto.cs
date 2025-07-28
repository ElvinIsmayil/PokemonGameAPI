namespace PokemonGameAPI.Contracts.DTOs.Trainer
{
    public record TrainerListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Level { get; init; } = 1;
        public int ExperiencePoints { get; init; } = 0;
        public string AppUserName { get; init; } = default!;
        public string? ProfilePictureUrl { get; init; }

        public int TrainerPokemonCount { get; init; } = 0;
        public int BattleCount { get; init; } = 0;
        public int TournamentCount { get; init; } = 0;
    }
}
