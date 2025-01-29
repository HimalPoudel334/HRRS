
using HRRS.Dto.FileUpload;
using HRRS.Dto;

namespace HRRS.Services.Interface;

public interface IFileUploadService
{
    Task<ResultWithDataDto<string>> UploadFileAsync(IFormFile file);
    Task<ResultWithDataDto<FileUploadDto>> RemoveFileAsync(List<String> filePaths);
}