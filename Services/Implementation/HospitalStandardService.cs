using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;

namespace HRRS.Services.Implementation;

public class HospitalStandardService : IHospitalStandardService
{
    private readonly IHospitalStandardRespository _hsrepo;
    private readonly IHealthFacilityRepositoroy _hfrepo;
    private readonly IMapdandaRepository _mdrepo;
    public HospitalStandardService(IHospitalStandardRespository hsrepo, IHealthFacilityRepositoroy hfrepo, IMapdandaRepository mdrepo)
    {
        _hsrepo = hsrepo;
        _hfrepo = hfrepo;
        _mdrepo = mdrepo;

    }
    public async Task Create(HospitalStandardDto dto)
    {
        var healthFacility = await _hfrepo.GetById(dto.HealthFacilityId);
        //var mapdanda = await _mdrepo.

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