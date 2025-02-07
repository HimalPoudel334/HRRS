using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddedDafaColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DafaNo",
                table: "SubParichheds");

            migrationBuilder.AddColumn<string>(
                name: "DafaNo",
                table: "Anusuchis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DafaNo",
                table: "Anusuchis");

            migrationBuilder.AddColumn<string>(
                name: "DafaNo",
                table: "SubParichheds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
