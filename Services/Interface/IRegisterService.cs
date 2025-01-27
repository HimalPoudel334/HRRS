using HRRS.Dto.Auth;
using HRRS.Dto;
using HRRS.Dto.User;

namespace HRRS.Services.Interface;

public interface IRegisterService
{
    Task<ResultDto> RegisterAsync(UserRegisterDto dto);
}
