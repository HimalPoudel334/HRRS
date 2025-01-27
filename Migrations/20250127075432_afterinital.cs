using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class afterinital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    SpecialistCount = table.Column<int>(type: "int", nullable: false),
                    AvailableServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardNumber = table.Column<int>(type: "int", nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfInspection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityHeadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityHeadPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityHeadEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutiveHeadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutiveHeadMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutiveHeadEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionReceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRenewedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApporvingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RenewingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalValidityTill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RenewalValidityTill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpgradeDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpgradingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLetterOfIntent = table.Column<bool>(type: "bit", nullable: false),
                    IsExecutionPermission = table.Column<bool>(type: "bit", nullable: false),
                    IsRenewal = table.Column<bool>(type: "bit", nullable: false),
                    IsUpgrade = table.Column<bool>(type: "bit", nullable: false),
                    IsServiceExtension = table.Column<bool>(type: "bit", nullable: false),
                    IsBranchExtension = table.Column<bool>(type: "bit", nullable: false),
                    IsRelocation = table.Column<bool>(type: "bit", nullable: false),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalStandards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: false),
                    MadpandaId = table.Column<int>(type: "int", nullable: false),
                    MapdandaId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasFile = table.Column<bool>(type: "bit", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiscalYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalStandards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalStandards_HealthFacilities_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "HealthFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalStandards_Mapdandas_MapdandaId",
                        column: x => x.MapdandaId,
                        principalTable: "Mapdandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_HealthFacilityId",
                table: "HospitalStandards",
                column: "HealthFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_MapdandaId",
                table: "HospitalStandards",
                column: "MapdandaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalStandards");

            migrationBuilder.DropTable(
                name: "HealthFacilities");
        }
    }
}
