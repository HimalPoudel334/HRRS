using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation
{
    public class BedCountService(ApplicationDbContext context) : IBedCountService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ResultWithDataDto<List<BedCount>>> GetAllBedCounts()
        {
            var bedCounts = await _context.BedCounts.ToListAsync();
            return new ResultWithDataDto<List<BedCount>>(true, bedCounts, null);
        }

        //get by facaility type
        public async Task<ResultWithDataDto<List<BedCount>>> GetBedCountsByFacilityType(int facilityTypeId)
        {
            var bedCounts = await _context.BedCounts
                .Where(x => x.FacilityTypes.Any(y => y.SN == facilityTypeId))
                .ToListAsync();

            return new ResultWithDataDto<List<BedCount>>(true, bedCounts, null);
        }
    }
}
