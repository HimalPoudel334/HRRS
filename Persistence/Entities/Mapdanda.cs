
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using HRRS.Persistence.Entities;

public class Mapdanda
{
    public int  Id { get; set; }
    public int SerialNumber { get; set; }
    public string Name { get; set; }
    public int AnusuchiId { get; set; }
    public Anusuchi Anusuchi { get; set; }
    public int? ParichhedId { get; set; }
    
    public Parichhed? Parichhed { get; set; }

    [ForeignKey(nameof(Mapdanda))]
    public int? MapdandaId { get; set; }
    public virtual ICollection<Mapdanda> SubMapdandas { get; set; }

}


