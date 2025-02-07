using HRRS.Dto;

namespace HRRS.Services.Interface;
public interface IMapdandaService1
{
    Task<ResultWithDataDto<List<MapdandaDto1>>> GetByAnusuchi(int? anusuchi_id);
    Task<ResultDto> Add(MapdandaDto1 dto);
    Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto1 dto);
    Task<ResultWithDataDto<MapdandaDto1>> GetById(int id);
    Task<ResultWithDataDto<List<MapdandaDto1>>> GetByParichhed(int parichhedId, int? anusuchiId);
    Task<ResultWithDataDto<List<MapdandaDto1>>> GetBySubParichhed(int subParichhedId, int? parichhedId, int? anusuchiId);
    Task<ResultWithDataDto<List<MapdandaDto1>>> GetBySubSubParichhed(int subSubParichhedId, int? subParichhedId, int? parichhedId, int? anusuchiId);

    Task<ResultDto> AddSubMapdanda(SubMapdandaDto dto);
    Task<ResultDto> UpdateSubMapdanda(int subMapdandaId, SubMapdandaDto dto);
    Task<ResultWithDataDto<SubMapdandaDto>> GetSubMapdandaById(int id);
    Task<ResultWithDataDto<List<SubMapdandaDto>>> GetSubMapdandaByMapdanda(int mapdandaId);


}