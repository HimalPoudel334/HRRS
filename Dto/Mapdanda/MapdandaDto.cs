public class MapdandaDto
{
    public int Id { get; set; }
    public int? SerialNumber { get; set; }
    public string Name { get; set; }
    public int AnusuchiNumber { get; set; }
}

public class MapdandaDto1
{
    public int Id { get; set; }
    public int? SerialNumber { get; set; }
    public string Name { get; set; }
    public int AnusuchiId { get; set; }
    public bool IsAvailableDivided { get; set; }
    public bool Is25Active { get; set; }
    public bool Is50Active { get; set; }
    public bool Is100Active { get; set; }
    public bool Is200Active { get; set; }
    public int? ParichhedId { get; set; }
    public int? SubParichhedId { get; set; }
    public int? SubSubParichhedId { get; set; }

}

public class SubMapdandaDto
{
    public int Id { get; set; }
    public int SerialNumber { get; set; }
    public string Name { get; set; }
    public int MapdandaId { get; set; }
}