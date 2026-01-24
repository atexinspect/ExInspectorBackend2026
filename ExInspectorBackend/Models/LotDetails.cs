using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class LotDetails : EntityTableData
    {
        public string LotName { get; set; }
        public string LotCode { get; set; }
        public string LotNotes { get; set; }
        public int LotRevision { get; set; }
        public string SiteID { get; set; }
        public string Owner { get; set; }

    }
}
