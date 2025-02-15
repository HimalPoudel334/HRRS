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

public class HospitalEntryDto
{
    public int Id { get; set; }
    public EntryStatus Status { get; set; }
    public string? Parichhed { get; set; }
    public string? SubParichhed { get; set; }
    public string Anusuchi { get; set; }
    public string? Remarks { get; set; }

}

public class StandardGroupModel
{
    public string? GroupName { get; set; } // Represents `GroupName`
    public List<MapdandaModel> GroupedMapdanda { get; set; } // Represents `GroupedMapdanda`
}

public class MapdandaModel
{
    public int Id { get; set; } // Represents `m.Id`
    public string Name { get; set; } // Represents `m.Mapdanda.Name`
    public string SerialNumber { get; set; } // Represents `m.Mapdanda.SerialNumber`
    public bool? Has100 { get; set; } // Represents `m.Has100`
    public bool? Has200 { get; set; } // Represents `m.Has200`
    public bool? Has50 { get; set; } // Represents `m.Has50`
    public bool? Has25 { get; set; } 
    public bool? IsAvailable { get; set; } 
    public string? FilePath { get; set; } 
    public string? Parimaad { get; set; } // Represents `m.Mapdanda.Parimaad`
    public string? Group { get; set; } // Represents `m.Mapdanda.Group`
    public bool? IsAvailableDivided { get; set; } // Represents `m.Mapdanda.IsAvailableDivided`
}

public class HospitalStandardModel
{
    public bool? HasBedCount { get; set; } // Represents `HasBedCount`
    public string SubSubParixed { get; set; } // Represents `SubSubParixed`
    public List<StandardGroupModel> List { get; set; } // Represents `List`
}

public class StandardRemarkDto
{
    public string Remarks { get; set; }
}