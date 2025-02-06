
using HRRS.Dto.Anusuchi;
using HRRS.Dto.MapdandaTableValue;
using HRRS.Dto.Parichhed;

namespace HRRS.Dto.MapdandaTableHeader
{
    public class MapdandaTableHeaderDto
    {
        public int? Id { get; set; }
        public string CellName { get; set; }
        public int? ParentCellId { get; set; }
        public ICollection<MapdandaTableHeaderDto> SubCells { get; set; } = [];
        public int AnusuchiId { get; set; }
        public AnusuchiDto AnusuchiDto { get; set; }
        public int? ParichhedId { get; set; }
        public ParichhedDto? ParichhedDto { get; set; }
        public ICollection<MapdandaTableValueDto> MapdandaTableValue { get; set; } = [];
    }
}