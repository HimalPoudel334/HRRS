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
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

    public class AnusuchiMapdandaTableMapping
    {
        public int Id { get; set; }
        public int AnusuchiMappingId { get; set; }
        public AnusuchiMapping AnusuchiMapping { get; set; }

        public int AnusuchiId { get; set; }
        public Anusuchi Anusuchi { get; set; }

        public int? ParichhedId { get; set; }
        public Parichhed? Parichhed { get; set; }
    }
}
