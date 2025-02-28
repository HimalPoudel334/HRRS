using HRRS.Dto;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IFacilityAddressService
    {
        Task<ResultWithDataDto<List<Province>>> GetAllProvinces();
        Task<ResultWithDataDto<List<District>>> GetAllDistricts();
        Task<ResultWithDataDto<List<District>>> GetDistrictsByProvince(int provinceId);
        Task<ResultWithDataDto<List<LocalLevel>>> GetLocalLevelsByDistrict(int districtId);
    }
}
