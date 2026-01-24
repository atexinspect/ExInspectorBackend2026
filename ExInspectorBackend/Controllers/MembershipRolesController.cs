using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/membershiproles")]
    public class MembershipRolesController : TableController<MembershipRoles>
    {
        public MembershipRolesController(AppDbContext context)
            : base(new EntityTableRepository<MembershipRoles>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}