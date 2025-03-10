using HRRS.Dto;
using HRRS.Persistence.Entities;

namespace HRRS.Services.Interface
{
    public interface IUserPostService
    {
        Task<ResultWithDataDto<List<UserPost>>> GetAll();
    }
}
