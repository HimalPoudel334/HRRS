
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

namespace HRRS.Services.Interface;

public interface IParichhedService
{
    Task<ResultWithDataDto<ParichhedDto>> Create(ParichhedDto dto);
    Task<ResultDto> Update(int parichhedId, ParichhedDto dto);
    Task<ResultWithDataDto<List<ParichhedDto>>> GetAllParichhed(int? anusuchiId);
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
    Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfAnusuchi(int id);
    Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfParichhed(int parichhedId);
    Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfSubParichhed(int subParichhedId);
    Task<ResultWithDataDto<List<Mapdanda>>> GetMapdandasOfSubSubParichhed(int subSubParichhedId);

}