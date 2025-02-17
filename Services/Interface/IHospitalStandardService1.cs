
using HRRS.Dto;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService1
{
    Task<ResultDto> Create(HospitalStandardDto1 dto, int id);
    Task<ResultDto> Update(HospitalStandardDto1 dto, int id);
    Task<ResultWithDataDto<List<HospitalStandardModel>>> GetHospitalStandardForEntry(int entryId);
    Task<ResultWithDataDto<HospitalEntryDto>> GetHospitalEntryById(int entryId);
    Task<ResultWithDataDto<List<MasterStandardEntryDto>>> AdminGetMasterStandardsEntry(int hospitalId);
    Task<ResultWithDataDto<List<MasterStandardEntryDto>>> UserGetMasterStandardsEntry(int hospitalId);
    Task<ResultWithDataDto<List<HospitalEntryDto>>> GetStandardEntries(Guid submissionCode);
    Task<ResultWithDataDto<List<HospitalStandard>>> GetStandards(int entryId);



}