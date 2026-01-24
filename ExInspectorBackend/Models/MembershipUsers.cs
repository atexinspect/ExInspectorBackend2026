using CommunityToolkit.Datasync.Server.EntityFrameworkCore;

namespace ExInspectorBackend.Models
{
    public class MembershipUsers : EntityTableData
    {
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordQuestion { get; set; }
        public byte[] PasswordAnswer { get; set; }
        public string Email { get; set; }
        public string Reports { get; set; }

    }
}
