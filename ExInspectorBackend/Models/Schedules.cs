using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class Schedules : EntityTableData
    {
        public string LotID { get; set; }
        public string AssetID { get; set; }
        public string InspectionName { get; set; }
        public DateTimeOffset DateStarted { get; set; }
        public DateTimeOffset DateDue { get; set; }
        public DateTimeOffset DateCompleted { get; set; }
        public string SuffixX { get; set; }
        public string Answers { get; set; }
        public string InspectionStatus { get; set; }
        public int InspectionRevision { get; set; }
        public string AssetType { get; set; }
        public string SiteID { get; set; }
        public string InspectionType { get; set; }
        public int LotRevision { get; set; }
        public string Owner { get; set; }

    }
}
