using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Locations_LocationId",
                table: "Gyms");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Locations_LocationId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Locations_LocationId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Trainer1BattlePokemons");

            migrationBuilder.DropTable(
                name: "Trainer2BattlePokemons");

            migrationBuilder.DropTable(
                name: "TrainerBadges");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_LocationId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_LocationId",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Gyms_LocationId",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Gyms");

            migrationBuilder.CreateTable(
                name: "BattlePokemon",
                columns: table => new
                {
                    BattleId = table.Column<int>(type: "int", nullable: false),
                    TrainerPokemonId = table.Column<int>(type: "int", nullable: false),
                    Side = table.Column<int>(type: "int", nullable: false),
                    CurrentHP = table.Column<int>(type: "int", nullable: false),
                    CurrentLevel = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattlePokemon", x => new { x.BattleId, x.TrainerPokemonId });
                    table.ForeignKey(
                        name: "FK_BattlePokemon_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattlePokemon_TrainerPokemons_TrainerPokemonId",
                        column: x => x.TrainerPokemonId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerBadge",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    BadgeId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerBadge", x => new { x.TrainerId, x.BadgeId });
                    table.ForeignKey(
                        name: "FK_TrainerBadge_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerBadge_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattlePokemon_TrainerPokemonId",
                table: "BattlePokemon",
                column: "TrainerPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerBadge_BadgeId",
                table: "TrainerBadge",
                column: "BadgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles",
                column: "WinnerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles");

            migrationBuilder.DropTable(
                name: "BattlePokemon");

            migrationBuilder.DropTable(
                name: "TrainerBadge");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Pokemons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainer1BattlePokemons",
                columns: table => new
                {
                    BattleId = table.Column<int>(type: "int", nullable: false),
                    Trainer1PokemonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer1BattlePokemons", x => new { x.BattleId, x.Trainer1PokemonsId });
                    table.ForeignKey(
                        name: "FK_Trainer1BattlePokemons_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainer1BattlePokemons_TrainerPokemons_Trainer1PokemonsId",
                        column: x => x.Trainer1PokemonsId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainer2BattlePokemons",
                columns: table => new
                {
                    Battle1Id = table.Column<int>(type: "int", nullable: false),
                    Trainer2PokemonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer2BattlePokemons", x => new { x.Battle1Id, x.Trainer2PokemonsId });
                    table.ForeignKey(
                        name: "FK_Trainer2BattlePokemons_Battles_Battle1Id",
                        column: x => x.Battle1Id,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainer2BattlePokemons_TrainerPokemons_Trainer2PokemonsId",
                        column: x => x.Trainer2PokemonsId,
                        principalTable: "TrainerPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerBadges",
                columns: table => new
                {
                    BadgesId = table.Column<int>(type: "int", nullable: false),
                    TrainersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerBadges", x => new { x.BadgesId, x.TrainersId });
                    table.ForeignKey(
                        name: "FK_TrainerBadges_Badges_BadgesId",
                        column: x => x.BadgesId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerBadges_Trainers_TrainersId",
                        column: x => x.TrainersId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_LocationId",
                table: "Tournaments",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_LocationId",
                table: "Pokemons",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_LocationId",
                table: "Gyms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainer1BattlePokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons",
                column: "Trainer1PokemonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainer2BattlePokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons",
                column: "Trainer2PokemonsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerBadges_TrainersId",
                table: "TrainerBadges",
                column: "TrainersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Trainers_WinnerId",
                table: "Battles",
                column: "WinnerId",
                principalTable: "Trainers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Locations_LocationId",
                table: "Gyms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Locations_LocationId",
                table: "Pokemons",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Locations_LocationId",
                table: "Tournaments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
