using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class Location : EntityTableData
    {
        public string LocationName { get; set; }
        public string Owner { get; set; }

    }
}

