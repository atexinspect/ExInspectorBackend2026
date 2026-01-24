using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/membershiptasks")]
    public class MembershipTasksController : TableController<MembershipTasks>
    {
        public MembershipTasksController(AppDbContext context)
            : base(new EntityTableRepository<MembershipTasks>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}