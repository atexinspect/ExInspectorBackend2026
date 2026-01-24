using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class Asset : EntityTableData
    {
        public string Tag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string Geolocation { get; set; }

        public string AssetName { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; } // varbinary column type in backend DB
        public string ProtectionType { get; set; }
        public string GroupType { get; set; }
        public string TempClass { get; set; }
        public int YearOfManufacture { get; set; }
        public string EnvironmentType { get; set; }
        public string IPRating { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string CertificateNumber { get; set; }
        public string SchematicDWG { get; set; }
        public string PIDDWG { get; set; }
        public string LocationSystemsID { get; set; }
        public string Notes { get; set; }
        public string LotName { get; set; }
        public string Status { get; set; }
        public string AssetType { get; set; }
        public string EquipmentCriticality { get; set; }
        public string Rules { get; set; }
        public string ScheduleGroupName { get; set; }
        public DateTimeOffset DateInstalled { get; set; }
        public string AmbientTemp { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public string Category { get; set; }
        public string Owner { get; set; }

    }
}
