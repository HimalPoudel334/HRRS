
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;

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
            Parichheds = dto.Parichheds?.Select(x => new Parichhed()
            {
                ParichhedName = x.ParichhedName,
                SubParichheds = x.SubParichheds?.Select(y => new Parichhed()
                {
                    ParichhedName = y.ParichhedName,
                }).ToList(),
                Mapdandas = x.Mapdandas?.Select(y => new Mapdanda()
                {
                    Name = y.Name,
                    SerialNumber = y.SerialNumber,
                }).ToList()

            }).ToList(),
            Mapdandas = dto.Mapdandas?.Select(x => new Mapdanda()
            {
                Name = x.Name,
                SerialNumber = x.SerialNumber,
            }).ToList()

            
        };

        await _context.AddAsync(anusuchi);
        await _context.SaveChangesAsync();

        return ResultDto.Success();
    }
}