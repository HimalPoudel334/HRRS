using HRRS.Dto.Auth;
using HRRS.Services.Interface;

namespace HRRS.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/signin", async (LoginDto dto, IAuthService authService) =>
            TypedResults.Ok(await authService.LoginUser(dto)));

        //endpoints.MapPost("api/signup", async (RegisterDto dto, IAuthService authService) =>
        //    TypedResults.Ok(await authService.RegisterAsync(dto)));

        endpoints.MapGet("api/mapdanda/{anusuchi_id}", async (int anusuchi_id, IMapdandaService mapdandaService) =>
            TypedResults.Ok(await mapdandaService.GetByAnusuchi(anusuchi_id)));

        endpoints.MapPost("api/HealthFaciltiy", async (HealthFacilityDto dto, IHealthFacilityService service) =>
        {
            await service.Create(dto);
            return TypedResults.Created();
        });


           


        return endpoints;
    }
}
