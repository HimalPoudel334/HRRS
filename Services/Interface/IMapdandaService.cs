using HRRS.Dto;
using HRRS.Dto.AdminMapdanda;
using HRRS.Dto.HealthStandard;

namespace HRRS.Services.Interface;
public interface IMapdandaService
{
    Task<ResultWithDataDto<List<GroupedSubSubParichhedAndMapdanda>>> GetAdminMapdandas(HospitalStandardQueryParams dto);
    Task<ResultDto> Add(MapdandaDto dto);
    Task<ResultDto> UpdateMapdanda(int mapdandaId, MapdandaDto dto);
    Task<ResultDto> ToggleStatus(int mapdandaId);
    Task<ResultWithDataDto<List<string>>> GetMapdandaGroups(string? searchKey);

}