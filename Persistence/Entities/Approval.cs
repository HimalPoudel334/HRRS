using Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRRS.Persistence.Entities;

public class SubmissionStatus
{
    public long Id { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public ApprovalStatus Status { get; set; }

    public string Remarks { get; set; }
    public User CreatedBy { get; set; }
    public long CreatedById { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public MasterStandardEntry Entry { get; set; }
    public Guid EntryId { get; set; }
}

public enum ApprovalStatus
{
    Approved,
    Rejected
}

