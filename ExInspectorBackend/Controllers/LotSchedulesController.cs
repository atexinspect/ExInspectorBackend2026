using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/lotschedules")]
    public class LotSchedulesController : TableController<LotSchedules>
    {
        public LotSchedulesController(AppDbContext context)
            : base(new EntityTableRepository<LotSchedules>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}