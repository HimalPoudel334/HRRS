
using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IHealthFacilityService
{
    Task<ResultWithDataDto<HealthFacilityDto>> Create(HealthFacilityDto healthFacilityDto);
    Task Update(HealthFacilityDto healthFacilityDto);
    Task<HealthFacilityDto> GetById(int id);


}