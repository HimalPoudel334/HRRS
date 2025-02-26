namespace HRRS.Dto.User;

public class UserRegisterDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string UserType { get; set; }
}

public class UserRoleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int BedCount { get; set; }
}

public class UserDto
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string UserType { get; set; }
    public int? RoleId { get; set; }
    public string? Role { get; set; }
    public int? BedCount { get; set; }
}
