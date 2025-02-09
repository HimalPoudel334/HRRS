
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class AnusuchiService(ApplicationDbContext context) : IAnusuchiService
{
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<ResultDto> Create(AnusuchiDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.SerialNo))
        {
            return ResultDto.Failure("Anusuchi must have name and serial number");
        }
        if(await _dbContext.Anusuchis.AnyAsync(x => x.SerialNo == dto.SerialNo))
        {
            return ResultDto.Failure("Serial Number already exists");
        }
        Anusuchi anusuchi = new()
        {
            Name = dto.Name,
            DafaNo = dto.DafaNo,
            SerialNo = dto.SerialNo //await _dbContext.Anusuchis.MaxAsync(x => x.SerialNo) + 1
        };

        await _dbContext.Anusuchis.AddAsync(anusuchi);
        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultDto> Update(int anusuchiId, AnusuchiDto dto)
    {
        var anusuchi = await _dbContext.Anusuchis.FindAsync(anusuchiId);
        if (anusuchi == null)
        {
            return ResultDto.Failure("Anusuchi Not Found");
        }
        
        if (dto.SerialNo != anusuchi.SerialNo && await _dbContext.Anusuchis.AnyAsync(x => x.SerialNo == dto.SerialNo)) 
        {
                return ResultDto.Failure("Serial Number already taken");
        }
        anusuchi.SerialNo = dto.SerialNo;
        anusuchi.Name = dto.Name;
        anusuchi.DafaNo = dto.DafaNo;

        await _dbContext.SaveChangesAsync();
        return ResultDto.Success();
    }

    public async Task<ResultWithDataDto<List<AnusuchiDto>>> GetAll()
    {
        var anusuchis = await _dbContext.Anusuchis.Include(x => x.Parichheds).Select(x => new AnusuchiDto()
        {
            Id = x.Id,
            Name = x.Name,
            DafaNo = x.DafaNo,
            SerialNo = x.SerialNo,
            Parichheds = x.Parichheds.Select(p => new ParichhedDto()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList()
        }).ToListAsync();
        return ResultWithDataDto<List<AnusuchiDto>>.Success(anusuchis);
    }

    public async Task<ResultWithDataDto<AnusuchiDto>> GetById(int id)
    {
        var anusuchi = await _dbContext.Anusuchis.Include(x => x.Parichheds).FirstOrDefaultAsync(x => x.Id == id);
        if (anusuchi == null)
        {
            return ResultWithDataDto<AnusuchiDto>.Failure("Anusuchi Not Found");
        }

        var dto = new AnusuchiDto()
        {
            Id = anusuchi.Id,
            Name = anusuchi.Name,
            DafaNo = anusuchi.DafaNo,
            SerialNo = anusuchi.SerialNo,
        };

        if (anusuchi.Parichheds == null || anusuchi.Parichheds.Count == 0)
        {
            dto.Mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == id && x.ParichhedId == null).Select(x => new MapdandaDto()
            {
                Id = x.Id,
                Name = x.Name,
                AnusuchiId = x.AnusuchiId,
                ParichhedId = x.ParichhedId,
                Is25Active = x.Is25Active,
                Is50Active = x.Is50Active,
                Is100Active = x.Is100Active,
                Is200Active = x.Is200Active,
                Parimaad = x.Parimaad,
                IsAvailableDivided = x.IsAvailableDivided,
                SerialNumber = x.SerialNumber,
                SubParichhedId = x.SubParichhedId,
                SubSubParichhedId = x.SubSubParichhedId
            }).ToListAsync();

            return ResultWithDataDto<AnusuchiDto>.Success(dto);
        }
        else
        {
            anusuchi.Parichheds = await _dbContext.Parichheds.Include(x => x.SubParichheds).ToListAsync();
            var parichhedDtos = new List<ParichhedDto>();
            foreach (var parichhed in anusuchi.Parichheds)
            {
                var parichhedDto = new ParichhedDto()
                {
                    Id = parichhed.Id,
                    SerialNo = parichhed.SerialNo,
                    AnusuchiId = parichhed.AnusuchiId,
                    Name = parichhed.Name,

                };

                //var subParichheds = await _dbContext.SubParichheds.Where(x => x.Parichhed == parichhed).ToListAsync();
                if (parichhed.SubParichheds == null || parichhed.SubParichheds.Count == 0)
                {

                    parichhedDto.Mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == id && x.ParichhedId == parichhed.Id && x.SubParichhedId == null).Select(x => new MapdandaDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        AnusuchiId = x.AnusuchiId,
                        ParichhedId = x.ParichhedId,
                        Is25Active = x.Is25Active,
                        Is50Active = x.Is50Active,
                        Is100Active = x.Is100Active,
                        Is200Active = x.Is200Active,
                        Parimaad = x.Parimaad,
                        IsAvailableDivided = x.IsAvailableDivided,
                        SerialNumber = x.SerialNumber,
                        SubParichhedId = x.SubParichhedId,
                        SubSubParichhedId = x.SubSubParichhedId
                    }).ToListAsync();
                    parichhedDtos.Add(parichhedDto);
                    dto.Parichheds = parichhedDtos;
                }

                else
                {
                    parichhed.SubParichheds = await _dbContext.SubParichheds.Include(x => x.SubSubParichheds).ToListAsync();
                    var subParichhedDtos = new List<SubParichhedDto>();
                    foreach (var subParichhed in parichhed.SubParichheds)
                    {
                        var subParichhedDto = new SubParichhedDto()
                        {
                            Id = subParichhed.Id,
                            SerialNo = subParichhed.SerialNo,
                            ParichhedId = subParichhed.ParichhedId,
                            Name = subParichhed.Name,

                        };

                        //var subParichheds = await _dbContext.SubParichheds.Where(x => x.Parichhed == parichhed).ToListAsync();
                        if (subParichhed.SubSubParichheds == null || subParichhed.SubSubParichheds.Count == 0)
                        {

                            subParichhedDto.Mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == id && x.ParichhedId == parichhed.Id && x.SubParichhedId == subParichhed.Id).Select(x => new MapdandaDto()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                AnusuchiId = x.AnusuchiId,
                                ParichhedId = x.ParichhedId,
                                Is25Active = x.Is25Active,
                                Is50Active = x.Is50Active,
                                Is100Active = x.Is100Active,
                                Is200Active = x.Is200Active,
                                Parimaad = x.Parimaad,
                                IsAvailableDivided = x.IsAvailableDivided,
                                SerialNumber = x.SerialNumber,
                                SubParichhedId = x.SubParichhedId,
                                SubSubParichhedId = x.SubSubParichhedId
                            }).ToListAsync();
                            parichhedDto.SubParichheds = subParichhedDtos;
                        }
                        else
                        {
                            var ssParichhedDtos = new List<SubSubParichhedDto>();
                            foreach(var ssParichhed in subParichhed.SubSubParichheds)
                            {
                                var ssDto = new SubSubParichhedDto()
                                {
                                    Id = ssParichhed.Id,
                                    Name = ssParichhed.Name,
                                    SerialNo = ssParichhed.SerialNo,
                                    SubParichhedId = ssParichhed.SubParichhedId,
                                    Mapdandas = await _dbContext.Mapdandas.Where(x => x.AnusuchiId == id && x.ParichhedId == parichhed.Id && x.SubParichhedId == subParichhed.Id && x.SubSubParichhedId == ssParichhed.Id).Select(x => new MapdandaDto()
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        AnusuchiId = x.AnusuchiId,
                                        ParichhedId = x.ParichhedId,
                                        Is25Active = x.Is25Active,
                                        Is50Active = x.Is50Active,
                                        Is100Active = x.Is100Active,
                                        Is200Active = x.Is200Active,
                                        Parimaad = x.Parimaad,
                                        IsAvailableDivided = x.IsAvailableDivided,
                                        SerialNumber = x.SerialNumber,
                                        SubParichhedId = x.SubParichhedId,
                                        SubSubParichhedId = x.SubSubParichhedId
                                    }).ToListAsync()
                                };
                                ssParichhedDtos.Add(ssDto);
                            }
                            subParichhedDto.SubSubParichheds = ssParichhedDtos;
                        }
                    }
                }
            }

            return ResultWithDataDto<AnusuchiDto>.Success(dto);

        }
    }

    //public async Task<ResultWithDataDto<ResponseDto>> GetAnusuchiMapdanda(int anusuchiId)
    //{
    //    var anusuchi = await _dbContext.Anusuchis.FindAsync();
    //    if (anusuchi == null)
    //    {
    //        return ResultWithDataDto<ResponseDto>.Failure("Anusuchi Not Found");
    //    }
    //    var mapdandas = anusuchi.Mapdandas.Select(x => new MapdandaDto()
    //    {
    //        Id = x.Id,
    //        Name = x.Name,
    //        SerialNumber = x.SerialNumber,
    //        AnusuchiNumber = x.AnusuchiId
    //    }).ToList();
    //    var response = new ResponseDto()
    //    {
    //        Anusuchi = new AnusuchiDto()
    //        {
    //            Id = anusuchi.Id,
    //            Name = anusuchi.Name,
    //            DafaNo = anusuchi.DafaNo,
    //            SerialNo = anusuchi.SerialNo
    //        },
    //        Mapdandas = mapdandas
    //    };
    //    return ResultWithDataDto<ResponseDto>.Success(response);

    //}

}