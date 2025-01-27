using HRRS.Dto.Auth;
using Microsoft.EntityFrameworkCore;
using HRRS.Dto;
using HRRS.Dto.User;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using HRRS.Persistence.Context;

namespace HRRS.Services.Implementation;

public class RegisterService(ApplicationDbContext context) : IRegisterService
{

    private readonly ApplicationDbContext _context = context;

    public async Task<ResultDto> RegisterAsync(UserRegisterDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == dto.UserName);
        if (user is not null)
        {
            return ResultDto.Failure("Username already exists");
        }

        User newUser = new()
        {
            UserName = dto.UserName,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password, 12),
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return ResultDto.Success();
    }
}
