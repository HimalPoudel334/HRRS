using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddMappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmissionType",
                table: "MasterStandardEntries",
                newName: "SubmissionTypeId");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "TempHealthFacilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "TempHealthFacilities",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "TempHealthFacilities",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "HealthFacilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "HealthFacilities",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "HealthFacilities",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubmissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterStandardEntries_SubmissionTypeId",
                table: "MasterStandardEntries",
                column: "SubmissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterStandardEntries_SubmissionTypes_SubmissionTypeId",
                table: "MasterStandardEntries",
                column: "SubmissionTypeId",
                principalTable: "SubmissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterStandardEntries_SubmissionTypes_SubmissionTypeId",
                table: "MasterStandardEntries");

            migrationBuilder.DropTable(
                name: "SubmissionTypes");

            migrationBuilder.DropIndex(
                name: "IX_MasterStandardEntries_SubmissionTypeId",
                table: "MasterStandardEntries");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "TempHealthFacilities");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "HealthFacilities");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "HealthFacilities");

            migrationBuilder.RenameColumn(
                name: "SubmissionTypeId",
                table: "MasterStandardEntries",
                newName: "SubmissionType");
        }
    }
}
