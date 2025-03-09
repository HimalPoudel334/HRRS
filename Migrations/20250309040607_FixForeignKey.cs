using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_Districts_DistrictId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_Provinces_ProvinceId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalLevels_Districts_DistrictId",
                table: "LocalLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_Districts_DistrictId",
                table: "TempHealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_LocalLevels_LocalLevelId",
                table: "TempHealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                table: "TempHealthFacilities");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_Districts_DistrictId",
                table: "HealthFacilities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                table: "HealthFacilities",
                column: "LocalLevelId",
                principalTable: "LocalLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_Provinces_ProvinceId",
                table: "HealthFacilities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalLevels_Districts_DistrictId",
                table: "LocalLevels",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_Districts_DistrictId",
                table: "TempHealthFacilities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_LocalLevels_LocalLevelId",
                table: "TempHealthFacilities",
                column: "LocalLevelId",
                principalTable: "LocalLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                table: "TempHealthFacilities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_Districts_DistrictId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_Provinces_ProvinceId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalLevels_Districts_DistrictId",
                table: "LocalLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_Districts_DistrictId",
                table: "TempHealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_LocalLevels_LocalLevelId",
                table: "TempHealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                table: "TempHealthFacilities");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_Districts_DistrictId",
                table: "HealthFacilities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                table: "HealthFacilities",
                column: "LocalLevelId",
                principalTable: "LocalLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_Provinces_ProvinceId",
                table: "HealthFacilities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalLevels_Districts_DistrictId",
                table: "LocalLevels",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_Districts_DistrictId",
                table: "TempHealthFacilities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_LocalLevels_LocalLevelId",
                table: "TempHealthFacilities",
                column: "LocalLevelId",
                principalTable: "LocalLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                table: "TempHealthFacilities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id");
        }
    }
}
