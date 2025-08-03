using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Infrastructure.Persistence.Configurations
{
    public class TrainerBadgeConfiguration : IEntityTypeConfiguration<TrainerBadge>
    {
        public void Configure(EntityTypeBuilder<TrainerBadge> builder)
        {
            builder.HasKey(tb => new { tb.TrainerId, tb.BadgeId });

            builder.HasOne(tb => tb.Trainer)
                .WithMany(t => t.TrainerBadges)
                .HasForeignKey(tb => tb.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tb => tb.Badge)
                .WithMany(b => b.TrainerBadges)
                .HasForeignKey(tb => tb.BadgeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(tb => tb.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
