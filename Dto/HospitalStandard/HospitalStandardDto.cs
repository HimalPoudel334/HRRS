using Persistence.Entities;

namespace HRRS.Dto.HealthStandard;

public class HospitalStandardDto
{
    public int HealthFacilityId { get; set; }
    public List<HospitalMapdandasDto> HospitalMapdandas { get; set; }

}


public class HospitalMapdandasDto
{
    public int MapdandaId { get; set; }
    public string? MapdandaName { get; set; }
    public bool? IsAvailable { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string? FiscalYear { get; set; }
    public bool? Status { get; set; }
}

public class HospitalStandardPartialDto {

    public int HealthFacilityId { get; set; }
    public int MapdandaId { get; set; }
    public bool IsAvailable { get; set; }
    public string Remarks { get; set; }
    public string FiscalYear { get; set; }
    public bool? Status { get; set; }
}