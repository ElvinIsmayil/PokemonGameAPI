using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PokemonEntityModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseStatsId",
                table: "Pokemons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseStatsId",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
