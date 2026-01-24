using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class Manufacturers : EntityTableData
    {
        public string Manufacturer { get; set; }
        public string Details { get; set; }
        public string Owner { get; set; }

    }
}
