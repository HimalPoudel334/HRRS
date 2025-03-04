using Persistence.Entities;

namespace HRRS.Dto.HealthStandard;

public class HospitalStandardPartialDto {

    public int HealthFacilityId { get; set; }
    public int MapdandaId { get; set; }
    public bool IsAvailable { get; set; }
    public string Remarks { get; set; }
    public string FiscalYear { get; set; }
    public bool? Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class HospitalMapdandasDto
{
    public int MapdandaId { get; set; }
    public int? EntryId { get; set; }
    public string SerialNumber { get; set; }
    public string? MapdandaName { get; set; }
    public bool? IsAvailable { get; set; }
    public string? Remarks { get; set; }
    public string? FilePath { get; set; }
    public string? FiscalYear { get; set; }
    public bool Status { get; set; }
    public bool IsApproved { get; set; }

}

public class HospitalStandardDto
{
    public Guid SubmissionCode { get; set; }
    public ICollection<HospitalMapdandasDto> Mapdandas { get; set; } = [];
}

public class HospitalEntryDto
{
    public int Id { get; set; }
    public EntryStatus Status { get; set; }
    public string? Parichhed { get; set; }
    public string? SubParichhed { get; set; }
    public string Anusuchi { get; set; }
    public string? Remarks { get; set; }
    public string SubmissionType { get; set; }

}

public class StandardGroupModel
{
    public string? SerialNumber { get; set; }
    public string? GroupName { get; set; }
    public List<MapdandaModel> GroupedMapdanda { get; set; }
}

public class MapdandaModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public bool? IsAvailable { get; set; } 
    public string? FilePath { get; set; } 
    public string? Parimaad { get; set; }
    public string? Group { get; set; }
    public string? Value { get; set; }
    public int EntryId { get; set; }
    public bool? IsApproved { get; set; }
    public string? Remarks { get; set; }

    public bool? IsAvailableDivided { get; set; }
}

public class HospitalStandardModel
{
    public int? AnusuchiId { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
    public FormType FormType { get; set; }
    public bool? HasBedCount { get; set; }
    public string SubSubParixed { get; set; }
    public List<StandardGroupModel> List { get; set; }


}
public class StandardRemarkDto
{
    public string Remarks { get; set; }
}

public class HospitalStandardQueryParams
{
    public int? AnusuchiId { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
}

public class StandardApprovalDto
{
    public int EntryId { get; set; }
    public bool IsApproved { get; set; }
    public string? Remarks { get; set; }
}