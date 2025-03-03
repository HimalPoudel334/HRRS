using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.RegistrationRequest;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IRegistrationRequestService
    {
        Task<ResultWithDataDto<List<RegistrationRequestDto?>>> GetAllRegistrationRequestsAsync();
        Task<ResultWithDataDto<RegistrationRequestDto?>> GetRegistrationRequestByIdAsync(int id);
        Task<ResultWithDataDto<string>> ApproveRegistrationRequestAsync(int id, long handledById);
        Task<ResultWithDataDto<string>> RejectRegistrationRequestAsync(int id, long handledById, StandardRemarkDto dto);
    }
}
