
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace HRRS.Services.Implementation;

public class AnusuchiService : IAnusuchiService
{
    private readonly ApplicationDbContext _context;

    public AnusuchiService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Add(AnusuchiDto dto)
    {
        var anusuchi = new Anusuchi()
        {
            AnusuchiName = dto.AnusuchiName,
            RelatedToDafaNo = dto.RelatedToDafaNo,    
        };

        if(dto.Parichheds != null)
        {
            anusuchi.Parichheds = [];
            foreach (var parichhed in dto.Parichheds)
            {
                anusuchi.Parichheds.Add(AttachParichheds(parichhed, anusuchi));
            }

        }

        if(dto.Mapdandas != null)
        {
            anusuchi.Mapdandas = [];
            foreach (var mapdanda in dto.Mapdandas)
            {
                anusuchi.Mapdandas.Add(AttachMapdandas(mapdanda, anusuchi, null));
            }

        }

        await _context.Anusuchis.AddAsync(anusuchi);
        await _context.SaveChangesAsync();
        return ResultDto.Success();
    }

    private static Parichhed AttachParichheds(ParichhedDto dto, Anusuchi anusuchi)
    {
        var parichhed = new Parichhed()
        {
            ParichhedName = dto.ParichhedName,
            Anusuchi = anusuchi,
            //AnusuchiId = dto.AnusuchiId,
        };

        if (dto.SubParichheds != null)
        {
            parichhed.SubParichheds = [];
            foreach (var subParichhed in dto.SubParichheds)
            {
                parichhed.SubParichheds.Add(AttachParichheds(subParichhed, anusuchi));
            }
        }
        if (dto.Mapdandas != null)
        {
            parichhed.Mapdandas = [];
            foreach (var mapdanda in dto.Mapdandas)
            {
                parichhed.Mapdandas.Add(AttachMapdandas(mapdanda, anusuchi, parichhed));
            }
        }
        return parichhed;
    }

    private static Mapdanda AttachMapdandas(MapdandaDto dto, Anusuchi anusuchi, Parichhed? parichhed)
    {
        var mapdanda = new Mapdanda
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            //AnusuchiId = dto.AnusuchiId,
            IsAvailableDivided = dto.IsAvailableDivided,
            Has25Enabled = dto.IsAvailableDivided ? dto.Has25Enabled : null,
            Has50Enabled = dto.IsAvailableDivided ? dto.Has50Enabled : null,
            Has100Enabled = dto.IsAvailableDivided ? dto.Has100Enabled : null,
            Has200Enabled = dto.IsAvailableDivided ? dto.Has200Enabled : null,
            Anusuchi = anusuchi,
            Parichhed = parichhed,
        };

        if (dto.SubMapdandas != null)
        {
            mapdanda.SubMapdandas = [];
            foreach (var subMapdanda in dto.SubMapdandas)
            {
                mapdanda.SubMapdandas.Add(AttachMapdandas(subMapdanda, anusuchi, parichhed));
            }
        }

        return mapdanda;

    }

    public async Task<ResultDto> Delete(int id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<ResultDto> Update(int id, AnusuchiUpdateDto dto)
    {
        var anusuchi = await _context.Anusuchis.FindAsync(id);
        if (anusuchi == null)
        {
            return ResultDto.Failure("Anusuchi not found");
        }

        anusuchi.AnusuchiName = dto.AnusuchiName;
        anusuchi.RelatedToDafaNo = dto.RelatedToDafaNo;
        
        await _context.SaveChangesAsync();

        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<AnusuchiDto?>> GetById(int id)
    {
        var anusuchi = await _context.Anusuchis.Include(x => x.Parichheds).Include(p => p.Mapdandas).FirstOrDefaultAsync(a => a.Id == id);
        if (anusuchi == null)
        {
            return ResultWithDataDto<AnusuchiDto?>.Failure("Anusuchi not found");
        }

        var anusuchiDto = ReturnAnusuchiDto(anusuchi);

        return ResultWithDataDto<AnusuchiDto?>.Success(anusuchiDto);

    }

    private static ParichhedDto AttachParichhedDto(Parichhed parichhed)
    {
        var dto = new ParichhedDto()
        {
            Id = parichhed.Id,
            AnusuchiId = parichhed.Anusuchi.Id,
            ParichhedName = parichhed.ParichhedName,
        };

        if (parichhed.SubParichheds != null)
        {
            dto.SubParichheds = [];
            foreach (var subParichhed in parichhed.SubParichheds)
            {
                dto.SubParichheds.Add(AttachParichhedDto(subParichhed));
            }
        }

        if (parichhed.Mapdandas != null)
        {
            dto.Mapdandas = [];
            foreach (var mapdanda in parichhed.Mapdandas)
            {
                dto.Mapdandas.Add(AttachMapdandaDto(mapdanda));
            }
        }
        return dto;
    }

    private static MapdandaDto AttachMapdandaDto(Mapdanda mapdanda)
    {
        var dto = new MapdandaDto()
        {
            Id = mapdanda.Id,
            Name = mapdanda.Name,
            SerialNumber = mapdanda.SerialNumber,
            AnusuchiId = mapdanda.AnusuchiId,
            Has25Enabled = mapdanda.Has25Enabled,
            Has50Enabled = mapdanda.Has50Enabled,
            Has100Enabled = mapdanda.Has100Enabled,
            Has200Enabled = mapdanda.Has200Enabled,
            IsAvailableDivided = mapdanda.IsAvailableDivided,
        };

        if (mapdanda.SubMapdandas != null)
        {
            dto.SubMapdandas = [];
            foreach (var subMapdanda in mapdanda.SubMapdandas)
            {
                dto.SubMapdandas.Add(AttachMapdandaDto(subMapdanda));
            }
        }
        return dto;
    }



    public async Task<ResultWithDataDto<IEnumerable<AnusuchiDto>>> GetAll()
    {
        var anusuchis = await _context.Anusuchis.Include(x => x.Parichheds).Include(p => p.Mapdandas).ToListAsync();
        var anusuchiDtos = new List<AnusuchiDto>();

        foreach (var anusuchi in anusuchis)
        {
            var anusuchiDto = new AnusuchiDto
            {
                Id = anusuchi.Id,
                AnusuchiName = anusuchi.AnusuchiName,
                RelatedToDafaNo = anusuchi.RelatedToDafaNo,
            };
            if (anusuchi.Parichheds != null)
            {
                anusuchiDto.Parichheds = [];
                foreach (var parichhed in anusuchi.Parichheds)
                {
                    anusuchiDto.Parichheds.Add(AttachParichhedDto(parichhed));
                }
            }
            else if (anusuchi.Mapdandas != null)
            {
                anusuchiDto.Mapdandas = [];
                foreach (var mapdanda in anusuchi.Mapdandas)
                {
                    anusuchiDto.Mapdandas.Add(AttachMapdandaDto(mapdanda));
                }
            }
            anusuchiDtos.Add(anusuchiDto);
        }
        return ResultWithDataDto<IEnumerable<AnusuchiDto>>.Success(anusuchiDtos);
    }

    private static AnusuchiDto ReturnAnusuchiDto(Anusuchi anusuchi)
    {
        var anusuchiDto = new AnusuchiDto
        {
            Id = anusuchi.Id,
            AnusuchiName = anusuchi.AnusuchiName,
            RelatedToDafaNo = anusuchi.RelatedToDafaNo,
        };
        if (anusuchi.Parichheds != null)
        {
            anusuchiDto.Parichheds = [];
            foreach (var parichhed in anusuchi.Parichheds)
            {
                anusuchiDto.Parichheds.Add(AttachParichhedDto(parichhed));
            }
        }
        else
        {
            anusuchiDto.Mapdandas = [];
            foreach (var mapdanda in anusuchi.Mapdandas)
            {
                anusuchiDto.Mapdandas?.Add(AttachMapdandaDto(mapdanda));
            }
        }

        return anusuchiDto;

    }
}
