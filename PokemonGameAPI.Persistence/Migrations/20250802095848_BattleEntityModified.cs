using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BattleEntityModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainer1BattlePokemons_Pokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer2BattlePokemons_Pokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainer1BattlePokemons_TrainerPokemons_Trainer1PokemonsId",
                table: "Trainer1BattlePokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainer2BattlePokemons_TrainerPokemons_Trainer2PokemonsId",
                table: "Trainer2BattlePokemons");

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
        }
    }
}
