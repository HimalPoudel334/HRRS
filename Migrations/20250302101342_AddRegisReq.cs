using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_Districts_DistrictId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_HospitalType_FacilityTypeId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                table: "HealthFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterStandardEntries_HealthFacilities_HealthFacilityId",
                table: "MasterStandardEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_HealthFacilities_HealthFacilityId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthFacilities",
                table: "HealthFacilities");

            migrationBuilder.RenameTable(
                name: "HealthFacilities",
                newName: "HealthFacility");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacility",
                newName: "IX_HealthFacility_PanNumber");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacilities_LocalLevelId",
                table: "HealthFacility",
                newName: "IX_HealthFacility_LocalLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacilities_FacilityTypeId",
                table: "HealthFacility",
                newName: "IX_HealthFacility_FacilityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacilities_DistrictId",
                table: "HealthFacility",
                newName: "IX_HealthFacility_DistrictId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthFacility",
                table: "HealthFacility",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RegistrationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HandledById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationRequests_HealthFacility_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "HealthFacility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationRequests_Users_HandledById",
                        column: x => x.HandledById,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_HandledById",
                table: "RegistrationRequests",
                column: "HandledById");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_HealthFacilityId",
                table: "RegistrationRequests",
                column: "HealthFacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacility_Districts_DistrictId",
                table: "HealthFacility",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacility_HospitalType_FacilityTypeId",
                table: "HealthFacility",
                column: "FacilityTypeId",
                principalTable: "HospitalType",
                principalColumn: "SN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacility_LocalLevels_LocalLevelId",
                table: "HealthFacility",
                column: "LocalLevelId",
                principalTable: "LocalLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterStandardEntries_HealthFacility_HealthFacilityId",
                table: "MasterStandardEntries",
                column: "HealthFacilityId",
                principalTable: "HealthFacility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HealthFacility_HealthFacilityId",
                table: "Users",
                column: "HealthFacilityId",
                principalTable: "HealthFacility",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacility_Districts_DistrictId",
                table: "HealthFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacility_HospitalType_FacilityTypeId",
                table: "HealthFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthFacility_LocalLevels_LocalLevelId",
                table: "HealthFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterStandardEntries_HealthFacility_HealthFacilityId",
                table: "MasterStandardEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_HealthFacility_HealthFacilityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RegistrationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthFacility",
                table: "HealthFacility");

            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "HealthFacility",
                newName: "HealthFacilities");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacility_PanNumber",
                table: "HealthFacilities",
                newName: "IX_HealthFacilities_PanNumber");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacility_LocalLevelId",
                table: "HealthFacilities",
                newName: "IX_HealthFacilities_LocalLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacility_FacilityTypeId",
                table: "HealthFacilities",
                newName: "IX_HealthFacilities_FacilityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthFacility_DistrictId",
                table: "HealthFacilities",
                newName: "IX_HealthFacilities_DistrictId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthFacilities",
                table: "HealthFacilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_Districts_DistrictId",
                table: "HealthFacilities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_HospitalType_FacilityTypeId",
                table: "HealthFacilities",
                column: "FacilityTypeId",
                principalTable: "HospitalType",
                principalColumn: "SN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                table: "HealthFacilities",
                column: "LocalLevelId",
                principalTable: "LocalLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterStandardEntries_HealthFacilities_HealthFacilityId",
                table: "MasterStandardEntries",
                column: "HealthFacilityId",
                principalTable: "HealthFacilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HealthFacilities_HealthFacilityId",
                table: "Users",
                column: "HealthFacilityId",
                principalTable: "HealthFacilities",
                principalColumn: "Id");
        }
    }
}
