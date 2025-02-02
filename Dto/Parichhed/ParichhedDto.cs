
using HRRS.Dto.Anusuchi;

namespace HRRS.Dto.Parichhed;

public class ParichhedDto
{
    public int Id { get; set; }
    public string ParichhedName { get; set; }

    public AnusuchiDto? Anusuchi { get; set; }
    public int AnusuchiId { get; set; }

    public ICollection<ParichhedDto>? SubParichheds { get; set; }
    public ICollection<MapdandaDto>? Mapdandas { get; set; }
}