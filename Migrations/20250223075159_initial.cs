using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DafaNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Parichheds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "MasterStandardEntries",
                columns: table => new
                {
                    SubmissionCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterStandardEntries", x => x.SubmissionCode);
                    table.ForeignKey(
                        name: "FK_MasterStandardEntries_HealthFacilities_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "HealthFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_HealthFacilities_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "HealthFacilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubParichheds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParichhedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubParichheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubParichheds_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalStandardEntrys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmissionType = table.Column<int>(type: "int", nullable: false),
                    MasterStandardEntrySubmissionCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalStandardEntrys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalStandardEntrys_MasterStandardEntries_MasterStandardEntrySubmissionCode",
                        column: x => x.MasterStandardEntrySubmissionCode,
                        principalTable: "MasterStandardEntries",
                        principalColumn: "SubmissionCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSubParichheds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubParichhedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSubParichheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSubParichheds_SubParichheds_SubParichhedId",
                        column: x => x.SubParichhedId,
                        principalTable: "SubParichheds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mapdandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parimaad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailableDivided = table.Column<bool>(type: "bit", nullable: false),
                    Is25Active = table.Column<bool>(type: "bit", nullable: false),
                    Is50Active = table.Column<bool>(type: "bit", nullable: false),
                    Is100Active = table.Column<bool>(type: "bit", nullable: false),
                    Is200Active = table.Column<bool>(type: "bit", nullable: false),
                    Value25 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value50 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value100 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value200 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: false),
                    ParichhedId = table.Column<int>(type: "int", nullable: true),
                    SubParichhedId = table.Column<int>(type: "int", nullable: true),
                    SubSubParichhedId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Mapdandas_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mapdandas_SubParichheds_SubParichhedId",
                        column: x => x.SubParichhedId,
                        principalTable: "SubParichheds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mapdandas_SubSubParichheds_SubSubParichhedId",
                        column: x => x.SubSubParichhedId,
                        principalTable: "SubSubParichheds",
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StandardEntryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalStandards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalStandards_HospitalStandardEntrys_StandardEntryId",
                        column: x => x.StandardEntryId,
                        principalTable: "HospitalStandardEntrys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalStandards_Mapdandas_MapdandaId",
                        column: x => x.MapdandaId,
                        principalTable: "Mapdandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubMapdandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parimaad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapdandaId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMapdandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMapdandas_Mapdandas_MapdandaId",
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
                name: "IX_HospitalStandardEntrys_MasterStandardEntrySubmissionCode",
                table: "HospitalStandardEntrys",
                column: "MasterStandardEntrySubmissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_MapdandaId",
                table: "HospitalStandards",
                column: "MapdandaId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalStandards_StandardEntryId",
                table: "HospitalStandards",
                column: "StandardEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_AnusuchiId",
                table: "Mapdandas",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_ParichhedId",
                table: "Mapdandas",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_SubParichhedId",
                table: "Mapdandas",
                column: "SubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_SubSubParichhedId",
                table: "Mapdandas",
                column: "SubSubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterStandardEntries_HealthFacilityId",
                table: "MasterStandardEntries",
                column: "HealthFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_AnusuchiId",
                table: "Parichheds",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMapdandas_MapdandaId",
                table: "SubMapdandas",
                column: "MapdandaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubParichheds_ParichhedId",
                table: "SubParichheds",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSubParichheds_SubParichhedId",
                table: "SubSubParichheds",
                column: "SubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HealthFacilityId",
                table: "Users",
                column: "HealthFacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalStandards");

            migrationBuilder.DropTable(
                name: "SubMapdandas");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "HospitalStandardEntrys");

            migrationBuilder.DropTable(
                name: "Mapdandas");

            migrationBuilder.DropTable(
                name: "MasterStandardEntries");

            migrationBuilder.DropTable(
                name: "SubSubParichheds");

            migrationBuilder.DropTable(
                name: "HealthFacilities");

            migrationBuilder.DropTable(
                name: "SubParichheds");

            migrationBuilder.DropTable(
                name: "Parichheds");

            migrationBuilder.DropTable(
                name: "Anusuchis");
        }
    }
}
