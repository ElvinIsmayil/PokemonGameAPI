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

            builder.HasOne(g => g.Location)
                   .WithMany(l => l.Gyms)
                   .HasForeignKey(g => g.LocationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Badge)
                   .WithMany(b => b.Gyms)
                   .HasForeignKey(g => g.BadgeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.GymLeader)
                   .WithMany()
                   .HasForeignKey(g => g.GymLeaderTrainerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Battles)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.Pokemons)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("GymPokemons"));
        }
    }
}
