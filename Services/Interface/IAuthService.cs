using EarProject.Dto.Auth;
using EarProject.Dto;

namespace EarProject.Services.Interface;

public interface IAuthService
{
    Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterAsync(RegisterDto dto);
}
