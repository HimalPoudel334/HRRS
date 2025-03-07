
using HRRS.Dto;
using HRRS.Dto.AdminMapdanda;
using HRRS.Dto.Parichhed;

namespace HRRS.Services.Interface;

public interface IParichhedService
{
    Task<ResultWithDataDto<ParichhedDto>> Create(ParichhedDto dto);
    Task<ResultDto> Update(int parichhedId, ParichhedDto dto);
    Task<ResultWithDataDto<List<ParichhedDto>>> GetAllParichhed(ParichhedQueryParams dto, long userId);
    Task<ResultWithDataDto<ParichhedDto>> GetParichhedById(int id);
    Task<ResultWithDataDto<List<ParichhedDto>>> GetParichhedByAnusuchi(int id);
    Task<ResultWithDataDto<SubParichhedDto>> CreateSubParichhed(SubParichhedDto dto);
    Task<ResultDto> UpdateSubParichhed(int subParichhedId, SubParichhedDto dto);
    Task<ResultWithDataDto<List<SubParichhedDto>>> GetAllSubParichheds();
    Task<ResultWithDataDto<List<SubParichhedDto>>> GetSubParichhedsByParichhed(int parichhedId);
    Task<ResultWithDataDto<SubParichhedDto>> GetSubParichhedById(int id);
    Task<ResultWithDataDto<SubSubParichhedDto>> CreateSubSubParichhed(SubSubParichhedDto dto);
    Task<ResultDto> UpdateSubSubParichhed(int subSubParichhedId, SubSubParichhedDto dto);
    Task<ResultWithDataDto<List<SubSubParichhedDto>>> GetSubSubParichhedsBySubParichhed(int subParichhedId);
    Task<ResultWithDataDto<SubSubParichhedDto>> GetSubSubParichhedById(int id);
    Task<ResultWithDataDto<List<SubSubParichhedDto>>> GetAllSubSubParichheds();

    /* mapdandas insides of anusuchis, parichheds, subparichheds, subsubparichheds */
    Task<ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>> GetMapdandasOfSubParichhed(int subParichhedId);

}