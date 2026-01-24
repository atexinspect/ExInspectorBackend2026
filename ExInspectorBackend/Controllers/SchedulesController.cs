using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/schedules")]
    public class SchedulesController : TableController<Schedules>
    {
        public SchedulesController(AppDbContext context)
            : base(new EntityTableRepository<Schedules>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}