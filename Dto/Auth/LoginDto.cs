namespace HRRS.Dto.Auth;

public record LoginDto(string Username, string Password);

public class ChangePasswordDto
{
    public string Username { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}

public class ResetPasswordDto
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}