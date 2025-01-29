using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Services.Implementation;

public class HospitalStandardService(ApplicationDbContext dbContext) : IHospitalStandardService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task<ResultDto> Create(HospitalStandardDto dto)
    {
        var healthFacility = await _dbContext.HealthFacilities.FindAsync(dto.HealthFacilityId);

        List<HospitalStandard> stdrs = [];
        if (healthFacility == null)
        {
            throw new Exception("Health Facility not found");
        }

        if(dto.HospitalMapdandas.Any(x => x.StandardId > 0))
        {
           return await Update(dto);
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

        return new ResultDto(true, null);
    }
    public async Task<ResultWithDataDto<HospitalStandardDto>> Get(int hospitalId, int anusuchiId)
    {
        var res = await _dbContext.HospitalStandards
            .Include(x => x.Mapdanda)
            .Where(x => x.HealthFacilityId == hospitalId && x.Mapdanda.AnusuchiNumber == anusuchiId)
            .GroupBy(x => x.HealthFacility.Id)
            .Select(x => new HospitalStandardDto()
            {
                HealthFacilityId = x.Key,
                HospitalMapdandas = x.Select(x => new HospitalMapdandasDto()
                {
                    StandardId = x.Id,
                    FilePath = x.FilePath,
                    FiscalYear = x.FiscalYear,
                    IsAvailable = x.IsAvailable,
                    Status = x.Status,
                    MapdandaName = x.Mapdanda.Name,
                    SerialNumber = x.Mapdanda.SerialNumber,
                    MapdandaId = x.Mapdanda.Id,
                    Remarks = x.Remarks
                }).ToList(),
            }).FirstOrDefaultAsync();

        if (res is not null) return new ResultWithDataDto<HospitalStandardDto>(true, res, null);

        var mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiNumber == anusuchiId).ToListAsync();
        var dtos = new HospitalStandardDto()
        {
            HealthFacilityId = hospitalId,
            HospitalMapdandas = mapdandas.Select(x => new HospitalMapdandasDto()
            {
                StandardId = 0,
                FilePath = "",
                FiscalYear = null,
                IsAvailable = false,
                Status = false,
                SerialNumber = x.SerialNumber,
                MapdandaName = x.Name,
                MapdandaId = x.Id,
                Remarks = ""
            }).ToList(),
        };

        return new ResultWithDataDto<HospitalStandardDto>(true, dtos, null);
    }
    public async Task<ResultWithDataDto<HospitalStandardDto>> GetById(int id)
    {
        var hospitalStandard = await _dbContext.HospitalStandards.FindAsync(id);
        if(hospitalStandard == null)
        {
            return new ResultWithDataDto<HospitalStandardDto>(false, null, "Hospital Standard not found");
        }
        var hospitalStandardDto = new HospitalStandardDto()
        {
            HealthFacilityId = hospitalStandard.HealthFacilityId,
            HospitalMapdandas =
            [
                new HospitalMapdandasDto()
                {
                    FilePath = hospitalStandard.FilePath,
                    FiscalYear = hospitalStandard.FiscalYear,
                    IsAvailable = hospitalStandard.IsAvailable,
                    Status = hospitalStandard.Status,
                    MapdandaName = hospitalStandard.Mapdanda.Name,
                    SerialNumber = hospitalStandard.Mapdanda.SerialNumber,
                    MapdandaId = hospitalStandard.MapdandaId,
                    Remarks = hospitalStandard.Remarks
                }
            ]
        };
        return new ResultWithDataDto<HospitalStandardDto>(true, hospitalStandardDto, null);
    }
    private async Task<ResultDto> Update(HospitalStandardDto dto)
    {
        var hospitalStandards = await _dbContext.HospitalStandards
            .Where(x => x.HealthFacilityId == dto.HealthFacilityId).ToListAsync();
        
        hospitalStandards = hospitalStandards.Where(x => dto.HospitalMapdandas.Any(y => y.MapdandaId == x.MapdandaId)).ToList();



        foreach (var standard in hospitalStandards)
        {
            var mapdanda = dto.HospitalMapdandas.FirstOrDefault(x => x.MapdandaId == standard.MapdandaId);
            if (mapdanda is not null)
            {
                standard.IsAvailable = mapdanda.IsAvailable;
                standard.Remarks = mapdanda.Remarks;
                standard.FilePath = mapdanda.FilePath;
                standard.FiscalYear = mapdanda.FiscalYear;
                standard.Status = mapdanda.Status;
            }
        }

        _dbContext.UpdateRange(hospitalStandards);
        await _dbContext.SaveChangesAsync();
        
        return new ResultDto(true, null);
    }
}