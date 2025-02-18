using System.ComponentModel.DataAnnotations;
using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class MasterStandardEntry
    {
        [Key]
        public Guid SubmissionCode { get; set; }
        public HealthFacility HealthFacility { get; set; }
        public int BedCount { get; set; }
        public int HealthFacilityId { get; set; }
        public EntryStatus EntryStatus { get; set; } = EntryStatus.Draft;
        public string? Remarks { get; set; }
        public SubmissionType SubmissionType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<HospitalStandardEntry> HospitalStandardEntries { get; set; } = [];
    }
}
