using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilityAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "LocalLevel",
                table: "HealthFacilities");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "HealthFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalLevelId",
                table: "HealthFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalLevels_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_DistrictId",
                table: "HealthFacilities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_LocalLevelId",
                table: "HealthFacilities",
                column: "LocalLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalLevels_DistrictId",
                table: "LocalLevels",
                column: "DistrictId");

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

            migrationBuilder.DropTable(
                name: "LocalLevels");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_DistrictId",
                table: "HealthFacilities");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_LocalLevelId",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "LocalLevelId",
                table: "HealthFacilities");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "HealthFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocalLevel",
                table: "HealthFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
