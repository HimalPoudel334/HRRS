using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreColsInMapdanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Col5",
                table: "Mapdandas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Col6",
                table: "Mapdandas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Col7",
                table: "Mapdandas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Col8",
                table: "Mapdandas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Col9",
                table: "Mapdandas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCol5Active",
                table: "Mapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCol6Active",
                table: "Mapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCol7Active",
                table: "Mapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCol8Active",
                table: "Mapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCol9Active",
                table: "Mapdandas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Col5",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "Col6",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "Col7",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "Col8",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "Col9",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "IsCol5Active",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "IsCol6Active",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "IsCol7Active",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "IsCol8Active",
                table: "Mapdandas");

            migrationBuilder.DropColumn(
                name: "IsCol9Active",
                table: "Mapdandas");
        }
    }
}
