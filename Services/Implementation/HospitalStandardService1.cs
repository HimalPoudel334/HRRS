using System.Reflection.Metadata.Ecma335;
using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Services.Implementation;

public class HospitalStandardService1(ApplicationDbContext dbContext) : IHospitalStandardService1
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task<ResultDto> Create(HospitalStandardDto1 dto)
    {
        var healthFacility = await _dbContext.HealthFacilities.FindAsync(dto.HealthFacilityId);

        List<HospitalStandard> stdrs = [];
        if (healthFacility == null)
        {
            ResultDto.Failure("Health Facility not found");
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
                Has25 = item.Has25,
                Has50 = item.Has50,
                Has100 = item.Has100,
                Has200 = item.Has200,

            });

        }

        await _dbContext.HospitalStandards.AddRangeAsync(stdrs);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }
    public async Task<ResultWithDataDto<HospitalStandardDto1>> Get(int hospitalId, int anusuchiId)
    {
        var res = await _dbContext.HospitalStandards
            .Include(x => x.Mapdanda)
            .Where(x => x.HealthFacilityId == hospitalId && x.Mapdanda.AnusuchiId == anusuchiId)
            .GroupBy(x => x.HealthFacility.Id)
            .Select(x => new HospitalStandardDto1()
            {
                HealthFacilityId = x.Key,
                HospitalMapdandas = x.Select(x => new HospitalMapdandasDto1()
                {
                    StandardId = x.Id,
                    FilePath = x.FilePath,
                    FiscalYear = x.FiscalYear,
                    IsAvailable = x.IsAvailable,
                    Status = x.Status,
                    MapdandaName = x.Mapdanda.Name,
                    SerialNumber = x.Mapdanda.SerialNumber,
                    MapdandaId = x.Mapdanda.Id,
                    Remarks = x.Remarks,
                    Has25 = x.Has25,
                    Has50 = x.Has50,
                    Has100 = x.Has100,
                    Has200 = x.Has200

                }).ToList(),
            }).FirstOrDefaultAsync();

        if (res is not null) return ResultWithDataDto<HospitalStandardDto1>.Success(res);

        var mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == anusuchiId).ToListAsync();
        var dtos = new HospitalStandardDto1()
        {
            HealthFacilityId = hospitalId,
            HospitalMapdandas = mapdandas.Select(x => new HospitalMapdandasDto1()
            {
                StandardId = 0,
                FilePath = "",
                FiscalYear = null,
                IsAvailable = false,
                Status = true,
                SerialNumber = x.SerialNumber,
                MapdandaName = x.Name,
                MapdandaId = x.Id,
                Remarks = "",
                Has25 = "",
                Has50 = "",
                Has100 = "",
                Has200 = ""
            }).ToList(),
        };

        return ResultWithDataDto<HospitalStandardDto1>.Success(dtos);
    }
    public async Task<ResultWithDataDto<HospitalStandardDto1>> GetById(int id)
    {
        var hospitalStandard = await _dbContext.HospitalStandards.FindAsync(id);
        if(hospitalStandard == null)
        {
            return ResultWithDataDto<HospitalStandardDto1>.Failure("Hospital Standard not found");
        }
        var hospitalStandardDto = new HospitalStandardDto1()
        {
            HealthFacilityId = hospitalStandard.HealthFacilityId,
            HospitalMapdandas =
            [
                new HospitalMapdandasDto1()
                {
                    FilePath = hospitalStandard.FilePath,
                    FiscalYear = hospitalStandard.FiscalYear,
                    IsAvailable = hospitalStandard.IsAvailable,
                    Status = hospitalStandard.Status,
                    MapdandaName = hospitalStandard.Mapdanda.Name,
                    SerialNumber = hospitalStandard.Mapdanda.SerialNumber,
                    MapdandaId = hospitalStandard.MapdandaId,
                    Remarks = hospitalStandard.Remarks,
                    Has25 = hospitalStandard.Has25,
                    Has50 = hospitalStandard.Has50,
                    Has100 = hospitalStandard.Has100,
                    Has200 = hospitalStandard.Has200
                }
            ]
        };
        return ResultWithDataDto<HospitalStandardDto1>.Success(hospitalStandardDto);
    }
    private async Task<ResultDto> Update(HospitalStandardDto1 dto)
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
                standard.Has25 = mapdanda.Has25;
                standard.Has50 = mapdanda.Has50;
                standard.Has100 = mapdanda.Has100;
                standard.Has200 = mapdanda.Has200;

            }
        }

        _dbContext.UpdateRange(hospitalStandards);
        await _dbContext.SaveChangesAsync();
        
        return ResultDto.Success();
    }
}