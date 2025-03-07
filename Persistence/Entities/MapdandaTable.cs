namespace HRRS.Persistence.Entities
{
    public class MapdandaTable
    {
        public int Id { get; set; }
        public string? TableName { get; set; }
        public int TableNumber { get; set; }
        public int AnusuchiId { get; set; }
        public Anusuchi Anusuchi { get; set; }
        public int? ParichhedId { get; set; }
        public Parichhed? Parichhed { get; set; }
        public int? SubParichhedId { get; set; }
        public SubParichhed? SubParichhed { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public FormType FormType { get; set; }
        public ICollection<AnusuchiMapping> AnusuchiMappings { get; set; } = [];
        public ICollection<Mapdanda> Mapdandas { get; set; } = [];
    }
}
