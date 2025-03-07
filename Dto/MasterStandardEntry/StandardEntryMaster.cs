using System.ComponentModel.DataAnnotations;
using HRRS.Persistence.Entities;
using Persistence.Entities;

namespace HRRS.Dto.MasterStandardEntry
{
    public class MasterStandardEntryDto
    {
        public Guid SubmissionCode { get; set; }
        public int HealthFacilityId { get; set; }
        public string? HealthFacility { get; set; }
        public EntryStatus EntryStatus { get; set; }
        public string? Remarks { get; set; }
        public string SubmissionType { get; set; }
        public ApprovalStatus? Decision { get; set; }
        public bool HasNewSubmission { get; set; }
    }

    public class SubmissionTypeDto 
    {
        public int Type { get; set; }
    }
}
