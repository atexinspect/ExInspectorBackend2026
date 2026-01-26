using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExInspectorBackend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Altitude = table.Column<double>(type: "float", nullable: false),
                    Geolocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProtectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: false),
                    EnvironmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchematicDWG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIDDWG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationSystemsID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentCriticality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInstalled = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AmbientTemp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationSystems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationNameID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LotDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotRevision = table.Column<int>(type: "int", nullable: false),
                    SiteID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LotSchedules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateStarted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDue = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateCompleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    InspectionGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotSize = table.Column<int>(type: "int", nullable: false),
                    SampleSize = table.Column<int>(type: "int", nullable: false),
                    Re = table.Column<int>(type: "int", nullable: false),
                    GFRLevel = table.Column<int>(type: "int", nullable: false),
                    InspectionCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    GFR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ASL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightedScore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotRevision = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenerationLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Results = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoggedOnUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolesRequiredForTask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordQuestion = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordAnswer = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reports = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipUsersInRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipUsersInRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateStarted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDue = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateCompleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SuffixX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionRevision = table.Column<int>(type: "int", nullable: false),
                    AssetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotRevision = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionRevision = table.Column<int>(type: "int", nullable: false),
                    DateStarted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDue = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateCompleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RequiredDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaultCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_UpdatedAt_Deleted",
                table: "Asset",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_UpdatedAt_Deleted",
                table: "AssetType",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Location_UpdatedAt_Deleted",
                table: "Location",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationSystems_UpdatedAt_Deleted",
                table: "LocationSystems",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_LotDetails_UpdatedAt_Deleted",
                table: "LotDetails",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_LotSchedules_UpdatedAt_Deleted",
                table: "LotSchedules",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_UpdatedAt_Deleted",
                table: "Manufacturers",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRoles_UpdatedAt_Deleted",
                table: "MembershipRoles",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTasks_UpdatedAt_Deleted",
                table: "MembershipTasks",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipUsers_UpdatedAt_Deleted",
                table: "MembershipUsers",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipUsersInRoles_UpdatedAt_Deleted",
                table: "MembershipUsersInRoles",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_UpdatedAt_Deleted",
                table: "Schedules",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UpdatedAt_Deleted",
                table: "Settings",
                columns: new[] { "UpdatedAt", "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_UpdatedAt_Deleted",
                table: "WorkOrders",
                columns: new[] { "UpdatedAt", "Deleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "LocationSystems");

            migrationBuilder.DropTable(
                name: "LotDetails");

            migrationBuilder.DropTable(
                name: "LotSchedules");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "MembershipRoles");

            migrationBuilder.DropTable(
                name: "MembershipTasks");

            migrationBuilder.DropTable(
                name: "MembershipUsers");

            migrationBuilder.DropTable(
                name: "MembershipUsersInRoles");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "WorkOrders");
        }
    }
}
