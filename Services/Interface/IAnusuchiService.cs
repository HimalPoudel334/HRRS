
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface;

public interface IAnusuchiService
{
    Task<ResultDto> Create(AnusuchiDto dto);
    Task<ResultWithDataDto<AnusuchiDto?>> GetById(int id);
    Task<ResultDto> Update(int id, AnusuchiUpdateDto dto);
    Task<ResultWithDataDto<IEnumerable<AnusuchiDto>>> GetAll();
}