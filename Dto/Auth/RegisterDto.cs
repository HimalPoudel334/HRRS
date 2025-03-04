namespace HRRS.Dto.Auth;

public record RegisterDto
{
    public string Username { get; init; }
    public string Password { get; init; }
    public int RoleId { get; init; }
    public int ProvinceId { get; init; }
    public int DistrictId { get; init; }
    public int FacilityTypeId { get; init; }
    public string Post { get; init; }
    public string? FullName { get; init; }
    public string? MobileNumber { get; init; }
    public string? FacilityMobileNumber { get; init; }
    public string? TelephoneNumber { get; init; }
    public string? FacilityEmail { get; init; }
    public string? PersonalEmail { get; init; }
    public string? Remarks { get; init; }
}