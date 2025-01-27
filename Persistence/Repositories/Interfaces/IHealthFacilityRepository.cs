
using Persistence.Entities;

namespace HRRS.Persistence.Repositories.Interfaces;

public interface IHealthFacilityRepositoroy
{
    Task Create(HealthFacility healthFacility);
    Task Update(HealthFacility healthFacility);
    Task<HealthFacility?> GetById(int id);

}