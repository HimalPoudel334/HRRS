

using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

public class MapdandaDto
{
    public int Id { get; set; }
    public int SerialNumber { get; set; }
    public string Name { get; set; }
    public int AnusuchiId { get; set; }
    public AnusuchiDto Anusuchi { get; set; }
    public int? ParichhedId { get; set; }
    public ParichhedDto? Parichhed { get; set; }

    public int? SubParichhedId { get; set; }
    public ParichhedDto? SubParichhed { get; set; }

    public ICollection<MapdandaDto>? SubMapdandas { get; set; }
}