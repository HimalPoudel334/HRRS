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

        User newUser = new User
        {
            UserName = dto.Username,
            Password = GenerateHashedPassword(dto.Password),
            UserType = "Admin"
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return GenerateAuthResponse(newUser);
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> RegisterHospitalAsync(RegisterHospitalDto dto)
    {

        
        if (await _context.HealthFacilities.AnyAsync(x => x.PanNumber == dto.FacilityDto.PanNumber))
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Health Facility already exists");
        }

        if (await _context.Users.AnyAsync(x => x.UserName == dto.Username))
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Username already exists");
        }

        var healthFacility = new HealthFacility()
        {
            FacilityName = dto.FacilityDto.FacilityName,
            FacilityType = dto.FacilityDto.FacilityType,
            PanNumber = dto.FacilityDto.PanNumber,
            BedCount = dto.FacilityDto.BedCount,
            SpecialistCount = dto.FacilityDto.SpecialistCount,
            AvailableServices = dto.FacilityDto.AvailableServices,
            District = dto.FacilityDto.District,
            LocalLevel = dto.FacilityDto.LocalLevel,
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

        User newUser = new User
        {
            UserName = dto.Username,
            Password = GenerateHashedPassword(dto.Password),
            HealthFacility = healthFacility
        };

        await _context.HealthFacilities.AddAsync(healthFacility);
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return GenerateAuthResponse(newUser);

    }

    private string GenerateHashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }


    private ResultWithDataDto<AuthResponseDto> GenerateAuthResponse(User user)
    {
        var loggedInUser = new LoggedInUser(user.UserId, user.UserName, user.UserType);
        var token = _tokenService.GenerateJwt(loggedInUser);

        var authResponse = new AuthResponseDto(loggedInUser, token);
        return ResultWithDataDto<AuthResponseDto>.Success(authResponse);
    }

}
