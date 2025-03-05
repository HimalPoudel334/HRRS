using Persistence.Entities;

namespace HRRS.Persistence.Entities;

public class User
{

    public long UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string UserType { get; set; } = "Hospital";

    public HealthFacility? HealthFacility { get; set; }
    public int? HealthFacilityId { get; set; }
    public int? RoleId { get; set; }
    public Role? Role { get; set; }
    public bool IsFirstLogin { get; set; } = false;

    public int ProvinceId { get; set; }
    public Province Province { get; set; }
    public int DistrictId { get; set; }
    public District District { get; set; }
    public int FacilityTypeId { get; set; }
    public FacilityType FacilityType { get; set; }
    public string? Post { get; set; }
    public string? FullName { get; set; }
    public string? MobileNumber { get; set; }
    public string? FacilityMobileNumber { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? FacilityEmail { get; set; }
    public string? PersonalEmail { get; set; }
    public string? Remarks { get; set; }
}



