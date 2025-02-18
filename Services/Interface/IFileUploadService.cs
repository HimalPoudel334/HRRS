
using HRRS.Dto.FileUpload;
using HRRS.Dto;

namespace HRRS.Services.Interface;

public interface IFileUploadService
{
    Task<ResultWithDataDto<string>> UploadFileAsync(FileDto file);
    public string GetContentType(string filename);
}