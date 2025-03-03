using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class AnusuchiMapping
    {
        public int Id { get; set; }
        public int FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }
        public int BedCountId { get; set; }
        public BedCount BedCount { get; set; }
        public int SubmissionTypeId { get; set; }
        public SubmissionType SubmissionType { get; set; }
        public int MapdandaTableId { get; set; }
        public MapdandaTable MapdandaTable { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
