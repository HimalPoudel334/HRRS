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
    public int ProvinceId { get; set; }
    public string Province { get; set; }
    public int DistrictId { get; init; }
    public string District { get; set; }
    public int FacilityTypeId { get; init; }
    public string FacilityType { get; set; }
    public string Post { get; init; }
    public string? FullName { get; init; }
    public string? MobileNumber { get; init; }
    public string? FacilityMobileNumber { get; init; }
    public string? TelephoneNumber { get; init; }
    public string? FacilityEmail { get; init; }
    public string? PersonalEmail { get; init; }
    public string? Remarks { get; init; }
}
