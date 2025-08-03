using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class BattleConfiguration : IEntityTypeConfiguration<Battle>
    {
        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.StartTime)
                   .IsRequired();

            builder.Property(b => b.EndTime)
                   .IsRequired();

            // Trainer1 relationship
            builder.HasOne(b => b.Trainer1)
                   .WithMany(t => t.BattlesAsTrainer1)
                   .HasForeignKey(b => b.Trainer1Id)
                   .OnDelete(DeleteBehavior.Restrict);

            // Trainer2 relationship
            builder.HasOne(b => b.Trainer2)
                   .WithMany(t => t.BattlesAsTrainer2)
                   .HasForeignKey(b => b.Trainer2Id)
                   .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many for Trainer1Pokemons participating in the battle
            builder.HasMany(b => b.Trainer1BattlePokemons)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("Trainer1BattlePokemons"));

            // Many-to-many for Trainer2Pokemons participating in the battle
            builder.HasMany(b => b.Trainer2BattlePokemons)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("Trainer2BattlePokemons"));
        }
    }
}
