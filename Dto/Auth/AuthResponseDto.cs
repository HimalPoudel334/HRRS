using HRRS.Dto.User;

namespace HRRS.Dto.Auth;

public record AuthResponseDto(LoggedInUser User, string Token);
