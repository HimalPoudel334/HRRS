
using System.ComponentModel.DataAnnotations.Schema;

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
    public bool Status { get; set; } = true;
    public int AnusuchiId { get; set; }
    public Anusuchi Anusuchi { get; set; }

    public int? ParichhedId { get; set; }
    public Parichhed? Parichhed { get; set; }

    public int? SubParichhedId { get; set; }
    public SubParichhed? SubParichhed { get; set; }

    public int? SubSubParichhedId { get; set; }
    public SubSubParichhed? SubSubParichhed { get; set; }

    public ICollection<SubMapdanda> SubMapdandas { get; set; }
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

