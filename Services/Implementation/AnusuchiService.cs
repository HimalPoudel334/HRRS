
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
        //if(await _dbContext.Anusuchis.AnyAsync(x => x.SerialNo == dto.SerialNo))
        //{
        //    return ResultDto.Failure("Serial Number already exists");
        //}
        Anusuchi anusuchi = new()
        {
            Name = dto.Name,
            DafaNo = dto.DafaNo,
            SerialNo = dto.SerialNo //await _dbContext.Anusuchis.MaxAsync(x => x.SerialNo) + 1
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
        
        //if (dto.SerialNo != anusuchi.SerialNo && await _dbContext.Anusuchis.AnyAsync(x => x.SerialNo == dto.SerialNo)) 
        //{
        //        return ResultDto.Failure("Serial Number already taken");
        //}
        anusuchi.SerialNo = dto.SerialNo;
        anusuchi.Name = dto.Name;
        anusuchi.DafaNo = dto.DafaNo;

        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<AnusuchiDto>>> GetAll()
    {
        var anusuchis = await _dbContext.Anusuchis.Include(x => x.Parichheds).Select(x => new AnusuchiDto()
        {
            Id = x.Id,
            Name = x.Name,
            DafaNo = x.DafaNo,
            SerialNo = x.SerialNo,
            Parichheds = x.Parichheds.Select(p => new ParichhedDto()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList()
        }).ToListAsync();
        return ResultWithDataDto<List<AnusuchiDto>>.Success(anusuchis);
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