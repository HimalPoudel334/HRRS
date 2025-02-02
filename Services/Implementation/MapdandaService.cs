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
        

        return ResultDto.Success();
    }

}
