using HRRS.Dto;
using HRRS.Dto.Auth;
using HRRS.Dto.User;

namespace HRRS.Services.Interface;

public interface IAuthService
{
    Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterAdminAsync(RegisterDto dto);
    Task<ResultWithDataDto<string>> RegisterHospitalAsync(RegisterFacilityDto dto);
    Task<ResultWithDataDto<List<UserDto>>> GetAllUsers();
    Task<ResultWithDataDto<string>> ChangePasswordAsync(ChangePasswordDto dto);
    Task<ResultWithDataDto<UserDto>> GetById(long userId);
    Task<ResultWithDataDto<string>> ResetAdminPasswordAsync(long userId, long adminId, ResetPasswordDto dto);

}
