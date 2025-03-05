
namespace HRRS.Dto.Parichhed;

public class ParichhedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public int AnusuchiId { get; set; }
}

public class SubParichhedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public int ParichhedId { get; set; }
}

public class SubSubParichhedDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public int SubParichhedId { get; set; }
}

public class ParichhedQueryParams
{
    public int? AnusuchiId { get; set; }
    public Guid? SubmissionCode { get; set; }
}