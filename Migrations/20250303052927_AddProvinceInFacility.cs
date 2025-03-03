using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddProvinceInFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TempHealthFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "TempHealthFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "TempHealthFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "TempHealthFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "HealthFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TempHealthFacilities_ProvinceId",
                table: "TempHealthFacilities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_ProvinceId",
                table: "HealthFacilities",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_Provinces_ProvinceId",
                table: "HealthFacilities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                table: "TempHealthFacilities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_Provinces_ProvinceId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                table: "TempHealthFacilities");

            migrationBuilder.DropIndex(
                name: "IX_TempHealthFacilities_ProvinceId",
                table: "TempHealthFacilities");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_ProvinceId",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "HealthFacilities");
        }
    }
}
