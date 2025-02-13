
using System.Text;
using HRRS.Dto;
using HRRS.Dto.FileUpload;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HRRS.Services.Implementation;

public class FileUploadService : IFileUploadService
{
    private readonly string _fileUploadPath;
    private readonly ApplicationDbContext _context;

    public FileUploadService(IConfiguration configuration, IWebHostEnvironment env, ApplicationDbContext context)
    {
        var appRoot = env.ContentRootPath;
        _fileUploadPath = Path.Combine(appRoot, configuration["FileUploadPath"] ?? Path.Combine("Media", "Mapdanda"));
        
        if (!Directory.Exists(_fileUploadPath))
        {
            Directory.CreateDirectory(_fileUploadPath);
        }

        _context = context;
    }

    public async Task<ResultWithDataDto<string>> UploadFileAsync(FileDto dto)
    {
        if (dto.File.Length == 0)
        {
            return ResultWithDataDto<string>.Failure("File is empty");
        }

        var user = await _context.Users.Include(x => x.HealthFacility).FirstOrDefaultAsync(x => x.UserId==dto.UserId);
        if(user is null || user.HealthFacility == null)
        {
            return ResultWithDataDto<string>.Failure("Hospital not found");
        }

        var uniqueFileName = $"H{user.HealthFacilityId}-M{dto.MapdandaId}{Path.GetExtension(dto.File.FileName)}";
        var filePath = Path.Combine(_fileUploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        return ResultWithDataDto<string>.Success(uniqueFileName);
    }

    public async Task<ResultWithDataDto<string>> UploadFileAsync(FileUploadDto dto)
    {
        if (dto.File.Length == 0)
        {
            return ResultWithDataDto<string>.Failure("File is empty");
        }
        var builder = new StringBuilder();
        builder.Append($"H{dto.HospitalId}-A{dto.AnusuchiId}-SN{dto.SerialNo}");
        if (dto.ParichhedId.HasValue)
        {
            builder.Append($"-P{dto.ParichhedId}");
        }
        if (dto.SubParichhedId.HasValue)
        {
            builder.Append($"-SP{dto.SubParichhedId}");
        }
        if (dto.SubSubParichhhedId.HasValue)
        {
            builder.Append($"-SSP{dto.SubSubParichhhedId}");
        }
        if (dto.MapdandaId.HasValue)
        {
            builder.Append($"-M{dto.MapdandaId}");
        }

        var uniqueFileName = $"{builder}{Path.GetExtension(dto.File.FileName)}";

        //var uniqueFileName = $"H{dto.HospitalId}-A{dto.AnusuchiId}-SN{dto.SerialNo}{Path.GetExtension(dto.File.FileName)}";
        var filePath = Path.Combine(_fileUploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        return ResultWithDataDto<string>.Success(uniqueFileName);
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