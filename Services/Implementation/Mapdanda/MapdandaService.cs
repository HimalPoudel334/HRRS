using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Interfaces;
using HRRS.Services.Interface;

public class MapdandaService : IMapdandaService
{
    
    private readonly IMapdandaRepository _mapdandaRepository;

    public MapdandaService(IMapdandaRepository repository)
    {
        _mapdandaRepository = repository;
    }

    public async Task<ResultWithDataDto<List<MapdandaDto>>> GetByAnusuchi(int anusuchi_id)
    {

        var mapdanda = await _mapdandaRepository.GetByAnusuchiId(anusuchi_id);
        if (mapdanda is null) return new ResultWithDataDto<List<MapdandaDto>>(false, null, "Data not found");

        var res = mapdanda.Select(x => new MapdandaDto
        {
            Id = x.Id,
            AnusuchiNumber = x.AnusuchiNumber,
            Name = x.Name,
            SerialNumber = x.SerialNumber
        }).OrderBy(x => x.SerialNumber)
        .ToList();

        return new ResultWithDataDto<List<MapdandaDto>>(true, res, null);
    }
}
