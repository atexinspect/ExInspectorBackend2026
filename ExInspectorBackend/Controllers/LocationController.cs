using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/location")]
    public class LocationController : TableController<Location>
    {
        public LocationController(AppDbContext context)
            : base(new EntityTableRepository<Location>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}