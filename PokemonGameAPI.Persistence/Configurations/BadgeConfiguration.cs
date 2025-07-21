using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Infrastructure.Persistence.Configurations
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.ImageUrl)
                .HasMaxLength(300);

            builder.HasMany(b => b.Gyms)
                .WithOne(g => g.Badge)
                .HasForeignKey(g => g.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Trainers)
                .WithMany(t => t.Badges)
                .UsingEntity(j => j.ToTable("TrainerBadges"));
        }
    }
}
