using Persistence.Entities;

public class HospitalStandard
{
    public int Id { get; set; }
    public int HealthFacilityId { get; set; }
    public int MapdandaId { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? Has25 { get; set; }
    public bool? Has50 { get; set; }
    public bool? Has100 { get; set; }
    public bool? Has200 { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string? FiscalYear { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool Status { get; set; } = false;
    public Mapdanda Mapdanda { get; set; }
    public HospitalStandardEntry StandardEntry { get; set; }
    public int StandardEntryId { get; set; }
}



