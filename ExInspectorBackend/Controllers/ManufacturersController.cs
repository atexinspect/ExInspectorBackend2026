using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/manufacturers")]
    public class ManufacturersController : TableController<Manufacturers>
    {
        public ManufacturersController(AppDbContext context)
            : base(new EntityTableRepository<Manufacturers>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}