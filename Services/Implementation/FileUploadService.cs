
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
    private readonly string _mapdandaUploadPath;
    private readonly string _facilityUploadPath;
    private readonly ApplicationDbContext _context;

    public FileUploadService(IConfiguration configuration, IWebHostEnvironment env, ApplicationDbContext context)
    {
        var appRoot = env.ContentRootPath;
        _mapdandaUploadPath = Path.Combine(appRoot, configuration["FileUploadPaths:MapdandaUpload"] ?? Path.Combine("Media", "Mapdanda"));
        
        if (!Directory.Exists(_mapdandaUploadPath))
        {
            Directory.CreateDirectory(_mapdandaUploadPath);
        }

        _facilityUploadPath = Path.Combine(appRoot, configuration["FacilityUpload:HealthFacility"] ?? Path.Combine("Media", "HealthFacility"));

        if (!Directory.Exists(_facilityUploadPath))
        {
            Directory.CreateDirectory(_facilityUploadPath);
        }

        _context = context;
    }

    public async Task<ResultWithDataDto<string>> UploadFileAsync(FileDto dto)
    {
        if (dto.File.Length == 0)
        {
            return ResultWithDataDto<string>.Failure("File is empty");
        }

        //var user = await _context.Users.Include(x => x.HealthFacility).FirstOrDefaultAsync(x => x.UserId==dto.UserId);
        //if(user is null || user.HealthFacility == null)
        //{
        //    return ResultWithDataDto<string>.Failure("Hospital not found");
        //}

        var uniqueFileName = $"H{Guid.NewGuid()}-M{dto.MapdandaId}{Path.GetExtension(dto.File.FileName)}";
        var filePath = Path.Combine(_mapdandaUploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        return ResultWithDataDto<string>.Success(uniqueFileName);
    }

    public async Task<string> UploadFacilityFileAsync(IFormFile file)
    {
        if (file.Length == 0)
        {
            return "File is empty";
        }

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_mapdandaUploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;

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