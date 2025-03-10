using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class BedCount
    {
        public int Id { get; set; }
        public string Count { get; set; }
        public ICollection<FacilityType> FacilityTypes { get; set; }
    }

    public class FacilityTypeBedCount
    {
        public int FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }
        public int BedCountId { get; set; }
        public BedCount BedCount { get; set; }
    }
}
