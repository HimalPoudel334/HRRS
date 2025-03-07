using HRRS.Endpoints;
using HRRS.MIddlewares;
using HRRS.Persistence.Context;
using HRRS.Services;
using HRRS.Services.Implementation;
using HRRS.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Allows all origins
              .AllowAnyMethod()  // Allows all HTTP methods
              .AllowAnyHeader(); // Allows all headers

    });
});

//builder.Services.AddAntiforgery();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<DapperHelper>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IMapdandaService, MapdandaService>();
builder.Services.AddScoped<IHospitalStandardService, HospitalStandardService>();
builder.Services.AddScoped<IHospitalStandardService, HospitalStandardService>();
builder.Services.AddScoped<IHealthFacilityService, HealthFacilityService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IAnusuchiService, AnusuchiService>();
builder.Services.AddScoped<IParichhedService, ParichhedService>();
builder.Services.AddScoped<IMasterStandardEntryService, MasterStandardEntryService>();

builder.Services.AddScoped<IFacilityTypeService, FacilityTypeService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IFacilityAddressService, FacilityAddressService>();
builder.Services.AddScoped<IRegistrationRequestService, RegistrationRequestService>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddOpenApi();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
    jwtOptions.TokenValidationParameters = TokenService.GetTokenValidationParameter(builder.Configuration));

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"))
    .AddPolicy("AllAdmins", policy => policy.RequireRole("SuperAdmin", "localadmin", "localadmin1"));

var app = builder.Build();



app.MapOpenApi();
app.MapScalarApiReference();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference();
//}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

//app.UseAntiforgery();

app.MapControllers();
app.MapEndPoints();

app.Run();