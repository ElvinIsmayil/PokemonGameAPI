using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFilteredUniqueIndexToBadges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
       name: "IX_Badges_GymId",
       table: "Badges");

            migrationBuilder.Sql(
                @"CREATE UNIQUE INDEX IX_Badges_GymId_NotDeleted ON Badges (GymId) WHERE IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
        @"DROP INDEX IX_Badges_GymId_NotDeleted ON Badges");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_GymId",
                table: "Badges",
                column: "GymId",
                unique: true);
        }
    }
}
