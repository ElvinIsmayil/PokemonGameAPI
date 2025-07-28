using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Gyms_GymId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Badges_BadgeId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer1BattlePokemons_TrainerPokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer2BattlePokemons_TrainerPokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons");

            migrationBuilder.DropTable(
                name: "GymPokemons");

            migrationBuilder.DropIndex(
                name: "IX_Gyms_BadgeId",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "BadgeId",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Battles");

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Battles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Badges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_GymId",
                table: "Trainers",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_GymId",
                table: "Badges",
                column: "GymId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Gyms_GymId",
                table: "Badges",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Gyms_GymId",
                table: "Battles",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles",
                column: "WinnerId",
                principalTable: "Trainers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer1BattlePokemons_Pokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons",
                column: "Trainer1PokemonsId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer2BattlePokemons_Pokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons",
                column: "Trainer2PokemonsId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Gyms_GymId",
                table: "Trainers",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Gyms_GymId",
                table: "Badges");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Gyms_GymId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer1BattlePokemons_Pokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer2BattlePokemons_Pokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Gyms_GymId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_GymId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Badges_GymId",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Badges");

            migrationBuilder.AddColumn<int>(
                name: "BadgeId",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GymPokemons",
                columns: table => new
                {
                    GymId = table.Column<int>(type: "int", nullable: false),
                    PokemonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymPokemons", x => new { x.GymId, x.PokemonsId });
                    table.ForeignKey(
                        name: "FK_GymPokemons_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymPokemons_Pokemons_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_BadgeId",
                table: "Gyms",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_GymPokemons_PokemonsId",
                table: "GymPokemons",
                column: "PokemonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Gyms_GymId",
                table: "Battles",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Badges_BadgeId",
                table: "Gyms",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer1BattlePokemons_TrainerPokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons",
                column: "Trainer1PokemonsId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer2BattlePokemons_TrainerPokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons",
                column: "Trainer2PokemonsId",
                principalTable: "TrainerPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
