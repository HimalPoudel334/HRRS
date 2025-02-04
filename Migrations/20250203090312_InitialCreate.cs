using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anusuchis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnusuchiName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedToDafaNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anusuchis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    SpecialistCount = table.Column<int>(type: "int", nullable: false),
                    AvailableServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardNumber = table.Column<int>(type: "int", nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfInspection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityHeadName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityHeadPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityHeadEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutiveHeadName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutiveHeadMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutiveHeadEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionReceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRenewedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApporvingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenewingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalValidityTill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenewalValidityTill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpgradeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpgradingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLetterOfIntent = table.Column<bool>(type: "bit", nullable: true),
                    IsExecutionPermission = table.Column<bool>(type: "bit", nullable: true),
                    IsRenewal = table.Column<bool>(type: "bit", nullable: true),
                    IsUpgrade = table.Column<bool>(type: "bit", nullable: true),
                    IsServiceExtension = table.Column<bool>(type: "bit", nullable: true),
                    IsBranchExtension = table.Column<bool>(type: "bit", nullable: true),
                    IsRelocation = table.Column<bool>(type: "bit", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationSubmittedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationSubmittedAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Parichheds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParichhedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: false),
                    ParichhedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parichheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parichheds_Anusuchis_AnusuchiId",
                        column: x => x.AnusuchiId,
                        principalTable: "Anusuchis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parichheds_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mapdandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: false),
                    ParichhedId = table.Column<int>(type: "int", nullable: true),
                    MapdandaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapdandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mapdandas_Anusuchis_AnusuchiId",
                        column: x => x.AnusuchiId,
                        principalTable: "Anusuchis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mapdandas_Mapdandas_MapdandaId",
                        column: x => x.MapdandaId,
                        principalTable: "Mapdandas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mapdandas_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HospitalStandards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: false),
                    MapdandaId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiscalYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
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
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacilities",
                column: "PanNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_HealthFacilityId",
                table: "HospitalStandards",
                column: "HealthFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_MapdandaId",
                table: "HospitalStandards",
                column: "MapdandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_AnusuchiId",
                table: "Mapdandas",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_MapdandaId",
                table: "Mapdandas",
                column: "MapdandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_ParichhedId",
                table: "Mapdandas",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_AnusuchiId",
                table: "Parichheds",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_ParichhedId",
                table: "Parichheds",
                column: "ParichhedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalStandards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "HealthFacilities");

            migrationBuilder.DropTable(
                name: "Mapdandas");

            migrationBuilder.DropTable(
                name: "Parichheds");

            migrationBuilder.DropTable(
                name: "Anusuchis");
        }
    }
}
