using System.ComponentModel.DataAnnotations;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

//public class MapdandaDto
//{
//    public int Id { get; set; }
//    public string? SerialNumber { get; set; }
//    public string Name { get; set; }
//    public int AnusuchiNumber { get; set; }
//    public bool Status { get; set; }
//}

public class MapdandaDto
{
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string? Parimaad { get; set; }
    public int AnusuchiId { get; set; }
    public bool IsAvailableDivided { get; set; }
    public bool Is25Active { get; set; }
    public bool Is50Active { get; set; }
    public bool Is100Active { get; set; }
    public bool Is200Active { get; set; }
    public bool Status { get; set; }
    public string? Group { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
    public SubSubParichhedDto SubSubParichhed { get; set; }
    public int? SubSubParichhedId { get; set; }

}

public class SubMapdandaDto
{
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string? Parimaad { get; set; }
    public int MapdandaId { get; set; }
    public bool Status { get; set; }
    public string? Group { get; set; }

}
