using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFile",
                table: "HospitalStandards");

            migrationBuilder.DropColumn(
                name: "MadpandaId",
                table: "HospitalStandards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasFile",
                table: "HospitalStandards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MadpandaId",
                table: "HospitalStandards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
