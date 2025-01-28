
using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService
{
    Task<ResultDto> Create(HospitalStandardDto dto);
    Task Update(int id, HospitalStandardDto dto);
    Task<ResultWithDataDto<HospitalStandardDto>> GetById(int id);
    Task<ResultWithDataDto<HospitalStandardDto>> Get(int hospitalId, int anusuchiId);
}