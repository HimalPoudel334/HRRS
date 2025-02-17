using System.ComponentModel.DataAnnotations;
using Persistence.Entities;

namespace HRRS.Dto.MasterStandardEntry
{
    public class MasterStandardEntryDto
    {
        [Key]
        public Guid SubmissionCode { get; set; }
        public int HealthFacilityId { get; set; }
        public EntryStatus EntryStatus { get; set; }
        public string? Remarks { get; set; }
        public SubmissionType SubmissionType { get; set; }
    }

    public class SubmissionTypeDto 
    {
        public SubmissionType Type { get; set; }
    }
}
