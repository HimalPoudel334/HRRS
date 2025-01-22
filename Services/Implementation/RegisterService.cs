using EarProject.Dto.Auth;
using EarProject.Dto;
using EarProject.Dto.User;
using EarProject.Persistence.Entities;
using EarProject.Services.Interface;
using Microsoft.EntityFrameworkCore;
using EarProject.Persistence.Context;

namespace EarProject.Services.Implementation;

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
