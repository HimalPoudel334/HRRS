
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class AnusuchiService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<ResultDto> Create(AnusuchiDto dto)
    {
        Anusuchi anusuchi = new()
        {
            Name = dto.Name,
            DafaNo = dto.DafaNo,
            SerialNo = await _dbContext.Anusuchis.MaxAsync(x => x.SerialNo) + 1
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
            return ResultDto.Failure("Anusuchi Not Found");
        }

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

    public async Task<ResultWithDataDto<AnusuchiDto>> GetById(int id)
    {
        var anusuchi = await _dbContext.Anusuchis.FindAsync(id);
        if (anusuchi == null)
        {
            return ResultWithDataDto<AnusuchiDto>.Failure("Anusuchi Not Found");
        }

        var dto = new AnusuchiDto()
        {
            Id = anusuchi.Id,
            Name = anusuchi.Name,
            DafaNo = anusuchi.DafaNo,
            SerialNo = anusuchi.SerialNo
        };

        return ResultWithDataDto<AnusuchiDto>.Success(dto);
       
    }

}