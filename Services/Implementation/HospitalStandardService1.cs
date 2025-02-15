using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.Mapdanda1;
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

        var standardEntry = new HospitalStandardEntry
        {
            Status = EntryStatus.Draft,
            HospitalStandards = stdrs,
        };

        await _dbContext.HospitalStandardEntrys.AddAsync(standardEntry);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }



    public async Task<ResultWithDataDto<List<HospitalEntryDto>>> GetHospitalStandardsEntry(int hospitalId, long userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if(user is null)
        {
            return ResultWithDataDto<List<HospitalEntryDto>>.Failure("Health Facility not found");
        }

        var query = _dbContext.HospitalStandards.AsQueryable();
        if(user.UserType == "SuperAdmin")
        {
            query = query.Where(x => x.StandardEntry.Status != EntryStatus.Draft).Where(x => x.HealthFacilityId == hospitalId);
        } 
        else
        {
            query = query.Where(x => x.StandardEntry.Status != EntryStatus.Draft).Where(x => x.HealthFacilityId == user.HealthFacilityId);
        }


        var entries = await query
            .GroupBy(x => x.StandardEntry)
            .Select(x => new HospitalEntryDto
            {
                Id = x.Key.Id,
                Status = x.Key.Status,
                Parichhed = x.FirstOrDefault() != null ? x.First().Mapdanda != null ? x.First().Mapdanda.Parichhed != null ? x.First().Mapdanda.Parichhed.Name : "" : "" : "",
                SubParichhed = x.FirstOrDefault() != null ? x.First().Mapdanda != null ? x.First().Mapdanda.SubParichhed != null ? x.First().Mapdanda.SubParichhed.Name : "" : "" : "",
                Anusuchi = x.FirstOrDefault() != null ? x.First().Mapdanda.Anusuchi.Name : "",
                Remarks = x.Key.Remarks
            }).ToListAsync();

        return new ResultWithDataDto<List<HospitalEntryDto>>(true, entries, null);
    }

   public async Task<ResultWithDataDto<HospitalEntryDto>> GetHospitalEntryById(int entryId)
    {
        var entry = await _dbContext.HospitalStandardEntrys.FirstOrDefaultAsync(x => x.Id == entryId);
        if(entry is null)
        {
            return ResultWithDataDto<HospitalEntryDto>.Failure("Entry not found");
        }
        var dto = new HospitalEntryDto
        {
            Id = entry.Id,
            Status = entry.Status,
            Remarks = entry.Remarks,
        };


        return new ResultWithDataDto<HospitalEntryDto>(true, dto, null);
    }

    public async Task<ResultDto> ApproveStandardsWithRemark(int entryId, StandardRemarkDto dto)
    {
        var entry = await _dbContext.HospitalStandardEntrys.FindAsync(entryId);
        
        if (entry == null) {
            return ResultDto.Failure("Cannot find entry");
        }

        var stds = await _dbContext.HospitalStandards.Where(x => x.StandardEntry == entry).ToListAsync();
        foreach(var std in stds)
        {
            std.Status = true;
        }

        entry.Status = EntryStatus.Approved;
        entry.Remarks = dto.Remarks;
        entry.UpdatedAt = DateTime.Now;

        _dbContext.HospitalStandards.UpdateRange(stds);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }

    public async Task<ResultDto> RejectStandardsWithRemark(int entryId, StandardRemarkDto dto)
    {

        var entry = await _dbContext.HospitalStandardEntrys.FindAsync(entryId);

        if (entry == null)
        {
            return ResultDto.Failure("Cannot find entry");
        }

        var stds = await _dbContext.HospitalStandards.Where(x => x.StandardEntry == entry).ToListAsync();
        foreach (var std in stds)
        {
            std.Status = false;
        }

        entry.Status = EntryStatus.Rejected;
        entry.Remarks = dto.Remarks;
        entry.UpdatedAt = DateTime.Now;

        _dbContext.HospitalStandards.UpdateRange(stds);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();

    }


    public async Task<ResultDto> PendingHospitalStandardsEntry(int entryId, long userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        if(user is null)
        {
            return ResultDto.Failure("Cannot find user");
        }

        var entry = await _dbContext.HospitalStandardEntrys.FindAsync(entryId);

        if (entry == null)
        {
            return ResultDto.Failure("Cannot find entry");
        }
        var healthFacility = await _dbContext.HealthFacilities.FirstOrDefaultAsync(x => x.Id == user.HealthFacilityId);
        if (healthFacility is null)
        {
            return ResultDto.Failure("You are not allowed to preform this action");
        }
        var isAlreadyPending = await _dbContext.HospitalStandards.Where(x => x.HealthFacilityId == healthFacility!.Id).AnyAsync(x => x.StandardEntry.Status == EntryStatus.Pending);
        if (isAlreadyPending)
        {
            return ResultDto.Failure("You have alread a pending submission");
        }


        entry.Status = EntryStatus.Pending;

        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();

    }


    public async Task<ResultWithDataDto<List<HospitalStandardModel>>> GetHospitalStandardForEntry(int entryId)
    {
        var standards = await _dbContext.HospitalStandards
            .AsSplitQuery()
            .Where(x => x.StandardEntryId == entryId)
            .GroupBy(m => m.Mapdanda.SubSubParichhed)
            .Select(m => new HospitalStandardModel
            {
                HasBedCount = m.FirstOrDefault() != null ? m.First().Mapdanda.IsAvailableDivided : false,
                SubSubParixed = m.Key != null ? m.Key.Name : "",
                List = m
                    .GroupBy(m => m.Mapdanda.Group)
                    .Select(group => new StandardGroupModel
                    {
                        GroupName = group.Key,
                        GroupedMapdanda = group.Select(item => new MapdandaModel
                        {
                            Id = item.Id,
                            Name = item.Mapdanda.Name,
                            SerialNumber = item.Mapdanda.SerialNumber,
                            Has100 = item.Has100,
                            Has200 = item.Has200,
                            Has50 = item.Has50,
                            Has25 = item.Has25,
                            Parimaad = item.Mapdanda.Parimaad,
                            Group = item.Mapdanda.Group,
                            FilePath = item.FilePath,
                            IsAvailable = item.IsAvailable,
                            IsAvailableDivided = item.Mapdanda.IsAvailableDivided,
                        }).ToList()
                    }).ToList()
            })
        .ToListAsync();


        return new ResultWithDataDto<List<HospitalStandardModel>>(true, standards, null);
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