
namespace HRRS.Dto.FileUpload;

public class FileDto
{
    public int UserId { get; set; }
    public int MapdandaId { get; set; }
    public IFormFile File { get; set; }
}

public class FileUploadDto
{
    public string InspectionDate { get; set; }
    public int HospitalId { get; set; }
    public int AnusuchiId { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
    public int? SubSubParichhhedId { get; set; }
    public int? MapdandaId { get; set; }
    public int SerialNo { get; set; }
    public IFormFile File { get; set; }

}