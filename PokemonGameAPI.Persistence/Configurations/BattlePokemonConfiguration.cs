using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

public class BattlePokemonConfiguration : IEntityTypeConfiguration<BattlePokemon>
{
    public void Configure(EntityTypeBuilder<BattlePokemon> builder)
    {
        // Composite key
        builder.HasKey(bp => new { bp.BattleId, bp.TrainerPokemonId });

        // Relationships
        builder.HasOne(bp => bp.Battle)
            .WithMany(b => b.Trainer1BattlePokemons) // or Trainer2BattlePokemons, you'll need to clarify usage
            .HasForeignKey(bp => bp.BattleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bp => bp.TrainerPokemon)
            .WithMany() // Assuming TrainerPokemon doesn't have BattlePokemons navigation
            .HasForeignKey(bp => bp.TrainerPokemonId)
            .OnDelete(DeleteBehavior.Restrict);

        // Property configs (optional)
        builder.Property(bp => bp.CurrentHP)
            .IsRequired();

        builder.Property(bp => bp.CurrentLevel)
            .IsRequired();
    }
}
