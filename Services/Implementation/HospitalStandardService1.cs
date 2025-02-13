using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
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
    
    public async Task<ResultDto> Create(List<HospitalMapdandasDto1> dto, int id)
    {
        var user = await _dbContext.Users.FindAsync((long) id); 
        if(user == null)
        {
            return ResultDto.Failure("User not found");
        }

        var healthFacility = await _dbContext.HealthFacilities.FindAsync(user.HealthFacilityId);

        List<HospitalStandard> stdrs = [];
        if (healthFacility == null)
        {
            ResultDto.Failure("Health Facility not found");
        }

        if(dto.Any(x => x.StandardId > 0))
        {
           return await Update(dto, healthFacility.Id);
        }

        foreach(var item in dto)
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
                Has25 = item.Has25,
                Has50 = item.Has50,
                Has100 = item.Has100,
                Has200 = item.Has200,
                Status = false,
            });

        }

        await _dbContext.HospitalStandards.AddRangeAsync(stdrs);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<HospitalMapdandasDto1>>> Get(int hospitalId, int anusuchiId)
    {
        var res = await _dbContext.HospitalStandards
            .Include(x => x.Mapdanda)
            .Where(x => x.HealthFacilityId == hospitalId && x.Mapdanda.AnusuchiId == anusuchiId)
            .Select(x => new HospitalMapdandasDto1()
            {
                    StandardId = x.Id,
                    FilePath = x.FilePath,
                    FiscalYear = x.FiscalYear,
                    IsAvailable = x.IsAvailable,
                    MapdandaName = x.Mapdanda.Name,
                    SerialNumber = x.Mapdanda.SerialNumber,
                    MapdandaId = x.Mapdanda.Id,
                    Remarks = x.Remarks,
                    Has25 = x.Has25,
                    Has50 = x.Has50,
                    Has100 = x.Has100,
                    Has200 = x.Has200,
                    Status = x.Status

            }).ToListAsync();

        if (res is not null) return ResultWithDataDto<List<HospitalMapdandasDto1>>.Success(res);

        var mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == anusuchiId).Select(x => new HospitalMapdandasDto1()
        {
            StandardId = 0,
            FilePath = "",
            FiscalYear = null,
            IsAvailable = false,
            SerialNumber = x.SerialNumber,
            MapdandaName = x.Name,
            MapdandaId = x.Id,
            Remarks = "",
            Has25 = false,
            Has50 = false,
            Has100 = false,
            Has200 = false,
            Status = false,
        }).ToListAsync();

        return ResultWithDataDto<List<HospitalMapdandasDto1>>.Success(mapdandas);
    }

    private async Task<ResultDto> Update(List<HospitalMapdandasDto1> dto, int facilityId)
    {
        var hospitalStandards = await _dbContext.HospitalStandards
            .Where(x => x.HealthFacilityId == facilityId).ToListAsync();
        
        hospitalStandards = hospitalStandards.Where(x => dto.Any(y => y.MapdandaId == x.MapdandaId)).ToList(); 

        foreach (var standard in hospitalStandards)
        {
            var mapdanda = dto.FirstOrDefault(x => x.MapdandaId == standard.MapdandaId);
            if (mapdanda is not null)
            {
                standard.IsAvailable = mapdanda.IsAvailable;
                standard.Remarks = mapdanda.Remarks;
                standard.FilePath = mapdanda.FilePath;
                standard.FiscalYear = mapdanda.FiscalYear;
                standard.Has25 = mapdanda.Has25;
                standard.Has50 = mapdanda.Has50;
                standard.Has100 = mapdanda.Has100;
                standard.Has200 = mapdanda.Has200;
                standard.Status = mapdanda.Status;
                standard.UpdatedAt = DateTime.Now;
            }
        }

        _dbContext.UpdateRange(hospitalStandards);
        await _dbContext.SaveChangesAsync();
        
        return ResultDto.Success();
    }
}