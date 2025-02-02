using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IMapdandaService
{
    Task<ResultWithDataDto<List<MapdandaDto>>> GetByAnusuchi(int anusuchi_id);
    Task<ResultDto> Add(MapdandaDto dto);
}