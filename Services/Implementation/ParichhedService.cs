
using System.Linq;
using HRRS.Dto;
using HRRS.Dto.Mapdanda1;
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
            return ResultWithDataDto<ParichhedDto>.Failure("परिच्छेदको नाम र क्रम सङ्ख्या हुनु पर्छ।");
        }

        var anusuchi = await _context.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultWithDataDto<ParichhedDto>.Failure("अनुसूची फेला परेन।");
        }

        //if (await _context.Parichheds.AnyAsync(x => x.SerialNo == dto.SerialNo)) 
        //{
        //    return ResultWithDataDto<ParichhedDto>.Failure("Serial number already taken");

        //}

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
            return ResultDto.Failure("परिच्छेद फेला परेन।");
        }

        var anusuchi = await _context.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("अनुसूची फेला परेन।");
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
            return ResultWithDataDto<ParichhedDto>.Failure("परिच्छेद फेला परेन।");
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
            return ResultWithDataDto<SubParichhedDto>.Failure("उपपरिच्छेदको नाम र क्रम सङ्ख्या हुनु पर्छ।");
        }

        var parichhed = await _context.Parichheds.FindAsync(dto.ParichhedId);
        if (parichhed == null)
        {
            return ResultWithDataDto<SubParichhedDto>.Failure("परिच्छेद फेला परेन।");
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
            return ResultDto.Failure("Parichhed फेला परेन।");
        }

        var parichhed = await _context.Parichheds.FindAsync(dto.ParichhedId);
        if (parichhed == null)
        {
            return ResultDto.Failure("Parichhed फेला परेन।");
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
        var subParichhed = await _context.SubParichheds.Include(x => x.Mapdandas).Include(x => x.SubSubParichheds).FirstOrDefaultAsync(x => x.Id == id);
        if (subParichhed == null)
        {
            return ResultWithDataDto<SubParichhedDto>.Failure("Sub Parichhed फेला परेन।");
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
            return ResultWithDataDto<SubSubParichhedDto>.Failure("Parichhed फेला परेन।");
        }
        //if(await _context.SubSubParichheds.AnyAsync(x => x.SerialNo == dto.SerialNo))
        //{
        //    return ResultWithDataDto<SubSubParichhedDto>.Failure("Serial number already taken");
        //}
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
            return ResultDto.Failure("Parichhed फेला परेन।");
        }

        var subParichhed = await _context.SubParichheds.FindAsync(dto.SubParichhedId);
        if (subParichhed == null)
        {
            return ResultDto.Failure("Parichhed फेला परेन।");
        }

        //if (subSubParichhed.SerialNo != dto.SerialNo && await _context.SubSubParichheds.AnyAsync(x => x.SerialNo == dto.SerialNo))
        //{
        //    return ResultDto.Failure("Serial number already taken");
        //}

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
            return ResultWithDataDto<SubSubParichhedDto>.Failure("Sub Parichhed फेला परेन।");
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

    public async Task<ResultWithDataDto<List<MapdandaByAnusuchiDto>>> GetMapdandasOfAnusuchi(int id)
    {
        var mapdandas = await _context.Mapdandas.Where(x => x.AnusuchiId == id)
        .Where(x => x.Parichhed == null).ToListAsync();

        var res = mapdandas.GroupBy(m => new
        {
            Parichhed = m.Parichhed != null ? m.Parichhed.Name : "",
            m.IsAvailableDivided,
        })
        .Select(g => new Dto.Mapdanda1.MapdandaByAnusuchiDto
        {
            IsAvailableDivided = g.Key.IsAvailableDivided,
            Parichhed = g.GroupBy(x => x.Parichhed).Select(x => new Dto.Mapdanda1.GroupdParichhed
            {
                Name = x.Key != null ? x.Key.Name : "",
                GroupedPariched = x.GroupBy(y => y.SubParichhed).Select(a => new Dto.Mapdanda1.GroupdSubParichhed
                {
                    Name = a.Key != null ? a.Key.Name : "",
                    GroupdSubSubParichhed = a.GroupBy(b => b.SubSubParichhed).Select(c => new Dto.Mapdanda1.GroupdSubSubParichhed
                    {
                        Name = c.Key != null ? c.Key.Name : "",
                        GroupedMapdandaGroup = c.GroupBy(e => e.Group).Select(f => new Dto.Mapdanda1.GroupedMapdandaByGroupName
                        {
                            GroupName = f.Key,
                            GroupedMapdanda = f.Select(h => new Dto.Mapdanda1.GroupedMapdanda
                            {
                                Id = h.Id,
                                Name = h.Name,
                                SerialNumber = h.SerialNumber,
                                Is100Active = h.Is100Active,
                                Is200Active = h.Is200Active,
                                Is50Active = h.Is50Active,
                                Is25Active = h.Is25Active,
                                IsAvailableDivided = g.Key.IsAvailableDivided,
                                Status = h.Status,
                                Parimaad = h.Parimaad,
                                Group = h.Group,
                            }).ToList()
                        }).ToList(),
                    }).ToList(),
                }).ToList(),

            }).ToList(),
        })
        .ToList();

        return new ResultWithDataDto<List<Dto.Mapdanda1.MapdandaByAnusuchiDto>>(true, res, null);
    }

    public async Task<ResultWithDataDto<List<GroupdParichhed>>> GetMapdandasOfParichhed(int parichhedId)
    {
        var map = await _context.Mapdandas
            .Where(x => x.ParichhedId == parichhedId)
            .Where(x => x.SubParichhedId == null)
            .ToListAsync();

        var res = map.GroupBy(m => new
        {
            SubParichhed = m.SubParichhed != null ? m.SubParichhed.Name : "",
            m.IsAvailableDivided,
        })
        .Select(g => new GroupdParichhed
        {
            Name = g.Key != null ? g.Key.SubParichhed : "",
            GroupedPariched = g.GroupBy(x => x.SubParichhed).Select(z => new GroupdSubParichhed
            {
                Name = z.Key != null ? z.Key.Name : "",
                GroupdSubSubParichhed = z.GroupBy(a => a.SubSubParichhed).Select(b => new GroupdSubSubParichhed
                {
                    Name = b.Key.Name != null ? b.Key.Name : "",
                    GroupedMapdandaGroup = b.GroupBy(c => c.Group).Select(d => new GroupedMapdandaByGroupName
                    {
                        GroupName = d.Key,
                        GroupedMapdanda = d.Select(e => new GroupedMapdanda
                        {
                            Id = e.Id,
                            Name = e.Name,
                            SerialNumber = e.SerialNumber,
                            Is100Active = e.Is100Active,
                            Is200Active = e.Is200Active,
                            Is50Active = e.Is50Active,
                            Is25Active = e.Is25Active,
                            Value25 = e.Value25,
                            Value50 = e.Value50,
                            Value100 = e.Value100,
                            Value200 = e.Value200,
                            Status = e.Status,
                            Parimaad = e.Parimaad,
                            Group = e.Group,

                        }).ToList()
                    }).ToList()
                }).ToList(),
            }).ToList()
        }).ToList();

        return new ResultWithDataDto<List<Dto.Mapdanda1.GroupdParichhed>>(true, res, null);

    }

    public async Task<ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>> GetMapdandasOfSubParichhed(int subParichhedId)
    {
        var map = await _context.Mapdandas
            .Include(x => x.SubSubParichhed)
            .Where(x => x.SubParichhedId == subParichhedId)
            .ToListAsync();


        var res = map
            .GroupBy(m => m.SubSubParichhed)
            .Select(m => new GroupedSubSubParichhedAndMapdanda
            {
                HasBedCount = m.FirstOrDefault()?.IsAvailableDivided,
                SubSubParixed = m.Key?.Name,
                List = m
                .GroupBy(m => m.Group)
                .Select(m => new GroupedMapdandaByGroupName
                {
                    GroupName = m.Key,
                    GroupedMapdanda = m.Select( m => new GroupedMapdanda
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

                }).ToList()
            })
            .ToList();

        return ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>.Success(res);

        



        //var res = map.GroupBy(m => new
        //{
        //    SubSubParichhed = m.SubSubParichhed != null ? m.SubSubParichhed.Name : "",
        //    m.IsAvailableDivided,
        //}).Select(x => new GroupdSubParichhed
        //{
        //    Name = x.Key.SubSubParichhed != null ? x.Key.SubSubParichhed : "",
        //    GroupdSubSubParichhed = x.GroupBy(y => y.SubSubParichhed).Select(z => new GroupdSubSubParichhed
        //    {
        //        Name = z.Key != null ? z.Key.Name : "",
        //        GroupedMapdandaGroup = z.GroupBy(a => a.Group).Select(b => new GroupedMapdandaByGroupName
        //        {
        //            GroupName = b.Key,
        //            GroupedMapdanda = b.Select(c => new GroupedMapdanda
        //            {
        //                Id = c.Id,
        //                Name = c.Name,
        //                SerialNumber = c.SerialNumber,
        //                Is100Active = c.Is100Active,
        //                Is200Active = c.Is200Active,
        //                Is50Active = c.Is50Active,
        //                Is25Active = c.Is25Active,
        //                Status = c.Status,
        //                Parimaad = c.Parimaad
        //            }).ToList()
        //        }).ToList(),
        //    }).ToList(),
        //}).ToList();

        //return new ResultWithDataDto<List<GroupdSubParichhed>>(true, res, null);
    }



    public async Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfSubSubParichhed(int subSubParichhedId)
    {
        var map = await _context.Mapdandas
            .Where(x => x.SubSubParichhedId != subSubParichhedId).ToListAsync();

        return new ResultWithDataDto<List<Mapdanda>>(true, map, null);
    }

}