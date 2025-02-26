using HRRS.Dto;
using HRRS.Dto.Auth;
using HRRS.Dto.User;

namespace HRRS.Services.Interface;

public interface IAuthService
{
    Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterAdminAsync(RegisterDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterHospitalAsync(RegisterHospitalDto dto);
    Task<ResultWithDataDto<List<UserDto>>> GetAllUsers();

}
