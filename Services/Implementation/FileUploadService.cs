
using HRRS.Dto;
using HRRS.Dto.FileUpload;
using HRRS.Services.Interface;

namespace HRRS.Services.Implementation;

public class FileUploadService : IFileUploadService
{
    private readonly string _fileUploadPath;

    public FileUploadService(IConfiguration configuration, IWebHostEnvironment env)
    {
        var appRoot = env.ContentRootPath;
        _fileUploadPath = Path.Combine(appRoot, configuration["FileUploadPath"] ?? Path.Combine("Media", "Mapdanda"));
        
        if (!Directory.Exists(_fileUploadPath))
        {
            Directory.CreateDirectory(_fileUploadPath);
        }
    }

    public async Task<ResultWithDataDto<string>> UploadFileAsync(FIleDto dto)
    {
        if (dto.File.Length == 0)
        {
            throw new Exception("File is empty");
        }

        var uniqueFileName = $"H{dto.HospitalId}-A{dto.AnusuchiNo}-SN{dto.SerialNo}-{dto.File.FileName}";
        var filePath = Path.Combine(_fileUploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }
        return new ResultWithDataDto<string>(true, uniqueFileName, null);
    }

    public async Task<ResultWithDataDto<FileUploadDto>> RemoveFileAsync(List<String> filePaths)
    {
        if (filePaths == null || filePaths.Count == 0)
        {
            throw new Exception("File paths are required.");
        }

        List<string> deletedFiles = [];
        List<string> notFoundFiles = [];
        List<string> errors = [];

        foreach (string filePath in filePaths)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                errors.Add($"File path is null or empty.");
                continue;
            }

            try
            {
                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    deletedFiles.Add(filePath);
                }
                else
                {
                    notFoundFiles.Add(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
                errors.Add($"Error deleting {filePath}: {ex.Message}");
            }
        }

        var fileUploadDto = new FileUploadDto()
        {
            DeletedFiles = deletedFiles,
            NotFoundFiles = notFoundFiles,
            Errors = errors
        };

        return new ResultWithDataDto<FileUploadDto>(true, fileUploadDto, null);
    }

    public ResultWithDataDto<FileStream> GetFileForPath(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return new ResultWithDataDto<FileStream>(false, null, "File path is required.");
        }
        if (!File.Exists(filePath))
        {
            return new ResultWithDataDto<FileStream>(false, null, "File not found.");
        }

        try
        {            
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true); // true for async

            return new ResultWithDataDto<FileStream>(true, fileStream, null);
        }
        catch (Exception ex)
        {
            return new ResultWithDataDto<FileStream>(false, null, $"Error opening file: {ex.Message}"); // Return error message
        }
    }

    // Helper function to get MIME type (you can use a more comprehensive library if needed).
    public string GetContentType(string filename)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filename, out var contentType))
        {
            contentType = "application/octet-stream"; // Default type if unknown
        }
        return contentType;

    }
}