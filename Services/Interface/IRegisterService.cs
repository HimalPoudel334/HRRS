using EarProject.Dto.Auth;
using EarProject.Dto;
using EarProject.Dto.User;

namespace EarProject.Services.Interface;

public interface IRegisterService
{
    Task<ResultDto> RegisterAsync(UserRegisterDto dto);
}
