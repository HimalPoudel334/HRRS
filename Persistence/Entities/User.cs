namespace EarProject.Persistence.Entities;

public class User
{

    public long UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string UserType { get; set; } = "General";
}
