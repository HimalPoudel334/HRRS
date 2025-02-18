using HRRS.Dto;
using HRRS.Dto.Mapdanda1;
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
        if(string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNumber))
        {
            return ResultDto.Failure("मापदण्डामा नाम र क्रम सङ्ख्या हुनु पर्छ।");    
        }

        var anusuchi = await _dbContext.Anusuchis.FindAsync(dto.AnusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("अनुसूची फेला परेन।");
        }

        var exsitingMapdanda = _dbContext.Mapdandas
            .Where(x => x.AnusuchiId == dto.AnusuchiId);

        if(dto.ParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.ParichhedId == dto.ParichhedId);
        if(dto.SubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubParichhedId == dto.SubParichhedId);
        if(dto.SubSubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubSubParichhedId == dto.SubSubParichhedId);

        // Validations 

        var testDanda = exsitingMapdanda.FirstOrDefault();

        if(testDanda != null)
        {
            if (testDanda.IsAvailableDivided && !dto.IsAvailableDivided)
            {
                var msg = testDanda.IsAvailableDivided ? "मापदण्डामा शय्या सङ्ख्याको गणना हुनु पर्छ।" : "मापदण्डामा शय्या सङ्ख्याको गणना हुनु हुँदैन।";
                return ResultDto.Failure(msg);
            }

            if(!string.IsNullOrEmpty(testDanda.Parimaad) && string.IsNullOrEmpty(dto.Parimaad))
                return ResultDto.Failure("मापदण्डामा परिमाण हुनु पर्छ।");

            if(string.IsNullOrEmpty(testDanda.Parimaad) && !string.IsNullOrEmpty(dto.Parimaad))
                return ResultDto.Failure("मापदण्डामा परिमाण हुनु हुँदैन।");
        }


        var mapdanda = new Mapdanda()
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Anusuchi = anusuchi,
            Parimaad = dto.Parimaad,
            IsAvailableDivided = dto.IsAvailableDivided,
            Group = dto.Group,
        };

        if (dto.ParichhedId.HasValue)
        {
            var parichhed = await _dbContext.Parichheds.FindAsync(dto.ParichhedId);
            if (parichhed == null)
            {
                return ResultDto.Failure("परिच्छेद फेला परेन।");
            }
            mapdanda.Parichhed = parichhed;

            if (dto.SubParichhedId.HasValue)
            {
                var subParichhed = await _dbContext.SubParichheds.FindAsync(dto.SubParichhedId);
                if (subParichhed == null)
                {
                    return ResultDto.Failure("उपपरिच्छेद फेला परेन।");
                }
                
                mapdanda.SubParichhed = subParichhed;

                if (dto.SubSubParichhedId.HasValue)
                {
                    var subSubParichhed = await _dbContext.SubSubParichheds.FindAsync(dto.SubSubParichhedId);
                    if (subSubParichhed == null)
                    {
                        return ResultDto.Failure("उपपरिच्छेदको भाग परिच्छेद फेला परेन।");
                    }
                    mapdanda.SubSubParichhed = subSubParichhed;
                }
            }
        }

        if (dto.IsAvailableDivided)
        {
            mapdanda.Is25Active = dto.Is25Active;
            mapdanda.Is50Active = dto.Is50Active;
            mapdanda.Is100Active = dto.Is100Active;
            mapdanda.Is200Active = dto.Is200Active;
            mapdanda.Value25 = dto.Value25;
            mapdanda.Value50 = dto.Value50;
            mapdanda.Value100 = dto.Value100;
            mapdanda.Value200 = dto.Value200;
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
            return ResultDto.Failure("मापदण्ड फेला परेन।");
        }

        var exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.AnusuchiId == dto.AnusuchiId);

        if (dto.ParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.ParichhedId == dto.ParichhedId);
        if (dto.SubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubParichhedId == dto.SubParichhedId);
        if (dto.SubSubParichhedId.HasValue) exsitingMapdanda = _dbContext.Mapdandas.Where(x => x.SubSubParichhedId == dto.SubSubParichhedId);

        mapdanda.Name = dto.Name;
        mapdanda.SerialNumber = dto.SerialNumber;
        mapdanda.Is25Active = dto.Is25Active;
        mapdanda.Is50Active = dto.Is50Active;
        mapdanda.Is100Active = dto.Is100Active;
        mapdanda.Is200Active = dto.Is200Active;
        mapdanda.Value25 = dto.Value25;
        mapdanda.Value50 = dto.Value50;
        mapdanda.Value100 = dto.Value100;
        mapdanda.Value200 = dto.Value200;
        mapdanda.Parimaad = dto.Parimaad;

        //mapdanda.AnusuchiId = dto.AnusuchiId;
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>> GetByAnusuchi(int? anusuchiId, string userType)
    {
        var mapdandas = _dbContext.Mapdandas.AsQueryable();

        if (anusuchiId != null)
        {
            mapdandas = mapdandas.Where(x => x.AnusuchiId == anusuchiId && x.Parichhed == null && x.SubParichhed == null && x.SubSubParichhed == null);
        }
        if (userType == "Hospital") {
            mapdandas = mapdandas.Where(x => x.Status == true);
        }

        var res = await mapdandas.ToListAsync();

        var dto = res
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

               }).ToList()
           })
           .ToList();

        return ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>.Success(dto);
    }

    public async Task<ResultDto> ToggleStatus(int mapdandaId)
    {
        var mapdanda = await _dbContext.Mapdandas.FindAsync(mapdandaId);
        if (mapdanda is null)
        {
            return ResultDto.Failure("मापदण्ड फेला परेन।");
        }

        mapdanda.Status = !mapdanda.Status;
        await _dbContext.SaveChangesAsync();

        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<string>>> GetMapdandaGroups(string? searchKey)
    {
        var distinctGroupsQuery = _dbContext.Mapdandas
            .Where(m => m.Group != null)
            .Select(x => x.Group)
            .Distinct();

        if (!string.IsNullOrEmpty(searchKey))
        {
            distinctGroupsQuery = distinctGroupsQuery.Where(x => x.Contains(searchKey));
        }

        var distinctGroups = await distinctGroupsQuery.ToListAsync();

        return ResultWithDataDto<List<string>>.Success(distinctGroups);
    }

}
