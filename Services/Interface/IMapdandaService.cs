using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IMapdandaService
{
    Task<ResultDto> AddNewMapdanda(MapdandaDto dto);
}