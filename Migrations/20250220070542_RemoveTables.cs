using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapdandaTableValues");

            migrationBuilder.DropTable(
                name: "SubHeaders");

            migrationBuilder.DropTable(
                name: "MapdandaTableHeaders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MapdandaTableHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnusuchiId = table.Column<int>(type: "int", nullable: true),
                    ParichhedId = table.Column<int>(type: "int", nullable: true),
                    SubParichhedId = table.Column<int>(type: "int", nullable: true),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    MapdandaTableId = table.Column<int>(type: "int", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MapdandaTableValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapdandaId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MapdandaTableHeaderId = table.Column<int>(type: "int", nullable: true),
                    SubHeaderId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapdandaTableValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapdandaTableValues_MapdandaTableHeaders_MapdandaTableHeaderId",
                        column: x => x.MapdandaTableHeaderId,
                        principalTable: "MapdandaTableHeaders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MapdandaTableValues_Mapdandas_MapdandaId",
                        column: x => x.MapdandaId,
                        principalTable: "Mapdandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapdandaTableValues_SubHeaders_SubHeaderId",
                        column: x => x.SubHeaderId,
                        principalTable: "SubHeaders",
                        principalColumn: "Id");
                });

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
                name: "IX_MapdandaTableValues_MapdandaId",
                table: "MapdandaTableValues",
                column: "MapdandaId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableValues_MapdandaTableHeaderId",
                table: "MapdandaTableValues",
                column: "MapdandaTableHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTableValues_SubHeaderId",
                table: "MapdandaTableValues",
                column: "SubHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubHeaders_MapdandaTableId",
                table: "SubHeaders",
                column: "MapdandaTableId");
        }
    }
}
