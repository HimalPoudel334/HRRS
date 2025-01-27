
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Persistence.Repositories.Implementations;

public class HealthFacilityRepository : IHealthFacilityRepositoroy
{
    private readonly ApplicationDbContext _context;
    public HealthFacilityRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Create(HealthFacility healthFacility)
    {
        await _context.HealthFacilities.AddAsync(healthFacility);
        await _context.SaveChangesAsync();
    }
    public async Task Update(HealthFacility healthFacility)
    {
        _context.HealthFacilities.Update(healthFacility);
        await _context.SaveChangesAsync();
    }
    public async Task<HealthFacility?> GetById(int id)
    {
        return await _context.HealthFacilities.FindAsync(id);
    }

}