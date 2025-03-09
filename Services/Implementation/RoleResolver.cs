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
            var user =  _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("User not found");

            if(user.UserType == "SuperAdmin")
                return _context.MasterStandardEntries.Where(x => x.EntryStatus != EntryStatus.Draft);

            if (user.UserType == "Hospital")
                return _context.MasterStandardEntries.Where(x => x.HealthFacilityId == user.HealthFacilityId);

            var submissions =  _context.MasterStandardEntries
                .Where(x => user.Role!.UserRoleFacilityTypes.Any(y => y.FacilityTypeId == x.HealthFacility.FacilityTypeId))
                .Where(x => user.Role!.UserRoleFacilityTypes.Any(y => y.BedCountId == x.HealthFacility.BedCountId));

            if(user.Post!.Post != UserPost.Samiti)
                return submissions.Where(x => x.EntryStatus == EntryStatus.STP);

            return submissions;

        }

        public IQueryable<HealthFacility> FacilitiesResolver(long userId)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("User not found");

            if (user.UserType == "SuperAdmin")
                return _context.HealthFacilities;

            if (user.UserType == "Hospital")
                return _context.HealthFacilities.Where(x => x.Id == user.HealthFacilityId);

            var facilities = _context.HealthFacilities
                .Include(x => x.Province)
                .Include(x => x.District)
                .Include(x => x.FacilityType)
                .Include(x => x.LocalLevel)
                .Where(x => user.Role!.UserRoleFacilityTypes.Any(y => y.FacilityTypeId == x.FacilityTypeId))
                .Where(x => user.Role!.UserRoleFacilityTypes.Any(y => y.BedCountId == x.BedCountId));

            return facilities;
        }
    }
}
