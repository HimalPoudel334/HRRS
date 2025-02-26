using HRRS.Dto;
using HRRS.Dto.User;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using Persistence.Entities;

namespace HRRS.Services.Implementation
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ApplicationDbContext _context;

        public UserRoleService(ApplicationDbContext context) => _context = context;

        public async Task<ResultWithDataDto<List<Role>>> GetAll()
        {
            var roles = await _context.UserRoles.ToListAsync();
            if (roles.Count == 0 || roles is null)
                return new ResultWithDataDto<List<Role>>(true, null, "Cannot find user roles");
            return new ResultWithDataDto<List<Role>>(true, roles, null);
        }

        public async Task<ResultDto> Create(UserRoleDto dto)
        {
            var role = new Role()
            {
                Title = dto.Title,
                BedCount = dto.BedCount,
                
            };

            await _context.UserRoles.AddAsync(role);
            await _context.SaveChangesAsync();

            return ResultDto.Success();
        }
    }
}
