
using HRRS.Dto.FileUpload;
using HRRS.Dto;

namespace HRRS.Services.Interface;

public interface IFileUploadService
{
    Task<ResultWithDataDto<string>> UploadFileAsync(FileDto file);
    Task<string> UploadFacilityFileAsync(IFormFile file);
    string GetContentType(string filename);
    string GetHealthFacilityFilePath(string fileName);
}