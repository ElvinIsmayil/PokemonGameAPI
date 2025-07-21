using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class PokemonAbilityConfiguration : IEntityTypeConfiguration<PokemonAbility>
    {
        public void Configure(EntityTypeBuilder<PokemonAbility> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pa => pa.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            // Many-to-many between PokemonAbility and Pokemon
            builder.HasMany(pa => pa.Pokemons)
                   .WithMany(p => p.Abilities)
                   .UsingEntity<Dictionary<string, object>>(
                       "PokemonPokemonAbility",
                       j => j
                            .HasOne<Pokemon>()
                            .WithMany()
                            .HasForeignKey("PokemonId")
                            .OnDelete(DeleteBehavior.Cascade),
                       j => j
                            .HasOne<PokemonAbility>()
                            .WithMany()
                            .HasForeignKey("PokemonAbilityId")
                            .OnDelete(DeleteBehavior.Cascade),
                       j =>
                       {
                           j.HasKey("PokemonId", "PokemonAbilityId");
                           j.ToTable("PokemonPokemonAbilities");
                       });
        }
    }
}
