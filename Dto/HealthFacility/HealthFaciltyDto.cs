
public class HealthFacilityDto
{
    public int? Id { get; set; }
    public string FacilityName { get; set; }
    public int FacilityTypeId { get; set; }
    public string FacilityType { get; set; }
    public string PanNumber { get; set; }
    public string BedCount { get; set; }
    public int SpecialistCount { get; set; }
    public string AvailableServices { get; set; }
    public string Province { get; set; }
    public int DistrictId { get; set; }
    public string District { get; set; }
    public int LocalLevelId { get; set; }
    public string LocalLevel { get; set; }
    public int WardNumber { get; set; }
    public string Tole { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string? FilePath { get; set; }
    public string? DateOfInspection { get; set; }
    public string? FacilityEmail { get; set; }
    public string? FacilityPhoneNumber { get; set; }
    public string? FacilityHeadName { get; set; }
    public string? FacilityHeadPhone { get; set; }
    public string? FacilityHeadEmail { get; set; }
    public string? ExecutiveHeadName { get; set; }
    public string? ExecutiveHeadMobile { get; set; }
    public string? ExecutiveHeadEmail { get; set; }
    public string? PermissionReceivedDate { get; set; }
    public string? LastRenewedDate { get; set; }
    public string? ApporvingAuthority { get; set; }
    public string? RenewingAuthority { get; set; }
    public string? ApprovalValidityTill { get; set; }
    public string? RenewalValidityTill { get; set; }
    public string? UpgradeDate { get; set; }
    public string? UpgradingAuthority { get; set; }
    public bool? IsLetterOfIntent { get; set; }
    public bool? IsExecutionPermission { get; set; }
    public bool? IsRenewal { get; set; }
    public bool? IsUpgrade { get; set; }
    public bool? IsServiceExtension { get; set; }
    public bool? IsBranchExtension { get; set; }
    public bool? IsRelocation { get; set; }
    public string? Others { get; set; }
    public string? ApplicationSubmittedDate { get; set; }
    public string? ApplicationSubmittedAuthority { get; set; }
    public bool? HasNewSubmission { get; set; }

}

public class RegisterFacilityDto
{
    public int? Id { get; set; }
    public string FacilityName { get; set; }
    public int FacilityTypeId { get; set; }
    public string? FacilityType { get; set; }
    public string PanNumber { get; set; }
    public int BedCountId { get; set; }
    public string BedCount { get; set; }
    public int SpecialistCount { get; set; }
    public string AvailableServices { get; set; }
    public int ProvinceId { get; set; }
    public string? Province { get; set; }
    public int DistrictId { get; set; }
    public string? District { get; set; }
    public int LocalLevelId { get; set; }
    public string? LocalLevel { get; set; }
    public int WardNumber { get; set; }
    public string Tole { get; set; }
    public string DateOfInspection { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public IFormFile? Photo { get; set; }
    public string? FilePath { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? Email { get; set; }
    public int RoleId { get; set; }
}

public class FacilityTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? HospitalCode { get; set; }
    public bool IsActive { get; set; }
}