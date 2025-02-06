

using HRRS.Dto.HealthStandard;
using HRRS.Dto.MapdandaTableHeader;

namespace HRRS.Dto.MapdandaTableValue
{
    public class MapdandaTableValueDto
    {
        public int Id { get; set; }

        public string Value { get; set; }
        public bool IsDisabled { get; set; } = false;

        public int MapdandaTableHeaderId { get; set; }

        public int HospitalStandardId { get; set; }
    }
}