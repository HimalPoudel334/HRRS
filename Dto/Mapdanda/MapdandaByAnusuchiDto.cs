using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

public class MapdandaByAnusuchiDto
{
    public string SubSubParichhed { get; set; }
    public string SubParichhed { get; set; }
    public string Parichhed { get; set; }
    public bool IsAvailableDivided { get; set; }

    public List<GroupedMapdanda> Mapdandas { get; set; }
}


public class GroupedMapdanda
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public bool Is100Active { get; set; }
    public bool Is200Active { get; set; }
    public bool Is50Active { get; set; }
    public bool Is25Active { get; set; }
}

