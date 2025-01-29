
namespace HRRS.Dto.FileUpload;

public class FIleDto
{
    public DateTime InceptionDate { get; set; }
    public int HospitalId { get; set; }
    public int AnusuchiNo { get; set; }
    public int SerialNo { get; set; }
    public IFormFile file { get; set; }
}