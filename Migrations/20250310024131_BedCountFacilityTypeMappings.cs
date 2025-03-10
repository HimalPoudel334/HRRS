using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class BedCountFacilityTypeMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BedCounts_HospitalType_FacilityTypeSN",
                table: "BedCounts");

            migrationBuilder.DropIndex(
                name: "IX_BedCounts_FacilityTypeSN",
                table: "BedCounts");

            migrationBuilder.DropColumn(
                name: "FacilityTypeSN",
                table: "BedCounts");

            migrationBuilder.CreateTable(
                name: "BedCountFacilityType",
                columns: table => new
                {
                    BedCountsId = table.Column<int>(type: "int", nullable: false),
                    FacilityTypesSN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedCountFacilityType", x => new { x.BedCountsId, x.FacilityTypesSN });
                    table.ForeignKey(
                        name: "FK_BedCountFacilityType_BedCounts_BedCountsId",
                        column: x => x.BedCountsId,
                        principalTable: "BedCounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BedCountFacilityType_HospitalType_FacilityTypesSN",
                        column: x => x.FacilityTypesSN,
                        principalTable: "HospitalType",
                        principalColumn: "SN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BedCountFacilityType_FacilityTypesSN",
                table: "BedCountFacilityType",
                column: "FacilityTypesSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BedCountFacilityType");

            migrationBuilder.AddColumn<int>(
                name: "FacilityTypeSN",
                table: "BedCounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BedCounts_FacilityTypeSN",
                table: "BedCounts",
                column: "FacilityTypeSN");

            migrationBuilder.AddForeignKey(
                name: "FK_BedCounts_HospitalType_FacilityTypeSN",
                table: "BedCounts",
                column: "FacilityTypeSN",
                principalTable: "HospitalType",
                principalColumn: "SN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
