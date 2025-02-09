using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusInMapdanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "HospitalStandards");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "SubMapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Mapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubMapdandas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Mapdandas");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "HospitalStandards",
                type: "bit",
                nullable: true);
        }
    }
}
