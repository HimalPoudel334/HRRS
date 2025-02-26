using HRRS.Dto;
using Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IFacilityTypeService
    {
        Task<ResultWithDataDto<List<FacilityType>>> GetAll();
        Task<ResultDto> Create(FacilityTypeDto dto);
    }
}
