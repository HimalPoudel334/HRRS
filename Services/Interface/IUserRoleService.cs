using HRRS.Dto;
using HRRS.Dto.User;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IUserRoleService
    {
        Task<ResultWithDataDto<List<Role>>> GetAll();
        Task<ResultDto> Create(UserRoleDto dto);
    }
}
