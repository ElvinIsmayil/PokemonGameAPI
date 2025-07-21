using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class TrainerPokemonConfiguration : IEntityTypeConfiguration<TrainerPokemon>
    {
        public void Configure(EntityTypeBuilder<TrainerPokemon> builder)
        {
            builder.HasKey(tp => tp.Id);

            // Trainer relationship (many TrainerPokemons to one Trainer)
            builder.HasOne(tp => tp.Trainer)
                   .WithMany(t => t.TrainerPokemons)
                   .HasForeignKey(tp => tp.TrainerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Pokemon relationship (many TrainerPokemons to one Pokemon)
            builder.HasOne(tp => tp.Pokemon)
                   .WithMany(p => p.TrainerPokemons)
                   .HasForeignKey(tp => tp.PokemonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-one with TrainerPokemonStats
            builder.HasOne(tp => tp.TrainerPokemonStats)
                   .WithOne(tps => tps.TrainerPokemon)
                   .HasForeignKey<TrainerPokemonStats>(tps => tps.TrainerPokemonId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
