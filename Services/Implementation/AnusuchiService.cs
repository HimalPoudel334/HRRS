
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

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
        throw new System.NotImplementedException();
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

        var anusuchiDto = new AnusuchiDto
        {
            Id = anusuchi.Id,
            AnusuchiName = anusuchi.AnusuchiName,
            RelatedToDafaNo = anusuchi.RelatedToDafaNo,
        };
        if(anusuchi.Parichheds != null)
        {
            foreach (var parichhed in anusuchi.Parichheds)
            {
                anusuchiDto.Parichheds?.Add(AttachParichhed(parichhed));
            }
        }
        if (anusuchi.Mapdandas != null)
        {
            foreach (var mapdanda in anusuchi.Mapdandas)
            {
                anusuchiDto.Mapdandas?.Add(AttachMapdanda(mapdanda));
            }
        }

        return ResultWithDataDto<AnusuchiDto?>.Success(anusuchiDto);

    }

    private static ParichhedDto AttachParichhed(Parichhed parichhed)
    {
        var dto = new ParichhedDto()
        {
            Id = parichhed.Id,
            AnusuchiId = parichhed.Anusuchi.Id,
            ParichhedName = parichhed.ParichhedName,
        };

        if (parichhed.SubParichheds != null)
        {
            foreach (var subParichhed in parichhed.SubParichheds)
            {
                dto.SubParichheds?.Add(AttachParichhed(subParichhed));
            }
        }

        if (parichhed.Mapdandas != null)
        {
            foreach (var mapdanda in parichhed.Mapdandas)
            {
                dto.Mapdandas?.Add(AttachMapdanda(mapdanda));
            }
        }
        return dto;
    }

    private static MapdandaDto AttachMapdanda(Mapdanda mapdanda)
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
                dto.SubMapdandas?.Add(AttachMapdanda(subMapdanda));
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
                    anusuchiDto.Parichheds.Add(AttachParichhed(parichhed));
                }
            }
            if (anusuchi.Mapdandas != null)
            {
                anusuchiDto.Mapdandas = [];
                foreach (var mapdanda in anusuchi.Mapdandas)
                {
                    anusuchiDto.Mapdandas.Add(AttachMapdanda(mapdanda));
                }
            }
            anusuchiDtos.Add(anusuchiDto);
        }
        return ResultWithDataDto<IEnumerable<AnusuchiDto>>.Success(anusuchiDtos);
    }
}
