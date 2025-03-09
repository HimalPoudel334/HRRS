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
using Azure.Core;

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
        {
            return new ResultWithDataDto<AuthResponseDto>(true, new AuthResponseDto(
                null,
                Token: "", // No token yet since they must change their password
                RequiresPasswordChange: true,
                RedirectUrl: "/change-password"
            ), "Please change your password to continue");
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

        var province = await _context.Provinces.FindAsync(dto.ProvinceId);
        if (province is null)
            return ResultWithDataDto<AuthResponseDto>.Failure("Province cannot be found");

        var district = await _context.Districts.FindAsync(dto.DistrictId);
        if (district is null)
            return ResultWithDataDto<AuthResponseDto>.Failure("District cannot be found");

        var facilityType = await _context.HospitalType.FindAsync(dto.FacilityTypeId);
        if (facilityType is null)
            return ResultWithDataDto<AuthResponseDto>.Failure("Facility type cannot be found");

        var post = await _context.UserPosts.FindAsync(dto.PostId);
        if(post is null) return ResultWithDataDto<AuthResponseDto>.Failure("Post cannot be found");

        User newUser = new User
        {
            UserName = dto.Username,
            Password = GenerateHashedPassword(dto.Password),
            UserType = "localadmin",
            Role = role,
            District = district,
            Province = province,
            FacilityType = facilityType,
            Post = post,
            FullName = dto.FullName,
            MobileNumber = dto.MobileNumber,
            FacilityMobileNumber = dto.FacilityMobileNumber,
            TelephoneNumber = dto.TelephoneNumber,
            FacilityEmail = dto.FacilityEmail,
            PersonalEmail = dto.PersonalEmail,
            Remarks = dto.Remarks,
            IsFirstLogin = true
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return GenerateAuthResponse(newUser);
    }

    public async Task<ResultWithDataDto<string>> RegisterHospitalAsync(RegisterFacilityDto dto)
    {

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

        if (await _context.HealthFacilities.AnyAsync(x => x.PanNumber == dto.PanNumber))
            return ResultWithDataDto<string>.Failure("Health Facility already exists");

        if (await _context.HealthFacilities.AnyAsync(x => x.FacilityEmail != null && x.FacilityEmail.Equals(dto.Email))) return ResultWithDataDto<string>.Failure("Email already exists");
        if (await _context.HealthFacilities.AnyAsync(x => x.FacilityPhoneNumber != null && x.FacilityPhoneNumber.Equals(dto.PhoneNumber))) return ResultWithDataDto<string>.Failure("Phone number already exists");


        var bedCount = await _context.BedCounts.FindAsync(dto.BedCountId);
        if (bedCount is null)
            return ResultWithDataDto<string>.Failure("Bed count cannot be found");

        var healthFacility = new TempHealthFacility()
        {
            FacilityName = dto.FacilityName,
            FacilityType = facilityType,
            PanNumber = dto.PanNumber,
            BedCount = bedCount,
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
            healthFacility.FilePath = await _fileService.UploadFacilityFileAsync(dto.Photo);

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
        var users = await _context.Users
            .Include(x => x.District)
            .Include(x => x.Province)
            .Include(x => x.FacilityType)
            .Include(x => x.Role)
            .Include(x => x.Post)
            .Select(x => new UserDto
            {
                UserId = x.UserId,
                Username = x.UserName,
                UserType = x.UserType,
                RoleId = x.RoleId,
                Role = x.Role != null? x.Role.Title : "",
                FacilityType = x.FacilityType.HOSP_TYPE,
                DistrictId = x.DistrictId,
                District = x.District.Name,
                ProvinceId = x.ProvinceId,
                Province = x.Province.Name,
                Post = x.Post  != null? x.Post.Post : "",
                FullName = x.FullName,
                MobileNumber = x.MobileNumber,
                FacilityMobileNumber = x.FacilityMobileNumber,
                TelephoneNumber = x.TelephoneNumber,
                FacilityEmail = x.FacilityEmail,
                PersonalEmail = x.PersonalEmail,
                Remarks = x.Remarks


            }).ToListAsync();

        return ResultWithDataDto<List<UserDto>>.Success(users);
    }

    public async Task<ResultWithDataDto<UserDto>> GetById(long userId)
    {
        var user = await _context.Users
            .Include(x => x.District)
            .Include(x => x.Province)
            .Include(x => x.FacilityType)
            .Include(x => x.Role)
            .Select(x => new UserDto
            {
                UserId = x.UserId,
                Username = x.UserName,
                UserType = x.UserType,
                RoleId = x.RoleId,
                Role = x.Role != null ? x.Role.Title : "",
                FacilityType = x.FacilityType.HOSP_TYPE,
                DistrictId = x.DistrictId,
                District = x.District.Name,
                ProvinceId = x.ProvinceId,
                Province = x.Province.Name,
                Post = x.Post != null ? x.Post.Post : "",
                FullName = x.FullName,
                MobileNumber = x.MobileNumber,
                FacilityMobileNumber = x.FacilityMobileNumber,
                TelephoneNumber = x.TelephoneNumber,
                FacilityEmail = x.FacilityEmail,
                PersonalEmail = x.PersonalEmail,
                Remarks = x.Remarks


            })
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return ResultWithDataDto<UserDto>.Success(user);
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
        if(dto.NewPassword != dto.ConfirmPassword) return ResultWithDataDto<string>.Failure("Passwords do not match");
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password))
        {
            return ResultWithDataDto<string>.Failure("Invalid Username or Password");
        }
        if(BCrypt.Net.BCrypt.Verify(dto.NewPassword, user.Password))
        {
            return ResultWithDataDto<string>.Failure("New password cannot be the same as the old password");
        }
        user.Password = GenerateHashedPassword(dto.NewPassword);
        user.IsFirstLogin = false;
        await _context.SaveChangesAsync();
        return ResultWithDataDto<string>.Success("Password changed successfully");
    }

    public async Task<ResultWithDataDto<string>> ResetAdminPasswordAsync(long userId, long adminId, ResetPasswordDto dto)
    {
        if (dto.Password != dto.ConfirmPassword) return ResultWithDataDto<string>.Failure("Passwords do not match");
        var adminUser = await _context.Users.FindAsync(adminId);
        if (adminUser is null)
            return ResultWithDataDto<string>.Failure("Something went wrong");

        if(adminUser.UserType != "SuperAdmin")
            return ResultWithDataDto<string>.Failure("You do not have permission to reset password");

        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            return ResultWithDataDto<string>.Failure("User not found");

        if(user.UserType == "Hospital")
            return ResultWithDataDto<string>.Failure("Cannot reset password for this user");


        user.Password = GenerateHashedPassword(dto.Password);
        user.IsFirstLogin = false;

        await _context.SaveChangesAsync();
        return ResultWithDataDto<string>.Success("Password changed successfully");
    }
}
