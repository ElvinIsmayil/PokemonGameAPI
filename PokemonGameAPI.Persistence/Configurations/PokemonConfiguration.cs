using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.IsLegendary)
                .HasDefaultValue(false);

            builder.Property(p => p.IsWild)
                .HasDefaultValue(false);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(250)
                .IsRequired(false);

            // Relations

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Pokemons)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.BaseStats)
                .WithOne(bs => bs.Pokemon)
                .HasForeignKey<Pokemon>(p => p.BaseStatsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Abilities)
                .WithMany(a => a.Pokemons)
                .UsingEntity<Dictionary<string, object>>(
                    "PokemonAbilityMapping",
                    j => j.HasOne<PokemonAbility>()
                          .WithMany()
                          .HasForeignKey("AbilityId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Pokemon>()
                          .WithMany()
                          .HasForeignKey("PokemonId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("PokemonId", "AbilityId");
                        j.ToTable("PokemonAbilityMappings");
                    });

            builder.HasMany(p => p.TrainerPokemons)
                .WithOne(tp => tp.Pokemon)
                .HasForeignKey(tp => tp.PokemonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
