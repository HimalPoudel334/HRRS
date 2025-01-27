using HRRS.Dto;
using HRRS.Dto.Auth;

namespace HRRS.Services.Interface;

public interface IAuthService
{
    Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterAsync(RegisterDto dto);
}
