﻿// <auto-generated />
using System;
using HRRS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRRS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Anusuchi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DafaNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Anusuchis");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.MasterStandardEntry", b =>
                {
                    b.Property<Guid>("SubmissionCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntryStatus")
                        .HasColumnType("int");

                    b.Property<int>("HealthFacilityId")
                        .HasColumnType("int");

                    b.Property<int>("SubmissionType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SubmissionCode");

                    b.HasIndex("HealthFacilityId");

                    b.ToTable("MasterStandardEntries");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BedCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.SubmissionStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("EntryId");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<int?>("HealthFacilityId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("HealthFacilityId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HospitalStandard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiscalYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HealthFacilityId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("MapdandaId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StandardEntryId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MapdandaId");

                    b.HasIndex("StandardEntryId");

                    b.ToTable("HospitalStandards");
                });

            modelBuilder.Entity("HospitalStandardEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MasterStandardEntrySubmissionCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MasterStandardEntrySubmissionCode");

                    b.ToTable("HospitalStandardEntrys");
                });

            modelBuilder.Entity("Mapdanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnusuchiId")
                        .HasColumnType("int");

                    b.Property<string>("Col5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Col6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Col7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Col8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Col9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FormType")
                        .HasColumnType("int");

                    b.Property<string>("Group")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is100Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Is200Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Is25Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Is50Active")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAvailableDivided")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCol5Active")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCol6Active")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCol7Active")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCol8Active")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCol9Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParichhedId")
                        .HasColumnType("int");

                    b.Property<string>("Parimaad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("SubParichhedId")
                        .HasColumnType("int");

                    b.Property<int?>("SubSubParichhedId")
                        .HasColumnType("int");

                    b.Property<string>("Value100")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value200")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value25")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value50")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnusuchiId");

                    b.HasIndex("ParichhedId");

                    b.HasIndex("SubParichhedId");

                    b.HasIndex("SubSubParichhedId");

                    b.ToTable("Mapdandas");
                });

            modelBuilder.Entity("Parichhed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnusuchiId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnusuchiId");

                    b.ToTable("Parichheds");
                });

            modelBuilder.Entity("Persistence.Entities.FacilityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FacilityTypes");
                });

            modelBuilder.Entity("Persistence.Entities.HealthFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationSubmittedAuthority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationSubmittedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApporvingAuthority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApprovalValidityTill")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvailableServices")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BedCount")
                        .HasColumnType("int");

                    b.Property<string>("DateOfInspection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExecutiveHeadEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExecutiveHeadMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExecutiveHeadName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityHeadEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityHeadName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityHeadPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacilityTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsBranchExtension")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsExecutionPermission")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsLetterOfIntent")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRelocation")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRenewal")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsServiceExtension")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsUpgrade")
                        .HasColumnType("bit");

                    b.Property<string>("LastRenewedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Others")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PanNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PermissionReceivedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RenewalValidityTill")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RenewingAuthority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecialistCount")
                        .HasColumnType("int");

                    b.Property<string>("Tole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpgradeDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpgradingAuthority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WardNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacilityTypeId");

                    b.HasIndex("PanNumber")
                        .IsUnique();

                    b.ToTable("HealthFacilities");
                });

            modelBuilder.Entity("SubMapdanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MapdandaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parimaad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MapdandaId");

                    b.ToTable("SubMapdandas");
                });

            modelBuilder.Entity("SubParichhed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParichhedId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParichhedId");

                    b.ToTable("SubParichheds");
                });

            modelBuilder.Entity("SubSubParichhed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubParichhedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubParichhedId");

                    b.ToTable("SubSubParichheds");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.MasterStandardEntry", b =>
                {
                    b.HasOne("Persistence.Entities.HealthFacility", "HealthFacility")
                        .WithMany()
                        .HasForeignKey("HealthFacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HealthFacility");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.SubmissionStatus", b =>
                {
                    b.HasOne("HRRS.Persistence.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRRS.Persistence.Entities.MasterStandardEntry", "Entry")
                        .WithMany("Status")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.User", b =>
                {
                    b.HasOne("Persistence.Entities.HealthFacility", "HealthFacility")
                        .WithMany()
                        .HasForeignKey("HealthFacilityId");

                    b.HasOne("HRRS.Persistence.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("HealthFacility");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HospitalStandard", b =>
                {
                    b.HasOne("Mapdanda", "Mapdanda")
                        .WithMany()
                        .HasForeignKey("MapdandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalStandardEntry", "StandardEntry")
                        .WithMany("HospitalStandards")
                        .HasForeignKey("StandardEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mapdanda");

                    b.Navigation("StandardEntry");
                });

            modelBuilder.Entity("HospitalStandardEntry", b =>
                {
                    b.HasOne("HRRS.Persistence.Entities.MasterStandardEntry", "MasterStandardEntry")
                        .WithMany("HospitalStandardEntries")
                        .HasForeignKey("MasterStandardEntrySubmissionCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MasterStandardEntry");
                });

            modelBuilder.Entity("Mapdanda", b =>
                {
                    b.HasOne("Anusuchi", "Anusuchi")
                        .WithMany()
                        .HasForeignKey("AnusuchiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parichhed", "Parichhed")
                        .WithMany()
                        .HasForeignKey("ParichhedId");

                    b.HasOne("SubParichhed", "SubParichhed")
                        .WithMany("Mapdandas")
                        .HasForeignKey("SubParichhedId");

                    b.HasOne("SubSubParichhed", "SubSubParichhed")
                        .WithMany("Mapdandas")
                        .HasForeignKey("SubSubParichhedId");

                    b.Navigation("Anusuchi");

                    b.Navigation("Parichhed");

                    b.Navigation("SubParichhed");

                    b.Navigation("SubSubParichhed");
                });

            modelBuilder.Entity("Parichhed", b =>
                {
                    b.HasOne("Anusuchi", "Anusuchi")
                        .WithMany("Parichheds")
                        .HasForeignKey("AnusuchiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anusuchi");
                });

            modelBuilder.Entity("Persistence.Entities.HealthFacility", b =>
                {
                    b.HasOne("Persistence.Entities.FacilityType", "FacilityType")
                        .WithMany()
                        .HasForeignKey("FacilityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FacilityType");
                });

            modelBuilder.Entity("SubMapdanda", b =>
                {
                    b.HasOne("Mapdanda", "Mapdanda")
                        .WithMany("SubMapdandas")
                        .HasForeignKey("MapdandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mapdanda");
                });

            modelBuilder.Entity("SubParichhed", b =>
                {
                    b.HasOne("Parichhed", "Parichhed")
                        .WithMany("SubParichheds")
                        .HasForeignKey("ParichhedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parichhed");
                });

            modelBuilder.Entity("SubSubParichhed", b =>
                {
                    b.HasOne("SubParichhed", "SubParichhed")
                        .WithMany("SubSubParichheds")
                        .HasForeignKey("SubParichhedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubParichhed");
                });

            modelBuilder.Entity("Anusuchi", b =>
                {
                    b.Navigation("Parichheds");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.MasterStandardEntry", b =>
                {
                    b.Navigation("HospitalStandardEntries");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("HRRS.Persistence.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("HospitalStandardEntry", b =>
                {
                    b.Navigation("HospitalStandards");
                });

            modelBuilder.Entity("Mapdanda", b =>
                {
                    b.Navigation("SubMapdandas");
                });

            modelBuilder.Entity("Parichhed", b =>
                {
                    b.Navigation("SubParichheds");
                });

            modelBuilder.Entity("SubParichhed", b =>
                {
                    b.Navigation("Mapdandas");

                    b.Navigation("SubSubParichheds");
                });

            modelBuilder.Entity("SubSubParichhed", b =>
                {
                    b.Navigation("Mapdandas");
                });
#pragma warning restore 612, 618
        }
    }
}
