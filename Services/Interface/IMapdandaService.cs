using HRRS.Dto;
using HRRS.Dto.Mapdanda1;

namespace HRRS.Services.Interface;
public interface IMapdandaService
{
    Task<ResultWithDataDto<List<GroupedMapdandaByGroupName>>> GetByAnusuchi(int? anusuchi_id);
    Task<ResultDto> Add(MapdandaDto dto);
    Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto dto);
    Task<ResultDto> ToggleStatus(int mapdandaId);
    Task<ResultWithDataDto<List<string>>> GetMapdandaGroups(string? searchKey);
}