
using HRRS.Dto;
using HRRS.Dto.FileUpload;
using HRRS.Services.Interface;

namespace HRRS.Services.Implementation;

public class FileUploadService : IFileUploadService
{
    private readonly IConfiguration _configuration;

    private readonly string _fileUploadPath;

    public FileUploadService(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        var appRoot = env.ContentRootPath;
        _fileUploadPath = Path.Combine(appRoot, _configuration["FileUploadPath"] ?? Path.Combine("Media", "Mapdanda"));

        if (!Directory.Exists(_fileUploadPath))
        {
            Directory.CreateDirectory(_fileUploadPath);
        }
    }

    public async Task<ResultWithDataDto<string>> UploadFileAsync(FIleDto dto)
    {
        if (dto.file.Length == 0)
        {
            throw new Exception("File is empty");
        }

        var uniqueFileName = $"H{dto.HospitalId}-A{dto.AnusuchiNo}-SN{dto.SerialNo}-{dto.file.FileName}";  // Or $"{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}"
        var filePath = Path.Combine(_fileUploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.file.CopyToAsync(stream);
        }
        return new ResultWithDataDto<string>(true, filePath, null);
    }

    public async Task<ResultWithDataDto<FileUploadDto>> RemoveFileAsync(List<String> filePaths)
    {
        if (filePaths == null || filePaths.Count == 0) // Check for null or empty list
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
                continue; // Skip to the next file
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
                errors.Add($"Error deleting {filePath}: {ex.Message}"); // Add specific error
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
}