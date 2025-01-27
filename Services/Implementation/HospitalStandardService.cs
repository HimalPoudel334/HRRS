using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class HospitalStandardService(ApplicationDbContext dbContext) : IHospitalStandardService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task Create(HospitalStandardDto dto)
    {
        var healthFacility = await _dbContext.HealthFacilities.FindAsync(dto.HealthFacilityId);

        List<HospitalStandard> stdrs = [];
        if (healthFacility == null)
        {
            throw new Exception("Health Facility not found");
        }

        foreach(var item in dto.HospitalMapdandas)
        {
            var mapdanda = await _dbContext.Mapdandas.FindAsync(item.MapdandaId) ?? throw new Exception("Mapdanda not found");

            stdrs.Add(new HospitalStandard()
            {
                HealthFacility = healthFacility,
                Mapdanda = mapdanda,
                IsAvailable = item.IsAvailable,
                Remarks = item.Remarks,
                FilePath = item.FilePath,
                FiscalYear = item.FiscalYear,
                Status = item.Status,
            });

        }

        await _dbContext.HospitalStandards.AddRangeAsync(stdrs);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<HospitalStandardDto> Get(int hospitalId, int anusuchiId, string? fiscalYear)
    {
        var res = await _dbContext.HospitalStandards
            .Where(x => x.HealthFacilityId == hospitalId)
            .Where(x => x.FiscalYear == fiscalYear)
            .Where(x => x.Mapdanda.AnusuchiNumber == anusuchiId)
            .GroupBy(x => x.HealthFacility.Id)
            .Select(x => new HospitalStandardDto()
            {
                HealthFacilityId = x.Key,
                HospitalMapdandas = x.Select(x => new HospitalMapdandasDto()
                {
                    FilePath = x.FilePath,
                    FiscalYear = x.FiscalYear,
                    IsAvailable = x.IsAvailable,
                    Status = x.Status,
                    MapdandaName = x.Mapdanda.Name,
                    MapdandaId = x.Mapdanda.Id,
                    Remarks = x.Remarks
                }).ToList(),
            })
            .FirstOrDefaultAsync();

        if (res is not null) return res;

        var mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiNumber == anusuchiId).ToListAsync();
        return new HospitalStandardDto()
        {
            HealthFacilityId = hospitalId,
            HospitalMapdandas = mapdandas.Select(x => new HospitalMapdandasDto()
            {
                FilePath = null,
                FiscalYear = fiscalYear,
                IsAvailable = false,
                Status = false,
                MapdandaName = x.Name,
                MapdandaId = x.Id,
                Remarks = ""
            }).ToList(),
        };
    }

    //public async Task<List<HospitalStandardDto>>()


    Task<ResultWithDataDto<HospitalStandardDto>> IHospitalStandardService.GetById(int id)
    {
        throw new NotImplementedException();
    }

    Task IHospitalStandardService.Update(int id, HospitalStandardDto dto)
    {
        throw new NotImplementedException();
    }
}