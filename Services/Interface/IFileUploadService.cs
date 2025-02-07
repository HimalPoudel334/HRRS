
using HRRS.Dto.FileUpload;
using HRRS.Dto;

namespace HRRS.Services.Interface;

public interface IFileUploadService
{
    Task<ResultWithDataDto<string>> UploadFileAsync(FileDto file);
    Task<ResultWithDataDto<string>> UploadFileAsync(FileUploadDto file);
    public string GetContentType(string filename);
    //ResultWithDataDto<PhysicalFile> GetFileForPath(string filePath);
}