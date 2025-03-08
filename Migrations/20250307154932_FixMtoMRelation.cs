using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRRS.Migrations
{
    /// <inheritdoc />
    public partial class FixMtoMRelation : Migration
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
                name: "BedCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalType",
                columns: table => new
                {
                    SN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HOSP_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HOSP_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalType", x => x.SN);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
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
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "LocalLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalLevels_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapdandaTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: false),
                    ParichhedId = table.Column<int>(type: "int", nullable: true),
                    SubParichhedId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapdandaTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapdandaTables_Anusuchis_AnusuchiId",
                        column: x => x.AnusuchiId,
                        principalTable: "Anusuchis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapdandaTables_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MapdandaTables_SubParichheds_SubParichhedId",
                        column: x => x.SubParichhedId,
                        principalTable: "SubParichheds",
                        principalColumn: "Id");
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
                name: "HealthFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    SpecialistCount = table.Column<int>(type: "int", nullable: false),
                    AvailableServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    LocalLevelId = table.Column<int>(type: "int", nullable: false),
                    WardNumber = table.Column<int>(type: "int", nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfInspection = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_HealthFacilities_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthFacilities_HospitalType_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "HospitalType",
                        principalColumn: "SN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthFacilities_LocalLevels_LocalLevelId",
                        column: x => x.LocalLevelId,
                        principalTable: "LocalLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthFacilities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempHealthFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    SpecialistCount = table.Column<int>(type: "int", nullable: false),
                    AvailableServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    LocalLevelId = table.Column<int>(type: "int", nullable: false),
                    WardNumber = table.Column<int>(type: "int", nullable: false),
                    Tole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHealthFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempHealthFacilities_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempHealthFacilities_HospitalType_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "HospitalType",
                        principalColumn: "SN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempHealthFacilities_LocalLevels_LocalLevelId",
                        column: x => x.LocalLevelId,
                        principalTable: "LocalLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempHealthFacilities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnusuchiMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: false),
                    BedCountId = table.Column<int>(type: "int", nullable: false),
                    SubmissionTypeId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnusuchiMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnusuchiMappings_BedCount_BedCountId",
                        column: x => x.BedCountId,
                        principalTable: "BedCount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnusuchiMappings_HospitalType_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "HospitalType",
                        principalColumn: "SN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnusuchiMappings_SubmissionTypes_SubmissionTypeId",
                        column: x => x.SubmissionTypeId,
                        principalTable: "SubmissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnusuchiMappings_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mapdandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parimaad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailableDivided = table.Column<bool>(type: "bit", nullable: false),
                    Is25Active = table.Column<bool>(type: "bit", nullable: false),
                    Is50Active = table.Column<bool>(type: "bit", nullable: false),
                    Is100Active = table.Column<bool>(type: "bit", nullable: false),
                    Is200Active = table.Column<bool>(type: "bit", nullable: false),
                    IsCol5Active = table.Column<bool>(type: "bit", nullable: false),
                    IsCol6Active = table.Column<bool>(type: "bit", nullable: false),
                    IsCol7Active = table.Column<bool>(type: "bit", nullable: false),
                    IsCol8Active = table.Column<bool>(type: "bit", nullable: false),
                    IsCol9Active = table.Column<bool>(type: "bit", nullable: false),
                    Value25 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value50 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value100 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value200 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    FormType = table.Column<int>(type: "int", nullable: false),
                    MapdandaTableId = table.Column<int>(type: "int", nullable: false),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false),
                    IsSubGroup = table.Column<bool>(type: "bit", nullable: false),
                    IsSection = table.Column<bool>(type: "bit", nullable: false),
                    HasGroup = table.Column<bool>(type: "bit", nullable: false),
                    SubParichhedId = table.Column<int>(type: "int", nullable: true),
                    SubSubParichhedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapdandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mapdandas_MapdandaTables_MapdandaTableId",
                        column: x => x.MapdandaTableId,
                        principalTable: "MapdandaTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    IsFirstLogin = table.Column<bool>(type: "bit", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: false),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_HealthFacilities_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "HealthFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_HospitalType_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "HospitalType",
                        principalColumn: "SN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnusuchiMapdandaTableMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnusuchiMappingId = table.Column<int>(type: "int", nullable: false),
                    AnusuchiId = table.Column<int>(type: "int", nullable: false),
                    ParichhedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnusuchiMapdandaTableMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnusuchiMapdandaTableMappings_AnusuchiMappings_AnusuchiMappingId",
                        column: x => x.AnusuchiMappingId,
                        principalTable: "AnusuchiMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnusuchiMapdandaTableMappings_Anusuchis_AnusuchiId",
                        column: x => x.AnusuchiId,
                        principalTable: "Anusuchis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnusuchiMapdandaTableMappings_Parichheds_ParichhedId",
                        column: x => x.ParichhedId,
                        principalTable: "Parichheds",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "MasterStandardEntries",
                columns: table => new
                {
                    SubmissionCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: false),
                    EntryStatus = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedById = table.Column<long>(type: "bigint", nullable: true),
                    RejectedById = table.Column<long>(type: "bigint", nullable: true),
                    SubmissionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsNewEntry = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_MasterStandardEntries_SubmissionTypes_SubmissionTypeId",
                        column: x => x.SubmissionTypeId,
                        principalTable: "SubmissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterStandardEntries_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_MasterStandardEntries_Users_RejectedById",
                        column: x => x.RejectedById,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

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
                        name: "FK_RegistrationRequests_TempHealthFacilities_HealthFacilityId",
                        column: x => x.HealthFacilityId,
                        principalTable: "TempHealthFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationRequests_Users_HandledById",
                        column: x => x.HandledById,
                        principalTable: "Users",
                        principalColumn: "UserId");
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
                name: "HospitalStandards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthFacilityId = table.Column<int>(type: "int", nullable: false),
                    MapdandaId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMapdandaTableMappings_AnusuchiId",
                table: "AnusuchiMapdandaTableMappings",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMapdandaTableMappings_AnusuchiMappingId",
                table: "AnusuchiMapdandaTableMappings",
                column: "AnusuchiMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMapdandaTableMappings_ParichhedId",
                table: "AnusuchiMapdandaTableMappings",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMappings_BedCountId",
                table: "AnusuchiMappings",
                column: "BedCountId");

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMappings_FacilityTypeId",
                table: "AnusuchiMappings",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMappings_RoleId",
                table: "AnusuchiMappings",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AnusuchiMappings_SubmissionTypeId",
                table: "AnusuchiMappings",
                column: "SubmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_DistrictId",
                table: "HealthFacilities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_FacilityTypeId",
                table: "HealthFacilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_LocalLevelId",
                table: "HealthFacilities",
                column: "LocalLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_PanNumber",
                table: "HealthFacilities",
                column: "PanNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthFacilities_ProvinceId",
                table: "HealthFacilities",
                column: "ProvinceId");

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
                name: "IX_LocalLevels_DistrictId",
                table: "LocalLevels",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_MapdandaTableId",
                table: "Mapdandas",
                column: "MapdandaTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_SubParichhedId",
                table: "Mapdandas",
                column: "SubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapdandas_SubSubParichhedId",
                table: "Mapdandas",
                column: "SubSubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTables_AnusuchiId",
                table: "MapdandaTables",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTables_ParichhedId",
                table: "MapdandaTables",
                column: "ParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_MapdandaTables_SubParichhedId",
                table: "MapdandaTables",
                column: "SubParichhedId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterStandardEntries_ApprovedById",
                table: "MasterStandardEntries",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_MasterStandardEntries_HealthFacilityId",
                table: "MasterStandardEntries",
                column: "HealthFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterStandardEntries_RejectedById",
                table: "MasterStandardEntries",
                column: "RejectedById");

            migrationBuilder.CreateIndex(
                name: "IX_MasterStandardEntries_SubmissionTypeId",
                table: "MasterStandardEntries",
                column: "SubmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parichheds_AnusuchiId",
                table: "Parichheds",
                column: "AnusuchiId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_HandledById",
                table: "RegistrationRequests",
                column: "HandledById");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_HealthFacilityId",
                table: "RegistrationRequests",
                column: "HealthFacilityId");

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
                name: "IX_TempHealthFacilities_DistrictId",
                table: "TempHealthFacilities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_TempHealthFacilities_FacilityTypeId",
                table: "TempHealthFacilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TempHealthFacilities_LocalLevelId",
                table: "TempHealthFacilities",
                column: "LocalLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TempHealthFacilities_ProvinceId",
                table: "TempHealthFacilities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DistrictId",
                table: "Users",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FacilityTypeId",
                table: "Users",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HealthFacilityId",
                table: "Users",
                column: "HealthFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProvinceId",
                table: "Users",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnusuchiMapdandaTableMappings");

            migrationBuilder.DropTable(
                name: "HospitalStandards");

            migrationBuilder.DropTable(
                name: "RegistrationRequests");

            migrationBuilder.DropTable(
                name: "SubMapdandas");

            migrationBuilder.DropTable(
                name: "AnusuchiMappings");

            migrationBuilder.DropTable(
                name: "HospitalStandardEntrys");

            migrationBuilder.DropTable(
                name: "TempHealthFacilities");

            migrationBuilder.DropTable(
                name: "Mapdandas");

            migrationBuilder.DropTable(
                name: "BedCount");

            migrationBuilder.DropTable(
                name: "MasterStandardEntries");

            migrationBuilder.DropTable(
                name: "MapdandaTables");

            migrationBuilder.DropTable(
                name: "SubSubParichheds");

            migrationBuilder.DropTable(
                name: "SubmissionTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubParichheds");

            migrationBuilder.DropTable(
                name: "HealthFacilities");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Parichheds");

            migrationBuilder.DropTable(
                name: "HospitalType");

            migrationBuilder.DropTable(
                name: "LocalLevels");

            migrationBuilder.DropTable(
                name: "Anusuchis");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
