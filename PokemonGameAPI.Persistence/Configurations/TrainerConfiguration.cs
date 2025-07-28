using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Level)
               .IsRequired();

        builder.Property(t => t.ExperiencePoints)
               .IsRequired();

        // One-to-one relation with AppUser
        builder.HasOne(t => t.AppUser)
               .WithOne()
               .HasForeignKey<Trainer>(t => t.AppUserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        // Many-to-many with Badge
        builder.HasMany(t => t.Badges)
               .WithMany(b => b.Trainers);

        // One-to-many with TrainerPokemon
        builder.HasMany(t => t.TrainerPokemons)
               .WithOne(tp => tp.Trainer)
               .HasForeignKey(tp => tp.TrainerId)
               .OnDelete(DeleteBehavior.Cascade);

        // Many-to-many with Tournament (Participants)
        builder.HasMany(t => t.Tournaments)
               .WithMany(tourn => tourn.Participants);

    }
}
