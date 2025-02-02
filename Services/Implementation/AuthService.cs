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

    public async Task<ResultWithDataDto<AuthResponseDto>> RegisterAsync(RegisterDto dto)
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
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return GenerateAuthResponse(newUser);
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> RegisterHospitalAsync(RegisterHospitalDto dto)
    {
        var healthFacility = new HealthFacility()
        {
            FacilityName = dto.facilityDto.FacilityName,
            FacilityType = dto.facilityDto.FacilityType,
            PanNumber = dto.facilityDto.PanNumber,
            BedCount = dto.facilityDto.BedCount,
            SpecialistCount = dto.facilityDto.SpecialistCount,
            AvailableServices = dto.facilityDto.AvailableServices,
            District = dto.facilityDto.District,
            LocalLevel = dto.facilityDto.LocalLevel,
            WardNumber = dto.facilityDto.WardNumber,
            Tole = dto.facilityDto.Tole,
            DateOfInspection = dto.facilityDto.DateOfInspection,
            FacilityEmail = dto.facilityDto.FacilityEmail,
            FacilityPhoneNumber = dto.facilityDto.FacilityPhoneNumber,
            FacilityHeadName = dto.facilityDto.FacilityHeadName,
            FacilityHeadPhone = dto.facilityDto.FacilityHeadPhone,
            FacilityHeadEmail = dto.facilityDto.FacilityHeadEmail,
            ExecutiveHeadName = dto.facilityDto.ExecutiveHeadName,
            ExecutiveHeadMobile = dto.facilityDto.ExecutiveHeadMobile,
            ExecutiveHeadEmail = dto.facilityDto.ExecutiveHeadEmail,
            PermissionReceivedDate = dto.facilityDto.PermissionReceivedDate,
            LastRenewedDate = dto.facilityDto.LastRenewedDate,
            ApporvingAuthority = dto.facilityDto.ApporvingAuthority,
            RenewingAuthority = dto.facilityDto.RenewingAuthority,
            ApprovalValidityTill = dto.facilityDto.ApprovalValidityTill,
            RenewalValidityTill = dto.facilityDto.RenewalValidityTill,
            UpgradeDate = dto.facilityDto.UpgradeDate,
            UpgradingAuthority = dto.facilityDto.UpgradingAuthority,
            IsLetterOfIntent = dto.facilityDto.IsLetterOfIntent,
            IsExecutionPermission = dto.facilityDto.IsExecutionPermission,
            IsRenewal = dto.facilityDto.IsRenewal,
            IsUpgrade = dto.facilityDto.IsUpgrade,
            IsServiceExtension = dto.facilityDto.IsServiceExtension,
            IsBranchExtension = dto.facilityDto.IsBranchExtension,
            IsRelocation = dto.facilityDto.IsRelocation,
            Others = dto.facilityDto.Others,
            ApplicationSubmittedAuthority = dto.facilityDto.ApplicationSubmittedAuthority,
            ApplicationSubmittedDate = dto.facilityDto.ApplicationSubmittedDate
        };

        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
        if (user is not null)
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Username already exists");
        }

        User newUser = new User
        {
            UserName = dto.Username,
            Password = GenerateHashedPassword(dto.Password),
            UserType = "Hospital",
            HealthFacility = healthFacility
        };

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
