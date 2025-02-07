
using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService1
{
    Task<ResultDto> Create(HospitalStandardDto1 dto);
    Task<ResultWithDataDto<HospitalStandardDto1>> GetById(int id);
    Task<ResultWithDataDto<HospitalStandardDto1>> Get(int hospitalId, int anusuchiId);
}