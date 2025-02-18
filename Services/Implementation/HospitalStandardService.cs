using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.Mapdanda1;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class HospitalStandardService(ApplicationDbContext dbContext) : IHospitalStandardService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task<ResultDto> Create(HospitalStandardDto dto, int id)
    {
        var user = await _dbContext.Users.FindAsync((long) id); 
        if(user == null)
        {
            return ResultDto.Failure("User not found");
        }

        var healthFacility = await _dbContext.HealthFacilities.FindAsync(user.HealthFacilityId);

        if (healthFacility == null)
        {
            ResultDto.Failure("Health Facility not found");
        }

        var masterEntry = await _dbContext.MasterStandardEntries.FindAsync(dto.SubmissionCode);
        if (masterEntry == null)
        {
            return ResultDto.Failure("Registrations type unknown for health facility");
        }

        List<HospitalStandard> stdrs = [];

        foreach(var item in dto.Mapdandas)
        {
            var mapdanda = await _dbContext.Mapdandas.FindAsync(item.MapdandaId) ?? throw new Exception("Mapdanda not found");

            stdrs.Add(new HospitalStandard()
            {
                Mapdanda = mapdanda,
                IsAvailable = item.IsAvailable,
                Remarks = item.Remarks,
                FilePath = item.FilePath,
                FiscalYear = item.FiscalYear,
                Status = false,
            });
        }

        var standardEntry = new HospitalStandardEntry
        {
            Status = EntryStatus.Draft,
            HospitalStandards = stdrs,
            MasterStandardEntry = masterEntry,

        };

        await _dbContext.HospitalStandardEntrys.AddAsync(standardEntry);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }


    public async Task<ResultWithDataDto<List<MasterStandardEntryDto>>> AdminGetMasterStandardsEntry(int hospitalId)
    {
       
        var res = await _dbContext.MasterStandardEntries.Where(x => x.HealthFacilityId  == hospitalId).Select(x => new MasterStandardEntryDto
        {
            EntryStatus = x.EntryStatus,
            HealthFacilityId = x.HealthFacilityId,
            Remarks = x.Remarks,
            SubmissionCode = x.SubmissionCode,
            SubmissionType = x.SubmissionType
        }).ToListAsync();


        return new ResultWithDataDto<List<MasterStandardEntryDto>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<MasterStandardEntryDto>>> UserGetMasterStandardsEntry(int hospitalId)
    {

        var res = await _dbContext.MasterStandardEntries.Where(x => x.HealthFacilityId == hospitalId).Select(x => new MasterStandardEntryDto
        {
            EntryStatus = x.EntryStatus,
            HealthFacilityId = x.HealthFacilityId,
            Remarks = x.Remarks,
            SubmissionCode = x.SubmissionCode,
            SubmissionType = x.SubmissionType
        }).ToListAsync();


        return new ResultWithDataDto<List<MasterStandardEntryDto>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<HospitalEntryDto>>> GetStandardEntries(Guid submissionCode)
    {
        var res = await _dbContext.HospitalStandardEntrys
            .Where(x => x.MasterStandardEntry.SubmissionCode == submissionCode)
            .Select(x => new HospitalEntryDto
            {
                Id = x.Id,
                SubmissionType = x.SubmissionType,
                Remarks = x.Remarks,
                Status = x.Status,
                Anusuchi = x.HospitalStandards.First().Mapdanda.Anusuchi.Name,
                Parichhed = x.HospitalStandards.First().Mapdanda.Parichhed != null ? x.HospitalStandards.First().Mapdanda.Parichhed!.Name : "",
                SubParichhed = x.HospitalStandards.First().Mapdanda.SubParichhed != null ? x.HospitalStandards.First().Mapdanda.SubParichhed!.Name : ""
            }).ToListAsync();

        return new ResultWithDataDto<List<HospitalEntryDto>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<HospitalStandard>>> GetStandards(int entryId)
    {

        var res = await _dbContext.HospitalStandards.Where(x => x.StandardEntryId == entryId).ToListAsync();

        return new ResultWithDataDto<List<HospitalStandard>>(true, res, null);
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
            SubmissionType = entry.SubmissionType
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


    public async Task<ResultDto> PendingHospitalStandardsEntry(Guid entryId, int userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user is null)
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



    public async Task<ResultWithDataDto<List<HospitalMapdandasDto>>> Get(int hospitalId, int anusuchiId)
    {
        var res = await _dbContext.HospitalStandards
            .Include(x => x.Mapdanda)
            .Where(x => x.HealthFacilityId == hospitalId && x.Mapdanda.AnusuchiId == anusuchiId)
            .Select(x => new HospitalMapdandasDto()
            {
                    StandardId = x.Id,
                    FilePath = x.FilePath,
                    FiscalYear = x.FiscalYear,
                    IsAvailable = x.IsAvailable,
                    MapdandaName = x.Mapdanda.Name,
                    SerialNumber = x.Mapdanda.SerialNumber,
                    MapdandaId = x.Mapdanda.Id,
                    Remarks = x.Remarks,
                    Status = x.Status

            }).ToListAsync();

        if (res is not null) return ResultWithDataDto<List<HospitalMapdandasDto>>.Success(res);

        var mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == anusuchiId).Select(x => new HospitalMapdandasDto()
        {
            StandardId = 0,
            FilePath = "",
            FiscalYear = null,
            IsAvailable = false,
            SerialNumber = x.SerialNumber,
            MapdandaName = x.Name,
            MapdandaId = x.Id,
            Remarks = "",
            Status = false,
        }).ToListAsync();

        return ResultWithDataDto<List<HospitalMapdandasDto>>.Success(mapdandas);
    }

    public async Task<ResultDto> Update(HospitalStandardDto dto, int id)
    {
        var masterEntry = await _dbContext.MasterStandardEntries.FirstOrDefaultAsync(x => x.SubmissionCode==dto.SubmissionCode && x.HealthFacilityId==id);
        if(masterEntry is null)
        {
            return ResultDto.Failure("Entry not found for hospital");
        }

        if(masterEntry.EntryStatus != EntryStatus.Draft)
        {
            return ResultDto.Failure("You have already submitted. You cannot edit now!");
        }

        foreach (var map in dto.Mapdandas)
        {
            var standard = await _dbContext.HospitalStandards.FindAsync(map.StandardId);
            if(standard is not null)
            {
                standard.IsAvailable = map.IsAvailable;
                standard.Remarks = map.Remarks;
                standard.FilePath = map.FilePath;
                standard.FiscalYear = map.FiscalYear;
                standard.UpdatedAt = DateTime.Now;

            }

        }

        await _dbContext.SaveChangesAsync();
        
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>> GetHospitalStandardForEntry(Guid submissionCode, HospitalStandardQueryParams dto, int healthFacilityId)
    {
        var healthFacility = await _dbContext.HealthFacilities.FindAsync(healthFacilityId);
        if (healthFacility is null)
        {
            return ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>.Failure("Health Facility not found");

        }

        int bedCount = healthFacility.BedCount;

        var mapdandaQuery = _dbContext.Mapdandas.AsQueryable();

        if(dto.AnusuchiId.HasValue) mapdandaQuery = mapdandaQuery.Where(x => x.AnusuchiId == dto.AnusuchiId.Value && x.ParichhedId == null);
        if(dto.ParichhedId.HasValue) mapdandaQuery = mapdandaQuery.Where(x => x.ParichhedId == dto.ParichhedId.Value && x.SubParichhedId == null);
        if(dto.SubParichhedId.HasValue) mapdandaQuery = mapdandaQuery.Where(x => x.SubParichhedId == dto.SubParichhedId.Value);

        var mapdandas = (await mapdandaQuery.ToListAsync())
           .GroupBy(m => m.SubSubParichhed)
           .Select(m => new GroupedSubSubParichhedAndMapdanda
           {
               HasBedCount = m.FirstOrDefault()?.IsAvailableDivided,
               SubSubParixed = m.Key?.Name,
               List = m
               .GroupBy(m => m.Group)
               .Select(m => new GroupedMapdandaByGroupName
               {
                   GroupName = m.Key,
                   GroupedMapdanda = m.Select(m => new GroupedMapdanda
                   {
                       Id = m.Id,
                       Name = m.Name,
                       SerialNumber = m.SerialNumber,
                       IsActive = determineActive(bedCount, m.Is25Active, m.Is50Active, m.Is100Active, m.Is200Active),
                       Value = determineValue(bedCount, m.Value25, m.Value50, m.Value100, m.Value200),
                       Status = m.Status,
                       Parimaad = m.Parimaad,
                       Group = m.Group,
                       IsAvailableDivided = m.IsAvailableDivided,
                   }).ToList()

               }).ToList()
           })
           .ToList();

        static string? determineValue(int bedCount, string? value25, string? value50, string? value100, string? value200)
        {
            if (bedCount == 25) return value25;
            if (bedCount == 50) return value50;
            if (bedCount == 100) return value100;
            return value200;
        }

        static bool determineActive(int bedCount, bool value25, bool value50, bool value100, bool value200)
        {
            if (bedCount == 25) return value25;
            if (bedCount == 50) return value50;
            if (bedCount == 100) return value100;
            return value200;
        }

        return ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>.Success(mapdandas);
    }

}