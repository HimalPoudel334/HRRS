using System.Security.Claims;
using HRRS.Dto;
using HRRS.Dto.Anusuchi;
using HRRS.Dto.Auth;
using HRRS.Dto.FileUpload;
using HRRS.Dto.HealthStandard;
using HRRS.Dto.MasterStandardEntry;
using HRRS.Dto.Parichhed;
using HRRS.Dto.User;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace HRRS.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => Results.Redirect("/scalar"));

        endpoints.MapPost("api/signin", async (LoginDto dto, IAuthService authService) => TypedResults.Ok(await authService.LoginUser(dto)));
        endpoints.MapPost("api/signup", async (RegisterDto dto, IAuthService authService) => TypedResults.Ok(await authService.RegisterAdminAsync(dto))).RequireAuthorization("SuperAdmin");
        endpoints.MapPost("api/changepassword", async (ChangePasswordDto dto, IAuthService authService, ClaimsPrincipal user) => TypedResults.Ok(await authService.ChangePasswordAsync(dto)));
        endpoints.MapPost("api/resetpassword/{userId}", async (long userId, ResetPasswordDto dto, IAuthService authService, ClaimsPrincipal user) => TypedResults.Ok(await authService.ResetAdminPasswordAsync(userId, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"), dto))).RequireAuthorization("SuperAdmin"); ;

        //mapdanda
        endpoints.MapGet("api/mapdanda", async ([AsParameters] HospitalStandardQueryParams dto, IMapdandaService service) => TypedResults.Ok(await service.GetAdminMapdandas(dto))).RequireAuthorization();
        endpoints.MapPost("api/mapdanda/", async (MapdandaDto dto, IMapdandaService mapdandaService) => TypedResults.Ok(await mapdandaService.Add(dto))).RequireAuthorization("SuperAdmin");
        endpoints.MapPost("api/mapdanda/{mapdandaId}/update", async (int mapdandaId, MapdandaDto dto, IMapdandaService mapdandaService) => TypedResults.Ok(await mapdandaService.UpdateMapdanda(mapdandaId, dto))).RequireAuthorization("SuperAdmin");
        endpoints.MapPost("api/Mapdanda/{mapdandaId}/toggle-status", async (int mapdandaId, IMapdandaService service) => TypedResults.Ok(await service.ToggleStatus(mapdandaId))).RequireAuthorization("SuperAdmin");
        //mapdanda form type
        endpoints.MapGet("api/mapdanda/formtype", async ([AsParameters] HospitalStandardQueryParams dto, IMapdandaService service) => TypedResults.Ok(await service.GetFormTypeForMapdanda(dto))).RequireAuthorization("SuperAdmin");
        //mapdanda file get
        MapFileEndpoint(endpoints, "api/getmapdandafile/{filePath}", "MapdandaUpload");
        //mapdanda file upload
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


        //Facility address
        endpoints.MapGet("api/province", async (IFacilityAddressService service) => TypedResults.Ok(await service.GetAllProvinces()));
        endpoints.MapGet("api/district", async (IFacilityAddressService service) => TypedResults.Ok(await service.GetAllDistricts()));
        endpoints.MapGet("api/district/province/{provinceId}", async (int provinceId, IFacilityAddressService service) => TypedResults.Ok(await service.GetDistrictsByProvince(provinceId)));
        endpoints.MapGet("api/locallevel/district/{districtId}", async (int districtId, IFacilityAddressService service) => TypedResults.Ok(await service.GetLocalLevelsByDistrict(districtId)));

        
        // health facility registration
        endpoints.MapGet("api/registrationrequest", async (IRegistrationRequestService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetAllRegistrationRequestsAsync(long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/registrationrequest/{id}", async (int id, IRegistrationRequestService service) => TypedResults.Ok(await service.GetRegistrationRequestByIdAsync(id)));
        endpoints.MapPost("api/registrationrequest/{id}/approve", async (int id, LoginDto dto, IRegistrationRequestService service, ClaimsPrincipal user) => TypedResults.Ok(await service.ApproveRegistrationRequestAsync(id, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"), dto))).RequireAuthorization();
        endpoints.MapPost("api/registrationrequest/{id}/reject", async (int id, StandardRemarkDto dto, IRegistrationRequestService service, ClaimsPrincipal user) => TypedResults.Ok(await service.RejectRegistrationRequestAsync(id, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"), dto))).RequireAuthorization();
        
        //health facility
        endpoints.MapPost("api/healthfacility/register", async ([FromForm] RegisterFacilityDto dto, IAuthService service) => TypedResults.Ok(await service.RegisterHospitalAsync(dto))).DisableAntiforgery();
        endpoints.MapGet("api/healthfacility", async (IHealthFacilityService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetAll(long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/healthfacility/{id}", async (int id, IHealthFacilityService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetById(id, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"))));

        //health facility type
        endpoints.MapGet("api/facilitytypes", async (IFacilityTypeService service) => TypedResults.Ok(await service.GetAll()));
        endpoints.MapGet("api/facilitytypes/{id}/subtypes", async (int id, IFacilityTypeService service) => TypedResults.Ok(await service.GetSubTypesOfParent(id)));
        endpoints.MapPost("api/facilitytypes", async (FacilityTypeDto dto, IFacilityTypeService service) => TypedResults.Ok(await service.Create(dto))).RequireAuthorization("SuperAdmin");

        //bed counts
        endpoints.MapGet("api/bedcount/{facilityTypeId}", async (int facilityTypeId, IBedCountService service) => TypedResults.Ok(await service.GetBedCountsByFacilityType(facilityTypeId)));

        //user get
        endpoints.MapGet("api/users", async (IAuthService service) => TypedResults.Ok(await service.GetAllUsers())).RequireAuthorization("SuperAdmin");
        endpoints.MapGet("api/users/{userId}", async (long userId, IAuthService service) => TypedResults.Ok(await service.GetById(userId))).RequireAuthorization("SuperAdmin");

        //user post
        endpoints.MapGet("api/userposts", async (IUserPostService service) => TypedResults.Ok(await service.GetAll())).RequireAuthorization();

        //user role services
        endpoints.MapGet("api/userrole", async (IUserRoleService service) => TypedResults.Ok(await service.GetAll()));
        endpoints.MapPost("api/userrole", async (UserRoleDto dto, IUserRoleService service) => TypedResults.Ok(await service.Create(dto))).RequireAuthorization("SuperAdmin");

        // anusuchi services
        endpoints.MapPost("api/anusuchi", async (AnusuchiDto dto, IAnusuchiService service) => TypedResults.Ok(await service.Create(dto))).RequireAuthorization("SuperAdmin");
        endpoints.MapPost("api/anusuchi/{anusuchiId}", async (int anusuchiId, AnusuchiDto dto, IAnusuchiService service) => TypedResults.Ok(await service.Update(anusuchiId, dto))).RequireAuthorization("SuperAdmin");
        endpoints.MapGet("api/anusuchi", async ([FromQuery] Guid? submissionCode, IAnusuchiService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetAllUserAnusuchi(long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim() ?? "0"), submissionCode))).RequireAuthorization();
        endpoints.MapGet("api/anusuchi/{id}", async (int id, IAnusuchiService service) => TypedResults.Ok(await service.GetById(id))).RequireAuthorization();

        // parichhed services
        endpoints.MapPost("api/parichhed", async (ParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.Create(dto)));
        endpoints.MapPost("api/parichhed/{parichhedId}/update", async (int parichhedId, ParichhedDto dto, IParichhedService service) => TypedResults.Ok(await service.Update(parichhedId, dto)));
        endpoints.MapGet("api/parichhed", async ([AsParameters] ParichhedQueryParams dto, IParichhedService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetAllParichhed(dto, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim() ?? "0"))));
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
        endpoints.MapPost("api/hospitalstandard", async (HospitalStandardDto dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.Create(dto, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/master/{submissionCode}", async (Guid submissionCode, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetStandardEntries(submissionCode))).RequireAuthorization();
        endpoints.MapGet("api/standardentry/{entryId}", async (int entryId, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetHospitalEntryById(entryId))).RequireAuthorization();
        endpoints.MapGet("api/standard/entry/{entryId}", async (int entryId, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.AdminGetHospitalStandardForEntry(entryId)));
        endpoints.MapGet("api/standard/{submissionCode}", async (Guid submissionCode, [AsParameters] HospitalStandardQueryParams dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetHospitalStandardForEntry(submissionCode, dto, int.Parse(user.FindFirstValue("HealthFacilityId") ?? "0")))).RequireAuthorization();
        endpoints.MapPost("api/hospitalstandard/approval", async (List<StandardApprovalDto> dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.UpdateStandardDecisionByAdmin(dto))).RequireAuthorization("AllAdmins");

        endpoints.MapPost("api/standard/status/approve/{entryId}", async (Guid entryId, StandardRemarkDto dto, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.ApproveStandardsWithRemark(entryId, dto, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization("AllAdmins");
        endpoints.MapPost("api/standard/status/reject/{entryId}", async (Guid entryId, StandardRemarkDto dto, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.RejectStandardsWithRemark(entryId, dto, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization("AllAdmins");
        endpoints.MapPost("api/standard/status/pending/{entryId}", async (Guid entryId, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.PendingHospitalStandardsEntry(entryId, int.Parse(user.FindFirstValue("HealthFacilityId")?.Trim() ?? "0")))).RequireAuthorization();
        endpoints.MapPost("api/standard/status/sifaris/{entryId}", async (Guid entryId, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.SifarisToPramukh(entryId, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();

        endpoints.MapGet("api/hospitalentry/{entryId}", async (int entryId, IHospitalStandardService service) => TypedResults.Ok(await service.GetHospitalEntryById(entryId)));

        //submission code
        endpoints.MapPost("api/submission", async ([FromBody] SubmissionTypeDto dto, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.Create(dto, int.Parse(user.FindFirstValue("HealthFacilityId")?.Trim() ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/submission", async (IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetByUserHospitalId(int.Parse(user.FindFirstValue("HealthFacilityId") ?? "0")))).RequireAuthorization();
        endpoints.MapGet("api/submission/{submissionCode}", async (Guid submissionCode, IMasterStandardEntryService service) => TypedResults.Ok(await service.GetMasterEntryById(submissionCode)));
        endpoints.MapGet("api/submission/hospital/{hospitalId}", async (int hospitalId, IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetByHospitalId(hospitalId, long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0")))).RequireAuthorization();
        endpoints.MapPost("api/submissions/update", async (HospitalStandardDto dto, IHospitalStandardService service, ClaimsPrincipal user) => TypedResults.Ok(await service.Update(dto, int.Parse(user.FindFirstValue("HealthFacilityId")?.Trim() ?? "0")))).RequireAuthorization();

        //submission type
        endpoints.MapGet("api/submissiontype", async (IMasterStandardEntryService service) => TypedResults.Ok(await service.GetAllSubmissionTypes())).RequireAuthorization();

        // all new submissions
        endpoints.MapGet("api/submission/new", async (IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetAllNewSubmission(long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim() ?? "0")))).RequireAuthorization();
        
        //new submission count
        endpoints.MapGet("api/submission/count", async (IMasterStandardEntryService service, ClaimsPrincipal user) => TypedResults.Ok(await service.GetNewSubmissionCount(long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim() ?? "0")))).RequireAuthorization();



        return endpoints;

    }

    private static void MapFileEndpoint(IEndpointRouteBuilder endpoints, string route, string configKey)
    {
        endpoints.MapGet(route, (string filePath, IFileUploadService service, IConfiguration config) =>
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return Results.BadRequest(new ResultDto(false, "Filename is required"));
            }

            var basePath = config[$"FileUploadPaths:{configKey}"] ?? Path.Combine("Media", configKey);
            var fullPath = Path.Combine(basePath, filePath);

            if (!File.Exists(fullPath))
            {
                return Results.NotFound(new ResultDto(false, "File not found."));
            }

            var contentType = service.GetContentType(fullPath);
            var fileBytes = File.ReadAllBytes(fullPath);

            return Results.File(fileBytes, contentType);
        });
    }

}


