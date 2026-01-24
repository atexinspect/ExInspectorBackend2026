using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/asset")]
    public class AssetController : TableController<Asset>
    {
        public AssetController(AppDbContext context)
            : base(new EntityTableRepository<Asset>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}