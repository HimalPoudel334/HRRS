
using HRRS.Dto.FileUpload;
using HRRS.Dto;

namespace HRRS.Services.Interface;

public interface IFileUploadService
{
    Task<ResultWithDataDto<string>> UploadFileAsync(FIleDto file);
    Task<ResultWithDataDto<FileUploadDto>> RemoveFileAsync(List<String> filePaths);
    public string GetContentType(string filename);
    //ResultWithDataDto<PhysicalFile> GetFileForPath(string filePath);
}