using HRRS.Dto;
using Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IFacilityTypeService
    {
        Task<ResultWithDataDto<List<FacilityTypeDto>>> GetAll(int? id);
        Task<ResultDto> Create(FacilityTypeDto dto);
    }
}
