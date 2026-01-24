using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class LotSchedules : EntityTableData
    {
        public string LotID { get; set; }
        public string LotName { get; set; }
        public DateTimeOffset DateStarted { get; set; }
        public DateTimeOffset DateDue { get; set; }
        public DateTimeOffset DateCompleted { get; set; }
        public string InspectionGrade { get; set; }
        public int LotSize { get; set; }
        public int SampleSize { get; set; }
        public int Re { get; set; }
        public int GFRLevel { get; set; }
        public string InspectionCategory { get; set; }
        public int Frequency { get; set; }
        public string GFR { get; set; }
        public string ASL { get; set; }
        public string WeightedScore { get; set; }
        public string LotStatus { get; set; }
        public int LotRevision { get; set; }
        public string Notes { get; set; }
        public string GenerationLog { get; set; }
        public string Results { get; set; }
        public string Summary { get; set; }
        public string LoggedOnUser { get; set; }
        public string SiteID { get; set; }
        public string Owner { get; set; }

    }
}
