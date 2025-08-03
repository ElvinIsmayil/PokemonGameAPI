using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class TrainerPokemonStatsConfiguration : IEntityTypeConfiguration<TrainerPokemonStats>
    {
        public void Configure(EntityTypeBuilder<TrainerPokemonStats> builder)
        {
            builder.HasKey(tps => tps.Id);

            builder.Property(tps => tps.Level)
                   .IsRequired();

            builder.Property(tps => tps.ExperiencePoints)
                   .IsRequired();

            builder.Property(tps => tps.HealthPoints)
                   .IsRequired();

            builder.Property(tps => tps.MaxHealthPoints)
                   .IsRequired();

            builder.Property(tps => tps.AttackPoints)
                   .IsRequired();

            builder.Property(tps => tps.DefensePoints)
                   .IsRequired();

            // Specific property
            builder.Property(tps => tps.AvailableSkillPoints)
                   .IsRequired();

            // One-to-one with TrainerPokemon
            builder.HasOne(tps => tps.TrainerPokemon)
                   .WithOne(tp => tp.TrainerPokemonStats)
                   .HasForeignKey<TrainerPokemonStats>(tps => tps.TrainerPokemonId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
