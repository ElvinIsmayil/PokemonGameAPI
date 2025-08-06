using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(g => g.Description)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.HasOne(g => g.Badge)
                   .WithOne(b => b.Gym)
                   .HasForeignKey<Badge>(b => b.GymId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.GymLeader)
                   .WithMany()
                   .HasForeignKey(g => g.GymLeaderTrainerId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(g => g.NpcTrainers)
                   .WithOne(t => t.Gym)
                   .HasForeignKey(t => t.GymId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
