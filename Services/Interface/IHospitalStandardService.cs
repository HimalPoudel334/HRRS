
using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService
{
    Task Create(HospitalStandardDto dto);
    Task Update(HospitalStandardDto dto);
    Task<ResultWithDataDto<HospitalStandardDto>> GetById(int id);
}