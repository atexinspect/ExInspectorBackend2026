using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class AssetType : EntityTableData
    {
        public string AssetTypes { get; set; }
        public string Owner { get; set; }

    }
}
