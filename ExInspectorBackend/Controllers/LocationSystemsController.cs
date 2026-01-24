using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/locationsystems")]
    public class LocationSystemsController : TableController<LocationSystems>
    {
        public LocationSystemsController(AppDbContext context)
            : base(new EntityTableRepository<LocationSystems>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}