using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IMasterStandardEntryService
    {
        Task<ResultWithDataDto<string>> Create(SubmissionTypeDto dto, int healthFacilityId);
        Task<ResultWithDataDto<List<MasterStandardEntryDto>>> GetByHospitalId(int healthFacilityId, long userId);
        Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByUserHospitalId(int healthFacilityId);
        Task<ResultDto> UserFinalSubmission(Guid submissionCode);
        Task<ResultDto> ApproveStandardsWithRemark(Guid entryId, long userId, StandardRemarkDto dto);
        Task<ResultDto> RejectStandardsWithRemark(Guid entryId, long userId, StandardRemarkDto dto);
        Task<ResultDto> PendingHospitalStandardsEntry(Guid entryId, long facilityId);
        Task<ResultWithDataDto<MasterStandardEntryDto>> GetMasterEntryById(Guid submissionCode, long userId);

        Task<ResultWithDataDto<List<SubmissionStatusDto>>> SubmissionStatus(Guid submissionCode);

    }
}
