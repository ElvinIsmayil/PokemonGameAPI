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

            builder.Property(b => b.Result)
                   .IsRequired();

            builder.HasOne(b => b.Trainer1)
      .WithMany(t => t.BattlesAsTrainer1)
      .HasForeignKey(b => b.Trainer1Id)
      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Trainer2)
                .WithMany(t => t.BattlesAsTrainer2)
                .HasForeignKey(b => b.Trainer2Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Trainer1Pokemons)
        .WithMany()
        .UsingEntity(j => j.ToTable("Trainer1BattlePokemons"));

            builder.HasMany(b => b.Trainer2Pokemons)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("Trainer2BattlePokemons"));





        }
    }
}
