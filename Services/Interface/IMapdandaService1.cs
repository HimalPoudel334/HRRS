using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IMapdandaService1
{
    Task<ResultWithDataDto<List<MapdandaTableDto>>> GetByAnusuchi(int? anusuchi_id, string userType);
    Task<ResultDto> Add(MapdandaDto dto);
    Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto dto);
    Task<ResultDto> ToggleStatus(int mapdandaId);
    Task<ResultWithDataDto<MapdandaDto>> GetById(int id);
    Task<ResultWithDataDto<List<MapdandaTableDto>>> GetByParichhed(int parichhedId, int? anusuchiId);
    Task<ResultWithDataDto<List<MapdandaDto>>> GetBySubParichhed(int subParichhedId, int? parichhedId, int? anusuchiId);
    Task<ResultWithDataDto<List<MapdandaDto>>> GetBySubSubParichhed(int subSubParichhedId, int? subParichhedId, int? parichhedId, int? anusuchiId);

}