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
        public int? BedCount { get; set; }
        public ICollection<User> Users { get; set; } = [];
    }
}
