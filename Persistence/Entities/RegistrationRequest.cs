using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class RegistrationRequest
    {
        public int Id { get; set; }
        public int HealthFacilityId { get; set; }
        public TempHealthFacility HealthFacility { get; set; }
        public RequestStatus Status { get; set; }
        public long? HandledById { get; set; }
        public User? HandledBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Remarks { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }

    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
