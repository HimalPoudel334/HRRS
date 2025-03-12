
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class AnusuchiService(ApplicationDbContext context) : IAnusuchiService
{
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<ResultDto> Create(AnusuchiDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNo))
        {
            return ResultDto.Failure("अनुसूचीमा नाम र क्रम सङ्ख्या हुनु पर्छ।");
        }
        
        Anusuchi anusuchi = new()
        {
            Name = dto.Name,
            DafaNo = dto.DafaNo,
            SerialNo = dto.SerialNo 
        };

        await _dbContext.Anusuchis.AddAsync(anusuchi);
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultDto> Update(int anusuchiId, AnusuchiDto dto)
    {
        var anusuchi = await _dbContext.Anusuchis.FindAsync(anusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("अनुसूची फेला परेन।");
        }
        
        anusuchi.SerialNo = dto.SerialNo;
        anusuchi.Name = dto.Name;
        anusuchi.DafaNo = dto.DafaNo;

        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<AnusuchiDto>>> GetAll()
    {
        var anusuchis = await _dbContext.Anusuchis.Select(x => new AnusuchiDto()
        {
            Id = x.Id,
            Name = x.Name,
            DafaNo = x.DafaNo,
            SerialNo = x.SerialNo
        }).ToListAsync();

        return ResultWithDataDto<List<AnusuchiDto>>.Success(anusuchis);
    }
    public async Task<ResultWithDataDto<List<AnusuchiDto>>> GetAllUserAnusuchi(long userId, Guid? submissionCode)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
            return ResultWithDataDto<List<AnusuchiDto>>.Failure("User not found");

        if (user.UserType != "Hospital")
        {
            var anusuchis = await _dbContext.Anusuchis
                .Select(x => new AnusuchiDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    DafaNo = x.DafaNo,
                    SerialNo = x.SerialNo
                })
                .ToListAsync();
            return ResultWithDataDto<List<AnusuchiDto>>.Success(anusuchis);
        }

        //var standardEntries = await _dbContext.HospitalStandardEntrys
        //    .Where(x => x.MasterStandardEntry.SubmissionCode == submissionCode)
        //    .ToListAsync();

        //if (standardEntries.Count == 0)
        //{
        //    var anusuchis = await _dbContext.Anusuchis
        //        .Where(x => new[] { "17", "१७", "3", "३" }.Contains(x.SerialNo))
        //        .Select(x => new AnusuchiDto
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            DafaNo = x.DafaNo,
        //            SerialNo = x.SerialNo
        //        })
        //        .ToListAsync();
        //    return ResultWithDataDto<List<AnusuchiDto>>.Success(anusuchis);
        //}

        var healthFacility = await _dbContext.HealthFacilities.FindAsync(user.HealthFacilityId);
        if (healthFacility == null)
            return ResultWithDataDto<List<AnusuchiDto>>.Failure("Health Facility not found");

        var masterEntry = await _dbContext.MasterStandardEntries.FindAsync(submissionCode);
        if (masterEntry == null)
            return ResultWithDataDto<List<AnusuchiDto>>.Failure("Master Entry not found");

        var anusuchiMapping = await _dbContext.AnusuchiMappings
            .FirstOrDefaultAsync(x => x.SubmissionTypeId == masterEntry.SubmissionTypeId
                && x.FacilityTypeId == healthFacility.FacilityTypeId && x.BedCountId == healthFacility.BedCountId);
        
        if (anusuchiMapping == null)
            return new ResultWithDataDto<List<AnusuchiDto>>(true, null, "अनुसूची फेला परेन।");

        var anusuchisMapped = await _dbContext.AnusuchiMapdandaTableMappings
            .Include(x => x.Anusuchi)
            .Where(x => x.AnusuchiMapping == anusuchiMapping)
            .Select(x => new AnusuchiDto
            {
                Id = x.Anusuchi.Id,
                Name = x.Anusuchi.Name,
                DafaNo = x.Anusuchi.DafaNo,
                SerialNo = x.Anusuchi.SerialNo
            })
            .ToListAsync();

        return ResultWithDataDto<List<AnusuchiDto>>.Success(anusuchisMapped);
    
    }

    public async Task<ResultWithDataDto<AnusuchiDto>> GetById(int id)
    {
        var anusuchi = await _dbContext.Anusuchis.Include(x => x.Parichheds).FirstOrDefaultAsync(x => x.Id == id);
        if (anusuchi == null)
        {
            return ResultWithDataDto<AnusuchiDto>.Failure("अनुसूची फेला परेन।");
        }

        var dto = new AnusuchiDto()
        {
            Id = anusuchi.Id,
            Name = anusuchi.Name,
            DafaNo = anusuchi.DafaNo,
            SerialNo = anusuchi.SerialNo,
        };

        return ResultWithDataDto<AnusuchiDto>.Success(dto);
        
    }

}