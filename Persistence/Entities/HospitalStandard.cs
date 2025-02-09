using Persistence.Entities;

public class HospitalStandard
{
    public int Id { get; set; }
    public int HealthFacilityId { get; set; }
    public int MapdandaId { get; set; }
    public bool? IsAvailable { get; set; }
    public string? Has25 { get; set; }
    public string? Has50 { get; set; }
    public string? Has100 { get; set; }
    public string? Has200 { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string? FiscalYear { get; set; }

    public HealthFacility HealthFacility { get; set; }
    public Mapdanda Mapdanda { get; set; }
}



