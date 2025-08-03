using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SlightChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Tournaments_TournamentId",
                table: "Battles");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Tournaments_TournamentId",
                table: "Battles",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Tournaments_TournamentId",
                table: "Battles");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Tournaments_TournamentId",
                table: "Battles",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
