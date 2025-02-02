using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthFacilityAndUserField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthFacilityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PanNumber",
                table: "HealthFacilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HealthFacilityId",
                table: "Users",
                column: "HealthFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacilities",
                column: "PanNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HealthFacilities_HealthFacilityId",
                table: "Users",
                column: "HealthFacilityId",
                principalTable: "HealthFacilities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_HealthFacilities_HealthFacilityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HealthFacilityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "HealthFacilityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PanNumber",
                table: "HealthFacilities");
        }
    }
}
