using HRRS.Persistence.Entities;
using Persistence.Entities;

namespace HRRS.Services.Interface;

public interface IRoleResolver
{
    IQueryable<MasterStandardEntry> Submissions(long userId);
    IQueryable<HealthFacility> FacilitiesResolver(long userId);
}
