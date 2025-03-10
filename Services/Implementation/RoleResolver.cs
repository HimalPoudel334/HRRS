using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Services.Implementation
{
    public class RoleResolver(ApplicationDbContext context) : IRoleResolver
    {
        private readonly ApplicationDbContext _context = context;
        public  IQueryable<MasterStandardEntry> Submissions(long userId)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .Include(x => x.Post)
                .FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("User not found");

            if (user.UserType == "SuperAdmin")
                return _context.MasterStandardEntries.Where(x => x.EntryStatus != EntryStatus.Draft);

            if (user.UserType == "Hospital")
                return _context.MasterStandardEntries.Where(x => x.HealthFacilityId == user.HealthFacilityId);

            var roleId = user.RoleId!;

            var submissions = _context.MasterStandardEntries
                .Where(x => x.EntryStatus != EntryStatus.Draft)
                .Where(x => x.HealthFacility.RoleId == roleId);

            //var submissions = _context.MasterStandardEntries
            //.Include(x => x.HealthFacility)
            //.AsSplitQuery();

            //var facilityTypeIds = _context.AnusuchiMappings.Where(x => x.RoleId == user.RoleId).Select(x => x.FacilityTypeId).ToList();
            ////var facilityTypeIds = user.Role!.UserRoleFacilityTypes.Select(y => y.FacilityTypeId).ToList();
            //var bedCountIds = user.Role!.UserRoleFacilityTypes.Select(y => y.BedCountId).ToList();

            //submissions = submissions
            //    .Where(x => facilityTypeIds.Contains(x.HealthFacility.FacilityTypeId))
            //    .Where(x => bedCountIds.Contains(x.HealthFacility.BedCountId));

            //if (user.Post!.Post != UserPost.Samiti)
            //    return submissions.Where(x => x.EntryStatus == EntryStatus.STP);

            return submissions;

        }

        [Obsolete("This is for the automation, need to come back to this")]
        public  IQueryable<MasterStandardEntry> SubmissionsOld(long userId)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .Include(x => x.Post)
                .FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("User not found");

            if (user.UserType == "SuperAdmin")
                return _context.MasterStandardEntries.Where(x => x.EntryStatus != EntryStatus.Draft);

            if (user.UserType == "Hospital")
                return _context.MasterStandardEntries.Where(x => x.HealthFacilityId == user.HealthFacilityId);

            var submissions = _context.MasterStandardEntries
            .Include(x => x.HealthFacility)
            .AsSplitQuery();

            var facilityTypeIds = _context.AnusuchiMappings.Where(x => x.RoleId == user.RoleId).Select(x => x.FacilityTypeId).ToList();
            //var facilityTypeIds = user.Role!.UserRoleFacilityTypes.Select(y => y.FacilityTypeId).ToList();
            var bedCountIds = user.Role!.UserRoleFacilityTypes.Select(y => y.BedCountId).ToList();

            submissions = submissions
                .Where(x => facilityTypeIds.Contains(x.HealthFacility.FacilityTypeId))
                .Where(x => bedCountIds.Contains(x.HealthFacility.BedCountId));

            if (user.Post!.Post != UserPost.Samiti)
                return submissions.Where(x => x.EntryStatus == EntryStatus.STP);

            return submissions;

        }

        public IQueryable<HealthFacility> FacilitiesResolver(long userId)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .Include(x => x.Post)
                .FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("User not found");

            var healthfacilitiesQuery = _context.HealthFacilities.Include(x => x.FacilityType)
                .Include(x => x.BedCount)
                .Include(x => x.District)
                .Include(x => x.Province)
                .Include(x => x.LocalLevel);

            if (user.UserType == "SuperAdmin")
                return healthfacilitiesQuery;

            if (user.UserType == "Hospital")
                return healthfacilitiesQuery.Where(x => x.Id == user.HealthFacilityId);

            var facilityTypeIds = user.Role!.UserRoleFacilityTypes.Select(y => y.FacilityTypeId).ToList();
            var bedCountIds = user.Role!.UserRoleFacilityTypes.Select(y => y.BedCountId).ToList();

            var facilities = healthfacilitiesQuery
                .Where(x => facilityTypeIds.Contains(x.FacilityTypeId))
                .Where(x => bedCountIds.Contains(x.BedCountId));

            return facilities;
        }
    }
}
