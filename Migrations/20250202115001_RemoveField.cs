using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class RemoveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parichheds_Anusuchis_AnusuchiId1",
                table: "Parichheds");

            migrationBuilder.DropIndex(
                name: "IX_Parichheds_AnusuchiId1",
                table: "Parichheds");

            migrationBuilder.DropColumn(
                name: "AnusuchiId1",
                table: "Parichheds");

            migrationBuilder.AlterColumn<int>(
                name: "AnusuchiId",
                table: "Parichheds",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_AnusuchiId",
                table: "Parichheds",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_ParichhedId",
                table: "Mapdandas",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_SubParichhedId",
                table: "Mapdandas",
                column: "SubParichhedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapdandas_Mapdandas_SubParichhedId",
                table: "Mapdandas",
                column: "SubParichhedId",
                principalTable: "Mapdandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mapdandas_Parichheds_ParichhedId",
                table: "Mapdandas",
                column: "ParichhedId",
                principalTable: "Parichheds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mapdandas_Parichheds_SubParichhedId",
                table: "Mapdandas",
                column: "SubParichhedId",
                principalTable: "Parichheds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parichheds_Anusuchis_AnusuchiId",
                table: "Parichheds",
                column: "AnusuchiId",
                principalTable: "Anusuchis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parichheds_Parichheds_AnusuchiId",
                table: "Parichheds",
                column: "AnusuchiId",
                principalTable: "Parichheds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mapdandas_Mapdandas_SubParichhedId",
                table: "Mapdandas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mapdandas_Parichheds_ParichhedId",
                table: "Mapdandas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mapdandas_Parichheds_SubParichhedId",
                table: "Mapdandas");

            migrationBuilder.DropForeignKey(
                name: "FK_Parichheds_Anusuchis_AnusuchiId",
                table: "Parichheds");

            migrationBuilder.DropForeignKey(
                name: "FK_Parichheds_Parichheds_AnusuchiId",
                table: "Parichheds");

            migrationBuilder.DropIndex(
                name: "IX_Parichheds_AnusuchiId",
                table: "Parichheds");

            migrationBuilder.DropIndex(
                name: "IX_Mapdandas_ParichhedId",
                table: "Mapdandas");

            migrationBuilder.DropIndex(
                name: "IX_Mapdandas_SubParichhedId",
                table: "Mapdandas");

            migrationBuilder.AlterColumn<string>(
                name: "AnusuchiId",
                table: "Parichheds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AnusuchiId1",
                table: "Parichheds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_AnusuchiId1",
                table: "Parichheds",
                column: "AnusuchiId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Parichheds_Anusuchis_AnusuchiId1",
                table: "Parichheds",
                column: "AnusuchiId1",
                principalTable: "Anusuchis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
