using HRRS.Dto;
using HRRS.Dto.Auth;

namespace HRRS.Services.Interface;

public interface IAuthService
{
    Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterAdminAsync(RegisterDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterHospitalAsync(RegisterHospitalDto dto);
}
