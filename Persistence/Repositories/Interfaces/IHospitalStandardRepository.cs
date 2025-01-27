namespace HRRS.Persistence.Repositories.Interfaces;

public interface IHospitalStandardRespository
{
    Task Create(HospitalStandard healthStandard);
    Task Update(HospitalStandard healthStandard);
    Task<HospitalStandard?> GetById(int id);
    Task<List<HospitalStandard>> GetAll();
}