﻿using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IMasterStandardEntryService
    {
        Task<ResultWithDataDto<string>> Create(SubmissionTypeDto dto, int healthFacilityId);
        Task<ResultWithDataDto<List<MasterStandardEntryDto>>> GetByHospitalId(int healthFacilityId, long userId);
        Task<ResultWithDataDto<List<MasterStandardEntryDto>>> GetByUserHospitalId(int healthFacilityId);
        Task<ResultDto> UserFinalSubmission(Guid submissionCode);
        Task<ResultDto> ApproveStandardsWithRemark(Guid entryId, StandardRemarkDto dto, long userId);
        Task<ResultDto> RejectStandardsWithRemark(Guid entryId, StandardRemarkDto dto, long userId);
        Task<ResultDto> PendingHospitalStandardsEntry(Guid entryId, long facilityId);
        Task<ResultWithDataDto<MasterStandardEntryDto>> GetMasterEntryById(Guid submissionCode);
        Task<ResultWithDataDto<List<SubmissionType>>> GetAllSubmissionTypes();
        Task<ResultWithDataDto<List<MasterStandardEntryDto>>> GetAllNewSubmission(long userId);
        Task<ResultWithDataDto<int>> GetNewSubmissionCount(long userId);
        Task<ResultDto> SifarisToPramukh(Guid submissionCode, long userId);

    }
}
