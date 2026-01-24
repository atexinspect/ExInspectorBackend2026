using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorBackend.Controllers
{
    [Route("tables/lotdetails")]
    public class LotDetailsController : TableController<LotDetails>
    {
        public LotDetailsController(AppDbContext context)
            : base(new EntityTableRepository<LotDetails>(context))
        {
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}