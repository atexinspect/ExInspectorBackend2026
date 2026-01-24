using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/membershipusers")]
    public class MembershipUsersController : TableController<MembershipUsers>
    {
        public MembershipUsersController(AppDbContext context)
            : base(new EntityTableRepository<MembershipUsers>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}