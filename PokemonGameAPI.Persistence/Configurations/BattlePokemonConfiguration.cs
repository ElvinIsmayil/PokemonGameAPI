using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

public class BattlePokemonConfiguration : IEntityTypeConfiguration<BattlePokemon>
{
    public void Configure(EntityTypeBuilder<BattlePokemon> builder)
    {
        // Composite key
        builder.HasKey(bp => new { bp.BattleId, bp.TrainerPokemonId });

        // Relationship with Battle
        builder.HasOne(bp => bp.Battle)
            .WithMany(b => b.BattlePokemons)  // single collection in Battle
            .HasForeignKey(bp => bp.BattleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relationship with TrainerPokemon
        builder.HasOne(bp => bp.TrainerPokemon)
            .WithMany() // assuming TrainerPokemon does not track BattlePokemons
            .HasForeignKey(bp => bp.TrainerPokemonId)
            .OnDelete(DeleteBehavior.Restrict);

        // Required properties
        builder.Property(bp => bp.CurrentHP)
            .IsRequired();

        builder.Property(bp => bp.CurrentLevel)
            .IsRequired();

        // Configure TrainerSide as int (enum)
        builder.Property(bp => bp.Side)
            .IsRequired()
            .HasConversion<int>();
    }
}
