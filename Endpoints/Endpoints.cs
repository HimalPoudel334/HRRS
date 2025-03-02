using System.Security.Claims;
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Auth;
using HRRS.Dto.FileUpload;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.Mapdanda;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Dto.Parichhed;
using HRRS.Dto.User;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => Results.Redirect("/scalar"));

        endpoints.MapPost("api/signin", async (LoginDto dto, IAuthService authService) => TypedResults.Ok(await authService.LoginUser(dto)));
        endpoints.MapPost("api/signup", [Authorize(Roles = "SuperAdmin")] async (RegisterDto dto, IAuthService authService) => TypedResults.Ok(await authService.RegisterAdminAsync(dto)));
        endpoints.MapPost("api/changepassword", async (ChangePasswordDto dto, IAuthService authService, ClaimsPrincipal user) => TypedResults.Ok(await authService.ChangePasswordAsync(dto)));


        //mapdanda
        endpoints.MapGet("api/mapdanda", async ([AsParameters] HospitalStandardQueryParams dto, IMapdandaService service) => TypedResults.Ok(await service.GetAdminMapdandas(dto))).RequireAuthorization();
        endpoints.MapPost("api/mapdanda/", [Authorize(Roles = "SuperAdmin")] async (MapdandaDto dto, IMapdandaService mapdandaService) => TypedResults.Ok(await mapdandaService.Add(dto))).RequireAuthorization();
        endpoints.MapPost("api/mapdanda/{mapdandaId}/update", [Authorize(Roles = "SuperAdmin")] async (int mapdandaId, MapdandaDto dto, IMapdandaService mapdandaService) => TypedResults.Ok(await mapdandaService.UpdateMapdanda(mapdandaId, dto))).RequireAuthorization();
        endpoints.MapPost("api/Mapdanda/{mapdandaId}/toggle-status", [Authorize(Roles = "SuperAdmin")] async (int mapdandaId, IMapdandaService service) => TypedResults.Ok(await service.ToggleStatus(mapdandaId))).RequireAuthorization() ;
        endpoints.MapGet("api/mapdanda-group", async ([FromQuery] string? searchKey, IMapdandaService service) => TypedResults.Ok(await service.GetMapdandaGroups(searchKey)));

        //mapdanda form type
        endpoints.MapGet("api/mapdanda/formtype", [Authorize(Roles = "SuperAdmin")] async ([AsParameters] HospitalStandardQueryParams dto, IMapdandaService service) => TypedResults.Ok(await service.GetFormTypeForMapdanda(dto)));

        //Facility address
        endpoints.MapGet("api/province", async (IFacilityAddressService service) => TypedResults.Ok(await service.GetAllProvinces()));
        endpoints.MapGet("api/district", async (IFacilityAddressService service) => TypedResults.Ok(await service.GetAllDistricts()));
        endpoints.MapGet("api/district/province/{provinceId}", async (int provinceId, IFacilityAddressService service) => TypedResults.Ok(await service.GetDistrictsByProvince(provinceId)));
        endpoints.MapGet("api/locallevel/district/{districtId}", async (int districtId, IFacilityAddressService service) => TypedResults.Ok(await service.GetLocalLevelsByDistrict(districtId)));

        //mapdanda file upload
        endpoints.MapGet("api/getmapdandafile/{filePath}", (string filePath, IFileUploadService service, IConfiguration config) =>
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
        endpoints.MapPost("api/mapdandaupload", async ([FromQuery] int mapdandaId, IFormFile file, IFileUploadService service, ClaimsPrincipal user) =>
        {
            var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var dto = new FileDto()
            {
                UserId = userId,
                MapdandaId = mapdandaId,
                File = file
            };
            return TypedResults.Ok(await service.UploadFileAsync(dto));
        }).RequireAuthorization().DisableAntiforgery();

        // health facility registration
        endpoints.MapGet("api/registrationrequest", [Authorize(Roles = "SuperAdmin")] async (IRegistrationRequestService service) => TypedResults.Ok(await service.GetAllRegistrationRequestsAsync()));
        endpoints.MapGet("api/registrationrequest/{id}", [Authorize(Roles = "SuperAdmin")] async (int id, IRegistrationRequestService service) => TypedResults.Ok(await service.GetRegistrationRequestByIdAsync(id)));
        endpoints.MapPost("api/registrationrequest/{id}/approve", [Authorize(Roles = "SuperAdmin")] async (int id, IRegistrationRequestService service, ClaimsPrincipal user) => TypedResults.Ok(await service.ApproveRegistrationRequestAsync(id, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));
        endpoints.MapPost("api/registrationrequest/{id}/reject", [Authorize(Roles = "SuperAdmin")] async (int id, StandardRemarkDto dto, IRegistrationRequestService service, ClaimsPrincipal user) => TypedResults.Ok(await service.RejectRegistrationRequestAsync(id, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"), dto)));

        //health facility
        endpoints.MapPost("api/healthfacility/register", async (RegisterHospitalDto dto, IAuthService service) => TypedResults.Ok(await service.RegisterHospitalAsync(dto)));
        endpoints.MapGet("api/healthfacility", async (IHealthFacilityService service, HttpContext context) => TypedResults.Ok(await service.GetAll(context))).RequireAuthorization();
        endpoints.MapPost("api/healthfacility", [Authorize(Roles = "SuperAdmin")] async (RegisterHospitalDto dto, IAuthService service) => TypedResults.Ok(await service.RegisterHospitalAsync(dto))).RequireAuthorization();
        endpoints.MapGet("api/healthfacility/{id}", async (int id, IHealthFacilityService service) => TypedResults.Ok(await service.GetById(id)));

        //health facility type
        endpoints.MapGet("api/facilitytypes", async (IFacilityTypeService service) => TypedResults.Ok(await service.GetAll()));
        endpoints.MapPost("api/facilitytypes", [Authorize(Roles = "SuperAdmin")] async (FacilityTypeDto dto, IFacilityTypeService service) => TypedResults.Ok(await service.Create(dto))).RequireAuthorization();

        //user get
        endpoints.MapGet("api/users", [Authorize(Roles = "SuperAdmin")] async (IAuthService service) => TypedResults.Ok(await service.GetAllUsers())).RequireAuthorization();

        //user role services
        endpoints.MapGet("api/userrole", [Authorize(Roles = "SuperAdmin")] async (IUserRoleService service) => TypedResults.Ok(await service.GetAll())).RequireAuthorization();
        endpoints.MapPost("api/userrole", [Authorize(Roles = "SuperAdmin")] async (UserRoleDto dto, IUserRoleService service) => TypedResults.Ok(await service.Create(dto))).RequireAuthorization();

        // anusuchi services
        endpoints.MapPost("api/anusuchi", async (AnusuchiDto dto, IAnusuchiService service) => TypedResults.Ok(await service.Create(dto)));
        endpoints.MapPost("api/anusuchi/{anusuchiId}", async (int anusuchiId, AnusuchiDto dto, IAnusuchiService service) => TypedResults.Ok(await service.Update(anusuchiId, dto)));
        endpoints.MapGet("api/anusuchi", async (IAnusuchiService service) => TypedResults.Ok(await service.GetAll()));
        endpoints.MapGet("api/anusuchi/{id}", async (int id, IAnusuchiService service) => TypedResults.Ok(await service.GetById(id)));

        // parichhed services
        endpoints.MapPost("api/parichhed", async (ParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.Create(dto)));
        endpoints.MapPost("api/parichhed/{parichhedId}/update", async (int parichhedId, ParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.Update(parichhedId, dto)));
        endpoints.MapGet("api/parichhed", async ([FromQuery] int? anusuchiId, IParichhedService service) => TypedResults.Ok(await service.GetAllParichhed(anusuchiId)));
        endpoints.MapGet("api/parichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetParichhedById(id)));

        //sub parichhed services
        endpoints.MapPost("api/subparichhed", async (SubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.CreateSubParichhed(dto)));
        endpoints.MapPost("api/subparichhed/{subParichhedId}/update", async (int subParichhedId, SubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.UpdateSubParichhed(subParichhedId, dto)));
        endpoints.MapGet("api/subparichhed", async ([FromQuery] int parichhedId, IParichhedService service) => TypedResults.Ok(await service.GetSubParichhedsByParichhed(parichhedId)));
        endpoints.MapGet("api/subparichhed/all", async ( IParichhedService service) => TypedResults.Ok(await service.GetAllSubParichheds()));
        endpoints.MapGet("api/subparichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetSubParichhedById(id)));

        //sub sub parichhed services
        endpoints.MapPost("api/subsubparichhed", async (SubSubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.CreateSubSubParichhed(dto)));
        endpoints.MapPost("api/subsubparichhed/{subSubParichhedId}/update", async (int subSubParichhedId, SubSubParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.UpdateSubSubParichhed(subSubParichhedId, dto)));
        endpoints.MapGet("api/subsubparichhed", async (IParichhedService service) => TypedResults.Ok(await service.GetAllSubSubParichheds()));
        endpoints.MapGet("api/subsubparichhed/subparichhed/{subParichhedId}", async (int subParichhedId, IParichhedService service) => TypedResults.Ok(await service.GetSubSubParichhedsBySubParichhed(subParichhedId)));
        endpoints.MapGet("api/subsubparichhed/{id}", async (int id, IParichhedService service) => TypedResults.Ok(await service.GetSubSubParichhedById(id)));

        //hospital standard services
        endpoints.MapPost("api/v2/hospitalstandard", async (HospitalStandardDto dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.Create(dto, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/v2/master/{submissionCode}", async (Guid submissionCode, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetStandardEntries(submissionCode))).RequireAuthorization();
        endpoints.MapGet("api/v2/standardentry/{entryId}", async (int entryId, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetHospitalEntryById(entryId))).RequireAuthorization();
        endpoints.MapGet("api/v2/standard/entry/{entryId}", async (int entryId, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.AdminGetHospitalStandardForEntry(entryId)));
        endpoints.MapGet("api/standard/{submissionCode}", async (Guid submissionCode, [AsParameters] HospitalStandardQueryParams dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetHospitalStandardForEntry(submissionCode, dto, int.Parse(user.FindFirstValue("HealthFacilityId") ?? "0"))));
        endpoints.MapPost("api/hospitalstandard/approval", [Authorize(Roles = "SuperAdmin,localadmin,localadmin1")] async (List<StandardApprovalDto> dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.UpdateStandardDecisionByAdmin(dto))).RequireAuthorization();

        endpoints.MapPost("api/v2/standard/status/approve/{entryId}", [Authorize(Roles = "SuperAdmin,localadmin,localadmin1")] async (Guid entryId, StandardRemarkDto dto, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.ApproveStandardsWithRemark(entryId, dto, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapPost("api/v2/standard/status/reject/{entryId}", [Authorize(Roles = "SuperAdmin,localadmin,localadmin1")] async (Guid entryId, StandardRemarkDto dto, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.RejectStandardsWithRemark(entryId, dto, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapPost("api/v2/standard/status/pending/{entryId}", async (Guid entryId, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.PendingHospitalStandardsEntry(entryId, int.Parse(user.FindFirstValue("HealthFacilityId")?.Trim() ?? "0")))).RequireAuthorization();

        endpoints.MapGet("api/v2/hospitalentry/{entryId}", async (int entryId, IHospitalStandardService service) => TypedResults.Ok(await service.GetHospitalEntryById(entryId)));

        //submission code
        endpoints.MapPost("api/submission", async ([FromBody] SubmissionTypeDto dto, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.Create(dto, int.Parse(user.FindFirstValue("HealthFacilityId")?.Trim() ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/submission", async (IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetByUserHospitalId(int.Parse(user.FindFirstValue("HealthFacilityId") ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/submission/{submissionCode}", async (Guid submissionCode, IMasterStandardEntryService service) => TypedResults.Ok(await service.GetMasterEntryById(submissionCode)));
        endpoints.MapGet("api/submission/hospital/{hospitalId}", async (int hospitalId, IMasterStandardEntryService service) => TypedResults.Ok(await service.GetByHospitalId(hospitalId))).RequireAuthorization();
        endpoints.MapPost("api/submissions/update", async (HospitalStandardDto dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.Update(dto, int.Parse(user.FindFirstValue("HealthFacilityId")?.Trim() ?? "0")))).RequireAuthorization();

        return endpoints;
    }
}


