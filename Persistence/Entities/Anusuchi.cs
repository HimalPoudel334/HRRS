using HRRS.Persistence.Entities;

public class Anusuchi
{
    public int Id { get; set; }
    public string SerialNo { get; set; }
    public string Name { get; set; }
    public string DafaNo { get; set; }
    public ICollection<Parichhed> Parichheds { get; set; } = [];
}

public class Parichhed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public int AnusuchiId { get; set; }
    public Anusuchi Anusuchi { get; set; }
    public ICollection<SubParichhed> SubParichheds { get; set; } = [];
}

public class SubParichhed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public Parichhed Parichhed { get; set; }
    public int ParichhedId { get; set; }
    public ICollection<SubSubParichhed> SubSubParichheds { get; set; } = [];
    public ICollection<Mapdanda> Mapdandas { get; set; } = [];
}

public class SubSubParichhed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public SubParichhed SubParichhed { get; set; }
    public int SubParichhedId { get; set; }
    public ICollection<Mapdanda> Mapdandas { get; set; } = [];


}