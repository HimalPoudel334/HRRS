using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class BedCount
    {
        public int Id { get; set; }
        public string Count { get; set; }
        public FacilityType FacilityType { get; set; }
    }
}
