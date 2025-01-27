using Persistence.Entities;

namespace HRRS.Dto.HealthStandard;

public class HealthStandardDto
{
    public int Id { get; set; }
    public int HealthFacilityId { get; set; }
    public HealthFacility HealthFacility { get; set; }
    public int MadpandaId { get; set; }
    public Mapdanda Mapdanda { get; set; }
    public bool IsAvailable { get; set; }
    public string? Remarks { get; set; }
    public bool HasFile { get; set; }
    public string? FilePath { get; set; }
    public string FiscalYear { get; set; }
    public bool Status { get; set; }

}