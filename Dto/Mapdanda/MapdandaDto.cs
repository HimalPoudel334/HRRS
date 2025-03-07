using System.ComponentModel.DataAnnotations;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

public class MapdandaDto
{
    public int Id { get; set; }
    public int OrderNo { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string? Parimaad { get; set; }
    public int AnusuchiId { get; set; }
    public bool IsAvailableDivided { get; set; }
    public bool Is25Active { get; set; }
    public bool Is50Active { get; set; }
    public bool Is100Active { get; set; }
    public bool Is200Active { get; set; }
    public string? Value25 { get; set; }
    public string? Value50 { get; set; }
    public string? Value100 { get; set; }
    public string? Value200 { get; set; }
    public string? Col5 { get; set; }
    public string? Col6 { get; set; }
    public string? Col7 { get; set; }
    public string? Col8 { get; set; }
    public string? Col9 { get; set; }
    public bool IsCol5Active { get; set; }
    public bool IsCol6Active { get; set; }
    public bool IsCol7Active { get; set; }
    public bool IsCol8Active { get; set; }
    public bool IsCol9Active { get; set; }
    public FormType FormType { get; set; }
    public bool Status { get; set; }
    public string? Group { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
    public bool IsGroup { get; set; } = false;
    public bool IsSubGroup { get; set; } = false;
    public bool IsSection { get; set; } = false;
    public bool HasGroup { get; set; } = false;

}


public class MapdandaTableDto
{
    public int Id { get; set; }
    public string? TableName { get; set; }
    public int TableNumber { get; set; }
    public int AnusuchiId { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
    public string? Description { get; set; }
    public string? Note { get; set; }
    public FormType FormType { get; set; }
    public ICollection<MapdandaDto> Mapdandas { get; set; } = [];
}

