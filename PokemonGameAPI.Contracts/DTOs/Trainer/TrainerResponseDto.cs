using PokemonGameAPI.Contracts.DTOs.Badge;
using PokemonGameAPI.Contracts.DTOs.TrainerPokemon;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Contracts.DTOs.Trainer
{
    public record TrainerResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Level { get; init; } = 1;
        public int ExperiencePoints { get; init; } = 0;
        public string AppUserName { get; init; } = default!;
        public string? ProfilePictureUrl { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
        public ICollection<TrainerPokemonResponseDto> TrainerPokemons { get; init; } = new List<TrainerPokemonResponseDto>();
        public ICollection<BadgeResponseDto> Badges { get; init; } = new List<BadgeResponseDto>();

    }
}
