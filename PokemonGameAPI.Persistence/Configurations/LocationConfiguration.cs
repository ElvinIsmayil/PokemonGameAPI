using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.Description)
                   .HasMaxLength(300);

            builder.Property(l => l.Latitude)
                   .IsRequired();

            builder.Property(l => l.Longitude)
                   .IsRequired();

            builder.HasMany(l => l.WildPokemons)
                   .WithOne()
                   .HasForeignKey("LocationId")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(l => l.Gyms)
                   .WithOne(g => g.Location)
                   .HasForeignKey(g => g.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Tournaments)
                   .WithOne()
                   .HasForeignKey(t => t.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
