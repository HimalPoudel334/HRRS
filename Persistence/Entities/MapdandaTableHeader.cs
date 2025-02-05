
namespace HRRS.Persistence.Entities
{
    public class MapdandaTableHeader
    {
        public int Id { get; set; }
        public Anusuchi Anusuchi { get; set; }
        public int AnusuchiId { get; set; }
        public Parichhed? Parichhed { get; set; }
        public int? ParichhedId { get; set; }
        public Mapdanda Mapdanda { get; set; }
        public int MapdandaId { get; set; }
        public Row Row { get; set; }
        public virtual ICollection<Column> Columns { get; set; } = [];

    }
}