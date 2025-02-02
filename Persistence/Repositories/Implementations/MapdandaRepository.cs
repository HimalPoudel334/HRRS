using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Persistence.Repositories.Implementations;
public class MapdandaRepository : IMapdandaRepository
{
    private readonly ApplicationDbContext _context;

    public MapdandaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Mapdanda?> GetByIdAsync(int id)
    {
        return await _context.Mapdandas.FindAsync(id);
    }

    public async Task<IEnumerable<Mapdanda>> GetAllAsync()
    {
        return await _context.Mapdandas.ToListAsync();
    }

    public async Task AddAsync(Mapdanda entity)
    {
        await _context.Mapdandas.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Mapdanda entity)
    {
        _context.Mapdandas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Mapdandas.FindAsync(id);
        if (entity != null)
        {
            _context.Mapdandas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public Task<List<Mapdanda>> GetByAnusuchiId(int anusuchi_id)
    {
        return _context.Mapdandas.Where(x => x.AnusuchiId == anusuchi_id).ToListAsync();
    }
}

