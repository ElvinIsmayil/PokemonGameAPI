using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PokemonEntityPropChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_PokemonCategories_CategoryId",
                table: "Pokemons");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Pokemons",
                newName: "PokemonCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemons_CategoryId",
                table: "Pokemons",
                newName: "IX_Pokemons_PokemonCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_PokemonCategories_PokemonCategoryId",
                table: "Pokemons",
                column: "PokemonCategoryId",
                principalTable: "PokemonCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_PokemonCategories_PokemonCategoryId",
                table: "Pokemons");

            migrationBuilder.RenameColumn(
                name: "PokemonCategoryId",
                table: "Pokemons",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Pokemons_PokemonCategoryId",
                table: "Pokemons",
                newName: "IX_Pokemons_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_PokemonCategories_CategoryId",
                table: "Pokemons",
                column: "CategoryId",
                principalTable: "PokemonCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
