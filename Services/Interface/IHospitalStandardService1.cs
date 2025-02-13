
using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService1
{
    Task<ResultDto> Create(List<HospitalMapdandasDto1> dto, int id);
    Task<ResultWithDataDto<List<HospitalMapdandasDto1>>> Get(int hospitalId, int anusuchiId);
}