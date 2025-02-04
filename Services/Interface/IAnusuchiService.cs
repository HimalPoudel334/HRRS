
using HRRS.Dto;
using HRRS.Dto.Anusuchi;

namespace HRRS.Services.Interface;

public interface IAnusuchiService
{
    Task<ResultDto> Add(AnusuchiDto dto);
    Task<ResultWithDataDto<AnusuchiDto?>> GetById(int id);
    Task<ResultDto> Update(int id, AnusuchiUpdateDto dto);
    Task<ResultWithDataDto<IEnumerable<AnusuchiDto>>> GetAll();
}