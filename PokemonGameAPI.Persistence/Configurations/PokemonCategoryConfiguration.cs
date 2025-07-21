using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class PokemonCategoryConfiguration : IEntityTypeConfiguration<PokemonCategory>
    {
        public void Configure(EntityTypeBuilder<PokemonCategory> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pc => pc.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            // One-to-many relationship: Category has many Pokemons
            builder.HasMany(pc => pc.Pokemons)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
