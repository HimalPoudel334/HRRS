
using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IHealthFacilityService
{
    Task Update(int id, HealthFacilityDto healthFacilityDto);
    Task<ResultWithDataDto<HealthFacilityDto>> GetById(int id, long userId);
    Task<ResultWithDataDto<List<HealthFacilityDto>>> GetAll(long userId);


}