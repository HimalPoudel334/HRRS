
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class MapdandaTableHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CellName { get; set; }

        [ForeignKey(nameof(MapdandaTableHeader))]
        public int? ParentCellId { get; set; }
        public MapdandaTableHeader? ParentCell { get; set; }
        public virtual ICollection<MapdandaTableHeader> SubCells { get; set; } = [];

        public virtual ICollection<MapdandaTableValue> TableValues { get; set; } = [];

        public Anusuchi Anusuchi { get; set; }
        public int AnusuchiId { get; set; }
        public Parichhed? Parichhed { get; set; }
        public int? ParichhedId { get; set; }

    }

    public class MapdandaTableValue
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }
        public bool IsDisabled { get; set; } = false;

        public int MapdandaTableHeaderId { get; set; }
        public MapdandaTableHeader MapdandaTableHeader { get; set; }

        public int HospitalStandardId { get; set; }
        public HospitalStandard HospitalStandard { get; set; }

    }
}