
using System.ComponentModel.DataAnnotations.Schema;
using HRRS.Persistence.Entities;

public class Mapdanda
{

    public int  Id { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string? Parimaad { get; set; }
    public string? Group { get; set; }
    public bool IsAvailableDivided { get; set; }
    public bool Is25Active { get; set; }
    public bool Is50Active { get; set; }
    public bool Is100Active { get; set; }
    public bool Is200Active { get; set; }
    public bool IsCol5Active { get; set; }
    public bool IsCol6Active { get; set; }
    public bool IsCol7Active { get; set; }
    public bool IsCol8Active { get; set; }
    public bool IsCol9Active { get; set; }
    public string? Value25 { get; set; }
    public string? Value50 { get; set; }
    public string? Value100 { get; set; }
    public string? Value200 { get; set; }
    public string? Col5 { get; set; }
    public string? Col6 { get; set; }
    public string? Col7 { get; set; }
    public string? Col8 { get; set; }
    public string? Col9 { get; set; }
    public bool Status { get; set; } = true;
    public int AnusuchiId { get; set; }
    public Anusuchi Anusuchi { get; set; }

    public int? ParichhedId { get; set; }
    public Parichhed? Parichhed { get; set; }

    public int? SubParichhedId { get; set; }
    public SubParichhed? SubParichhed { get; set; }

    public int? SubSubParichhedId { get; set; }
    public SubSubParichhed? SubSubParichhed { get; set; }
    public FormType FormType { get; set; }
    public int TableNumber { get; set; }

    public ICollection<SubMapdanda> SubMapdandas { get; set; }

}


public enum FormType
{
    A1, //A1 to A3
    A4, //A4 all
    A5P3, //A5p1 to p3 
    A5P4, 
    A5P8,
    A5P8_10,
    A5P10,

}

public class SubMapdanda
{
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string? Parimaad { get; set; }
    public int MapdandaId { get; set; }
    public Mapdanda Mapdanda { get; set; }
    public bool Status { get; set; }

}

