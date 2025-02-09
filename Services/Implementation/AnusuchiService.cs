
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
            return ResultWithDataDto<AnusuchiDto>.Failure("Anusuchi Not Found");
        }

        var dto = new AnusuchiDto()
        {
            Id = anusuchi.Id,
            Name = anusuchi.Name,
            DafaNo = anusuchi.DafaNo,
            SerialNo = anusuchi.SerialNo,
        };

        if (anusuchi.Parichheds == null || anusuchi.Parichheds.Count == 0) {
            dto.Mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == id && x.ParichhedId == null).Select(x => new MapdandaDto1()
            {
                Id = x.Id,
                Name = x.Name,
                AnusuchiId = x.AnusuchiId,
                ParichhedId = x.ParichhedId,
                Is25Active = x.Is25Active,
                Is50Active = x.Is50Active,
                Is100Active = x.Is100Active,
                Is200Active = x.Is200Active,
                Parimaad = x.Parimaad,
                IsAvailableDivided = x.IsAvailableDivided,
                SerialNumber = x.SerialNumber,
                SubParichhedId = x.SubParichhedId,
                SubSubParichhedId = x.SubSubParichhedId
            }).ToListAsync();

            return ResultWithDataDto<AnusuchiDto>.Success(dto);


        }


        return ResultWithDataDto<AnusuchiDto>.Success(dto);
       
    }

    //public async Task<ResultWithDataDto<ResponseDto>> GetAnusuchiMapdanda(int anusuchiId)
    //{
    //    var anusuchi = await _dbContext.Anusuchis.FindAsync();
    //    if (anusuchi == null)
    //    {
    //        return ResultWithDataDto<ResponseDto>.Failure("Anusuchi Not Found");
    //    }
    //    var mapdandas = anusuchi.Mapdandas.Select(x => new MapdandaDto()
    //    {
    //        Id = x.Id,
    //        Name = x.Name,
    //        SerialNumber = x.SerialNumber,
    //        AnusuchiNumber = x.AnusuchiId
    //    }).ToList();
    //    var response = new ResponseDto()
    //    {
    //        Anusuchi = new AnusuchiDto()
    //        {
    //            Id = anusuchi.Id,
    //            Name = anusuchi.Name,
    //            DafaNo = anusuchi.DafaNo,
    //            SerialNo = anusuchi.SerialNo
    //        },
    //        Mapdandas = mapdandas
    //    };
    //    return ResultWithDataDto<ResponseDto>.Success(response);

    //}

}