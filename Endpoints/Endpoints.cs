using HRRS.Dto;
using HRRS.Dto.Auth;
using HRRS.Dto.FileUpload;
using HRRS.Dto.HealthStandard;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRRS.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => Results.Redirect("/scalar"));
        endpoints.MapPost("api/signin", [Authorize(Roles = "Admin")] async (LoginDto dto, IAuthService authService) =>
            TypedResults.Ok(await authService.LoginUser(dto)));

        //endpoints.MapPost("api/signup", async (RegisterDto dto, IAuthService authService) =>
        //    TypedResults.Ok(await authService.RegisterAsync(dto)));

        endpoints.MapGet("api/mapdanda/{anusuchi_id}", async (int anusuchi_id, IMapdandaService mapdandaService) =>
            TypedResults.Ok(await mapdandaService.GetByAnusuchi(anusuchi_id)));

        endpoints.MapPost("api/mapdanda/", [Authorize(Roles = "Admin")] async (MapdandaDto dto, IMapdandaService mapdandaService) =>
            TypedResults.Ok(await mapdandaService.Add(dto)));

        endpoints.MapPost("api/HealthFacility", async (HealthFacilityDto dto, IHealthFacilityService service) =>
        {            
            return TypedResults.Ok(await service.Create(dto));
        });

        endpoints.MapGet("api/HealthFacility", async (IHealthFacilityService service) =>
        {
            return TypedResults.Ok(await service.GetAll());
        });

        endpoints.MapGet("api/HealthFacility/{id}", async (int id, IHealthFacilityService service) =>
        {
            return TypedResults.Ok(await service.GetById(id));
        });

        endpoints.MapPost("api/HospitalStandard", async (HospitalStandardDto dto, IHospitalStandardService service) =>
        {
            return TypedResults.Ok(await service.Create(dto));  
        });

        endpoints.MapGet("api/HospitalStandard", async ([FromQuery] int hospitalId, [FromQuery] int anusuchiId, IHospitalStandardService service) =>
        {
            return TypedResults.Ok(await service.Get(hospitalId, anusuchiId));
        });

        endpoints.MapGet("api/GetMapdandaFile/{filePath}", (string filePath, IFileUploadService service, IConfiguration config) =>
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return Results.BadRequest(new ResultDto(false, "Filename is required"));
            }

            var path = config["FileUploadPath"]  ?? Path.Combine("Media", "Mapdanda");
            var fullPath = Path.Combine(path, filePath);

            if (!File.Exists(fullPath))
            {
                Results.NotFound(new ResultDto(false, "File not found."));
            }
            
            var contentType = service.GetContentType(fullPath);
            var fileBytes = File.ReadAllBytes(fullPath);

            return Results.File(fileBytes, contentType);

        });

        endpoints.MapPost("api/MapdandaUpload", async ([FromQuery] int hospitalId, [FromQuery] int serialNo, [FromQuery] int anusuchiNo, [FromQuery] string InspectionDate, IFormFile file, IFileUploadService service) =>
        {
            var dto = new FIleDto()
            {
                HospitalId = hospitalId,
                SerialNo = serialNo,
                AnusuchiNo = anusuchiNo,
                InspectionDate = InspectionDate,
                File = file
            };
            return TypedResults.Ok(await service.UploadFileAsync(dto));
        }).DisableAntiforgery();

        return endpoints;
    }
}
