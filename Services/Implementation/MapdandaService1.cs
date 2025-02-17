using HRRS.Dto;
using HRRS.Dto.Mapdanda1;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class MapdandaService1 : IMapdandaService1
{
    private readonly ApplicationDbContext _dbContext;

    public MapdandaService1(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultDto> Add(MapdandaDto dto)
    {
        var serialNo = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == dto.AnusuchiId).MaxAsync(x => x.SerialNumber);
        var maxAnusuchiNo = await _dbContext.Mapdandas.MaxAsync(x => x.AnusuchiId);
        if (dto.AnusuchiId > (maxAnusuchiNo + 1))
        {
            return ResultDto.Failure($"Anusuchi number should not be greater than {maxAnusuchiNo + 1}");

        }
        var anusuchi = await _dbContext.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("Anusuchi Not Found");
        }

        var mapdanda = new Mapdanda()
        {
            Name = dto.Name,
            SerialNumber = serialNo + 1,
            Anusuchi = anusuchi,
            Parimaad = dto.Parimaad,
            Group = dto.Group,
        };

        if (dto.ParichhedId.HasValue)
        {
            var parichhed = await _dbContext.Parichheds.FindAsync(dto.ParichhedId);
            if (parichhed == null)
            {
                return ResultDto.Failure("Parichhed Not Found");
            }
            mapdanda.Parichhed = parichhed;
        }

        if (dto.SubParichhedId.HasValue)
        {
            var subParichhed = await _dbContext.SubParichheds.FindAsync(dto.SubParichhedId);
            if (subParichhed == null)
            {
                return ResultDto.Failure("Sub Parichhed Not Found");
            }
            mapdanda.SubParichhed = subParichhed;
        }

        if (dto.SubSubParichhedId.HasValue)
        {
            var subSubParichhed = await _dbContext.SubSubParichheds.FindAsync(dto.SubSubParichhedId);
            if (subSubParichhed == null)
            {
                return ResultDto.Failure("Sub Sub Parichhed Not Found");
            }
            mapdanda.SubSubParichhed = subSubParichhed;
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

        mapdanda.Name = dto.Name;
        mapdanda.Parimaad = dto.Parimaad;
        mapdanda.Is25Active = dto.Is25Active;
        mapdanda.Is100Active = dto.Is100Active;
        mapdanda.Is50Active = dto.Is50Active;
        mapdanda.Is200Active = dto.Is200Active;
        mapdanda.Group = dto.Group;


        if (mapdanda.SerialNumber != dto.SerialNumber )

        if (dto.ParichhedId.HasValue)
        {
            var parichhed = await _dbContext.Parichheds.FindAsync(dto.ParichhedId);
            if (parichhed == null)
            {
                return ResultDto.Failure("Parichhed Not Found");
            }
            mapdanda.Parichhed = parichhed;
        }

        if (dto.SubParichhedId.HasValue)
        {
            var subParichhed = await _dbContext.SubParichheds.FindAsync(dto.SubParichhedId);
            if (subParichhed == null)
            {
                return ResultDto.Failure("Sub Parichhed Not Found");
            }
            mapdanda.SubParichhed = subParichhed;
        }

        if (dto.SubSubParichhedId.HasValue)
        {
            var subSubParichhed = await _dbContext.SubSubParichheds.FindAsync(dto.SubSubParichhedId);
            if (subSubParichhed == null)
            {
                return ResultDto.Failure("Sub Sub Parichhed Not Found");
            }
            mapdanda.SubSubParichhed = subSubParichhed;
        }

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
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId && x.Parichhed == null && x.SubParichhed == null && x.SubSubParichhed == null);
        }

        var res = await mapdandas.Select(mapdanda => new MapdandaDto()
        {
            Id = mapdanda.Id,
            Name = mapdanda.Name,
            SerialNumber = mapdanda.SerialNumber,
            AnusuchiId = mapdanda.AnusuchiId,
            ParichhedId = mapdanda.ParichhedId,
            SubParichhedId = mapdanda.SubParichhedId,
            SubSubParichhedId = mapdanda.SubSubParichhedId,
            Is100Active = mapdanda.Is100Active,
            Is200Active = mapdanda.Is200Active,
            Is50Active = mapdanda.Is50Active,
            Is25Active = mapdanda.Is25Active,
            Value25 = mapdanda.Value25,
            Value50 = mapdanda.Value50,
            Value100 = mapdanda.Value100,
            Value200 = mapdanda.Value200,
            Status = mapdanda.Status,
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad,
            Group = mapdanda.Group,
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto>>.Success(res);
    }

    public async Task<ResultWithDataDto<MapdandaDto>> GetById(int id)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(id);
        if (mapdanda == null)
        {
            return ResultWithDataDto<MapdandaDto>.Failure("Mapdanda Not Found");
        }
        var dto = new MapdandaDto()
        {
            Id = mapdanda.Id,
            Name = mapdanda.Name,
            SerialNumber = mapdanda.SerialNumber,
            AnusuchiId = mapdanda.AnusuchiId,
            ParichhedId = mapdanda.ParichhedId,
            SubParichhedId = mapdanda.SubParichhedId,
            SubSubParichhedId = mapdanda.SubSubParichhedId,
            Is100Active = mapdanda.Is100Active,
            Is200Active = mapdanda.Is200Active,
            Is50Active = mapdanda.Is50Active,
            Is25Active = mapdanda.Is25Active,
            Value25 = mapdanda.Value25,
            Value50 = mapdanda.Value50,
            Value100 = mapdanda.Value100,
            Value200 = mapdanda.Value200,
            Status = mapdanda.Status,
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad,
            Group = mapdanda.Group,
        };
        return ResultWithDataDto<MapdandaDto>.Success(dto);
    }

    public async Task<ResultWithDataDto<List<GroupedMapdandaByGroupName>>> GetByParichhed(int parichhedId, int? anusuchiId)
    {

        var mapdandas = _dbContext.Mapdandas.Where(x => x.ParichhedId == parichhedId && x.SubParichhed == null && x.SubSubParichhed == null).AsQueryable();

        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.GroupBy(m => new {m.IsAvailableDivided, m.Group})
            .Select(m => new GroupedMapdandaByGroupName
            {
                HasBedCount = m.Key.IsAvailableDivided,
                GroupName = m.Key.Group,
                GroupedMapdanda = m.Select(m => new GroupedMapdanda
                {
                    Id = m.Id,  
                    Name = m.Name,
                    SerialNumber = m.SerialNumber,
                    Is100Active = m.Is100Active,
                    Is200Active = m.Is200Active,
                    Is50Active = m.Is50Active,
                    Is25Active = m.Is25Active,
                    Value25 = m.Value25,
                    Value50 = m.Value50,
                    Value100 = m.Value100,
                    Value200 = m.Value200,
                    Status = m.Status,
                    Parimaad = m.Parimaad,
                    Group = m.Group,
                    IsAvailableDivided = m.IsAvailableDivided,
                }).ToList()

            }).ToListAsync();

        return ResultWithDataDto<List<GroupedMapdandaByGroupName>>.Success(res);
    }

    public async Task<ResultWithDataDto<List<MapdandaDto>>> GetBySubParichhed(int subParichhedId, int? parichhedId, int? anusuchiId)
    {
        var mapdandas = _dbContext.Mapdandas.Where(x => x.SubParichhedId == subParichhedId && x.SubSubParichhed == null).AsQueryable();
        if(parichhedId != null)
        {
            mapdandas = mapdandas.Where(x => x.ParichhedId == parichhedId);
        }
        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.Select(mapdanda => new MapdandaDto()
        {
            Id = mapdanda.Id,
            Name = mapdanda.Name,
            SerialNumber = mapdanda.SerialNumber,
            AnusuchiId = mapdanda.AnusuchiId,
            ParichhedId = mapdanda.ParichhedId,
            SubParichhedId = mapdanda.SubParichhedId,
            SubSubParichhedId = mapdanda.SubSubParichhedId,
            Is100Active = mapdanda.Is100Active,
            Is200Active = mapdanda.Is200Active,
            Is50Active = mapdanda.Is50Active,
            Is25Active = mapdanda.Is25Active,
            Value25 = mapdanda.Value25,
            Value50 = mapdanda.Value50,
            Value100 = mapdanda.Value100,
            Value200 = mapdanda.Value200,
            Status = mapdanda.Status,
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad,
            Group = mapdanda.Group,
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto>>.Success(res);

    }

    public async Task<ResultWithDataDto<List<MapdandaDto>>> GetBySubSubParichhed(int subSubParichhedId, int? subParichhedId, int? parichhedId, int? anusuchiId)
    {
        var mapdandas = _dbContext.Mapdandas.Where(x => x.SubSubParichhedId == subSubParichhedId).AsQueryable();
        if (subParichhedId != null)
        {
            mapdandas = mapdandas.Where(x => x.SubParichhedId == subParichhedId);
        }
        if (parichhedId != null)
        {
            mapdandas = mapdandas.Where(x => x.ParichhedId == parichhedId);
        }
        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.Select(mapdanda => new MapdandaDto()
        {
            Id = mapdanda.Id,
            Name = mapdanda.Name,
            SerialNumber = mapdanda.SerialNumber,
            AnusuchiId = mapdanda.AnusuchiId,
            ParichhedId = mapdanda.ParichhedId,
            SubParichhedId = mapdanda.SubParichhedId,
            SubSubParichhedId = mapdanda.SubSubParichhedId,
            Is100Active = mapdanda.Is100Active,
            Is200Active = mapdanda.Is200Active,
            Is50Active = mapdanda.Is50Active,
            Is25Active = mapdanda.Is25Active,
            Value25 = mapdanda.Value25,
            Value50 = mapdanda.Value50,
            Value100 = mapdanda.Value100,
            Value200 = mapdanda.Value200,
            Status = mapdanda.Status,
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad,
            Group = mapdanda.Group,
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
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

    //public async Task<ResultWithDataDto<List<a>>> GetMapdandaGroupByParichhedId(int id)
    //{

    //}
}
