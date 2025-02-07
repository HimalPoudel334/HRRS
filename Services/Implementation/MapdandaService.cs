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
        var serialNo = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == dto.AnusuchiNumber).MaxAsync(x => x.SerialNumber);
        var maxAnusuchiNo = await _dbContext.Mapdandas.MaxAsync(x => x.AnusuchiId);
        if (dto.AnusuchiNumber > (maxAnusuchiNo + 1))
        {
            return ResultDto.Failure($"Anusuchi number should not be greater than {maxAnusuchiNo + 1}");

        }
        var mapdanda = new Mapdanda()
        {
            Name = dto.Name,
            SerialNumber = serialNo + 1,
            AnusuchiId = dto.AnusuchiNumber
        };

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

        mapdanda.Name = dto.Name;
        //mapdanda.SerialNumber = dto.SerialNumber;
        //mapdanda.AnusuchiNumber = dto.AnusuchiNumber;
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
            AnusuchiNumber = x.AnusuchiId
        }).OrderBy(x=> x.SerialNumber)
        .OrderBy(x=> x.AnusuchiNumber)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto>>.Success(res);
    }
}
