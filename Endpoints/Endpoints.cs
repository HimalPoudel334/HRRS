using HRRS.Dto.Auth;
using HRRS.Dto.HealthStandard;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HRRS.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => Results.Redirect("/scalar"));
        endpoints.MapPost("api/signin", async (LoginDto dto, IAuthService authService) =>
            TypedResults.Ok(await authService.LoginUser(dto)));

        //endpoints.MapPost("api/signup", async (RegisterDto dto, IAuthService authService) =>
        //    TypedResults.Ok(await authService.RegisterAsync(dto)));

        //endpoints.MapGet("api/mapdanda/{anusuchi_id}", async (int anusuchi_id, IMapdandaService mapdandaService) =>
        //    TypedResults.Ok(await mapdandaService.GetByAnusuchi(anusuchi_id)));

        endpoints.MapPost("api/HealthFaciltiy", async (HealthFacilityDto dto, IHealthFacilityService service) =>
        {
            await service.Create(dto);
            return TypedResults.Created();
        });
        endpoints.MapPost("api/HospitalStandard", async (HospitalStandardDto dto, IHospitalStandardService service) =>
        {
            await service.Create(dto);
            return TypedResults.Created();  
        });

        endpoints.MapGet("api/HospitalStandard", async ([FromQuery] int hospitalId, [FromQuery] int anusuchiId, IHospitalStandardService service) =>
        {
            return TypedResults.Ok(await service.Get(hospitalId, anusuchiId));
        });





        return endpoints;
    }
}
