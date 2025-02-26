using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Services.Implementation
{
    public class FacilityTypeService : IFacilityTypeService
    {
        private readonly ApplicationDbContext _context;

        public FacilityTypeService(ApplicationDbContext context) => _context = context;
        public async Task<ResultWithDataDto<List<FacilityType>>> GetAll()
        {
            var facilityTypes = await _context.FacilityTypes.ToListAsync();
            if(facilityTypes.Count == 0 || facilityTypes is null) 
                return new ResultWithDataDto<List<FacilityType>>(true, null, "Cannot find health facility types");
            return new ResultWithDataDto<List<FacilityType>>(true, facilityTypes, null);

        }

        public async Task<ResultDto> Create(FacilityTypeDto dto)
        {
            var facilityType = new FacilityType()
            {
                Name = dto.Name
            };

            await _context.FacilityTypes.AddAsync(facilityType);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }
    }
}
