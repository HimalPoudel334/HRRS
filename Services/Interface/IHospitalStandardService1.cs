
using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;

public interface IHospitalStandardService1
{
    Task<ResultDto> Create(List<HospitalMapdandasDto1> dto, int id);
    Task<ResultWithDataDto<List<HospitalMapdandasDto1>>> Get(int hospitalId, int anusuchiId);

    Task<ResultWithDataDto<List<HospitalEntryDto>>> GetHospitalStandardsEntry(int hospitalId, long userId);
    Task<ResultWithDataDto<List<HospitalStandardModel>>> GetHospitalStandardForEntry(int entryId);
    Task<ResultDto> ApproveStandardsWithRemark(int entryId, StandardRemarkDto dto);
    Task<ResultDto> RejectStandardsWithRemark(int entryId, StandardRemarkDto dto);
    Task<ResultWithDataDto<HospitalEntryDto>> GetHospitalEntryById(int entryId);
    Task<ResultDto> PendingHospitalStandardsEntry(int entryId, long userId);
}