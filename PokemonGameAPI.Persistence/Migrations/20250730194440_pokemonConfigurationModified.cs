using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class pokemonConfigurationModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Locations_LocationId",
                table: "Pokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Locations_LocationId1",
                table: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_Pokemons_LocationId1",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Pokemons");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Locations_LocationId",
                table: "Pokemons",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Locations_LocationId",
                table: "Pokemons");

            migrationBuilder.AddColumn<int>(
                name: "LocationId1",
                table: "Pokemons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_LocationId1",
                table: "Pokemons",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Locations_LocationId",
                table: "Pokemons",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Locations_LocationId1",
                table: "Pokemons",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
