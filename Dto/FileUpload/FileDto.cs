
namespace HRRS.Dto.FileUpload;

public class FIleDto
{
    public DateTime InspectionDate { get; set; }
    public int HospitalId { get; set; }
    public int AnusuchiNo { get; set; }
    public int SerialNo { get; set; }
    public IFormFile File { get; set; }
}