using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
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

    public async Task<ResultDto> AddNewMapdanda(MapdandaDto dto)
    {
        var mapdanda = new Mapdanda()
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            AnusuchiId = dto.AnusuchiId,
            ParichhedId = dto.ParichhedId,
            Parichhed = new Parichhed()
            {
                ParichhedName = dto.Parichhed?.ParichhedName,
                SubParichheds = dto.Parichhed?.SubParichheds?.Select(x => new Parichhed()
                {
                    ParichhedName = x.ParichhedName,
                    AnusuchiId = dto.AnusuchiId.ToString()

                }).ToList(),
                AnusuchiId = dto.Parichhed?.AnusuchiId
            },
            SubParichhedId = dto.SubParichhedId,
            SubMapdandas = dto.SubMapdandas?.Select(x => new Mapdanda()
            {
                Name = x.Name,
                SerialNumber = x.SerialNumber,
                AnusuchiId = x.AnusuchiId,
                ParichhedId = x.ParichhedId,
                SubParichhedId = x.SubParichhedId
            }).ToList()

        };

        await _dbContext.AddAsync(mapdanda);
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }

}
