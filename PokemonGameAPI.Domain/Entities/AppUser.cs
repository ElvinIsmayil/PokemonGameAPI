using Microsoft.AspNetCore.Identity;

namespace PokemonGameAPI.Domain.Entities
{
    public sealed class AppUser : IdentityUser
    {
        public string Name { get; set; } = "Guest";
        public string? ProfilePictureUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public Trainer? Trainer { get; set; }
    }
}
