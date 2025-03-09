using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class FixBedCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnusuchiMappings_BedCount_BedCountId",
                table: "AnusuchiMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BedCount",
                table: "BedCount");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BedCount",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "BedCount",
                newName: "BedCounts");

            migrationBuilder.RenameColumn(
                name: "BedCount",
                table: "TempHealthFacilities",
                newName: "BedCountId");

            migrationBuilder.RenameColumn(
                name: "BedCount",
                table: "HealthFacilities",
                newName: "BedCountId");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacilityTypeId",
                table: "HospitalType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacilityTypeSN",
                table: "BedCounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BedCounts",
                table: "BedCounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleFacilityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BedCountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleFacilityType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleFacilityType_BedCounts_BedCountId",
                        column: x => x.BedCountId,
                        principalTable: "BedCounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleFacilityType_HospitalType_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "HospitalType",
                        principalColumn: "SN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleFacilityType_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PostId",
                table: "Users",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_TempHealthFacilities_BedCountId",
                table: "TempHealthFacilities",
                column: "BedCountId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalType_FacilityTypeId",
                table: "HospitalType",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_BedCountId",
                table: "HealthFacilities",
                column: "BedCountId");

            migrationBuilder.CreateIndex(
                name: "IX_BedCounts_FacilityTypeSN",
                table: "BedCounts",
                column: "FacilityTypeSN");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleFacilityType_BedCountId",
                table: "UserRoleFacilityType",
                column: "BedCountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleFacilityType_FacilityTypeId",
                table: "UserRoleFacilityType",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleFacilityType_RoleId",
                table: "UserRoleFacilityType",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnusuchiMappings_BedCounts_BedCountId",
                table: "AnusuchiMappings",
                column: "BedCountId",
                principalTable: "BedCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BedCounts_HospitalType_FacilityTypeSN",
                table: "BedCounts",
                column: "FacilityTypeSN",
                principalTable: "HospitalType",
                principalColumn: "SN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_BedCounts_BedCountId",
                table: "HealthFacilities",
                column: "BedCountId",
                principalTable: "BedCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalType_HospitalType_FacilityTypeId",
                table: "HospitalType",
                column: "FacilityTypeId",
                principalTable: "HospitalType",
                principalColumn: "SN");

            migrationBuilder.AddForeignKey(
                name: "FK_TempHealthFacilities_BedCounts_BedCountId",
                table: "TempHealthFacilities",
                column: "BedCountId",
                principalTable: "BedCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPosts_PostId",
                table: "Users",
                column: "PostId",
                principalTable: "UserPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnusuchiMappings_BedCounts_BedCountId",
                table: "AnusuchiMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_BedCounts_HospitalType_FacilityTypeSN",
                table: "BedCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_BedCounts_BedCountId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HospitalType_HospitalType_FacilityTypeId",
                table: "HospitalType");

            migrationBuilder.DropForeignKey(
                name: "FK_TempHealthFacilities_BedCounts_BedCountId",
                table: "TempHealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPosts_PostId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserPosts");

            migrationBuilder.DropTable(
                name: "UserRoleFacilityType");

            migrationBuilder.DropIndex(
                name: "IX_Users_PostId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TempHealthFacilities_BedCountId",
                table: "TempHealthFacilities");

            migrationBuilder.DropIndex(
                name: "IX_HospitalType_FacilityTypeId",
                table: "HospitalType");

            migrationBuilder.DropIndex(
                name: "IX_HealthFacilities_BedCountId",
                table: "HealthFacilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BedCounts",
                table: "BedCounts");

            migrationBuilder.DropIndex(
                name: "IX_BedCounts_FacilityTypeSN",
                table: "BedCounts");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FacilityTypeId",
                table: "HospitalType");

            migrationBuilder.DropColumn(
                name: "FacilityTypeSN",
                table: "BedCounts");

            migrationBuilder.RenameTable(
                name: "BedCounts",
                newName: "BedCount");

            migrationBuilder.RenameColumn(
                name: "BedCountId",
                table: "TempHealthFacilities",
                newName: "BedCount");

            migrationBuilder.RenameColumn(
                name: "BedCountId",
                table: "HealthFacilities",
                newName: "BedCount");

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BedCount",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BedCount",
                table: "BedCount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnusuchiMappings_BedCount_BedCountId",
                table: "AnusuchiMappings",
                column: "BedCountId",
                principalTable: "BedCount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
