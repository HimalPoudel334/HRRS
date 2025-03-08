using HRRS.Dto;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;
public interface IMapdandaService
{
    Task<ResultWithDataDto<MapdandaTableDto>> GetAdminMapdandas(HospitalStandardQueryParams dto);
    Task<ResultDto> Add(MapdandaDto dto);
    Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto dto);
    Task<ResultDto> ToggleStatus(int mapdandaId);
    Task<ResultWithDataDto<FormType?>> GetFormTypeForMapdanda(HospitalStandardQueryParams dto);

}