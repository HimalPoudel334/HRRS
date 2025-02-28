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

    public AuthService(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Invalid Username or Password");
        }
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
            UserType = "Admin",
            Role = role,
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return GenerateAuthResponse(newUser);
    }

    public async Task<ResultWithDataDto<string>> RegisterHospitalAsync(RegisterHospitalDto dto)
    {
        
        if (await _context.HealthFacilities.AnyAsync(x => x.PanNumber == dto.FacilityDto.PanNumber))
        {
            return ResultWithDataDto<string>.Failure("Health Facility already exists");
        }

        if (await _context.Users.AnyAsync(x => x.UserName == dto.Username))
        {
            return ResultWithDataDto<string>.Failure("Username already exists");
        }

        var facilityType = await _context.HospitalType.FindAsync(dto.FacilityDto.FacilityTypeId);
        if(facilityType is null)
            return ResultWithDataDto<string>.Failure("Facility type cannot be found");

        var localLevel = await _context.LocalLevels.FindAsync(dto.FacilityDto.LocalLevelId);
        if (localLevel is null)
            return ResultWithDataDto<string>.Failure("Local level cannot be found");

        var district = await _context.Districts.FindAsync(dto.FacilityDto.DistrictId);
        if (district is null)
            return ResultWithDataDto<string>.Failure("District cannot be found");

        var healthFacility = new HealthFacility()
        {
            FacilityName = dto.FacilityDto.FacilityName,
            FacilityType = facilityType,
            PanNumber = dto.FacilityDto.PanNumber,
            BedCount = dto.FacilityDto.BedCount,
            SpecialistCount = dto.FacilityDto.SpecialistCount,
            AvailableServices = dto.FacilityDto.AvailableServices,
            District = district,
            LocalLevel = localLevel,
            WardNumber = dto.FacilityDto.WardNumber,
            Tole = dto.FacilityDto.Tole,
            DateOfInspection = dto.FacilityDto.DateOfInspection,
            FacilityEmail = dto.FacilityDto.FacilityEmail,
            FacilityPhoneNumber = dto.FacilityDto.FacilityPhoneNumber,
            FacilityHeadName = dto.FacilityDto.FacilityHeadName,
            FacilityHeadPhone = dto.FacilityDto.FacilityHeadPhone,
            FacilityHeadEmail = dto.FacilityDto.FacilityHeadEmail,
            ExecutiveHeadName = dto.FacilityDto.ExecutiveHeadName,
            ExecutiveHeadMobile = dto.FacilityDto.ExecutiveHeadMobile,
            ExecutiveHeadEmail = dto.FacilityDto.ExecutiveHeadEmail,
            PermissionReceivedDate = dto.FacilityDto.PermissionReceivedDate,
            LastRenewedDate = dto.FacilityDto.LastRenewedDate,
            ApporvingAuthority = dto.FacilityDto.ApporvingAuthority,
            RenewingAuthority = dto.FacilityDto.RenewingAuthority,
            ApprovalValidityTill = dto.FacilityDto.ApprovalValidityTill,
            RenewalValidityTill = dto.FacilityDto.RenewalValidityTill,
            UpgradeDate = dto.FacilityDto.UpgradeDate,
            UpgradingAuthority = dto.FacilityDto.UpgradingAuthority,
            IsLetterOfIntent = dto.FacilityDto.IsLetterOfIntent,
            IsExecutionPermission = dto.FacilityDto.IsExecutionPermission,
            IsRenewal = dto.FacilityDto.IsRenewal,
            IsUpgrade = dto.FacilityDto.IsUpgrade,
            IsServiceExtension = dto.FacilityDto.IsServiceExtension,
            IsBranchExtension = dto.FacilityDto.IsBranchExtension,
            IsRelocation = dto.FacilityDto.IsRelocation,
            Others = dto.FacilityDto.Others,
            ApplicationSubmittedAuthority = dto.FacilityDto.ApplicationSubmittedAuthority,
            ApplicationSubmittedDate = dto.FacilityDto.ApplicationSubmittedDate
        };

        var registrationRequest = new RegistrationRequest()
        {
            HealthFacility = healthFacility,
            Status = RequestStatus.Pending,
            CreatedAt = DateTime.Now
        };

        //User newUser = new User
        //{
        //    UserName = dto.Username,
        //    Password = GenerateHashedPassword(dto.Password),
        //    HealthFacility = healthFacility
        //};

        await _context.TempHealthFacilities.AddAsync(healthFacility);
        await _context.RegistrationRequests.AddAsync(registrationRequest);
        await _context.SaveChangesAsync();
        //return GenerateAuthResponse(newUser);
        return ResultWithDataDto<string>.Success("Registration request submitted successfully");
    }

    public static string GenerateHashedPassword(string password)
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
        return new string(Enumerable.Repeat(chars, 5)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
