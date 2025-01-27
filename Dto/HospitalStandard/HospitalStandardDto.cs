using Persistence.Entities;

namespace HRRS.Dto.HealthStandard;

public class HospitalStandardDto
{
    public int Id { get; set; }
    public int HealthFacilityId { get; set; }
    public int MapdandaId { get; set; }
    public bool IsAvailable { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string FiscalYear { get; set; }
    public bool Status { get; set; }

}