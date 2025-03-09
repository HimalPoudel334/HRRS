using HRRS.Dto.User;
using HRRS.Persistence.Entities;
using Persistence.Entities;

namespace HRRS.Dto.RegistrationRequest;

public class RegistrationRequestWithFacilityDto
{
    public int Id { get; set; }
    public int HealthFacilityId { get; set; }
    public RegisterFacilityDto HealthFacility { get; set; }
    public RequestStatus Status { get; set; }
    public long? HandledById { get; set; }
    public string? HandledBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Remarks { get; set; }

}

public class RegistrationRequestDto
{
    public int Id { get; set; }
    public int HealthFacilityId { get; set; }
    public int FacilityId { get; set; }
    public string FacilityName { get; set; }
    public RequestStatus Status { get; set; }
    public long? HandledById { get; set; }
    public string? HandledBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Remarks { get; set; }
    public string BedCount { get; set; }

}
 
