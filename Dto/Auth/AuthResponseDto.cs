using EarProject.Dto.User;

namespace EarProject.Dto.Auth;

public record AuthResponseDto(LoggedInUser User, string Token);
