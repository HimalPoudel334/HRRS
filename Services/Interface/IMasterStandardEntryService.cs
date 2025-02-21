﻿using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IMasterStandardEntryService
    {
        Task<ResultWithDataDto<string>> Create(SubmissionTypeDto dto, int healthFacilityId);
        Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByHospitalId(int healthFacilityId);
        Task<ResultWithDataDto<List<MasterStandardEntry>>> GetByUserHospitalId(int healthFacilityId);
        Task<ResultDto> UserFinalSubmission(Guid submissionCode);
        Task<ResultDto> ApproveStandardsWithRemark(Guid entryId, StandardRemarkDto dto);
        Task<ResultDto> RejectStandardsWithRemark(Guid entryId, StandardRemarkDto dto);
        Task<ResultDto> PendingHospitalStandardsEntry(Guid entryId, long facilityId);

    }
}
