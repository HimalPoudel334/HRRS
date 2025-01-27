
using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IHealthFacilityService
{
    Task Create(HealthFacilityDto healthFacilityDto);
    Task Update(HealthFacilityDto healthFacilityDto);
    Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id);


}