using Persistence.Entities;

namespace HRRS.Persistence.Entities;

public class Approval
{
    public long Id { get; set; }
    public long ApprovedById { get; set; }
    public User ApprovedBy { get; set; }
    public DateTime ApprovedOn { get; set; }
    public MasterStandardEntry Entry { get; set; }
    public Guid EntryId { get; set; }
}
