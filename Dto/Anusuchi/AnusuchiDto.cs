
using HRRS.Dto.Parichhed;

namespace HRRS.Dto.Anusuchi;

public class AnusuchiDto
{
    public int? Id { get; set; }
    public string SerialNo { get; set; }
    public string Name { get; set; }
    public string DafaNo { get; set; }
}

public class AnusuchiQueryParams
{
    public int? Id { get; set; }
    public Guid? SubmissionCode { get; set; }
}