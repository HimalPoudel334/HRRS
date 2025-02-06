
using System.ComponentModel.DataAnnotations.Schema;

namespace HRRS.Persistence.Entities
{
    public class Anusuchi
    {
        public int Id { get; set; }
        public string AnusuchiName { get; set; }
        public string RelatedToDafaNo { get; set; } 

        [ForeignKey(nameof(Anusuchi))]
        public int? AnusuchiId { get; set; }
        public virtual Anusuchi? ParentAnusuchi { get; set; }
        public virtual ICollection<Anusuchi> SubAnusuchis { get; set; } = [];
        public virtual ICollection<MapdandaTableHeader> TableHeaders { get; set; } = [];

        public virtual ICollection<Parichhed> Parichheds { get; set; } = [];
        public virtual ICollection<Mapdanda> Mapdandas { get; set; } = [];

    }
}