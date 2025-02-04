
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace HRRS.Persistence.Entities;

public class Parichhed
{
    public int Id { get; set; }
    public string ParichhedName { get; set; }


    public Anusuchi Anusuchi { get; set; }
    public int AnusuchiId { get; set; }

    [ForeignKey(nameof(Parichhed))]
    public int? ParichhedId { get; set; }
    public virtual ICollection<Parichhed> SubParichheds { get; set; }

    public virtual ICollection<Mapdanda>? Mapdandas { get; set; }
}



