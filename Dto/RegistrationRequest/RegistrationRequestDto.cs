using HRRS.Dto.User;
using HRRS.Persistence.Entities;
using Persistence.Entities;

namespace HRRS.Dto.RegistrationRequest;

public class RegistrationRequestDto
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

 
