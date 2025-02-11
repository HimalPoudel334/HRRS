using HRRS.Dto.Anusuchi;
using HRRS.Dto.Parichhed;

namespace HRRS.Dto.Mapdanda
{

    public class MapdandaByAnusuchiDto
    {
        public bool IsAvailableDivided { get; set; }
        public List<GroupdParichhed> Parichhed { get; set; }
    }

    public class GroupdParichhed
    {
        public string? Name { get; set; }
        public List<GroupdSubParichhed> GroupedPariched { get; set; }

    }

    public class GroupdSubParichhed
    {
        public string? Name { get; set; }
        public List<GroupdSubSubParichhed> GroupdSubSubParichhed { get; set; }

    }

    public class GroupdSubSubParichhed
    {
        public string? Name { get; set; }
        public List<GroupedMapdanda> GroupedMapdanda { get; set; }
    }


    public class GroupedMapdanda
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string? Parimaad { get; set; }
        public bool Is100Active { get; set; }
        public bool Is200Active { get; set; }
        public bool Is50Active { get; set; }
        public bool Is25Active { get; set; }
        public bool IsAvailableDivided { get; set; }
        public bool Status { get; set; }

    }
}

namespace HRRS.Dto.Mapdanda1
{
    public class MapdandaByAnusuchiDto
    {
        public bool IsAvailableDivided { get; set; }
        public List<GroupdParichhed> Parichhed { get; set; }
    }

    public class GroupdParichhed
    {
        public string? Name { get; set; }
        public List<GroupdSubParichhed> GroupedPariched { get; set; }

    }

    public class GroupdSubParichhed
    {
        public string? Name { get; set; }
        public List<GroupdSubSubParichhed> GroupdSubSubParichhed { get; set; }

    }

    public class GroupdSubSubParichhed
    {
        public string? Name { get; set; }
        public List<GroupedMapdandaByGroupName> GroupedMapdandaGroup { get; set; }
    }


    public class GroupedMapdandaByGroupName
    {
        public string? GroupName { get; set; }
        public List<GroupedMapdanda> GroupedMapdanda { get; set; }

    }

    public class GroupedMapdanda
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string? Parimaad { get; set; }
        public bool Is100Active { get; set; }
        public bool Is200Active { get; set; }
        public bool Is50Active { get; set; }
        public bool Is25Active { get; set; }
        public bool IsAvailableDivided { get; set; }
        public bool Status { get; set; }

    }
    }


