namespace HRRS.Persistence.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }

    public class LocalLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public District District { get; set; }
        public int DistrictId { get; set; }
    }

}
