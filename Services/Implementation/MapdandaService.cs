using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

public class MapdandaService : IMapdandaService
{
    private readonly ApplicationDbContext _dbContext;

    public MapdandaService( ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultDto> Add(MapdandaDto dto)
    {
        //var serialNo = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == dto.AnusuchiId).MaxAsync(x => x.SerialNumber);
        //var maxAnusuchiNo = await _dbContext.Mapdandas.MaxAsync(x => x.AnusuchiId);
        //if (dto.AnusuchiId > (maxAnusuchiNo + 1))
        //{
        //    return ResultDto.Failure($"Anusuchi number should not be greater than {maxAnusuchiNo + 1}");

        //}
        if(string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNumber))
        {
            return ResultDto.Failure("Mapdanda must have name and serial number");    
        }

        var anusuchi = await _dbContext.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("Anusuchi Not Found");
        }

        var exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.AnusuchiId == dto.AnusuchiId);

        if(dto.ParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.ParichhedId == dto.ParichhedId);
        if(dto.SubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubParichhedId == dto.SubParichhedId);
        if(dto.SubSubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubSubParichhedId == dto.SubSubParichhedId);

        if(await exsitingMapdanda.AnyAsync(x => x.SerialNumber == dto.SerialNumber))
        {
            return ResultDto.Failure("Serial Number Already Exist");
        }

        if (await exsitingMapdanda.AnyAsync(x => x.IsAvailableDivided != dto.IsAvailableDivided))
        {
            var exm = await exsitingMapdanda.FirstAsync();
            if(exm.IsAvailableDivided != dto.IsAvailableDivided)
            {
                if(exm.IsAvailableDivided)
                    return ResultDto.Failure("Mapdanda should have bed counts");

                return ResultDto.Failure("Mapdanda should not have bed counts");
            }
        }



        var mapdanda = new Mapdanda()
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Anusuchi = anusuchi,
            Parimaad = dto.Parimaad,
            IsAvailableDivided = dto.IsAvailableDivided
        };

        if (dto.ParichhedId.HasValue)
        {
            var parichhed = await _dbContext.Parichheds.FindAsync(dto.ParichhedId);
            if (parichhed == null)
            {
                return ResultDto.Failure("Parichhed Not Found");
            }
            mapdanda.Parichhed = parichhed;

            if (dto.SubParichhedId.HasValue)
            {
                var subParichhed = await _dbContext.SubParichheds.FindAsync(dto.SubParichhedId);
                if (subParichhed == null)
                {
                    return ResultDto.Failure("Sub Parichhed Not Found");
                }
                
                mapdanda.SubParichhed = subParichhed;

                if (dto.SubSubParichhedId.HasValue)
                {
                    var subSubParichhed = await _dbContext.SubSubParichheds.FindAsync(dto.SubSubParichhedId);
                    if (subSubParichhed == null)
                    {
                        return ResultDto.Failure("Sub Sub Parichhed Not Found");
                    }
                    mapdanda.SubSubParichhed = subSubParichhed;
                }
            }
        }

        if (dto.IsAvailableDivided)
        {
            mapdanda.Is25Active = dto.Is25Active;
            mapdanda.Is50Active = dto.Is50Active;
            mapdanda.Is100Active = dto.Is100Active;
            mapdanda.Is200Active = dto.Is200Active;
        }

        await _dbContext.Mapdandas.AddAsync(mapdanda);
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();

    }

    public async Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto dto)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(mapdandaId);
        if (mapdanda == null)
        {
            return ResultDto.Failure("Mapdanda not found");
        }

        var exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.AnusuchiId == dto.AnusuchiId);

        if (dto.ParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.ParichhedId == dto.ParichhedId);
        if (dto.SubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubParichhedId == dto.SubParichhedId);
        if (dto.SubSubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubSubParichhedId == dto.SubSubParichhedId);

        if (dto.SerialNumber != mapdanda.SerialNumber && await exsitingMapdanda.AnyAsync(x => x.SerialNumber == dto.SerialNumber))
        {
            return ResultDto.Failure("Serial Number Already Exist");
        }

        mapdanda.Name = dto.Name;
        mapdanda.SerialNumber = dto.SerialNumber;
        //mapdanda.AnusuchiId = dto.AnusuchiId;
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<MapdandaDto>>> GetByAnusuchi(int? anusuchiId)
    {

        var mapdandas = _dbContext.Mapdandas.AsQueryable();

        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.Select(x => new MapdandaDto()
        {
            Id = x.Id,
            Name = x.Name,
            SerialNumber = x.SerialNumber,
            AnusuchiId = x.AnusuchiId,
            Status = x.Status,
            Is25Active = x.Is25Active,
            Is50Active = x.Is50Active,
            Is100Active = x.Is100Active,
            Is200Active = x.Is200Active,
            IsAvailableDivided = x.IsAvailableDivided
        }).OrderBy(x=> x.SerialNumber)
        .OrderBy(x=> x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto>>.Success(res);
    }

    public async Task<ResultDto> ToggleStatus(int mapdandaId)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(mapdandaId);
        if (mapdanda is null)
        {
            return ResultDto.Failure("Mapdanda doesnot exists");
        }

        mapdanda.Status = !mapdanda.Status;
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }
}
