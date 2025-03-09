using Persistence.Entities;

namespace HRRS.Persistence.Entities
{
    public class Role
    {
        public const string Mantraalaya = "Mantraalaya";
        public const string Jilla = "Jilla";
        public const string Nirdeshanalaya = "Nirdeshanalaya";
        public const string SuperAdmin = "SuperAdmin";
        public const string Hospital = "Hospital";
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<User> Users { get; set; } = [];
        public ICollection<FacilityType> FacilityTypes { get; set; } = [];
        public ICollection<UserRoleFacilityType> UserRoleFacilityTypes { get; set; } = [];
    }

    public class UserRoleFacilityType
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }

        public string? Title { get; set; }
        public int BedCountId { get; set; }
        public BedCount BedCount { get; set; }

    }
}
