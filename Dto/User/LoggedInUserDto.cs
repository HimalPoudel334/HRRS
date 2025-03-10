namespace HRRS.Dto.User;

public record LoggedInUser(long UserId, string Username, string UserRole, int? HealthFacilityId, string? Post);
