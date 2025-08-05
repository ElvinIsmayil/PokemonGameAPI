using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TrainerTournamenentEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles");

            migrationBuilder.DropTable(
                name: "TournamentParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_AppUserId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Battles");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "BattleResult",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainerTournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    IsEliminated = table.Column<bool>(type: "bit", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerTournament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerTournament_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerTournament_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_AppUserId",
                table: "Trainers",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerTournament_TournamentId",
                table: "TrainerTournament",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerTournament_TrainerId",
                table: "TrainerTournament",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerTournament");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_AppUserId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "BattleResult",
                table: "Battles");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Battles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TournamentParticipants",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentParticipants", x => new { x.TournamentId, x.TrainerId });
                    table.ForeignKey(
                        name: "FK_TournamentParticipants_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentParticipants_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_AppUserId",
                table: "Trainers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentParticipants_TrainerId",
                table: "TournamentParticipants",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles",
                column: "WinnerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
