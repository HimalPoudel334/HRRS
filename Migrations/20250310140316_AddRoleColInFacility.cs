using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleColInFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "RegistrationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "HealthFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_RoleId",
                table: "RegistrationRequests",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_RoleId",
                table: "HealthFacilities",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_UserRoles_RoleId",
                table: "HealthFacilities",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationRequests_UserRoles_RoleId",
                table: "RegistrationRequests",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_UserRoles_RoleId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationRequests_UserRoles_RoleId",
                table: "RegistrationRequests");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationRequests_RoleId",
                table: "RegistrationRequests");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_RoleId",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "RegistrationRequests");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "HealthFacilities");
        }
    }
}
