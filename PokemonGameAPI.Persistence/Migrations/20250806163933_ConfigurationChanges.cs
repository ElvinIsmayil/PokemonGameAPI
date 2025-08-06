using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonGameAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Gyms_GymId",
                table: "Badges");

            migrationBuilder.DropForeignKey(
                name: "FK_Gyms_Trainers_GymLeaderTrainerId",
                table: "Gyms");

            migrationBuilder.AlterColumn<int>(
                name: "GymLeaderTrainerId",
                table: "Gyms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Gyms_GymId",
                table: "Badges",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Trainers_GymLeaderTrainerId",
                table: "Gyms",
                column: "GymLeaderTrainerId",
                principalTable: "Trainers",
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
                name: "FK_Gyms_Trainers_GymLeaderTrainerId",
                table: "Gyms");

            migrationBuilder.AlterColumn<int>(
                name: "GymLeaderTrainerId",
                table: "Gyms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Gyms_GymId",
                table: "Badges",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gyms_Trainers_GymLeaderTrainerId",
                table: "Gyms",
                column: "GymLeaderTrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
