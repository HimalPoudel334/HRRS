
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

namespace HRRS.Services.Interface;

public interface IParichhedService
{
    Task<ResultDto> Create(ParichhedDto dto);
    Task<ResultDto> Update(int parichhedId, ParichhedDto dto);
    Task<ResultWithDataDto<List<ParichhedDto>>> GetAllParichhed();
    Task<ResultWithDataDto<ParichhedDto>> GetParichhedById(int id);
    Task<ResultWithDataDto<List<ParichhedDto>>> GetParichhedByAnusuchi(int id);
    Task<ResultDto> CreateSubParichhed(SubParichhedDto dto);
    Task<ResultDto> UpdateSubParichhed(int subParichhedId, SubParichhedDto dto);
    Task<ResultWithDataDto<List<SubParichhedDto>>> GetSubParichhedsByParichhed(int parichhedId);
    Task<ResultWithDataDto<SubParichhedDto>> GetSubParichhedById(int id);
    Task<ResultDto> CreateSubSubParichhed(SubSubParichhedDto dto);
    Task<ResultDto> UpdateSubSubParichhed(int subSubParichhedId, SubSubParichhedDto dto);
    Task<ResultWithDataDto<List<SubSubParichhedDto>>> GetSubSubParichhedsBySubParichhed(int subParichhedId);
    Task<ResultWithDataDto<SubSubParichhedDto>> GetSubSubParichhedById(int id);


}