using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class WorkOrders : EntityTableData
    {
        public string AssetID { get; set; }
        public string InspectionID { get; set; }
        public string InspectionName { get; set; }
        public int InspectionRevision { get; set; }
        public DateTimeOffset DateStarted { get; set; }
        public DateTimeOffset DateDue { get; set; }
        public DateTimeOffset DateCompleted { get; set; }
        public string RequiredDetail { get; set; }
        public string Comments { get; set; }
        public string Answers { get; set; }
        public string WorkOrderStatus { get; set; }
        public string SiteID { get; set; }
        public string FaultCode { get; set; }
        public string Owner { get; set; }

    }
}
