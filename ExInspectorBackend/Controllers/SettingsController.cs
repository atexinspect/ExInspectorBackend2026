using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/settings")]
    public class SettingsController : TableController<Settings>
    {
        public SettingsController(AppDbContext context)
            : base(new EntityTableRepository<Settings>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}
