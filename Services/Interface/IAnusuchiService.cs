
using HRRS.Dto;
using HRRS.Dto.Anusuchi;

namespace HRRS.Services.Interface;

public interface IAnusuchiService
{
    Task<ResultDto> Add(AnusuchiDto dto);
}