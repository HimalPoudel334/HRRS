using HRRS.Dto;
using HRRS.Persistence.Context;
using HRRS.Persistence.Entities;
using HRRS.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HRRS.Services.Implementation;

public class UserPostService(ApplicationDbContext context) : IUserPostService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ResultWithDataDto<List<UserPost>>> GetAll()
    {
        //get all posts
        var posts = await _context.UserPosts.ToListAsync();
        if (posts.Count == 0 || posts is null)
            return new ResultWithDataDto<List<UserPost>>(true, null, "Cannot find posts");
        return new ResultWithDataDto<List<UserPost>>(true, posts, null);
    }

}
