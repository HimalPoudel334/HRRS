namespace HRRS.Dto.MasterStandardEntry;

public class SubmissionStatusDto
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public bool IsApproved { get; set; }
    public DateTime Date { get; set; }
}
