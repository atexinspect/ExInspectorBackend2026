using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class Settings : EntityTableData
    {
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool Active { get; set; }
    }

}
