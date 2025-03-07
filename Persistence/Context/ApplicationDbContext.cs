using System.Threading;
using HRRS.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Mapdanda> Mapdandas { get; set; }
    public DbSet<HospitalStandard> HospitalStandards { get; set; }
    public DbSet<HealthFacility> HealthFacilities { get; set; }
    public DbSet<TempHealthFacility> TempHealthFacilities { get; set; }
    public DbSet<Anusuchi> Anusuchis { get; set; }
    public DbSet<Parichhed> Parichheds { get; set; }
    public DbSet<SubParichhed> SubParichheds { get; set; }
    public DbSet<SubSubParichhed> SubSubParichheds { get; set; }
    public DbSet<SubMapdanda> SubMapdandas { get; set; }
    public DbSet<HospitalStandardEntry> HospitalStandardEntrys { get; set; }
    public DbSet<MasterStandardEntry> MasterStandardEntries { get; set; }
    public DbSet<FacilityType> HospitalType { get; set; }
    public DbSet<Role> UserRoles { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<LocalLevel> LocalLevels { get; set; }
    public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
    public DbSet<SubmissionType> SubmissionTypes { get; set; }
    public DbSet<AnusuchiMapping> AnusuchiMappings { get; set; }
    public DbSet<AnusuchiMapdandaTableMapping> AnusuchiMapdandaTableMappings { get; set; }
    public DbSet<MapdandaTable> MapdandaTables { get; set; }

}

