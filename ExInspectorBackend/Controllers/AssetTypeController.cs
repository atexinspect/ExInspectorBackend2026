using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/assettype")]
    public class AssetTypeController : TableController<AssetType>
    {
        public AssetTypeController(AppDbContext context)
            : base(new EntityTableRepository<AssetType>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}