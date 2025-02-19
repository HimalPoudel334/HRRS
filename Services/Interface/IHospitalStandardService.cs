
using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.Mapdanda;
using HRRS.Dto.MasterStandardEntry;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService
{
    Task<ResultDto> Create(HospitalStandardDto dto, int id);
    Task<ResultDto> Update(HospitalStandardDto dto, int id);
    Task<ResultWithDataDto<List<HospitalStandardModel>>> AdminGetHospitalStandardForEntry(int entryId);
    Task<ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>> GetHospitalStandardForEntry(Guid submissionCode, HospitalStandardQueryParams dto, int healthFacilityId);
    Task<ResultWithDataDto<HospitalEntryDto>> GetHospitalEntryById(int entryId);
    Task<ResultWithDataDto<List<MasterStandardEntryDto>>> AdminGetMasterStandardsEntry(int hospitalId);
    Task<ResultWithDataDto<List<MasterStandardEntryDto>>> UserGetMasterStandardsEntry(int hospitalId);
    Task<ResultWithDataDto<List<HospitalEntryDto>>> GetStandardEntries(Guid submissionCode);
    Task<ResultWithDataDto<List<HospitalStandard>>> GetStandards(int entryId);



}