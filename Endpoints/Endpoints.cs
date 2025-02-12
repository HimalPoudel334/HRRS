using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Auth;
using HRRS.Dto.FileUpload;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.Mapdanda;
using HRRS.Dto.Parichhed;
using HRRS.Persistence.Context;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => Results.Redirect("/scalar"));

        endpoints.MapPost("api/signin", async (LoginDto dto, IAuthService authService) =>
            TypedResults.Ok(await authService.LoginUser(dto)));

        endpoints.MapPost("api/signup", [Authorize(Roles = "SuperAdmin")] async (RegisterDto dto, IAuthService authService) =>
            TypedResults.Ok(await authService.RegisterAdminAsync(dto)));

        endpoints.MapPost("api/healthfacility/register", async (RegisterHospitalDto dto, IAuthService service) =>
        {
            return TypedResults.Ok(await service.RegisterHospitalAsync(dto));
        });

        endpoints.MapGet("api/mapdanda/", async ([FromQuery] string? anusuchiId, IMapdandaService mapdandaService) =>
        {
            int? parsedId = string.IsNullOrWhiteSpace(anusuchiId) ? null : int.Parse(anusuchiId);
            return TypedResults.Ok(await mapdandaService.GetByAnusuchi(parsedId));
        });

        endpoints.MapPost("api/mapdanda/", [Authorize(Roles = "SuperAdmin")] async (MapdandaDto dto, IMapdandaService mapdandaService) =>
            TypedResults.Ok(await mapdandaService.Add(dto)));

        endpoints.MapPost("api/mapdanda/{mapdandaId}/update", [Authorize(Roles = "SuperAdmin")] async (int mapdandaId, MapdandaDto dto, IMapdandaService mapdandaService) =>
            TypedResults.Ok(await mapdandaService.UpdateMapdanda(mapdandaId, dto)));

        endpoints.MapPost("api/Mapdanda/{mapdandaId}/toggle-status", async (int mapdandaId, IMapdandaService service) => TypedResults.Ok(await service.ToggleStatus(mapdandaId)));

        endpoints.MapGet("api/HealthFacility", async (IHealthFacilityService service, HttpContext context) =>
        {
            return TypedResults.Ok(await service.GetAll(context));
        }).RequireAuthorization();

        endpoints.MapPost("api/HealthFacility", [Authorize(Roles = "SuperAdmin")] async (RegisterHospitalDto dto, IAuthService service) =>
        {
            return TypedResults.Ok(await service.RegisterHospitalAsync(dto));
        }).RequireAuthorization();


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

            var path = config["FileUploadPath"] ?? Path.Combine("Media", "Mapdanda");
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
            var dto = new FileDto()
            {
                HospitalId = hospitalId,
                SerialNo = serialNo,
                AnusuchiNo = anusuchiNo,
                InspectionDate = InspectionDate,
                File = file
            };
            return TypedResults.Ok(await service.UploadFileAsync(dto));
        }).DisableAntiforgery();


        // anusuchi services
        endpoints.MapPost("api/Anusuchi", async (AnusuchiDto dto, IAnusuchiService service) => TypedResults.Ok(await service.Create(dto)));
        endpoints.MapPost("api/Anusuchi/{anusuchiId}", async (int anusuchiId, AnusuchiDto dto, IAnusuchiService service) => TypedResults.Ok(await service.Update(anusuchiId, dto)));
        endpoints.MapGet("api/Anusuchi", async (IAnusuchiService service) => TypedResults.Ok(await service.GetAll()));
        endpoints.MapGet("api/Anusuchi/{id}", async (int id, IAnusuchiService service) => TypedResults.Ok(await service.GetById(id)));

        // parichhed services
        endpoints.MapPost("api/Parichhed", async (ParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.Create(dto)));
        endpoints.MapPost("api/Parichhed/{parichhedId}/update", async (int parichhedId, ParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.Update(parichhedId, dto)));
        endpoints.MapGet("api/Parichhed", async ([FromQuery] int? anusuchiId, IParichhedService service) => TypedResults.Ok(await service.GetAllParichhed(anusuchiId)));
        endpoints.MapGet("api/Parichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetParichhedById(id)));

        //sub parichhed services
        endpoints.MapPost("api/SubParichhed", async (SubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.CreateSubParichhed(dto)));
        endpoints.MapPost("api/SubParichhed/{subParichhedId}/update", async (int subParichhedId, SubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.UpdateSubParichhed(subParichhedId, dto)));
        endpoints.MapGet("api/SubParichhed", async ([FromQuery] int parichhedId, IParichhedService service) => TypedResults.Ok(await service.GetSubParichhedsByParichhed(parichhedId)));
        endpoints.MapGet("api/SubParichhed/all", async ( IParichhedService service) => TypedResults.Ok(await service.GetAllSubParichheds()));
        endpoints.MapGet("api/SubParichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetSubParichhedById(id)));

        //sub sub parichhed services
        endpoints.MapPost("api/SubSubParichhed", async (SubSubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.CreateSubSubParichhed(dto)));
        endpoints.MapPost("api/SubSubParichhed/{subSubParichhedId}/update", async (int subSubParichhedId, SubSubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.UpdateSubSubParichhed(subSubParichhedId, dto)));
        endpoints.MapGet("api/SubSubParichhed", async (IParichhedService service) => TypedResults.Ok(await service.GetAllSubSubParichheds()));
        endpoints.MapGet("api/SubSubParichhed/SubParichhed/{subParichhedId}", async (int subParichhedId, IParichhedService service) => TypedResults.Ok(await service.GetSubSubParichhedsBySubParichhed(subParichhedId)));
        endpoints.MapGet("api/SubSubParichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetSubSubParichhedById(id)));

        //hospital standard1 services
        endpoints.MapPost("api/v2/HospitalStandard", async (HospitalStandardDto1 dto, IHospitalStandardService1 service) => TypedResults.Ok(await service.Create(dto)));
        endpoints.MapGet("api/v2/HospitalStandard", async ([FromQuery] int hospitalId, [FromQuery] int anusuchiId, IHospitalStandardService1 service) => TypedResults.Ok(await service.Get(hospitalId, anusuchiId)));

        //file upload services
        endpoints.MapPost("api/v2/FileUpload", async (FileUploadDto dto, IFileUploadService service) => TypedResults.Ok(await service.UploadFileAsync(dto)));

        //mapdanda1 services
        endpoints.MapGet("api/v2/Mapdanda/{id}", async (int id, IMapdandaService1 service) => TypedResults.Ok(await service.GetById(id)));
        endpoints.MapGet("api/v2/Mapdanda/Anusuchi", async ([FromQuery] int? anusuchiId, IMapdandaService1 service) => TypedResults.Ok(await service.GetByAnusuchi(anusuchiId)));
        endpoints.MapGet("api/v2/Mapdanda/Parichhed", async ([FromQuery] int parichhedId, [FromQuery] int? anusuchiId, IMapdandaService1 service) => TypedResults.Ok(await service.GetByParichhed(parichhedId, anusuchiId)));
        endpoints.MapGet("api/v2/Mapdanda/SubParichhed", async ([FromQuery] int subParichhedId, [FromQuery] int? parichhedId, [FromQuery] int? anusuchiId, IMapdandaService1 service) => TypedResults.Ok(await service.GetBySubParichhed(subParichhedId, parichhedId, anusuchiId)));
        endpoints.MapGet("api/v2/Mapdanda/SubSubParichhed", async ([FromQuery] int subSubParichhedId, [FromQuery] int? subParichhedId, [FromQuery] int? parichhedId, [FromQuery] int? anusuchiId, IMapdandaService1 service) => TypedResults.Ok(await service.GetBySubSubParichhed(subSubParichhedId, subParichhedId, parichhedId, anusuchiId)));
        endpoints.MapPost("api/v2/Mapdanda", async (MapdandaDto dto, IMapdandaService1 service) => TypedResults.Ok(await service.Add(dto)));
        endpoints.MapPost("api/v2/Mapdanda/{mapdandaId}/update", async (int mapdandaId, MapdandaDto dto, IMapdandaService1 service) => TypedResults.Ok(await service.UpdateMapdanda(mapdandaId, dto)));

        endpoints.MapGet("api/v2/testgetanusuchi/{id}", async (int id, ApplicationDbContext context) =>
        {
            var groupedMapdandas = await context.Mapdandas
                .Include(m => m.SubSubParichhed)
                .Include(m => m.SubParichhed)
                .Include(m => m.Parichhed)
                .Include(m => m.SubMapdandas)
                .Where(x => x.AnusuchiId == id)
                .GroupBy(m => new
                {
                    SubSubParichhed = m.SubSubParichhed != null ? m.SubSubParichhed.Name : "",
                    SubParichhed = m.SubParichhed != null ? m.SubParichhed.Name : "",
                    Parichhed = m.Parichhed != null ? m.Parichhed.Name : "",
                    m.IsAvailableDivided,
                })
                .Select(g => new HRRS.Dto.MapdandaAnusuchi.MapdandaByAnusuchiDto
                {
                    SubSubParichhed = g.Key.SubSubParichhed,
                    SubParichhed = g.Key.SubParichhed,
                    Parichhed = g.Key.Parichhed,
                    IsAvailableDivided = g.Key.IsAvailableDivided,
                    Mapdandas = g.Select(x => new GroupedMapdanda
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SerialNumber = x.SerialNumber,
                        Is100Active = x.Is100Active,
                        Is200Active = x.Is200Active,
                        Is50Active = x.Is50Active,
                        Is25Active = x.Is25Active,
                        IsAvailableDivided = g.Key.IsAvailableDivided,
                        Status = x.Status,
                    }).ToList()
                })
                .ToListAsync();

            return TypedResults.Ok(new ResultWithDataDto<List<HRRS.Dto.MapdandaAnusuchi.MapdandaByAnusuchiDto>>(true, groupedMapdandas, null));

        });



        endpoints.MapGet("api/v3/testgetanusuchi/{id}", async (int id, ApplicationDbContext context) =>
        {
            var groupedMapdandas = await context.Mapdandas
                .Include(m => m.SubSubParichhed)
                .Include(m => m.SubParichhed)
                .Include(m => m.Parichhed)
                .Where(x => x.AnusuchiId == id)
                .ToListAsync();

            var res = groupedMapdandas.GroupBy(m => new
            {
                Parichhed = m.Parichhed != null ? m.Parichhed.Name : "",
                m.IsAvailableDivided,
            })
            .Select(g => new Dto.Mapdanda1.MapdandaByAnusuchiDto
            {
                IsAvailableDivided = g.Key.IsAvailableDivided,
                Parichhed = g.GroupBy(x => x.Parichhed).Select(x => new Dto.Mapdanda1.GroupdParichhed
                {
                    Name = x.Key != null ? x.Key.Name : "",
                    GroupedPariched = x.GroupBy(y => y.SubParichhed).Select(a => new Dto.Mapdanda1.GroupdSubParichhed
                    {
                        Name = a.Key != null ? a.Key.Name : "",
                        GroupdSubSubParichhed = a.GroupBy(b => b.SubSubParichhed).Select(c => new Dto.Mapdanda1.GroupdSubSubParichhed
                        {
                            Name = c.Key != null ? c.Key.Name : "",
                            GroupedMapdandaGroup = c.GroupBy(e => e.Group).Select(f => new Dto.Mapdanda1.GroupedMapdandaByGroupName
                            {
                                GroupName = f.Key,
                                GroupedMapdanda = f.Select(h => new Dto.Mapdanda1.GroupedMapdanda
                                {
                                    Id = h.Id,
                                    Name = h.Name,
                                    SerialNumber = h.SerialNumber,
                                    Is100Active = h.Is100Active,
                                    Is200Active = h.Is200Active,
                                    Is50Active = h.Is50Active,
                                    Is25Active = h.Is25Active,
                                    IsAvailableDivided = g.Key.IsAvailableDivided,
                                    Status = h.Status,
                                    Parimaad = h.Parimaad
                                }).ToList()
                            }).ToList(),
                        }).ToList(),
                    }).ToList(),
                        
                }).ToList(),
            })
            .ToList();

            return TypedResults.Ok(new ResultWithDataDto<List<Dto.Mapdanda1.MapdandaByAnusuchiDto>>(true, res, null));

        });


        // mapdandas inside of anusuchi / parichheds / subparichheds / subsubparichheds
        endpoints.MapGet("api/mapdandas/anusuchi/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetMapdandasOfAnusuchi(id)));
        endpoints.MapGet("api/mapdandas/parichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetMapdandasOfParichhed(id)));
        endpoints.MapGet("api/mapdandas/subparichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetMapdandasOfSubParichhed(id)));
        endpoints.MapGet("api/mapdandas/subsubparichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetMapdandasOfSubSubParichhed(id)));

        //mapdanda groups
        endpoints.MapGet("api/mapdanda-group", async ([FromQuery] string? searchKey, IMapdandaService service) => TypedResults.Ok(await service.GetMapdandaGroups(searchKey)));


        return endpoints;
    }
}
