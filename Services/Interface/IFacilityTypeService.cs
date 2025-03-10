using HRRS.Dto;
using Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IFacilityTypeService
    {
        Task<ResultWithDataDto<List<FacilityTypeDto>>> GetAll();
        Task<ResultDto> Create(FacilityTypeDto dto);
        Task<ResultWithDataDto<List<FacilityTypeDto>>> GetSubTypesOfParent(int facilityTypeId);
    }
}
