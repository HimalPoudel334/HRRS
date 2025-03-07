using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.Mapdanda;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

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

        if(masterEntry.EntryStatus != EntryStatus.Draft)
            return ResultDto.Failure("You cannot add or update standards after submission!");


        if (dto.Mapdandas.Any(x => x.EntryId > 0))
        {

            await Up(dto);
            return ResultDto.Success();
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
                IsApproved = item.IsApproved
            });
        }

        var standardEntry = new HospitalStandardEntry
        {
            HospitalStandards = stdrs,
            MasterStandardEntry = masterEntry,
        };

        await _dbContext.HospitalStandardEntrys.AddAsync(standardEntry);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }


    public async Task<ResultWithDataDto<List<MasterStandardEntryDto>>> AdminGetMasterStandardsEntry(int hospitalId)
    {
       
        var res = await _dbContext.MasterStandardEntries.Include(x => x.SubmissionType).Where(x => x.HealthFacilityId  == hospitalId).Select(x => new MasterStandardEntryDto
        {
            EntryStatus = x.EntryStatus,
            HealthFacilityId = x.HealthFacilityId,
            SubmissionCode = x.SubmissionCode,
            SubmissionType = x.SubmissionType.Title
        }).ToListAsync();


        return new ResultWithDataDto<List<MasterStandardEntryDto>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<MasterStandardEntryDto>>> UserGetMasterStandardsEntry(int hospitalId)
    {

        var res = await _dbContext.MasterStandardEntries.Include(x => x.SubmissionType).Where(x => x.HealthFacilityId == hospitalId).Select(x => new MasterStandardEntryDto
        {
            EntryStatus = x.EntryStatus,
            HealthFacilityId = x.HealthFacilityId,
            SubmissionCode = x.SubmissionCode,
            SubmissionType = x.SubmissionType.Title,
            HasNewSubmission = x.IsNewEntry
        }).ToListAsync();


        return new ResultWithDataDto<List<MasterStandardEntryDto>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<HospitalEntryDto>>> GetStandardEntries(Guid submissionCode)
    {
        //var res = await _dbContext.HospitalStandardEntrys
        //    .Where(x => x.MasterStandardEntry.SubmissionCode == submissionCode)
        //    .Select(x => new HospitalEntryDto
        //    {
        //        Id = x.Id,
        //        Remarks = x.Remarks,
        //        Anusuchi = x.HospitalStandards.First().Mapdanda.Anusuchi.Name,
        //        Parichhed = x.HospitalStandards.First().Mapdanda.Parichhed != null ? x.HospitalStandards.First().Mapdanda.Parichhed!.Name : "",
        //        SubParichhed = x.HospitalStandards.First().Mapdanda.SubParichhed != null ? x.HospitalStandards.First().Mapdanda.SubParichhed!.Name : ""
        //    }).ToListAsync();

        //return new ResultWithDataDto<List<HospitalEntryDto>>(true, res, null);

        throw new NotImplementedException();
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
            Remarks = entry.Remarks,
        };

        return new ResultWithDataDto<HospitalEntryDto>(true, dto, null);
    }

    public async Task<ResultWithDataDto<List<HospitalStandardModel>>> AdminGetHospitalStandardForEntry(int entryId)
    {
        var bedCount = (await _dbContext.HospitalStandardEntrys.Include(x => x.MasterStandardEntry).FirstOrDefaultAsync(x => x.Id == entryId))?.MasterStandardEntry.BedCount;

        if (bedCount is null)
            return ResultWithDataDto<List<HospitalStandardModel>>.Failure("Health faciltiy not found");

        /*var standards = await _dbContext.HospitalStandards
            .AsSplitQuery()
            .Where(x => x.StandardEntryId == entryId)
            .GroupBy(m => m.Mapdanda.SubSubParichhed)
            .Select(m => new HospitalStandardModel
            {
                AnusuchiId = m.FirstOrDefault() != null ? m.First().Mapdanda.AnusuchiId: null,
                ParichhedId = m.FirstOrDefault() != null ? m.First().Mapdanda.ParichhedId : null,
                SubParichhedId = m.FirstOrDefault() != null ? m.First().Mapdanda.SubParichhedId : null,
                FormType = m.FirstOrDefault() != null ? m.First().Mapdanda.FormType : FormType.A1,
                HasBedCount = m.FirstOrDefault() != null ? m.First().Mapdanda.IsAvailableDivided : false,
                SubSubParixed = m.Key != null ? m.Key.Name : "",
                List = m
                    .GroupBy(m => m.Mapdanda.SerialNumber)
                    .Select(group => new StandardGroupModel
                    {
                        SerialNumber = group.Key,
                        GroupName = group.First().Mapdanda.Group,
                        GroupedMapdanda = group.Select(item => new MapdandaModel
                        {
                            EntryId = item.Id,
                            Id = item.MapdandaId,
                            Name = item.Mapdanda.Name,
                            SerialNumber = item.Mapdanda.SerialNumber,
                            Parimaad = item.Mapdanda.Parimaad,
                            Group = item.Mapdanda.Group,
                            FilePath = item.FilePath,
                            IsAvailable = item.IsAvailable,
                            IsApproved = item.IsApproved,
                            Remarks = item.Remarks,
                            Value = determineValue(bedCount.Value, item.Mapdanda.FormType, item.Mapdanda.Value25, item.Mapdanda.Value50, item.Mapdanda.Value100, item.Mapdanda.Value200),
                            IsAvailableDivided = item.Mapdanda.IsAvailableDivided,
                        }).ToList()
                    }).ToList()
            })
        .ToListAsync();

        return new ResultWithDataDto<List<HospitalStandardModel>>(true, standards, null);*/
        throw new NotImplementedException();
    }

    public async Task<ResultDto> Update(HospitalStandardDto dto, int id)
    {
        var masterEntry = await _dbContext.MasterStandardEntries.FirstOrDefaultAsync(x => x.SubmissionCode == dto.SubmissionCode && x.HealthFacilityId == id);
        if (masterEntry is null)
        {
            return ResultDto.Failure("Entry not found for hospital");
        }

        if (masterEntry.EntryStatus != EntryStatus.Draft)
        {
            return ResultDto.Failure("You have already submitted. You cannot edit now!");
        }

        bool success = await Up(dto);

        if (success) return ResultDto.Success();

        return ResultDto.Failure("Something Went Wrong");
    }

    public async Task<ResultDto> UpdateStandardDecisionByAdmin(List<StandardApprovalDto> dto)
    {

        var id = dto.First().EntryId;

        var mas = await _dbContext.HospitalStandards
            .Include(x => x.StandardEntry)
            .ThenInclude(x => x.MasterStandardEntry)
            .FirstOrDefaultAsync(x => x.Id == id);

        if(mas is null) return ResultDto.Failure("Entry not found");

        if (mas.StandardEntry.MasterStandardEntry.EntryStatus != EntryStatus.Pending)
            return ResultDto.Failure("You have already submitted. You cannot edit now!");

        foreach (var map in dto)
        {
            var standard = await _dbContext.HospitalStandards.FindAsync(map.EntryId);
            if (standard is not null)
            {
                standard.IsApproved = map.IsApproved;
                standard.Remarks = map.Remarks;
                standard.UpdatedAt = DateTime.Now;
            }
        }

        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    private async Task<bool> Up(HospitalStandardDto dto)
    {
        foreach (var map in dto.Mapdandas)
        {
            var standard = await _dbContext.HospitalStandards.FindAsync(map.EntryId);
            if (standard is not null)
            {
                standard.IsAvailable = map.IsAvailable;
                standard.Remarks = map.Remarks;
                standard.FilePath = map.FilePath;
                standard.FiscalYear = map.FiscalYear;
                standard.UpdatedAt = DateTime.Now;

            }
        }
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ResultWithDataDto<HospitalStandardTableDto>> GetHospitalStandardForEntry(Guid submissionCode, HospitalStandardQueryParams dto, int healthFacilityId)
    {
        var healthFacility = await _dbContext.HealthFacilities.FindAsync(healthFacilityId);
        if (healthFacility is null) return ResultWithDataDto<HospitalStandardTableDto>.Failure("Health Facility not found");

        int bedCount = healthFacility.BedCount;
        
        var existing = _dbContext.HospitalStandards.Include(x => x.Mapdanda).Where(x => x.StandardEntry.MasterStandardEntry.SubmissionCode == submissionCode);
        if (dto.AnusuchiId.HasValue) existing = existing.Where(x => x.Mapdanda.MapdandaTable.AnusuchiId == dto.AnusuchiId.Value && x.Mapdanda.MapdandaTable.ParichhedId == null);
        if (dto.ParichhedId.HasValue) existing = existing.Where(x => x.Mapdanda.MapdandaTable.ParichhedId == dto.ParichhedId.Value && x.Mapdanda.MapdandaTable.SubParichhedId == null);
        if (dto.SubParichhedId.HasValue) existing = existing.Where(x => x.Mapdanda.MapdandaTable.SubParichhedId == dto.SubParichhedId.Value);

        if(existing.Any())
        {
            var mapdandaTable = (await existing.FirstAsync()).Mapdanda.MapdandaTable;
            
            var res = await existing
                .OrderBy(x => x.Mapdanda.OrderNo)
                .Select(m => new GroupedMapdanda
                {
                    Id = m.MapdandaId,
                    EntryId = m.Id,
                    Name = m.Mapdanda.Name,
                    SerialNumber = m.Mapdanda.SerialNumber,
                    IsAvailable = m.IsAvailable,
                    FilePath = m.FilePath,
                    IsActive = determineActive(bedCount, m.Mapdanda.Is25Active, m.Mapdanda.Is50Active, m.Mapdanda.Is100Active, m.Mapdanda.Is200Active),
                    Status = m.Status,
                    Remarks = m.Remarks,
                    IsApproved = m.IsApproved,
                    IsGroup = m.Mapdanda.IsGroup,
                    IsSubGroup = m.Mapdanda.IsSubGroup,
                    IsSection = m.Mapdanda.IsSection,
                    HasGroup = m.Mapdanda.HasGroup,
                    Value = determineValue(bedCount, m.Mapdanda.FormType, m.Mapdanda.Value25, m.Mapdanda.Value50, m.Mapdanda.Value100, m.Mapdanda.Value200),

                })
                .ToListAsync();

            var dtos = new HospitalStandardTableDto()
            {
                Id = mapdandaTable.Id,
                FormType = mapdandaTable.FormType,
                TableName = mapdandaTable.TableName,
                Description = mapdandaTable.Description,
                Note = mapdandaTable.Note,
                Mapdandas = res

            };

            return ResultWithDataDto<HospitalStandardTableDto>.Success(dtos);
        }

        var mapdandaTableQuery = _dbContext.MapdandaTables.AsQueryable();

        if (dto.AnusuchiId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.AnusuchiId == dto.AnusuchiId);
        if (dto.ParichhedId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.ParichhedId == dto.ParichhedId);
        if (dto.SubParichhedId.HasValue) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.SubParichhedId == dto.SubParichhedId);

        if (bedCount <= 25) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.Mapdandas.Any(y => y.Is25Active));
        if (bedCount <= 50) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.Mapdandas.Any(y => y.Is50Active));
        if (bedCount <= 100) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.Mapdandas.Any(y => y.Is100Active));
        if (bedCount <= 200) mapdandaTableQuery = mapdandaTableQuery.Where(x => x.Mapdandas.Any(y => y.Is200Active));


        var mapdandaTables = await mapdandaTableQuery
            .Select(x => new HospitalStandardTableDto
            {
                Id = x.Id,
                TableName = x.TableName,
                AnusuchiId = x.AnusuchiId,
                ParichhedId = x.ParichhedId,
                SubParichhedId = x.SubParichhedId,
                Description = x.Description,
                Note = x.Note,
                FormType = x.FormType,
                Mapdandas = x.Mapdandas
                    .OrderBy(x => x.OrderNo)
                    .Select(m => new GroupedMapdanda
                    {
                        Id = m.Id,
                        Name = m.Name,
                        SerialNumber = m.SerialNumber,
                        IsActive = determineActive(bedCount, m.Is25Active, m.Is50Active, m.Is100Active, m.Is200Active),
                        Status = m.Status,
                        IsGroup = m.IsGroup,
                        IsSubGroup = m.IsSubGroup,
                        IsSection = m.IsSection,
                        HasGroup = m.HasGroup,
                        Value = determineValue(bedCount, m.FormType, m.Value25, m.Value50, m.Value100, m.Value200),
                    }).ToList()
            }).FirstAsync();

        return ResultWithDataDto<HospitalStandardTableDto>.Success(mapdandaTables);
    }



    private static string? determineValue(int bedCount, FormType formType, string? value25, string? value50, string? value100, string? value200)
    {
        if (formType == FormType.A1 && bedCount >= 200) return value200;
        if (formType == FormType.A1 && bedCount >= 100) return value100;
        if (formType == FormType.A1 && bedCount >= 50) return value50;
        if (formType == FormType.A4 && bedCount >= 200) return value200;
        if (formType == FormType.A4 && bedCount >= 100) return value100;
        if (formType == FormType.A4 && bedCount >= 50) return value50;
        if (formType == FormType.A4 && bedCount >= 25) return value25;
        if (formType == FormType.A5P3 && bedCount >= 100) return value100;
        if (formType == FormType.A5P3 && bedCount >= 50) return value50;
        if (formType == FormType.A5P3 && bedCount >= 25) return value25;
        return value25;
    }

    private static bool determineActive(int bedCount, bool value25, bool value50, bool value100, bool value200)
    {
        if (bedCount <= 25) return value25;
        if (bedCount <= 50) return value50;
        if (bedCount <= 100) return value100;
        return value200;
    }

}