using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;

namespace HRRS.Services.Implementation;

public class HospitalStandardService : IHospitalStandardService
{
    public HospitalStandardService(IHospitalStandardRespository repository)
    {
        
    }
    public Task Create(HospitalStandardDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ResultWithDataDto<HospitalStandardDto>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(HospitalStandardDto dto)
    {
        throw new NotImplementedException();
    }
}