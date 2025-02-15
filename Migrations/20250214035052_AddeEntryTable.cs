using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddeEntryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StandardEntryId",
                table: "HospitalStandards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HospitalStandardEntrys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalStandardEntrys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_StandardEntryId",
                table: "HospitalStandards",
                column: "StandardEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalStandards_HospitalStandardEntrys_StandardEntryId",
                table: "HospitalStandards",
                column: "StandardEntryId",
                principalTable: "HospitalStandardEntrys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalStandards_HospitalStandardEntrys_StandardEntryId",
                table: "HospitalStandards");

            migrationBuilder.DropTable(
                name: "HospitalStandardEntrys");

            migrationBuilder.DropIndex(
                name: "IX_HospitalStandards_StandardEntryId",
                table: "HospitalStandards");

            migrationBuilder.DropColumn(
                name: "StandardEntryId",
                table: "HospitalStandards");
        }
    }
}
