using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;

namespace HRRS.Services.Implementation;

public class HospitalStandardService : IHospitalStandardService
{
    Task IHospitalStandardService.Create(HospitalStandardDto dto)
    {
        throw new NotImplementedException();
    }

    Task<ResultWithDataDto<HospitalStandardDto>> IHospitalStandardService.GetById(int id)
    {
        throw new NotImplementedException();
    }

    Task IHospitalStandardService.Update(int id, HospitalStandardDto dto)
    {
        throw new NotImplementedException();
    }
}