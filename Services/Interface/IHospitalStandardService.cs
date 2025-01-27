
using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService
{
    Task Create(HospitalStandardDto dto);
    Task Update(int id, HospitalStandardDto dto);
    Task<ResultWithDataDto<HospitalStandardDto>> GetById(int id);
    Task<HospitalStandardDto> Get(int hospitalId, int anusuchiId, string fiscalYear);
}