using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;

namespace HRRS.Persistence.Repositories.Implementations;

public class HospitalStandardRepository : IHospitalStandardRespository
{
    private readonly ApplicationDbContext _context;
    public HospitalStandardRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Create(HospitalStandard healthStandard)
    {
        await _context.HospitalStandards.AddAsync(healthStandard);
        await _context.SaveChangesAsync();
    }
    public async Task Update(HospitalStandard healthStandard)
    {
        _context.HospitalStandards.Update(healthStandard);
        await _context.SaveChangesAsync();
    }
    public async Task<HospitalStandard?> GetById(int id)
    {
        return await _context.HospitalStandards.FindAsync(id);

    }

    public Task<List<HospitalStandard>> GetAll()
    {
        throw new NotImplementedException();
    }
}

