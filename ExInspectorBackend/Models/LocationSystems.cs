using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class LocationSystems : EntityTableData
    {
        public string LocationNameID { get; set; }
        public string LocationName { get; set; }
        public string SystemName { get; set; }
        public string Zone { get; set; }
        public string Group { get; set; }
        public string TempClass { get; set; }
        public string Owner { get; set; }

    }
}
