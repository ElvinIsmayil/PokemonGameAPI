using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        // Primary Key
        builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.Level)
               .IsRequired();

        builder.Property(t => t.ExperiencePoints)
               .IsRequired();

        // AppUser (One-to-One, optional)
        builder.HasOne(t => t.AppUser)
               .WithOne()
               .HasForeignKey<Trainer>(t => t.AppUserId)
               .OnDelete(DeleteBehavior.Cascade); // Or Restrict if you want to keep the user

        // Gym (Many-to-One, optional)
        builder.HasOne(t => t.Gym)
               .WithMany(g => g.NpcTrainers)
               .HasForeignKey(t => t.GymId)
               .OnDelete(DeleteBehavior.SetNull); // Optional relationship

        // TrainerPokemons (One-to-Many)
        builder.HasMany(t => t.TrainerPokemons)
               .WithOne(tp => tp.Trainer)
               .HasForeignKey(tp => tp.TrainerId)
               .OnDelete(DeleteBehavior.Cascade);

        // TrainerBadges (One-to-Many)
        builder.HasMany(t => t.TrainerBadges)
               .WithOne(tb => tb.Trainer)
               .HasForeignKey(tb => tb.TrainerId)
               .OnDelete(DeleteBehavior.Cascade);

        // TrainerTournaments (One-to-Many via join entity)
        builder.HasMany(t => t.Tournaments)
               .WithOne(tt => tt.Trainer)
               .HasForeignKey(tt => tt.TrainerId)
               .OnDelete(DeleteBehavior.Cascade);

        // BattlesAsTrainer1 (One-to-Many)
        builder.HasMany(t => t.BattlesAsTrainer1)
               .WithOne(b => b.Trainer1)
               .HasForeignKey(b => b.Trainer1Id)
               .OnDelete(DeleteBehavior.Restrict);

        // BattlesAsTrainer2 (One-to-Many)
        builder.HasMany(t => t.BattlesAsTrainer2)
               .WithOne(b => b.Trainer2)
               .HasForeignKey(b => b.Trainer2Id)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
