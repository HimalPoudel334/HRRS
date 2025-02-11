
using HRRS.Dto;
using HRRS.Dto.Parichhed;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class ParichhedService : IParichhedService
{
    private readonly ApplicationDbContext _context;

    public ParichhedService(ApplicationDbContext context) => _context = context;

    public async Task<ResultWithDataDto<ParichhedDto>> Create(ParichhedDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNo))
        {
            return ResultWithDataDto<ParichhedDto>.Failure("Parichhed must have name and serial number");
        }

        var anusuchi = await _context.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultWithDataDto<ParichhedDto>.Failure("Anusuchi Not Found");
        }

        if (await _context.Parichheds.AnyAsync(x => x.SerialNo == dto.SerialNo)) 
        {
            return ResultWithDataDto<ParichhedDto>.Failure("Serial number already taken");

        }

        var parichhed = new Parichhed()
        {
            Anusuchi = anusuchi,
            Name = dto.Name,
            SerialNo = dto.SerialNo,

        };

        await _context.Parichheds.AddAsync(parichhed);
        await _context.SaveChangesAsync();

        dto.Id = parichhed.Id;

        return ResultWithDataDto<ParichhedDto>.Success(dto);
    }

    public async Task<ResultDto> Update(int parichhedId, ParichhedDto dto)
    {
        var parichhed = await _context.Parichheds.FindAsync(parichhedId);
        if (parichhed == null)
        {
            return ResultDto.Failure("Parichhed not found");
        }

        var anusuchi = await _context.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("Anusuchi Not Found");
        }

        if(parichhed.SerialNo != dto.SerialNo && await _context.Parichheds.AnyAsync(x => x.SerialNo == dto.SerialNo))
        {
            return ResultDto.Failure("Serial number already taken");

        }

        parichhed.Name = dto.Name;
        parichhed.Anusuchi = anusuchi;
        parichhed.SerialNo = dto.SerialNo;

        await _context.SaveChangesAsync();

        return ResultDto.Success();

    }

    public async Task<ResultWithDataDto<List<ParichhedDto>>> GetAllParichhed(int? anusuchiId)
    {
        var parichheds = _context.Parichheds.Select(x => new ParichhedDto()
        {
            Id = x.Id,
            AnusuchiId = x.AnusuchiId,
            Name = x.Name,
            SerialNo = x.SerialNo,
        });

        if (anusuchiId is not null)
            parichheds = parichheds.Where(x => x.AnusuchiId == anusuchiId);

        var res = await parichheds.ToListAsync();

        return ResultWithDataDto<List<ParichhedDto>>.Success(res);
    }

    public async Task<ResultWithDataDto<ParichhedDto>> GetParichhedById(int id)
    {
        var parichhed = await _context.Parichheds.FindAsync(id);
        if (parichhed == null)
        {
            return ResultWithDataDto<ParichhedDto>.Failure("Parichhed not found");
        }

        var parichhedDto = new ParichhedDto()
        {
            Id = parichhed.Id,
            AnusuchiId = parichhed.AnusuchiId,
            Name = parichhed.Name,
            SerialNo = parichhed.SerialNo,
        };

        return ResultWithDataDto<ParichhedDto>.Success(parichhedDto);
    }

    public async Task<ResultWithDataDto<List<ParichhedDto>>> GetParichhedByAnusuchi(int id)
    {
        var parichheds = await _context.Parichheds.Where(x => x.AnusuchiId == id).Select(x => new ParichhedDto()
        {
            Id = x.Id,
            AnusuchiId = x.AnusuchiId,
            Name = x.Name,
            SerialNo = x.SerialNo,
        }).ToListAsync();

        return ResultWithDataDto<List<ParichhedDto>>.Success(parichheds);

    }

    public async Task<ResultWithDataDto<SubParichhedDto>> CreateSubParichhed(SubParichhedDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNo))
        {
            return ResultWithDataDto<SubParichhedDto>.Failure("Parichhed must have name and serial number");
        }

        var parichhed = await _context.Parichheds.FindAsync(dto.ParichhedId);
        if (parichhed == null)
        {
            return ResultWithDataDto<SubParichhedDto>.Failure("Parichhed Not Found");
        }

        var subParichhed = new SubParichhed()
        {
            Parichhed = parichhed,
            Name = dto.Name,
            SerialNo = dto.SerialNo,

        };

        await _context.SubParichheds.AddAsync(subParichhed);
        await _context.SaveChangesAsync();

        dto.Id = subParichhed.Id;

        return ResultWithDataDto<SubParichhedDto>.Success(dto);
    }

    public async Task<ResultDto> UpdateSubParichhed(int subParichhedId, SubParichhedDto  dto)
    {
        var subParichhed = await _context.SubParichheds.FindAsync(subParichhedId);
        if (subParichhed == null)
        {
            return ResultDto.Failure("Parichhed Not Found");
        }

        var parichhed = await _context.Parichheds.FindAsync(dto.ParichhedId);
        if (parichhed == null)
        {
            return ResultDto.Failure("Parichhed Not Found");
        }

        subParichhed.Name = dto.Name;
        subParichhed.SerialNo = dto.SerialNo;
        subParichhed.Parichhed = parichhed;

        await _context.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<SubParichhedDto>>> GetSubParichhedsByParichhed(int parichhedId)
    {
        var subParichhedsDto = await _context.SubParichheds.Where(x => x.ParichhedId == parichhedId).Select(x => new SubParichhedDto()
        {
            Id = x.Id,
            ParichhedId = x.ParichhedId,
            Name = x.Name,
            SerialNo = x.SerialNo,
        }).ToListAsync();

        return ResultWithDataDto<List<SubParichhedDto>>.Success(subParichhedsDto);

    }

    public async Task<ResultWithDataDto<List<SubParichhedDto>>> GetAllSubParichheds()
    {
        var subParichhedsDto = await _context.SubParichheds.Select(x => new SubParichhedDto()
        {
            Id = x.Id,
            ParichhedId = x.ParichhedId,
            Name = x.Name,
            SerialNo = x.SerialNo,
        }).ToListAsync();

        return ResultWithDataDto<List<SubParichhedDto>>.Success(subParichhedsDto);

    }

    public async Task<ResultWithDataDto<SubParichhedDto>> GetSubParichhedById(int id)
    {
        var subParichhed = await _context.SubParichheds.FindAsync(id);
        if (subParichhed == null)
        {
            return ResultWithDataDto<SubParichhedDto>.Failure("Sub Parichhed not found");
        }
        var subParichhedDto = new SubParichhedDto()
        {
            Id = subParichhed.Id,
            ParichhedId = subParichhed.ParichhedId,
            Name = subParichhed.Name,
            SerialNo = subParichhed.SerialNo,
        };
        return ResultWithDataDto<SubParichhedDto>.Success(subParichhedDto);
    }

    public async Task<ResultWithDataDto<SubSubParichhedDto>> CreateSubSubParichhed(SubSubParichhedDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNo))
        {
            return ResultWithDataDto<SubSubParichhedDto>.Failure("Parichhed must have name and serial number");
        }

        var subParichhed = await _context.SubParichheds.FindAsync(dto.SubParichhedId);
        if (subParichhed == null)
        {
            return ResultWithDataDto<SubSubParichhedDto>.Failure("Parichhed Not Found");
        }
        if(await _context.SubSubParichheds.AnyAsync(x => x.SerialNo == dto.SerialNo))
        {
            return ResultWithDataDto<SubSubParichhedDto>.Failure("Serial number already taken");
        }
        var subSubParichhed = new SubSubParichhed()
        {
            SubParichhed = subParichhed,
            Name = dto.Name,
            SerialNo = dto.SerialNo,

        };

        await _context.SubSubParichheds.AddAsync(subSubParichhed);
        await _context.SaveChangesAsync();

        dto.Id = subSubParichhed.Id;
        return ResultWithDataDto<SubSubParichhedDto>.Success(dto);
    }

    public async Task<ResultDto> UpdateSubSubParichhed(int subSubParichhedId, SubSubParichhedDto dto)
    {
        var subSubParichhed = await _context.SubSubParichheds.FindAsync(subSubParichhedId);
        if (subSubParichhed == null)
        {
            return ResultDto.Failure("Parichhed Not Found");
        }

        var subParichhed = await _context.SubParichheds.FindAsync(dto.SubParichhedId);
        if (subParichhed == null)
        {
            return ResultDto.Failure("Parichhed Not Found");
        }

        if (subSubParichhed.SerialNo != dto.SerialNo && await _context.SubSubParichheds.AnyAsync(x => x.SerialNo == dto.SerialNo))
        {
            return ResultDto.Failure("Serial number already taken");
        }

        subSubParichhed.Name = dto.Name;
        subSubParichhed.SerialNo = dto.SerialNo;
        subSubParichhed.SubParichhed = subParichhed;

        await _context.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<SubSubParichhedDto>>> GetSubSubParichhedsBySubParichhed(int subParichhedId)
    {
        var subSubParichhedsDto = await _context.SubSubParichheds.Where(x => x.SubParichhedId == subParichhedId).Select(x => new SubSubParichhedDto()
        {
            Id = x.Id,
            SubParichhedId = x.SubParichhedId,
            Name = x.Name,
            SerialNo = x.SerialNo,
        }).ToListAsync();

        return ResultWithDataDto<List<SubSubParichhedDto>>.Success(subSubParichhedsDto);

    }

    public async Task<ResultWithDataDto<SubSubParichhedDto>> GetSubSubParichhedById(int id)
    {
        var subSubParichhed = await _context.SubSubParichheds.FindAsync(id);
        if (subSubParichhed == null)
        {
            return ResultWithDataDto<SubSubParichhedDto>.Failure("Sub Parichhed not found");
        }
        var subParichhedDto = new SubSubParichhedDto()
        {
            Id = subSubParichhed.Id,
            SubParichhedId = subSubParichhed.SubParichhedId,
            Name = subSubParichhed.Name,
            SerialNo = subSubParichhed.SerialNo,
        };
        return ResultWithDataDto<SubSubParichhedDto>.Success(subParichhedDto);
    }

    public async Task<ResultWithDataDto<List<SubSubParichhedDto>>> GetAllSubSubParichheds()
    {
        var subSubParichhedsDto = await _context.SubSubParichheds.Select(x => new SubSubParichhedDto()
        {
            Id = x.Id,
            SubParichhedId = x.SubParichhedId,
            Name = x.Name,
            SerialNo = x.SerialNo,
        }).ToListAsync();

        return ResultWithDataDto<List<SubSubParichhedDto>>.Success(subSubParichhedsDto);
    }

    public async Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfAnusuchi(int id)
    {
        var res = await _context.Mapdandas.Where(x => x.AnusuchiId == id)
            .Where(x => x.Parichhed == null).ToListAsync();

        return new ResultWithDataDto<List<Mapdanda>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfParichhed(int parichhedId)
    {
        //var parichhed = await _context.Parichheds.FindAsync(parichhedId);
        //var groupedMapdandas = await _context.Mapdandas
        //        .Include(m => m.SubSubParichhed)
        //        .Include(m => m.SubParichhed)
        //        .Include(m => m.Parichhed)
        //        .Include(m => m.SubMapdandas)
        //        .Where(x => x.Parichhed == parichhed)
        //        .GroupBy(m => new
        //        {
        //            SubSubParichhed = m.SubSubParichhed != null ? m.SubSubParichhed.Name : "",
        //            SubParichhed = m.SubParichhed != null ? m.SubParichhed.Name : "",
        //            Parichhed = m.Parichhed != null ? m.Parichhed.Name : "",
        //            m.IsAvailableDivided,
        //        })
        //        .Select(g => new MapdandaByAnusuchiDto
        //        {
        //            SubSubParichhed = g.Key.SubSubParichhed,
        //            SubParichhed = g.Key.SubParichhed,
        //            Parichhed = g.Key.Parichhed,
        //            IsAvailableDivided = g.Key.IsAvailableDivided,
        //            Mapdandas = g.Select(x => new GroupedMapdanda
        //            {
        //                Id = x.Id,
        //                Name = x.Name,
        //                SerialNumber = x.SerialNumber,
        //                Is100Active = x.Is100Active,
        //                Is200Active = x.Is200Active,
        //                Is50Active = x.Is50Active,
        //                Is25Active = x.Is25Active,
        //                IsAvailableDivided = g.Key.IsAvailableDivided,
        //                Status = x.Status,
        //            }).ToList()
        //        })
        //        .ToListAsync();

        var map = await _context.Mapdandas
            .Where(x => x.ParichhedId == parichhedId)
            .Where(x => x.SubParichhedId == null)
            .ToListAsync();


        return new ResultWithDataDto<List<Mapdanda>>(true, map, null);
        }

    public async Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfSubParichhed(int subParichhedId)
    {
        var map = await _context.Mapdandas
            .Where(x => x.SubParichhedId == subParichhedId)
            .Where(x => x.SubSubParichhedId == null)
            .ToListAsync();

        return new ResultWithDataDto<List<Mapdanda>>(true, map, null);
    }

    public async Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfSubSubParichhed(int subSubParichhedId)
    {
        var map = await _context.Mapdandas
            .Where(x => x.SubSubParichhedId != subSubParichhedId).ToListAsync();

        return new ResultWithDataDto<List<Mapdanda>>(true, map, null);
    }

}