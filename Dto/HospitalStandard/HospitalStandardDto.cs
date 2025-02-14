using Persistence.Entities;

namespace HRRS.Dto.HealthStandard;

public class HospitalStandardDto
{
    public int HealthFacilityId { get; set; }
    public required List<HospitalMapdandasDto> HospitalMapdandas { get; set; }

}


public class HospitalMapdandasDto
{
    public int StandardId { get; set; }
    public int MapdandaId { get; set; }
    public string SerialNumber { get; set; }
    public string? MapdandaName { get; set; }
    public bool? IsAvailable { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string? FiscalYear { get; set; }
}

public class HospitalStandardPartialDto {

    public int HealthFacilityId { get; set; }
    public int MapdandaId { get; set; }
    public bool IsAvailable { get; set; }
    public string Remarks { get; set; }
    public string FiscalYear { get; set; }
    public bool? Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class HospitalMapdandasDto1
{
    public int StandardId { get; set; }
    public int MapdandaId { get; set; }
    public string SerialNumber { get; set; }
    public bool? Has25 { get; set; }
    public bool? Has50 { get; set; }
    public bool? Has100 { get; set; }
    public bool? Has200 { get; set; }
    public string? MapdandaName { get; set; }
    public bool? IsAvailable { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string? FiscalYear { get; set; }
    public bool Status { get; set; }
}