using CommunityToolkit.Datasync.Server;
using CommunityToolkit.Datasync.Server.EntityFrameworkCore;
using ExInspectorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExInspectorService.Controllers
{
    [Route("tables/workorders")]
    public class WorkOrdersController : TableController<WorkOrders>
    {
        private readonly ILogger<WorkOrders> _logger;
        public WorkOrdersController(AppDbContext context, ILogger<WorkOrders> logger)
            : base(new EntityTableRepository<WorkOrders>(context))
        {
            _logger = logger;
            Options = new TableControllerOptions { EnableSoftDelete = true };
        }
    }
}