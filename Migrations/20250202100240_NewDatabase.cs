using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnusuchiNumber",
                table: "Mapdandas",
                newName: "AnusuchiId");

            migrationBuilder.AddColumn<int>(
                name: "ParichhedId",
                table: "Mapdandas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubParichhedId",
                table: "Mapdandas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PanNumber",
                table: "HealthFacilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Anusuchis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnusuchiName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedToDafaNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anusuchis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parichheds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParichhedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnusuchiId1 = table.Column<int>(type: "int", nullable: false),
                    AnusuchiId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parichheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parichheds_Anusuchis_AnusuchiId1",
                        column: x => x.AnusuchiId1,
                        principalTable: "Anusuchis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_AnusuchiId",
                table: "Mapdandas",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacilities",
                column: "PanNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_AnusuchiId1",
                table: "Parichheds",
                column: "AnusuchiId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapdandas_Anusuchis_AnusuchiId",
                table: "Mapdandas",
                column: "AnusuchiId",
                principalTable: "Anusuchis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mapdandas_Anusuchis_AnusuchiId",
                table: "Mapdandas");

            migrationBuilder.DropTable(
                name: "Parichheds");

            migrationBuilder.DropTable(
                name: "Anusuchis");

            migrationBuilder.DropIndex(
                name: "IX_Mapdandas_AnusuchiId",
                table: "Mapdandas");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "ParichhedId",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "SubParichhedId",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "PanNumber",
                table: "HealthFacilities");

            migrationBuilder.RenameColumn(
                name: "AnusuchiId",
                table: "Mapdandas",
                newName: "AnusuchiNumber");
        }
    }
}
