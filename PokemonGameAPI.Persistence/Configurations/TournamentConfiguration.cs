using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonGameAPI.Domain.Entities;

namespace PokemonGameAPI.Persistence.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired();

            builder.HasOne(t => t.Location)
    .WithMany(l => l.Tournaments)
    .HasForeignKey(t => t.LocationId)
    .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many: Tournament <-> Trainers (Participants)
            builder.HasMany(t => t.Participants)
                .WithMany(tr => tr.Tournaments)
                .UsingEntity<Dictionary<string, object>>(
                    "TournamentParticipant",
                    j => j.HasOne<Trainer>()
                          .WithMany()
                          .HasForeignKey("TrainerId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Tournament>()
                          .WithMany()
                          .HasForeignKey("TournamentId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("TournamentId", "TrainerId");
                        j.ToTable("TournamentParticipants");
                    });


            // Winner relation (optional)
            builder.HasOne(t => t.Winner)
                .WithMany()
                .HasForeignKey(t => t.WinnerId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
