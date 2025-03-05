
using HRRS.Dto;
using HRRS.Dto.Anusuchi;

namespace HRRS.Services.Interface;

public interface IAnusuchiService
{
    Task<ResultDto> Create(AnusuchiDto dto);
    Task<ResultDto> Update(int anusuchiId, AnusuchiDto dto);
    Task<ResultWithDataDto<List<AnusuchiDto>>> GetAll();
    Task<ResultWithDataDto<AnusuchiDto>> GetById(int id);
    Task<ResultWithDataDto<List<AnusuchiDto>>> GetAllUserAnusuchi(long userId, Guid? submissionCode);
}