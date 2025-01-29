using HRRS.Endpoints;
using HRRS.Persistence.Context;
using HRRS.Persistence.Repositories.Implementations;
using HRRS.Persistence.Repositories.Interfaces;
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
              .AllowAnyHeader()
              .DisallowCredentials(); // Allows all headers

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
builder.Services.AddScoped<IHealthFacilityService, HealthFacilityService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

builder.Services.AddScoped<IMapdandaRepository, MapdandaRepository>();
builder.Services.AddScoped<IHospitalStandardRespository, HospitalStandardRepository>();
builder.Services.AddScoped<IHealthFacilityRepositoroy, HealthFacilityRepository>();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
    jwtOptions.TokenValidationParameters = TokenService.GetTokenValidationParameter(builder.Configuration));

builder.Services.AddAuthorization();

var app = builder.Build();



app.MapOpenApi();
app.MapScalarApiReference();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

//app.UseAntiforgery();

app.MapControllers();
app.MapEndPoints();

app.Run();