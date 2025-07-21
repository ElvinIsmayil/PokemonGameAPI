using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class PokemonStatsConfiguration : IEntityTypeConfiguration<PokemonStats>
    {
        public void Configure(EntityTypeBuilder<PokemonStats> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Level)
                   .IsRequired();

            builder.Property(ps => ps.ExperiencePoints)
                   .IsRequired();

            builder.Property(ps => ps.HealthPoints)
                   .IsRequired();

            builder.Property(ps => ps.MaxHealthPoints)
                   .IsRequired();

            builder.Property(ps => ps.AttackPoints)
                   .IsRequired();

            builder.Property(ps => ps.DefensePoints)
                   .IsRequired();

            // One-to-one relationship with Pokemon
            builder.HasOne(ps => ps.Pokemon)
                   .WithOne(p => p.BaseStats)
                   .HasForeignKey<PokemonStats>(ps => ps.PokemonId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
