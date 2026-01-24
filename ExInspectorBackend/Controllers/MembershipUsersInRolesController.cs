using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorService.Controllers
{
    [Route("tables/membershipusersinroles")]
    public class MembershipUsersInRolesController : TableController<MembershipUsersInRoles>
    {
        public MembershipUsersInRolesController(AppDbContext context)
            : base(new EntityTableRepository<MembershipUsersInRoles>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}