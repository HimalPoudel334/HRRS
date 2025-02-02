
using System.ComponentModel.DataAnnotations.Schema;

namespace HRRS.Persistence.Entities
{
    public class Anusuchi
    {
        public int Id { get; set; }
        public string AnusuchiName { get; set; }
        public string RelatedToDafaNo { get; set; }

        public ICollection<Parichhed>? Parichheds { get; set; }
        public ICollection<Mapdanda>? Mapdandas { get; set; }

    }
}