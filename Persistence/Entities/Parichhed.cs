
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
    public ICollection<Parichhed>? SubParichheds { get; set; }

    public ICollection<Mapdanda>? Mapdandas { get; set; }
}



