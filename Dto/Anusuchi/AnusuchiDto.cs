
using HRRS.Dto.MapdandaTableHeader;
using HRRS.Dto.Parichhed;

namespace HRRS.Dto.Anusuchi;

public class AnusuchiDto
{
    public int? Id { get; set; }
    public string AnusuchiName { get; set; }
    public string RelatedToDafaNo { get; set; }
    public ICollection<MapdandaTableHeaderDto> TableHeaders { get; set; } = [];
    public ICollection<ParichhedDto> Parichheds { get; set; } = [];
    public ICollection<MapdandaDto> Mapdandas { get; set; } = [];

}

public class AnusuchiUpdateDto
{
    public string AnusuchiName { get; set; }
    public string RelatedToDafaNo { get; set; }

}

