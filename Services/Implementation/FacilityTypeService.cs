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
        public async Task<ResultWithDataDto<List<FacilityTypeDto>>> GetAll(int? id)
        {
            var facilityTypesQuery = _context.HospitalType.Where(x => x.ACTIVE);

            if(id.HasValue)
                facilityTypesQuery = facilityTypesQuery.Where(x => x.FacilityTypeId == id);

            var facilityTypes = await facilityTypesQuery.Select(x => new FacilityTypeDto
            {
                Id = x.SN,
                Name = x.HOSP_TYPE,
                HospitalCode = x.HOSP_CODE,
                IsActive = x.ACTIVE,
            }).ToListAsync();

            if (facilityTypes.Count == 0 || facilityTypes is null) 
                return new ResultWithDataDto<List<FacilityTypeDto>>(true, null, "Cannot find health facility types");
            return new ResultWithDataDto<List<FacilityTypeDto>>(true, facilityTypes, null);

        }

        public async Task<ResultDto> Create(FacilityTypeDto dto)
        {
            string maxHospCode = _context.HospitalType.Max(x => x.HOSP_CODE)!;
            var hospCode = (int.Parse(maxHospCode) + 1).ToString("D4");
            var facilityType = new FacilityType()
            {
                HOSP_TYPE = dto.Name,
                ACTIVE = true,
                HOSP_CODE = hospCode
            };

            await _context.HospitalType.AddAsync(facilityType);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }
    }
}
