
namespace HRRS.Dto.FileUpload;

public class FileUploadDto
{
    public List<String> DeletedFiles { get; set; }
    public List<String> NotFoundFiles { get; set; }
    public List<String> Errors { get; set; }
}