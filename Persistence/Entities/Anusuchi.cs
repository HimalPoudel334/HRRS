
using System.ComponentModel.DataAnnotations.Schema;

namespace HRRS.Persistence.Entities
{
    public class Anusuchi
    {
        public int Id { get; set; }
        public string AnusuchiName { get; set; }
        public string RelatedToDafaNo { get; set; }

        public virtual ICollection<Parichhed>? Parichheds { get; set; }
        public virtual ICollection<Mapdanda> Mapdandas { get; set; }

    }
}