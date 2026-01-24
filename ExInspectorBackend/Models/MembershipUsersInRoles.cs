using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class MembershipUsersInRoles : EntityTableData
    {
        public string UserID { get; set; }
        public string RoleID { get; set; }

    }
}
