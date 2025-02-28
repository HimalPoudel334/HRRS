using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation
{
    public class FacilityAddressService : IFacilityAddressService
    {
        private readonly ApplicationDbContext _context;

        public FacilityAddressService(ApplicationDbContext context) => _context = context;

        public async Task<ResultWithDataDto<List<Province>>> GetAllProvinces()
        {
            var facilityAddresses = await _context.Provinces.ToListAsync();
            if (facilityAddresses.Count == 0 || facilityAddresses is null)
                return new ResultWithDataDto<List<Province>>(true, null, "Cannot find provinces");
            return new ResultWithDataDto<List<Province>>(true, facilityAddresses, null);
        }

        //get all districts
        public async Task<ResultWithDataDto<List<District>>> GetAllDistricts()
        {
            var facilityAddresses = await _context.Districts.ToListAsync();
            if (facilityAddresses.Count == 0 || facilityAddresses is null)
                return new ResultWithDataDto<List<District>>(true, null, "Cannot find districts");
            return new ResultWithDataDto<List<District>>(true, facilityAddresses, null);
        }

        //get district of province
        public async Task<ResultWithDataDto<List<District>>> GetDistrictsByProvince(int provinceId)
        {
            var facilityAddresses = await _context.Districts.Where(x => x.ProvinceId == provinceId).ToListAsync();
            if (facilityAddresses.Count == 0 || facilityAddresses is null)
                return new ResultWithDataDto<List<District>>(true, null, "Cannot find districts");
            return new ResultWithDataDto<List<District>>(true, facilityAddresses, null);
        }

        //get locallevel of district
        public async Task<ResultWithDataDto<List<LocalLevel>>> GetLocalLevelsByDistrict(int districtId)
        {
            var facilityAddresses = await _context.LocalLevels.Where(x => x.DistrictId == districtId).ToListAsync();
            if (facilityAddresses.Count == 0 || facilityAddresses is null)
                return new ResultWithDataDto<List<LocalLevel>>(true, null, "Cannot find local levels");
            return new ResultWithDataDto<List<LocalLevel>>(true, facilityAddresses, null);
        }
    }

}
