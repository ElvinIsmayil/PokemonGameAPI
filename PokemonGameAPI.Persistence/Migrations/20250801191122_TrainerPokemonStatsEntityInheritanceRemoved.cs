using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TrainerPokemonStatsEntityInheritanceRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Trainers_TrainerId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonStats_TrainerPokemons_TrainerPokemonId",
                table: "PokemonStats");

            migrationBuilder.DropIndex(
                name: "IX_PokemonStats_TrainerPokemonId",
                table: "PokemonStats");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainerId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrainerPokemonStatsId",
                table: "TrainerPokemons");

            migrationBuilder.DropColumn(
                name: "AvailableSkillPoints",
                table: "PokemonStats");

            migrationBuilder.DropColumn(
                name: "StatsType",
                table: "PokemonStats");

            migrationBuilder.DropColumn(
                name: "TrainerPokemonId",
                table: "PokemonStats");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainerPokemonStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ExperiencePoints = table.Column<int>(type: "int", nullable: false),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    MaxHealthPoints = table.Column<int>(type: "int", nullable: false),
                    AttackPoints = table.Column<int>(type: "int", nullable: false),
                    DefensePoints = table.Column<int>(type: "int", nullable: false),
                    AvailableSkillPoints = table.Column<int>(type: "int", nullable: false),
                    TrainerPokemonId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerPokemonStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerPokemonStats_TrainerPokemons_TrainerPokemonId",
                        column: x => x.TrainerPokemonId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_AppUserId1",
                table: "Trainers",
                column: "AppUserId1",
                unique: true,
                filter: "[AppUserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerPokemonStats_TrainerPokemonId",
                table: "TrainerPokemonStats",
                column: "TrainerPokemonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_AppUserId1",
                table: "Trainers",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_AppUserId1",
                table: "Trainers");

            migrationBuilder.DropTable(
                name: "TrainerPokemonStats");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_AppUserId1",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Trainers");

            migrationBuilder.AddColumn<int>(
                name: "TrainerPokemonStatsId",
                table: "TrainerPokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSkillPoints",
                table: "PokemonStats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatsType",
                table: "PokemonStats",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrainerPokemonId",
                table: "PokemonStats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainerId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStats_TrainerPokemonId",
                table: "PokemonStats",
                column: "TrainerPokemonId",
                unique: true,
                filter: "[TrainerPokemonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainerId1",
                table: "AspNetUsers",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trainers_TrainerId1",
                table: "AspNetUsers",
                column: "TrainerId1",
                principalTable: "Trainers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonStats_TrainerPokemons_TrainerPokemonId",
                table: "PokemonStats",
                column: "TrainerPokemonId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
