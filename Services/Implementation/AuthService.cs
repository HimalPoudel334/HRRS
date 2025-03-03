using HRRS.Dto.User;
using HRRS.Dto;
using HRRS.Dto.Auth;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace HRRS.Services.Implementation;

public class AuthService : IAuthService
{

    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IFileUploadService _fileService;

    public AuthService(ApplicationDbContext context, TokenService tokenService, IFileUploadService fileService)
    {
        _context = context;
        _tokenService = tokenService;
        _fileService = fileService;
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Invalid Username or Password");
        }

        if (user.IsFirstLogin)
            return ResultWithDataDto<AuthResponseDto>.Failure("Please change your password to continue");

        return GenerateAuthResponse(user);
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> RegisterAdminAsync(RegisterDto dto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
        if (user is not null)
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Username already exists");
        }

        var role = await _context.UserRoles.FindAsync(dto.RoleId);
        if(role is null)
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Cannot find user role");
        }

        User newUser = new User
        {
            UserName = dto.Username,
            Password = GenerateHashedPassword(dto.Password),
            UserType = "localadmin",
            Role = role,
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return GenerateAuthResponse(newUser);
    }

    public async Task<ResultWithDataDto<string>> RegisterHospitalAsync(RegisterFacilityDto dto)
    {

        if (await _context.HealthFacilities.AnyAsync(x => x.PanNumber == dto.PanNumber))
        {
            return ResultWithDataDto<string>.Failure("Health Facility already exists");
        }

        var facilityType = await _context.HospitalType.FindAsync(dto.FacilityTypeId);
        if (facilityType is null)
            return ResultWithDataDto<string>.Failure("Facility type cannot be found");

        var province = await _context.Provinces.FindAsync(dto.ProvinceId);
        if (province is null)
            return ResultWithDataDto<string>.Failure("Province cannot be found");

        var localLevel = await _context.LocalLevels.FindAsync(dto.LocalLevelId);
        if (localLevel is null)
            return ResultWithDataDto<string>.Failure("Local level cannot be found");

        var district = await _context.Districts.FindAsync(dto.DistrictId);
        if (district is null)
            return ResultWithDataDto<string>.Failure("District cannot be found");

        if (await _context.TempHealthFacilities.AnyAsync(x => x.Email.Equals(dto.Email))) return ResultWithDataDto<string>.Failure("Email already exists");
        if (await _context.TempHealthFacilities.AnyAsync(x => x.PhoneNumber.Equals(dto.PhoneNumber))) return ResultWithDataDto<string>.Failure("Phone number already exists");
        if (await _context.TempHealthFacilities.AnyAsync(x => x.MobileNumber.Equals(dto.MobileNumber))) return ResultWithDataDto<string>.Failure("Mobile number already exists");

        var healthFacility = new TempHealthFacility()
        {
            FacilityName = dto.FacilityName,
            FacilityType = facilityType,
            PanNumber = dto.PanNumber,
            BedCount = dto.BedCount,
            SpecialistCount = dto.SpecialistCount,
            AvailableServices = dto.AvailableServices,
            District = district,
            LocalLevel = localLevel,
            WardNumber = dto.WardNumber,
            Tole = dto.Tole,
            Province = province,
            Longitude = dto.Longitude,
            Latitude = dto.Latitude,
            PhoneNumber = dto.PhoneNumber,
            MobileNumber = dto.MobileNumber,
            Email = dto.Email,
        };

        if (dto.Photo is not null)
        {
            var fileName = await _fileService.UploadFacilityFileAsync(dto.Photo);
            healthFacility.FilePath = fileName;

        }
        await _context.TempHealthFacilities.AddAsync(healthFacility);

        var registrationRequest = new RegistrationRequest()
        {
            HealthFacility = healthFacility,
            Status = RequestStatus.Pending,
            CreatedAt = DateTime.Now
        };

        await _context.RegistrationRequests.AddAsync(registrationRequest);
        await _context.SaveChangesAsync();

        return ResultWithDataDto<string>.Success("Registration request submitted successfully");
    }

    private string GenerateHashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }


    private ResultWithDataDto<AuthResponseDto> GenerateAuthResponse(User user)
    {
        var loggedInUser = new LoggedInUser(user.UserId, user.UserName, user.UserType, user.HealthFacilityId);
        var token = _tokenService.GenerateJwt(loggedInUser);

        var authResponse = new AuthResponseDto(loggedInUser, token);
        return ResultWithDataDto<AuthResponseDto>.Success(authResponse);
    }

    public async Task<ResultWithDataDto<List<UserDto>>> GetAllUsers()
    {
        var users = await _context.Users.Include(x => x.Role).Select(x => new UserDto
        {
            UserId = x.UserId,
            Username = x.UserName,
            UserType = x.UserType,
            RoleId = x.RoleId,
            Role = x.Role != null? x.Role.Title : "",
            BedCount = x.Role != null ? x.Role.BedCount : 0
        }).ToListAsync();

        return ResultWithDataDto<List<UserDto>>.Success(users);
    }

    public static string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public async Task<ResultWithDataDto<string>> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password))
        {
            return ResultWithDataDto<string>.Failure("Invalid Username or Password");
        }
        user.Password = GenerateHashedPassword(dto.NewPassword);
        user.IsFirstLogin = false;
        await _context.SaveChangesAsync();
        return ResultWithDataDto<string>.Success("Password changed successfully");
    }
}
