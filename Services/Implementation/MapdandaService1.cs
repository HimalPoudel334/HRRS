using HRRS.Dto;
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

    public async Task<ResultDto> Add(MapdandaDto1 dto)
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

    public async Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto1 dto)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(mapdandaId);
        if (mapdanda == null)
        {
            return ResultDto.Failure("Mapdanda not found");
        }

        mapdanda.Name = dto.Name;
        mapdanda.Parimaad = dto.Parimaad;

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

    public async Task<ResultWithDataDto<List<MapdandaDto1>>> GetByAnusuchi(int? anusuchiId)
    {

        var mapdandas = _dbContext.Mapdandas.AsQueryable();

        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.Select(mapdanda => new MapdandaDto1()
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
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto1>>.Success(res);
    }

    public async Task<ResultWithDataDto<MapdandaDto1>> GetById(int id)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(id);
        if (mapdanda == null)
        {
            return ResultWithDataDto<MapdandaDto1>.Failure("Mapdanda Not Found");
        }
        var dto = new MapdandaDto1()
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
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad
        };
        return ResultWithDataDto<MapdandaDto1>.Success(dto);
    }

    public async Task<ResultWithDataDto<List<MapdandaDto1>>> GetByParichhed(int parichhedId, int? anusuchiId)
    {

        var mapdandas = _dbContext.Mapdandas.Where(x => x.ParichhedId == parichhedId).AsQueryable();

        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.Select(mapdanda => new MapdandaDto1()
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
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto1>>.Success(res);
    }

    public async Task<ResultDto> AddSubMapdanda(SubMapdandaDto dto)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(dto.MapdandaId);
        if (mapdanda == null)
        {
            return ResultDto.Failure("Mapdanda Not Found");
        }
        var subMapdanda = new SubMapdanda()
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Mapdanda = mapdanda,
            Parimaad = dto.Parimaad
        };
        await _dbContext.SubMapdandas.AddAsync(subMapdanda);
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultDto> UpdateSubMapdanda(int subMapdandaId, SubMapdandaDto dto)
    {
        var subMapdanda = await _dbContext.SubMapdandas.FindAsync(subMapdandaId);
        if (subMapdanda == null)
        {
            return ResultDto.Failure("Sub Mapdanda Not Found");
        }
        var mapdanda = await _dbContext.Mapdandas.FindAsync(dto.MapdandaId);
        if (mapdanda == null)
        {
            return ResultDto.Failure("Mapdanda Not Found");
        }
        subMapdanda.Name = dto.Name;
        subMapdanda.SerialNumber = dto.SerialNumber;
        subMapdanda.Mapdanda = mapdanda;
        subMapdanda.Parimaad = dto.Parimaad;
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<SubMapdandaDto>>> GetSubMapdandaByMapdanda(int mapdandaId)
    {
        var subMapdandas = await _dbContext.SubMapdandas.Where(x => x.MapdandaId == mapdandaId).Select(x => new SubMapdandaDto()
        {
            Id = x.Id,
            Name = x.Name,
            SerialNumber = x.SerialNumber,
            MapdandaId = x.MapdandaId,
            Parimaad = x.Parimaad
        }).ToListAsync();
        return ResultWithDataDto<List<SubMapdandaDto>>.Success(subMapdandas);
    }

    public async Task<ResultWithDataDto<SubMapdandaDto>> GetSubMapdandaById(int id)
    {
        var subMapdanda = await _dbContext.SubMapdandas.FindAsync(id);
        if (subMapdanda == null)
        {
            return ResultWithDataDto<SubMapdandaDto>.Failure("Sub Mapdanda Not Found");
        }
        var dto = new SubMapdandaDto()
        {
            Id = subMapdanda.Id,
            Name = subMapdanda.Name,
            SerialNumber = subMapdanda.SerialNumber,
            MapdandaId = subMapdanda.MapdandaId,
            Parimaad = subMapdanda.Parimaad
        };
        return ResultWithDataDto<SubMapdandaDto>.Success(dto);
    }

    public async Task<ResultWithDataDto<List<MapdandaDto1>>> GetBySubParichhed(int subParichhedId, int? parichhedId, int? anusuchiId)
    {
        var mapdandas = _dbContext.Mapdandas.Where(x => x.SubParichhedId == subParichhedId).AsQueryable();
        if(parichhedId != null)
        {
            mapdandas = mapdandas.Where(x => x.ParichhedId == parichhedId);
        }
        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId);
        }

        var res = await mapdandas.Select(mapdanda => new MapdandaDto1()
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
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad,
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto1>>.Success(res);

    }

    public async Task<ResultWithDataDto<List<MapdandaDto1>>> GetBySubSubParichhed(int subSubParichhedId, int? subParichhedId, int? parichhedId, int? anusuchiId)
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

        var res = await mapdandas.Select(mapdanda => new MapdandaDto1()
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
            IsAvailableDivided = mapdanda.IsAvailableDivided,
            Parimaad = mapdanda.Parimaad
        }).OrderBy(x => x.SerialNumber)
        .OrderBy(x => x.AnusuchiId)
        .ToListAsync();

        return ResultWithDataDto<List<MapdandaDto1>>.Success(res);
    }

    //public async Task<ResultWithDataDto<List<a>>> GetMapdandaGroupByParichhedId(int id)
    //{
        
    //}
}
