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
        var serialNo = await _dbContext.Mapdandas.Where(x => x.AnusuchiNumber == dto.AnusuchiNumber).MaxAsync(x => x.AnusuchiNumber);
        var mapdanda = new Mapdanda()
        {
            Name = dto.Name,
            SerialNumber = serialNo,
            AnusuchiNumber = dto.AnusuchiNumber
        };

        await _dbContext.Mapdandas.AddAsync(mapdanda);
        return ResultDto.Success();

    }

    public async Task<ResultWithDataDto<List<MapdandaDto>>> GetByAnusuchi(int anusuchi_id)
    {
        var mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiNumber == anusuchi_id).Select(x => new MapdandaDto()
        {
            Name = x.Name,
            SerialNumber = x.SerialNumber,
            AnusuchiNumber = x.AnusuchiNumber
        }).ToListAsync();

        if (mapdandas == null)
        {
            return ResultWithDataDto<List<MapdandaDto>>.Failure($"No mapdandas found for Anusuchi {anusuchi_id}");
        }

        return ResultWithDataDto<List<MapdandaDto>>.Success(mapdandas);
    }
}
