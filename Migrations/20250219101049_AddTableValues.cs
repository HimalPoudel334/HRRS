using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddTableValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapdandaTableValue_Mapdandas_MapdandaId",
                table: "MapdandaTableValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapdandaTableValue",
                table: "MapdandaTableValue");

            migrationBuilder.RenameTable(
                name: "MapdandaTableValue",
                newName: "MapdandaTableValues");

            migrationBuilder.RenameIndex(
                name: "IX_MapdandaTableValue_MapdandaId",
                table: "MapdandaTableValues",
                newName: "IX_MapdandaTableValues_MapdandaId");

            migrationBuilder.AddColumn<int>(
                name: "MapdandaTableHeaderId",
                table: "MapdandaTableValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubHeaderId",
                table: "MapdandaTableValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapdandaTableValues",
                table: "MapdandaTableValues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MapdandaTableHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: true),
                    ParichhedId = table.Column<int>(type: "int", nullable: true),
                    SubParichhedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapdandaTableHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapdandaTableHeaders_Anusuchis_AnusuchiId",
                        column: x => x.AnusuchiId,
                        principalTable: "Anusuchis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MapdandaTableHeaders_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MapdandaTableHeaders_SubParichheds_SubParichhedId",
                        column: x => x.SubParichhedId,
                        principalTable: "SubParichheds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapdandaTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubHeaders_MapdandaTableHeaders_MapdandaTableId",
                        column: x => x.MapdandaTableId,
                        principalTable: "MapdandaTableHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableValues_MapdandaTableHeaderId",
                table: "MapdandaTableValues",
                column: "MapdandaTableHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableValues_SubHeaderId",
                table: "MapdandaTableValues",
                column: "SubHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableHeaders_AnusuchiId",
                table: "MapdandaTableHeaders",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableHeaders_ParichhedId",
                table: "MapdandaTableHeaders",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableHeaders_SubParichhedId",
                table: "MapdandaTableHeaders",
                column: "SubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_SubHeaders_MapdandaTableId",
                table: "SubHeaders",
                column: "MapdandaTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapdandaTableValues_MapdandaTableHeaders_MapdandaTableHeaderId",
                table: "MapdandaTableValues",
                column: "MapdandaTableHeaderId",
                principalTable: "MapdandaTableHeaders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapdandaTableValues_Mapdandas_MapdandaId",
                table: "MapdandaTableValues",
                column: "MapdandaId",
                principalTable: "Mapdandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapdandaTableValues_SubHeaders_SubHeaderId",
                table: "MapdandaTableValues",
                column: "SubHeaderId",
                principalTable: "SubHeaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapdandaTableValues_MapdandaTableHeaders_MapdandaTableHeaderId",
                table: "MapdandaTableValues");

            migrationBuilder.DropForeignKey(
                name: "FK_MapdandaTableValues_Mapdandas_MapdandaId",
                table: "MapdandaTableValues");

            migrationBuilder.DropForeignKey(
                name: "FK_MapdandaTableValues_SubHeaders_SubHeaderId",
                table: "MapdandaTableValues");

            migrationBuilder.DropTable(
                name: "SubHeaders");

            migrationBuilder.DropTable(
                name: "MapdandaTableHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapdandaTableValues",
                table: "MapdandaTableValues");

            migrationBuilder.DropIndex(
                name: "IX_MapdandaTableValues_MapdandaTableHeaderId",
                table: "MapdandaTableValues");

            migrationBuilder.DropIndex(
                name: "IX_MapdandaTableValues_SubHeaderId",
                table: "MapdandaTableValues");

            migrationBuilder.DropColumn(
                name: "MapdandaTableHeaderId",
                table: "MapdandaTableValues");

            migrationBuilder.DropColumn(
                name: "SubHeaderId",
                table: "MapdandaTableValues");

            migrationBuilder.RenameTable(
                name: "MapdandaTableValues",
                newName: "MapdandaTableValue");

            migrationBuilder.RenameIndex(
                name: "IX_MapdandaTableValues_MapdandaId",
                table: "MapdandaTableValue",
                newName: "IX_MapdandaTableValue_MapdandaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapdandaTableValue",
                table: "MapdandaTableValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapdandaTableValue_Mapdandas_MapdandaId",
                table: "MapdandaTableValue",
                column: "MapdandaId",
                principalTable: "Mapdandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
