
using System.ComponentModel.DataAnnotations.Schema;
using HRRS.Persistence.Entities;
using Persistence.Entities;

public class HospitalStandardEntry
{
    public int Id { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public EntryStatus Status { get; set; } = EntryStatus.Draft;
    public SubmissionType SubmissionType { get; set; } = SubmissionType.Registration;
    public MasterStandardEntry MasterStandardEntry { get; set; }
    public ICollection<HospitalStandard> HospitalStandards { get; set; } = [];
}

public enum EntryStatus
{
    Pending,
    Approved,
    Rejected,
    Draft
}

public enum SubmissionType
{
    Registration,
    Renewal
}
