using HRRS.Dto;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IBedCountService
    {
        Task<ResultWithDataDto<List<BedCount>>> GetAllBedCounts();
        Task<ResultWithDataDto<List<BedCount>>> GetBedCountsByFacilityType(int facilityTypeId);
    }
}
